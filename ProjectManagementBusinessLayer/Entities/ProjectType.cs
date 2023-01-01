using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementBusinessLayer.Entities
{
    public class ProjectType
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Project Type")]
        public string Type { get; set; }  // {Desgin , Supervision or Excute}
    }
}
