

using Microsoft.AspNetCore.Mvc; // This gives you acces to ASP.Net Core MVC features for Example = controllerBase - IActionResult - [ApiController]- [Route] - [HttpPost] - [HttpGet] - [FromBody]
using StudentApi; // This allows you to access classes inside the StudentApi namespace.
using System.Collections.Generic;// This gives you access to generic collections, including: List<T>
using System.Text.Json;
using System.IO;
//_________________________________________________
namespace StudentApi.Controllers
{
    [ApiController] // this called an attribute  it tells ASP.Net Core that this class follows API controller convention 
    [Route("api/guild")] // This defines the base URL for your controller means to say use as name 

    public class GuildController : ControllerBase // inherit 
    {
        private readonly string filePath =
           Path.Combine(Directory.GetCurrentDirectory(),
               "Data", 
               "members.json"
           );

         // Create the folder and JSON file if missing 
         private void EnsureFileExist()
        {
            Directory.CreateDirectory(
                Path.GetDirectoryName(filePath)!

            );

            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.WriteAllText(filePath, "[]");
            }

        }



        // Temporary storage for guild members
       // private static List<GuildMemberData> members = new List<GuildMemberData>(); // the basket that carry all of the data and include old data

        // Receive members from the console application
        [HttpPost("register")] // register name of the post and httpost is use to send data 
        public IActionResult Connect( // The IActionResult This defines what the method returns amd it allows you to return different HTTP responses for example = return Ok() - return Bad Reuquest and other code that use return        
            [FromBody] GuildMemberData member) // So The FromBody so it get information  from the body of the http request for example if your console application send data of json so it needs to something it to into GuildMemdata Object  
        {
            EnsureFileExist();

            // Read existing JSON records
            string json = System.IO.File.ReadAllText(filePath);

            var members =
            JsonSerializer.Deserialize<List<GuildMemberData>>(json)
            ?? new List<GuildMemberData>();
          
            // Add new member
            members.Add(member);

            // Convert updated list into JSON
            string updatedJson =
                JsonSerializer.Serialize(members);

            // Save updated records 
            System.IO.File.WriteAllText(filePath, updatedJson);


            // The format 
            Console.WriteLine("New Guild Member Received!");
            Console.WriteLine("ID: " + member.Id); // The ID is connecting to the get and set 
            Console.WriteLine("Name: " + member.Name);// same ^
            Console.WriteLine("Status: " + member.Status);// same ^

            return Ok(new // return from HttpPost the ok part is to make sure the return doesnt go back if its not ok
            {
                Message = "Guild member received successfully!",
                Member = member
            });
        }

        // Retrieve all registered members
[HttpGet("members")] // HttpGet Only retieves data and the members is just name 
        public IActionResult Connect()
        {
            EnsureFileExist();

            string json = System.IO.File.ReadAllText(filePath);

            var members =
            JsonSerializer.Deserialize<List<GuildMemberData>>(json)
            ?? new List<GuildMemberData>();


            return Ok(members); // the format of this is already in the list 
        }


    }
    
}