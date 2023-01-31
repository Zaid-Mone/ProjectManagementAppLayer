using AutoMapper;
using ProjectManagementAppLayer.DTOs.Insert;
using ProjectManagementAppLayer.DTOs.Update;
using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<InsertProjectDTO, Project>()
                .ForMember(dest=>dest.ContractFile,op=>op.Ignore());          
        }
    }
}
