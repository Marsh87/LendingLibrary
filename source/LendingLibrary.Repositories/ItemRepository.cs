using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendingLibrary.Domain;
using LendingLibrary.Domain.Models;

namespace LendingLibrary.Repositories
{
    public class ItemRepository:IItemRepository
    {
        private readonly ILendingLibraryContext _lendingLibraryContext;

        public ItemRepository(ILendingLibraryContext lendingLibraryContext)
        {
            if (lendingLibraryContext == null) throw new ArgumentNullException(nameof(lendingLibraryContext));
            _lendingLibraryContext = lendingLibraryContext;
        }

        public int Save(Item item)
        {
            var entity = _lendingLibraryContext.Items.FirstOrDefault(x => x.ItemId == item.ItemId);
            if (entity == null)
            {
                _lendingLibraryContext.Items.Add(item);
            }
            else
            {
                entity.Photo = item.Photo;
                entity.Description = item.Description;
                entity.Mimetype = item.Mimetype;
                entity.Title = item.Title;
            }
            _lendingLibraryContext.SaveChanges();
            return item.ItemId;
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _lendingLibraryContext.Items.ToList();
        }

        public Item GetItem(int itemId)
        {
            return _lendingLibraryContext.Items.FirstOrDefault(x => x.ItemId == itemId);
        }
    }
}
