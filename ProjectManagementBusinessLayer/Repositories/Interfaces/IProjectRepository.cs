using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        public Task<List<Project>> GetAllProjects();
        // to return only pending project
        public Task<List<Project>> GetAllPendingProjects();   
        // to return only approved project
        public Task<List<Project>> GetAllApprovedProjects();
        // to find all projects based IsApproved
        public Task<List<Project>> GetAllIsApprovedProjects();
        // to return only Count of pending project for notification
        public void GetAllPendingProjectsCount();
        // to find all projects based project manager id
        public Task<List<Project>> GetProjectManagerProjects(string userId);  
        public Task<Project> GetProjectById(Guid id);
        public Task<Project> GetProjectBySpecificId(Guid? id);
        public Task<Project> GetPhaseByProjectId(Guid? id);
        public void Insert(Project project);
        public void Update(Project project);
        public void Delete(Project project);
        public bool ProjectExists(Guid id);
        public void Save();
    }

}
