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
    public class TipoGastoDataService
    {
        private readonly MainDbContext _context;

        public TipoGastoDataService(MainDbContext ambiStoreDbContext)
        {
            _context = ambiStoreDbContext;
        }

        public async Task<List<TipoGasto>> GetAllAsNoTracking()
        {
            return await _context.TipoGastos.AsNoTracking().ToListAsync();
        }

        public async Task<List<TipoGasto>> GetAll()
        {
            return await _context.TipoGastos.ToListAsync();
        }
    }
}
