using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFrota.State
{
    public class CurrentScopeStore
    {
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope _escopoAtual;


        public CurrentScopeStore(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IServiceProvider CriaNovoEscopo()
        {
            _escopoAtual?.Dispose();
            _escopoAtual = _serviceProvider.CreateScope();
            return _escopoAtual.ServiceProvider;
        }

        public IServiceProvider PegaEscopoAtual()
        {
            if (_escopoAtual is null)
            {
                CriaNovoEscopo();
            }
            return _escopoAtual.ServiceProvider;
        }

        public void FinalizaEscopo()
        {
            _escopoAtual.Dispose();
        }

    }
}
