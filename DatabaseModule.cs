using Autofac;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTest
{
    public class DatabaseModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register((context) =>
            {
                var profiles = context.Resolve<IEnumerable<Profile>>();
                var config = new MapperConfiguration(cfg =>
                {
                    foreach (var profile in profiles)
                    {
                        cfg.AddProfile(profile);
                    }
                    cfg.AllowNullCollections = true;
                    cfg.AllowNullDestinationValues = true;
                    cfg.ConstructServicesUsing(context.Resolve);
                });
                var mapper = new Mapper(config, (type) =>
                {
                    return Activator.CreateInstance(type);
                });
                return mapper;
            }).AsImplementedInterfaces().AsSelf();

            builder.RegisterType<MGFContext>().AsImplementedInterfaces();
            builder.Register(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<MGFContext>();
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
                return optionsBuilder.Options;
            }).AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<BaseService<User>>().AsImplementedInterfaces();
            builder.RegisterType<BaseMapper<UserEntity, User>>().AsImplementedInterfaces();
        }
    }
}
