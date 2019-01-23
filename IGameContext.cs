using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTest
{
    public interface IGameContext
    {


        DbSet<UserEntity> Users { get; set; }





        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();



    }
}
