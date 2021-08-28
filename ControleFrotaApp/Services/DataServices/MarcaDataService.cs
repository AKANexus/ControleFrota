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
    public class MarcaDataService
    {
        private readonly MainDbContext _context;

        public MarcaDataService(MainDbContext ambiStoreDbContext)
        {
            _context = ambiStoreDbContext;
        }

        public async Task<List<Marca>> GetAllAsNoTracking()
        {
            return await _context.Marcas.AsNoTracking().ToListAsync();
        }

        public async Task<List<Marca>> GetAll()
        {
            return await _context.Marcas.OrderBy(x=>x.Nome).ToListAsync();
        }
    }
}
