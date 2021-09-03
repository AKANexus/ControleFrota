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
    public class VeículoDataService
    {

        private readonly MainDbContext _context;


        public VeículoDataService(MainDbContext ambiStoreDbContext)
        {
            _context = ambiStoreDbContext;
        }

        public async Task<List<Veículo>> GetAllAsNoTracking()
        {
            return await _context.Veículos.AsNoTracking()
                .Include(x => x.Abastecimentos)
                .Include(x => x.Manutenções)
                .Include(x => x.Modelo)
                .ThenInclude(x => x.ModeloManutenções)
                .ThenInclude(x=>x.ManutençãoProgramada)
                .Include(x => x.Viagens)
                .ThenInclude(x => x.Motorista)
                .ToListAsync();
        }

        public async Task<List<Veículo>> GetAll()
        {
            return await _context.Veículos
                .Include(x => x.Abastecimentos)
                .Include(x => x.Manutenções)
                .Include(x => x.Modelo)
                .ThenInclude(x => x.ModeloManutenções)
                .ThenInclude(x=>x.ManutençãoProgramada)
                .Include(x => x.Viagens)
                .OrderBy(x => x.Placa)
                .ToListAsync();
        }

        public async Task<Veículo> GetVeículoByID(int id)
        {
            return await _context.Veículos
                .Include(x => x.Abastecimentos)
                .Include(x => x.Manutenções)
                .Include(x => x.Modelo)
                .ThenInclude(x => x.ModeloManutenções)
                .ThenInclude(x=>x.ManutençãoProgramada)
                .Include(x => x.Viagens)
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Veículo> GetVeículoByPlaca(string placa)
        {
            return await _context.Veículos
                .Include(x => x.Abastecimentos)
                .Include(x => x.Manutenções)
                .Include(x => x.Modelo)
                .ThenInclude(x => x.ModeloManutenções)
                .ThenInclude(x=>x.ManutençãoProgramada)
                .Include(x => x.Viagens)
                .FirstOrDefaultAsync(x => x.Placa == placa);
        }

        public async Task<Veículo> AddOrUpdate(Veículo veículo)
        {
            _context.Update(veículo);
            await _context.SaveChangesAsync();
            return veículo;
        }

        public async Task<Veículo> GetFullVeículoAsNoTracking(Veículo veículoSelecionado)
        {
            return await _context.Veículos
                .Include(x => x.Abastecimentos)
                .ThenInclude(x=>x.Motorista)
                .Include(x => x.Manutenções)
                .ThenInclude(x => x.Motorista)
                .Include(x => x.Modelo)
                .ThenInclude(x => x.ModeloManutenções)
                .ThenInclude(x => x.ManutençãoProgramada)
                .Include(x => x.Viagens)
                .ThenInclude(x => x.Motorista)
                .FirstOrDefaultAsync(x => x.ID == veículoSelecionado.ID);
        }
    }
}
