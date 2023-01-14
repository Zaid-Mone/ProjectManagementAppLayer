using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProjectManagementBusinessLayer.Entities;

namespace ProjectManagementAppLayer.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Person> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly UserManager<Person> _userManager;
        public LogoutModel(SignInManager<Person> signInManager, ILogger<LogoutModel> logger, UserManager<Person> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            user.IsLoggedIn = false;
            await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
