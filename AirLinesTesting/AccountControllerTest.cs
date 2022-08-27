using AirLines.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace AirLinesTestingBeta_One
{
    public class AccountControllerTest
    {
        AccountController account = new AccountController();
        [Fact]
        public void Test_LoginPage()
        {

            var result = account.Login();
            var page=Assert.IsType<IActionResult>(result); 
            Assert.NotNull(page);
        }
        [Fact]
        public void Test_ErrorPage()
        {
            var result = account.ErrorPage();
            var errorPage =Assert.IsType<ViewResult>(result);
            Assert.NotNull(errorPage.ViewName);

        }
        [Fact]
        public void Test_IsEmail()
        {
            var result = account.IsEmailExsist("yogesh.tak@kellton.com") as IActionResult;
            
            Assert.Equal("Exist",result!.ToString());
        }
    }
}
