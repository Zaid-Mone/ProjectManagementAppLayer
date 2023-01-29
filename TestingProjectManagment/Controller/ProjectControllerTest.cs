using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAppLayer.Areas.ProjectManagment.Controllers;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ProjectManagementBusinessLayer.Data;

namespace TestingProjectManagment.Controller
{
   public  class ProjectControllerTest
    {
        private ProjectController _projectController;
        private readonly IProjectRepository _projectRepository;
        private readonly HttpContextAccessor _httpContext;
        private readonly IProjectStatusRepository _projectStatusRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly UserManager<Person> _userManager;
        private readonly IProjectPhaseRepository _projectPhaseRepository;
        private readonly ApplicationDbContext _context;
        public ProjectControllerTest()
        {
            //Dependinces
            _projectRepository = A.Fake<IProjectRepository>();
            _httpContext = A.Fake<HttpContextAccessor>();
            _clientRepository = A.Fake<IClientRepository>();
            _projectStatusRepository = A.Fake<IProjectStatusRepository>();
            _projectTypeRepository = A.Fake<IProjectTypeRepository>();
            _userManager = A.Fake<UserManager<Person>>();
            _projectPhaseRepository = A.Fake<IProjectPhaseRepository>();
            _context = A.Fake<ApplicationDbContext>();

            // sut => project controller
            _projectController = new ProjectController(
                _context,
                _projectRepository,
                _projectTypeRepository,
                _projectStatusRepository,
                _clientRepository,
                _userManager,
                _projectPhaseRepository
                
                );
        }
        [Fact]
        public void ProjectController_Test_IndexAction()
        {
            //Arrange
            var project = A.Fake<List<Project>>();
             A.CallTo(() => _projectRepository.GetAllProjects()).Returns(project);
            // act
            var result = _projectController.Index();
            // assert 
             result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ProjectController_Test_DetailsAction()
        {
            var id =Guid.NewGuid();
            
            //Arrange
            var project = A.Fake<Project>();
            A.CallTo(() =>  _projectRepository.GetPhaseByProjectId(id)).Returns(project);
            // act
            var result = _projectController.Details(id);
            // assert object chcek actions
            result.Should().BeOfType<Task<IActionResult>>();
        }

    }
}
