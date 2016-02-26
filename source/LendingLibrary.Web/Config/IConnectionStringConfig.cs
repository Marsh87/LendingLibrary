using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LendingLibrary.Web.Config
{
    public interface IConnectionStringConfig
    {
        string DefaultConnection { get; set; }
    }
}