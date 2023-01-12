using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAppLayer.Areas.ProjectManagment.Controllers;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestingProjectManagment.Controller
{
   public class ProjectPhaseControllerTest
    {
        private ProjectPhaseController projectPhaseController;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectPhaseRepository _projectPhaseRepository;
        private readonly IPhaseRepository _phaseRepository;

        public ProjectPhaseControllerTest()
        {
            _phaseRepository = A.Fake<IPhaseRepository>();
            _projectPhaseRepository = A.Fake<IProjectPhaseRepository>();
            _projectRepository = A.Fake<IProjectRepository>();
            projectPhaseController = new ProjectPhaseController(
                _projectRepository,
                _projectPhaseRepository,
                _phaseRepository);
        }
        /// <summary>
        /// Must bring a ProjectId in Index to run success
        /// </summary>

        [Fact]
        public void ProjectPhaseController_Test_IndexAction()
        {
            //Arrange
            Project project = new Project();
            project.Id = Guid.NewGuid();
            var projectPhases = A.Fake<List<ProjectPhase>>();
            A.CallTo(() => _projectPhaseRepository.GetAllSpecificProjectPhaseById(project.Id)).Returns(projectPhases);
            // act
            var result = projectPhaseController.Index(project.Id);
            // assert 
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ProjectPhaseController_Test_DetailsAction()
        {
            
            var id = new Guid();
            //Arrange
            var projectPhase = A.Fake<ProjectPhase>();
            A.CallTo(() => _projectPhaseRepository.GetProjectPhaseById(id))
                .Returns(projectPhase);
            // act
            var result = projectPhaseController.Details(id);
            // assert object chcek actions
            result.Should().BeOfType<Task<IActionResult>>();
        }

    }
}
