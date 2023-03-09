using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HelpDesk.Data.Repository
{
    public class GerenciadorRepository : Repository<Gerenciador>, IGerenciadorRepository
    {
        public GerenciadorRepository(HelpDeskContext db) : base(db)
        {
        }

    }
}
