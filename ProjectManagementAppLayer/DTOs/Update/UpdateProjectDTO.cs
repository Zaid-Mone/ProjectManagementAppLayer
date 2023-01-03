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
        public string ProjectName { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [ForeignKey("ProjectTypeId")]
        public Guid ProjectTypeId { get; set; }
        public ProjectType ProjectType { get; set; }
        [ForeignKey("ProjectStatusId")]
        public Guid ProjectStatusId { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public decimal ContractAmount { get; set; }
        public IFormFile ContractFile { get; set; }
        public string ProjectManagerId { get; set; }
        public ProjectManager ProjectManager { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}
