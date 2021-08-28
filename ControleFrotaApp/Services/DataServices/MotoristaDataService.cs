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
    public class MotoristaDataService
    {
        private readonly MainDbContext _context;


        public MotoristaDataService(MainDbContext ambiStoreDbContext)
        {
            _context = ambiStoreDbContext;
        }

        public async Task<List<Motorista>> GetAllAsNoTracking()
        {
            return await _context.Motoristas.AsNoTracking()
                .Include(x=>x.Setor)
                .ToListAsync();
        }

        public async Task<List<Motorista>> GetAll()
        {
            return await _context.Motoristas
                .Include(x=>x.Setor)
                .OrderBy(x=>x.Nome)
                .ToListAsync();
        }

        public async Task<Motorista> GetMotoristaByID(int id)
        {
            return await _context.Motoristas
                .Include(x=>x.Setor)
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Motorista> AddOrUpdate(Motorista motorista)
        {
            _context.Update(motorista);
            await _context.SaveChangesAsync();
            return motorista;
        }
    }
}
