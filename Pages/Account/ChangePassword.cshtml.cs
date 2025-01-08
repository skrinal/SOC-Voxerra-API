using Microsoft.AspNetCore.Mvc.RazorPages;
using Voxerra_API.Functions.Password;

namespace YourNamespace.Pages.Account
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IPasswordFunction _passwordFunction;

        public ChangePasswordModel(IPasswordFunction passwordFunction)
        {
            _passwordFunction = passwordFunction;
        }

        [BindProperty(SupportsGet = true)] // Use 'SupportsGet' to bind the query parameter
        public string Token { get; set; }
        [BindProperty]
        public string NewPassword { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _passwordFunction.ResetPassword(Token, NewPassword);
                if (result)
                {
                    RedirectToPage("/Account/PasswordChangedSuccessfully");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Password reset failed");
                }
            }
            return Page();
        }
    }
}
