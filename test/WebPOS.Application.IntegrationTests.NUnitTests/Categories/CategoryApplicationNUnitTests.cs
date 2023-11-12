using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPOS.Application.Commons.Foundations;
using WebPOS.Application.Contracts;
using WebPOS.Application.Dtos.Request;
using WebPOS.Utilitties.Statics.Strings;

namespace WebPOS.Application.Categories
{
    [TestFixture]
    public class CategoryApplicationNUnitTests
    {
        private static WebApplicationFactory<Program>? _factory = null;
        private static IServiceScopeFactory? _scopeFactory = null;

        [SetUp]
        public void SetUp()
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
        }

        [Test]
        public async Task AddCategory_InputNullOrEmptyValues_ReturnValidationError()
        {
            using IServiceScope scope = _scopeFactory!.CreateScope();
            ICategoryApplication? context = scope.ServiceProvider.GetService<ICategoryApplication>();

            //1- Arrange
            string name = "";
            string description = "";
            int state = 1;
            string expectedMessage = MessagesReply.MESSAGE_VALIDATE;

            //2- Act
            BaseResponse<bool> result = await context!.AddCategory(new CategoryRequestDto() { 
                Name = name,
                Description = description,
                State = state
            });

            string currentMessage = result.Message;

            //3- Assert
            Assert.That(currentMessage, Is.EqualTo(expectedMessage));
        }

        [Test]
        public async Task AddCategory_InputCorrectValues_ReturnSuccessfully()
        {
            using IServiceScope scope = _scopeFactory!.CreateScope();
            ICategoryApplication? context = scope.ServiceProvider.GetService<ICategoryApplication>();

            //1- Arrange
            string name = "Nuevo registro";
            string description = "Nueva descripcion";
            int state = 1;
            string expectedMessage = MessagesReply.MESSAGE_ADD;

            //2- Act
            BaseResponse<bool> result = await context!.AddCategory(new CategoryRequestDto()
            {
                Name = name,
                Description = description,
                State = state
            });

            string currentMessage = result.Message;

            //3- Assert
            Assert.That(currentMessage, Is.EqualTo(expectedMessage));
        }
    }
}
