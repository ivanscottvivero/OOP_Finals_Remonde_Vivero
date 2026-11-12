using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;



namespace TechGuildManager
{
    internal class Program
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
                            var response = await client.PostAsJsonAsync(
                                "http://localhost:5033/api/guild/register",
                                data
                            );

                            Console.WriteLine("HTTP Status: " + response.StatusCode);


                            string result = await response.Content.ReadAsStringAsync();
                           
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
                                member.DisplayInformation();
                            }
                        }

                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Clear();

                        Console.Write("Enter base points: ");

                        if (int.TryParse(Console.ReadLine(), out int basePoints)
                            && basePoints >= 0)
                        {
                            if (members.Count == 0)
                            {
                                Console.WriteLine("No members registered yet.");
                            }
                            else
                            {
                                foreach (GuildMember member in members)
                                {
                                    int points =
                                        member.CalculateEventPoints(basePoints);

                                    Console.WriteLine(
                                        $"{member.Name}: {points} points");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine(
                                "Invalid input. Enter a non-negative number.");
                        }

                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.Clear();

                        Console.Write("Enter member name to search: ");
                        string searchName = Console.ReadLine() ?? "";

                        var results = members
                            .Where(m => m.Name.Contains(
                                searchName,
                                StringComparison.OrdinalIgnoreCase))
                            .ToList();

                        if (results.Count > 0)
                        {
                            foreach (GuildMember member in results)
                            {
                                Console.WriteLine("------------------------------------");
                                member.DisplayInformation();
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