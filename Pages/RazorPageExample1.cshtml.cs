using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NormExample.Data;

namespace NormExample.Pages
{
    public class RazorPageModel1 : PageModel
    {
        public RazorPageModel1(UsersService service)
        {
            Service = service;
        }

        public UsersService Service { get; }

        public void OnGet()
        {

        }
    }
}