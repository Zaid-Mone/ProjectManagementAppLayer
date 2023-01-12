using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
    public class AdminControllerTest
    {
        private AdminController adminController;
        private readonly IAdminRepository _adminRepository;
        private readonly UserManager<Person> _userManager;
        private readonly IWebHostEnvironment _hosting;

        public AdminControllerTest()
        {
            _adminRepository = A.Fake<IAdminRepository>();
            _userManager = A.Fake<UserManager<Person>>();
            _hosting = A.Fake<IWebHostEnvironment>();
            adminController = new AdminController(_adminRepository,_userManager,_hosting);
        }

        //[Fact]
        //public async Task AdminController_Test_IndexAction()
        //{
        //    //Arrange
        //    var admins = A.Fake<List<Admin>>();
        //    A.CallTo(() =>  _adminRepository.GetAllAdmins()).Returns(admins);
        //    await _adminRepository.GetAllAdmins();
        //    // act
        //    var result = adminController.Index();
        //    // assert 
        //    result.Should().BeOfType<Task<IActionResult>>();
        //}


        //[Fact]
        //public async Task  AdminController_Test_DetailsAction()
        //{
        //    // maybe error here
        //    string id = Convert.ToString(Guid.NewGuid());

        //    //Arrange
        //    var admin = A.Fake<Admin>();
        //    A.CallTo(() => _adminRepository.GetAdminById(id)).Returns(admin);
        //    await _adminRepository.GetAdminById(id);
        //    // act
        //    var result = adminController.Details(id);
        //    // assert object chcek actions
        //    result.Should().BeOfType<Task<IActionResult>>();
        //}




    }
}
