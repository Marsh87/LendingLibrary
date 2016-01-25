using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendingLibrary.Domain.Models;

namespace LendingLibrary.Domain
{
    public class LendingLibraryContext : DbContext, ILendingLibraryContext
    {
        public IDbSet<Person> People { get; set; }

        private DbConnection _dbConnection;

        public LendingLibraryContext(DbConnection dbConnection): base(dbConnection, true)
        {
            _dbConnection = dbConnection;
        }

        static LendingLibraryContext()
        {
            Database.SetInitializer<LendingLibraryContext>(null);
        }

    }
}
