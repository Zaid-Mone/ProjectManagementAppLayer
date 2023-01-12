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
    public class ProjectTypeControllerTest
    {
        private ProjectTypeController projectTypeController;
        private IProjectTypeRepository _projectTypeRepository;
        public ProjectTypeControllerTest()
        {
            _projectTypeRepository = A.Fake<IProjectTypeRepository>();
            projectTypeController = new ProjectTypeController(_projectTypeRepository);
        }

        [Fact]
        public void ProjectTypeController_Test_IndexAction()
        {
            //Arrange
            var pType = A.Fake<List<ProjectType>>();
            A.CallTo(() => _projectTypeRepository.GetAllProjectTypes()).Returns(pType);
            // act
            var result = projectTypeController.Index();
            // assert 
            result.Should().BeOfType<Task<IActionResult>>();
        }


        [Fact]
        public void ProjcetTypeController_Test_DetailsAction()
        {
            var id = new Guid();

            //Arrange
            var pType = A.Fake<ProjectType>();
            A.CallTo(() => _projectTypeRepository.GetProjectTypeById(id)).Returns(pType);
            // act
            var result = projectTypeController.Details(id);
            // assert object chcek actions
            result.Should().BeOfType<Task<IActionResult>>();
        }


    }
}
