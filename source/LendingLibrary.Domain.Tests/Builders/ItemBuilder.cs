using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendingLibrary.Domain.Models;
using PeanutButter.RandomGenerators;

namespace LendingLibrary.Domain.Tests.Builders
{
    public class ItemBuilder : GenericBuilder<ItemBuilder, Item>
    {
        public ItemBuilder WithId(int id)
        {
            return WithProp(o => o.ItemId = id);
        }

        public ItemBuilder WithTitle()
        {
            return WithProp(o => o.Title = RandomValueGen.GetRandomString(1, 13));
        }

        public ItemBuilder WithDescription()
        {
            return WithProp(o => o.Description = RandomValueGen.GetRandomString(1, 13));
        }
    }
}
