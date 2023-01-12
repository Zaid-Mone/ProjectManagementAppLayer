using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAppLayer.Areas.Administrator.Controllers;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestingProjectManagment.Controller
{
    public class ProjectStatusControllerTest
    {
        private ProjectStatusController projectStatusController;
        private IProjectStatusRepository _projectStatusRepository;
        public ProjectStatusControllerTest()
        {
            _projectStatusRepository = A.Fake<IProjectStatusRepository>();
            projectStatusController = new ProjectStatusController(_projectStatusRepository);
        }


        [Fact]
        public void ProjectStatusController_Test_IndexAction()
        {
            //Arrange
            var pStatus = A.Fake<List<ProjectStatus>>();
            A.CallTo(() => _projectStatusRepository.GetAllProjectStatuses()).Returns(pStatus);
            // act
            var result = projectStatusController.Index();
            // assert 
            result.Should().BeOfType<Task<IActionResult>>();
        }


        [Fact]
        public void ProjcetStatusController_Test_DetailsAction()
        {
            var id = new Guid();

            //Arrange
            var pStatus = A.Fake<ProjectStatus>();
            A.CallTo(() => _projectStatusRepository.GetProjectStatusById(id)).Returns(pStatus);
            // act
            var result = projectStatusController.Details(id);
            // assert object chcek actions
            result.Should().BeOfType<Task<IActionResult>>();
        }


    }
}
