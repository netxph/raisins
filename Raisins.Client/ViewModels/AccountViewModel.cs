using Raisins.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raisins.Client.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Assigned Beneficiary")]
        public List<CheckModel> Checkboxes { get; set; }
        [Display(Name = "Role")]
        public string Role { get; set; }
        [Display(Name = "Role")]
        public IEnumerable<Role> Roles { get; set; }
        public void InitResources(List<Role> role, List<CheckModel> checkbox)
        {
            Roles = role;
            Checkboxes = checkbox;
        }

        public void InitResources(Account account, List<Role> role, List<CheckModel> checkbox)
        {
            Roles = role;
            Checkboxes = checkbox;
            Username = account.UserName;
            Name = account.Profile.Name;
            Role = account.Role.Name;
        }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }

    public class AccountsListViewModel
    {
        public List<Account> Accounts { get; set; }
        public AccountsListViewModel()
        {
        }
        public AccountsListViewModel(List<Account> accounts)
        {
            Accounts = accounts;
        }
    }
}