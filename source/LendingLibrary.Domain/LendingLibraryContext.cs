using System.Data.Common;
using System.Data.Entity;
using LendingLibrary.Domain.Models;
using PeanutButter.Utils.Entity;

namespace LendingLibrary.Domain
{
    public class LendingLibraryContext : DbContext, ILendingLibraryContext
    {
        public IDbSet<Person> People { get; set; }
        public IDbSet<Item> Items { get; set; }

        private DbConnection _dbConnection;

        public LendingLibraryContext() : base("DefaultConnection")
        {
            
        }

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
