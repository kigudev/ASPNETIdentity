using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASPNETIdentity.Data;

namespace ASPNETIdentity.Pages
{
    [Authorize(Roles = "Administrator")]
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        [Required]
        public string Role { get; set; }
        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole { Name = Role };
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    Role = string.Empty;
                }
                else
                {
                    foreach(var e in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, e.Description);
                    }
                }
            }
        }
    }
}
