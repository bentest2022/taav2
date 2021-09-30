using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalAdoption.Common.Domain;
using AnimalAdoption.Common.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnimalAdoption.Web.Portal.Pages
{
    public class OwnersModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private AnimalAdoptionContext _context;

        [BindProperty]
        public ICollection<Owner> Owners { get; set; }

        public OwnersModel(ILogger<IndexModel> logger, AnimalAdoptionContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet(string id)
        {
            var query = "Select * From Owners Where Id = " + id;
            Owners = _context.Owners
                        .FromSqlRaw(query)
                        .ToList();
        }
    }
}
