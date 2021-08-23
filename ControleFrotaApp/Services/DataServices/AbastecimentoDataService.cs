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
    public class AbastecimentoDataService
    {
        private readonly MainDbContext _context;

        public AbastecimentoDataService(MainDbContext ambiStoreDbContext)
        {
            _context = ambiStoreDbContext;
        }

        public async Task<List<Abastecimento>> GetAllAsNoTracking()
        {
            return await _context.Abastecimentos.AsNoTracking()
                .Include(x => x.Motorista)
                .Include(x=>x.Veículo)
                .Include(x=>x.Combustível)
                .ToListAsync();
        }

        public async Task<Abastecimento> GetByID(int id)
        {
            return await _context.Abastecimentos
                .Include(x => x.Motorista)
                .Include(x => x.Veículo)
                .Include(x => x.Combustível)
                .FirstOrDefaultAsync(x=>x.ID == id);
        }

        public async Task<List<Abastecimento>> GetAllByMotoristaAsNoTracking(Motorista motorista)
        {
            return await _context.Abastecimentos.AsNoTracking()
                .Include(x => x.Motorista)
                .Include(x => x.Veículo)
                .Include(x=>x.Combustível)
                .Where(x => x.Motorista.ID == motorista.ID)
                .ToListAsync();
        }

        public async Task<List<Abastecimento>> GetAllByVeículoAsNoTracking(Veículo veículo)
        {
            return await _context.Abastecimentos.AsNoTracking()
                .Include(x => x.Motorista)
                .Include(x => x.Veículo)
                .Include(x=>x.Combustível)
                .Where(x => x.Veículo.ID == veículo.ID)
                .ToListAsync();
        }

        public async Task<Abastecimento> AddOrUpdate(Abastecimento abastecimento)
        {
            _context.Update(abastecimento);
            await _context.SaveChangesAsync();
            return abastecimento;
        }
    }
}
