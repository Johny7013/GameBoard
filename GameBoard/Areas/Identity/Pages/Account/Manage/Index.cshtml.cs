﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GameBoard.DataLayer.Entities;
using GameBoard.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameBoard.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        [Display(Name = "Username")]
        public string UserName { get; set; }

        public string Email { get; set; }

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Error.FromPage(this).AccessDenied();
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);

            UserName = userName;
            Email = email;

            return Page();
        }
    }
}