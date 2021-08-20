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

        }

        public async Task<List<Modelo>> GetAllAsNoTracking()
        {
            return await _context.Modelos.AsNoTracking().ToListAsync();
        }

        public async Task<List<Modelo>> GetAll()
        {
            return await _context.Modelos.ToListAsync();
        }
    }
}
