using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementBusinessLayer.Entities
{
    public class ProjectStatus
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Project Status")]
        public string Status { get; set; }
    }
}
