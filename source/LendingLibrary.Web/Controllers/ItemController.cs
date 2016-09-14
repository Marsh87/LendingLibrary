using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using LendingLibrary.Repositories;

namespace LendingLibrary.Web.Controllers
{
    public class ItemController : Controller
    {
        private IItemRepository _itemRepository;
        private IMapper _mapper;

        public ItemController(IItemRepository itemRepository, IMapper mapper)
        {
            if (itemRepository == null) throw new ArgumentNullException(nameof(itemRepository));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
           _itemRepository = itemRepository;
           _mapper = mapper;
        }

        // GET: Item
        public ActionResult Index()
        {
            return View();
        }
    }
}