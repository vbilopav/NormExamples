using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NormExample.Data;

namespace NormExample.Pages
{
    public class RazorPageModel2 : PageModel
    {
        public RazorPageModel2(UsersService service)
        {
            Service = service;
        }

        public UsersService Service { get; }

        public void OnGet()
        {

        }
    }
}