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
    public class CombustívelDataService
    {
        private readonly MainDbContext _context;

        public CombustívelDataService(MainDbContext ambiStoreDbContext)
        {
            _context = ambiStoreDbContext;
        }

        public async Task<List<Combustível>> GetAllAsNoTracking()
        {
            return await _context.Combustívels.AsNoTracking().ToListAsync();
        }

        public async Task<List<Combustível>> GetAll()
        {
            return await _context.Combustívels.ToListAsync();
        }
    }
}
