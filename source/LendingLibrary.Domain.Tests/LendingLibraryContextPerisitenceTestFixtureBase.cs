using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using LendingLibrary.DB;
using PeanutButter.TestUtils.Entity;
using PeanutButter.Utils.Entity;

namespace LendingLibrary.Domain.Tests
{
    public class LendingLibraryContextPerisitenceTestFixtureBase:EntityPersistenceTestFixtureBase<LendingLibraryContext>
    {
        public LendingLibraryContextPerisitenceTestFixtureBase()
        {
            Configure(false,connectitonString=>new MigrationsRunner(connectitonString));
            DisableDatabaseRegeneration();
            RunBeforeFirstGettingContext(Clear);
        }

        private void Clear(LendingLibraryContext lendingLibraryContext)
        {
            lendingLibraryContext.People.Clear();
        }
    }
}
