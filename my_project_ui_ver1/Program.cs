using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;



namespace TechGuildManager
{
    internal class Program // internal is use to put inside in the different file like to make sure to put the file in the program    
    {
        static async Task Main(string[] args)
        {

          using HttpClient client = new HttpClient();


            List<GuildMember> members = new List<GuildMember>();

            int nextId = 1;
            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("      TECH GUILD MANAGER");
                Console.WriteLine("================================");
                Console.WriteLine("1. Register Member");
                Console.WriteLine("2. View Members");
                Console.WriteLine("3. Calculate Event Points");
                Console.WriteLine("4. Search Members");
                Console.WriteLine("5. Exit");
                Console.WriteLine("================================");
                Console.Write("Choose an option: ");
                 // Input To pick which those he/she choice 
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Clear();

                        Console.WriteLine("================================");
                        Console.WriteLine("       REGISTER MEMBER");
                        Console.WriteLine("================================");
                        Console.WriteLine("1. Officer");
                        Console.WriteLine("2. Regular Member");
                        Console.WriteLine("3. Alumni");
                        Console.WriteLine("4. Back to Main Menu");
                        Console.Write("Choose member type: ");
                        
                        // input for deciding where he from 
                        string memberType = Console.ReadLine();

                        if (memberType == "4")
                        {
                            break;
                        }

                        if (memberType != "1" &&
                            memberType != "2" &&
                            memberType != "3")
                        {
                            Console.WriteLine("Invalid member type.");
                            Console.ReadKey();
                            break;
                        }
        // ------------------------------------------------------------------------------
                        
                        // input for register member name  
                        Console.Write("Enter member name: ");
                        string name = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Name cannot be empty.");
                            Console.ReadKey();
                            break;
                        }

                        GuildMember newMember;

                        switch (memberType)
                        {
                            case "1":
                                newMember = new Officer(nextId, name);
                                break;

                            case "2":
                                newMember = new RegularMember(nextId, name);
                                break;

                            default:
                                newMember = new Alumni(nextId, name);
                                break;
                        }

                        members.Add(newMember);
                        nextId++;

                        var data = new
                        {
                            Id = newMember.Id,
                            Name = newMember.Name,
                            Status = newMember.Status
                        };

                        try
                        {
                            var response = await client.PostAsJsonAsync( // PostAsJsonAsync = Its use for As a method used to send data from your Console Application to an ASP.Net core Api Using an HTTP Post request in JSON format                                    
                                "http://localhost:5033/api/guild/register", // await = Its use to wait to send data or collect data to proceed to the next program 
                                data
                            );

                          string result = await response.Content.ReadAsStringAsync();
                            Console.WriteLine("HTTP Status: " + response.StatusCode);

                            Console.WriteLine("API Response: " + result);
                        }

                        catch (TaskCanceledException ex)
                        {
                            Console.WriteLine("The API request timed out or was cancelled!");
                            Console.WriteLine(ex.Message);
                        }

                     //   {
                     //       Console.WriteLine("API connection failed!");
                     //       Console.WriteLine("Error: " + ex.Message);
                     //       Console.WriteLine("Details: " + ex.InnerException?.Message);
                     //   }

                        catch (Exception ex)
                        {
                            Console.WriteLine("An unexpected error occurred!");
                            Console.WriteLine(ex.ToString());
                        }
               // -------------------------------------------------------------------------
                         // format from input when pick Register Member 
                        Console.WriteLine();
                        Console.WriteLine("Member registered successfully!");
                        Console.WriteLine($"Member ID: {newMember.Id}");
                        Console.WriteLine($"Name: {newMember.Name}");
                        Console.WriteLine($"Status: {newMember.Status}");

                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "2":
                       
                              
                        Console.Clear();

                        Console.WriteLine("========== MEMBER RECORDS ==========");

                        if (members.Count == 0)
                        {
                            Console.WriteLine("No members registered yet.");
                        }
                        else
                        {
                            foreach (GuildMember member in members)
                            {
                                Console.WriteLine("------------------------------------");
                                member.isplayInformation();
                            }
                        }

                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Clear();
                        Console.Write("Enter member ID:");

                        if (int.TryParse(Console.ReadLine(), out int searchId))
                        {
                            GuildMember? member = members
                                .Where(m => m.Id == searchId)
                                .FirstOrDefault();

                            if (member == null)
                            {
                                Console.WriteLine("Member not found.");
                            }
                            else
                            {

                                Console.WriteLine("Enter base points: ");

                                if (int.TryParse(Console.ReadLine(), out int basePoints)
                                   && basePoints >= 0)
                                {
                                    int points = member.AwardEventPoints(basePoints);

                                    Console.WriteLine();
                                    Console.WriteLine($"{member.Name} earned {points} points!");
                                    Console.WriteLine($"Total Points: {member.TotalPoints}");
                                }

                                else
                                {
                                    Console.WriteLine(
                                        "Invalid input. Enter a non-negative number.");
                                }
                            }                        
                            
                        }
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.Clear();

                        Console.Write("Enter member name to search: ");
                        string searchName = Console.ReadLine() ?? ""; // The empty string is use for as a set of condition that input to able to not recognize the null while searching it  

                        var results = members
                            .Where(m => m.Name.Contains(//Where = is use to filtering and restricting it also use to identify the data              
                                searchName,            //Contains use to check from the list that if it exist 
                                StringComparison.OrdinalIgnoreCase)) // StringComparison = Specifies how strings should be compared.
                            .ToList(); //Ordinal= Compares text using character values rather than language-specific sorting rules.
                                       //IgnoreCase= Ignores text that is uppercase and lowercase and Compare if its different.
                                       // ToList = Use to convert into a list 
                        if (results.Count > 0)
                        {
                            foreach (GuildMember member in results)
                            {
                                Console.WriteLine("------------------------------------");
                                member.isplayInformation();
                            }

                        }
                        else
                        {
                            Console.WriteLine("No members found.");
                        }

                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;


                    case "5":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        break;
                }
            }

            Console.WriteLine("Thank you for using Tech Guild Manager!");
        }
    }
}