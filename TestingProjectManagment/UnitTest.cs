using System;
using Xunit;
using ProjectManagementAppLayer;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net.Http;

namespace TestingProjectManagment
{
    //public class UnitTest:IClassFixture<WebApplicationFactory<ProjectManagementAppLayer.Startup>>
    //{
    //    private readonly WebApplicationFactory<ProjectManagementAppLayer.Startup> _factory;
    //    private readonly HttpClient _httpClient;
    //    public UnitTest(WebApplicationFactory<Startup> factory )
    //    {
    //        _factory = factory;
    //        _httpClient = _factory.CreateClient();
    //    }
    //    //
    //    //[Fact(Skip = "Moving to theory")]
    //    //public async void TestClients()
    //    //{
    //    //    // arrange
    //    //    var obj = _factory.CreateClient();

    //    //    // act
    //    //    //var response = await obj.GetAsync("/Administrator/Client/Index");
    //    //    //var code = (int)response.StatusCode;
    //    //    // assert
    //    //    //Assert.Equal(200,code);
    //    //}
    //    [Theory]
    //    [InlineData("/Administrator/Client/Index")]
    //    [InlineData("/Administrator/Admin/Index")]
    //    [InlineData("/Administrator/Phase/Index")]
    //    [InlineData("/Administrator/ProjectManager/Index")]
    //    [InlineData("/Administrator/ProjectStatus/Index")]
    //    [InlineData("/Administrator/ProjectType/Index")]
    //    //[InlineData("/ProjectManagment/Deliverable/Index")]
    //    //[InlineData("/ProjectManagment/Home/Index")]
    //    //[InlineData("/ProjectManagment/Invoice/Index")]
    //    //[InlineData("/ProjectManagment/PaymentTerm/Index")]
    //    //[InlineData("/ProjectManagment/Project/Index")]
    //    public async Task TestPages(string url)
    //    {
    //        // arrange
    //        var obj = _factory.CreateClient();
    //        // act
    //        var response = await obj.GetAsync(url);
    //        var code = (int)response.StatusCode;
    //        // assert
    //        Assert.Equal(200, code);
    //    }



    //}

}
