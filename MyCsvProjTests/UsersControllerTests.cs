
using Microsoft.AspNetCore.Mvc;
using MyCsvProj;
using MyCsvProj.Controllers;
using MyCsvProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyCsvProjTests
{
    public class UsersControllerTests
    {
        [Fact]
        public void UploadFile_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var dataStorage = new CsvDataStorage();

            var controller = new UsersController(dataStorage);

            var validData = new List<User>
            {
                new User
                {
                    Name = "Cairo",
                    City = "Divinópolis",
                    Country = "BRA",
                    FavoriteSport = "Basketball"
                },
                new User
                {
                    Name = "Luiz",
                    City = "Passos",
                    Country = "Bra",
                    FavoriteSport = "Foottball"
                },
                new User
                {
                    Name = "Ana",
                    City = "New York",
                    Country = "USA",
                    FavoriteSport = "Tenis"
                },
                new User
                {
                    Name = "Duda",
                    City = "New York",
                    Country = "USA",
                    FavoriteSport = "Crossfit"
                },
                
            };

            // Act
            var result = controller.UploadFile(validData);

            // Assert
            Assert.IsType<OkObjectResult>(result); 
        }

        [Fact]
        public void UploadFile_WithInvalidData_ReturnsBadRequest()
        {
            // Arrange
            var dataStorage = new CsvDataStorage();
            var controller = new UsersController(dataStorage);

         
            var invalidData = new List<User>
            {
                new User 
                {
                    Name = string.Empty,
                    City = "New York",
                    Country = "USA",
                    FavoriteSport = "Basketball"
                },
                new User 
                {
                    Name = "Alice",
                    City = string.Empty,
                    Country = "USA",
                    FavoriteSport = "Basketball"
                },
                new User 
                {
                    Name = "Alice",
                    City = "New Orleans",
                    Country = "USA",
                    FavoriteSport = string.Empty
                },
                new User 
                {
                    Name = "Alice",
                    City = string.Empty,
                    Country = string.Empty,
                    FavoriteSport = "Basketball"
                },
            };

            // Act
            var result = controller.UploadFile(invalidData);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void SearchUsers_WithValidQuery_ReturnsOkResult()
        {
            // Arrange
            var dataStorage = new CsvDataStorage();
            var controller = new UsersController(dataStorage);

            
            string validQuery = "Cairo";

            // Act
            var result = controller.SearchUsers(validQuery);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void SearchUsers_WithEmptyQuery_ReturnsBadRequest()
        {
            // Arrange
            var dataStorage = new CsvDataStorage();
            var controller = new UsersController(dataStorage);

            string emptyQuery = string.Empty;

            // Act
            var result = controller.SearchUsers(emptyQuery);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}