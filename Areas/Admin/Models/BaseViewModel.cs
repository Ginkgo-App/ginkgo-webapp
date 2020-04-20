using ginko_webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Models
{
    public class BaseViewModel
    {
        public UserModel Admin { get; set; }
    }
}