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
    public class ManutençãoDataService
    {
        private readonly MainDbContext _context;


        public ManutençãoDataService(MainDbContext ambiStoreDbContext)
        {
            _context = ambiStoreDbContext;
        }

        public async Task<List<Manutenção>> GetAllAsNoTracking()
        {
            return await _context.Manutençãos.AsNoTracking()
                .Include(x => x.Motorista)
                .Include(x => x.Veículo)
                .ThenInclude(y=>y.Modelo)
                .ToListAsync();
        }

        public async Task<Manutenção> GetManutençãoByID(int id)
        {
            return await _context.Manutençãos
                .Include(x => x.Veículo)
                .Include(x=>x.Motorista)
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Manutenção> AddOrUpdate(Manutenção Manutenção)
        {
            _context.Update(Manutenção);
            await _context.SaveChangesAsync();
            return Manutenção;
        }
    }
}
