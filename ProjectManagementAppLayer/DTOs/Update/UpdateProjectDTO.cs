using Microsoft.AspNetCore.Http;
using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.DTOs.Update
{
    public class UpdateProjectDTO
    {
        public Guid  ProjectId { get; set; }
        [Display(Name ="Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Display(Name = "Project Type")]
        [ForeignKey("ProjectTypeId")]
        public Guid ProjectTypeId { get; set; }
        public ProjectType ProjectType { get; set; }
        [ForeignKey("ProjectStatusId")]
        [Display(Name = "Project Status")]
        public Guid ProjectStatusId { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        [Display(Name = "Contract Amount")]
        public decimal ContractAmount { get; set; }
        [Display(Name = "Contract File")]
        public IFormFile ContractFile { get; set; }
       
        public string ProjectManagerId { get; set; }
        public ProjectManager ProjectManager { get; set; }
        [Display(Name = "Client")]
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}
