using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly.LoadFrom(@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.ComponentModel.DataAnnotations.dll");
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DatabaseModule());
            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {


                IDomainService<User> service = scope.Resolve<IDomainService<User>>();
                //service.Start();

            }
        }
    }
}
