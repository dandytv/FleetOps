using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.Abstracts
{
   public interface IDbContext : IDisposable
    {
       void Commit();
       DbContext CurrentContext { get; }
    }
}
