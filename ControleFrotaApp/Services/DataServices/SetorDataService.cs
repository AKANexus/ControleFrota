using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFrota.Domain;
using ControleFrota.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ControleFrota.Services.DataServices
{
    class SetorDataService
    {
        private readonly MainDbContext _context;

        public SetorDataService(MainDbContext ambiStoreDbContext)
        {
            _context = ambiStoreDbContext;
        }

        public async Task<List<Setor>> GetAllAsNoTracking()
        {
            return await _context.Setors.AsNoTracking().ToListAsync();
        }

        public async Task<List<Setor>> GetAll()
        {
            return await _context.Setors.ToListAsync();
        }
    }
}
