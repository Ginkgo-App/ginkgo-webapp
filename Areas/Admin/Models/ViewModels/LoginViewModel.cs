using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Chưa nhập email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}