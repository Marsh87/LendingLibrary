using System;
using System.Data.Entity;
using LendingLibrary.Domain.Models;

namespace LendingLibrary.Domain
{
    public interface ILendingLibraryContext:IDisposable
    {
        IDbSet<Person> People { get; set; }
        int SaveChanges();
    }
}