using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendingLibrary.Domain.Models;

namespace LendingLibrary.Repositories
{
    public interface IItemRepository
    {
        int Save(Item person);
        IEnumerable<Item> GetAllItems();
        Item GetItem(int itemId);
    }
}
