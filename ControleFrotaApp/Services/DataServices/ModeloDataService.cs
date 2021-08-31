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
    public class ModeloDataService
    {
        private readonly MainDbContext _context;

        public ModeloDataService(MainDbContext ambiStoreDbContext)
        {
            _context = ambiStoreDbContext;
        }

        public async Task<List<Modelo>> GetAllAsNoTracking()
        {
            return await _context.Modelos.AsNoTracking().ToListAsync();
        }

        public async Task<List<Modelo>> GetAll()
        {
            return await _context.Modelos
                .Include(x=>x.ModeloManutenções)
                .ThenInclude(mm=>mm.ManutençãoProgramada)
                .ToListAsync();
        }

        public async Task<List<Modelo>> GetAllByMarca(Marcas marca)
        {
            return await _context.Modelos.Where(x=>x.Marca == marca).OrderBy(x=>x.Nome).ToListAsync();
        }

        public async Task<Modelo> AddOrUpdate(Modelo modelo)
        {
            _context.Update(modelo);
            await _context.SaveChangesAsync();
            return modelo;
        }
    }
}
