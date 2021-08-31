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
    public class ManutençãoProgramadaDataService
    {
        private readonly MainDbContext _context;

        public ManutençãoProgramadaDataService(MainDbContext ambiStoreDbContext)
        {
            _context = ambiStoreDbContext;
        }

        public async Task<List<ManutençãoProgramada>> GetAllAsNoTracking()
        {
            return await _context.ManutençãoProgramadas
                .Include(x=>x.ModeloManutenções)
                .ThenInclude(mm=>mm.Modelo)
                .AsNoTracking().ToListAsync();
        }

        public async Task<List<ManutençãoProgramada>> GetAll()
        {
            return await _context.ManutençãoProgramadas.Include(x => x.ModeloManutenções)
                .ThenInclude(mm => mm.Modelo)
                .ToListAsync();
        }

        public async Task<List<ManutençãoProgramada>> GetAllByTipo(TipoVeículo tipoVeículo)
        {
            return await _context.ManutençãoProgramadas.Include(x => x.ModeloManutenções)
                .ThenInclude(mm => mm.Modelo)
                .Where(x=>x.TipoVeículo == tipoVeículo)
                .ToListAsync();
        }

        public async Task<ManutençãoProgramada> AddOrUpdate(ManutençãoProgramada manutençãoProgramada)
        {
            _context.Update(manutençãoProgramada);
            await _context.SaveChangesAsync();
            return manutençãoProgramada;
        }
    }
}
