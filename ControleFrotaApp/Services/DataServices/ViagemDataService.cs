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
    public class ViagemDataService
    {
        private readonly MainDbContext _context;


        public ViagemDataService(MainDbContext ambiStoreDbContext)
        {
            _context = ambiStoreDbContext;
        }

        public async Task<List<Viagem>> GetAllAsNoTracking()
        {
            return await _context.Viagems.AsNoTracking()
                .ToListAsync();
        }

        public async Task<Viagem> GetMotoristaByID(int id)
        {
            return await _context.Viagems
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Viagem> AddOrUpdate(Viagem viagem)
        {
            _context.Update(viagem);
            await _context.SaveChangesAsync();
            return viagem;
        }
    }
}
