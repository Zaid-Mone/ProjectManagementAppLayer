using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectManagementBusinessLayer.Entities
{
    public class Person : IdentityUser
    {
        [Display(Name = "Full Name")]
        public string  FullName { get; set; }
    }

}
