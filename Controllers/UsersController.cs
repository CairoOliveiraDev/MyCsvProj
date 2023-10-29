using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;
using MyCsvProj.Models;
using MyCsvProj;
using System.Formats.Asn1;
using System.Globalization;

namespace MyCsvProj.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CsvDataStorage _dataStorage;

        private List<User> ProcessCsvFile(CsvReader csv)
        {
            var records = csv.GetRecords<User>().ToList();
            return records;
        }

        public UsersController(CsvDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        // Using JSON 
        /***
                [HttpPost("Files")]
                public IActionResult UploadFile([FromBody] List<User> jsonData)
                {
                    if (jsonData == null || jsonData.Count == 0)
                    {
                        return BadRequest("Invalid data.");
                    }

                    try
                    {
                        _dataStorage.Users.AddRange(jsonData);
                        return Ok("Data loaded successfully.");
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Error processing data: {ex.Message}");
                    }
                }**/

        [HttpPost("Files")]
            public IActionResult UploadFile(IFormFile file)
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Invalid file.");
                }

                try
                {

                    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = true
                    };

                    using (var reader = new StreamReader(file.OpenReadStream()))
                    using (var csv = new CsvReader(reader, csvConfig))
                    {
                        var records = csv.GetRecords<User>().ToList();
                        _dataStorage.Users.AddRange(records);
                    }

                    return Ok("CSV file uploaded successfully..");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error processing CSV file: {ex.Message}");
                }
            }
        
        [HttpGet("Users")]
        public IActionResult SearchUsers(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return BadRequest("The 'q' search parameter is required.");
            }

            var results = _dataStorage.Users.Where(user =>
                user.Name.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                user.City.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                user.Country.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                user.FavoriteSport.Contains(q, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            return Ok(results);
        }


    }
}
