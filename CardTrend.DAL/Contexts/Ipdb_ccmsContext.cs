using CardTrend.Domain.Entities;
using CardTrend.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardTrend.Domain.Dto;

namespace CardTrend.DAL.Contexts
{
    public interface Ipdb_ccmsContext : IDbContext
    {
        /*
        IDbSet<iac_Card> iac_Card { get; set; }
        IDbSet<iac_Entity> iac_Entity { get; set; }
        IDbSet<iss_CardType> iss_CardType { get; set; }
        IDbSet<iss_RefLib> iss_RefLib { get; set; }
         */
    }
}
