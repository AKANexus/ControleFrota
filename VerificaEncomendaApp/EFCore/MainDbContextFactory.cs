using System;
using Microsoft.EntityFrameworkCore;

namespace ControleFrota.EFCore
{
    public class MainDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public MainDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        

        public MainDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<MainDbContext> options = new DbContextOptionsBuilder<MainDbContext>();
            _configureDbContext(options);
            return new MainDbContext(options.Options);
        }
    }

}
