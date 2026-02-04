using Application.UseServices;
using Application.Dto;
using Infrastructure.Persistance.Repository;

namespace Presentation_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var commandproc= new CommandProcessor(new JsonDataStore());
            int choice = 0;
            while(choice != 5)
            {
                Console.WriteLine("1. Add User Action");
                Console.WriteLine("2. Add Log Entry");
                Console.WriteLine("3. Process All Logs");
                Console.WriteLine("4. Undo Last Action");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                choice = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter User Action description: ");
                        string userActionDesc = Console.ReadLine() ?? "";
                        commandproc.AddData(new CommandDto (userActionDesc,DateTime.Now,true));
                        break;
                    case 2:
                        Console.Write("Enter Log Entry description: ");
                        string logEntryDesc = Console.ReadLine() ?? "";
                        commandproc.AddData(new CommandDto (logEntryDesc, DateTime.Now, false));
                        break;
                    case 3:
                        var processedLogs = commandproc.ProcessAllLogs();
                        Console.WriteLine("Processed Logs:");
                        if(processedLogs.Count() == 1 && processedLogs.First() == "No logs to process.")
                        {
                            Console.WriteLine(processedLogs.First());
                            break;
                        }
                        if(processedLogs.Count() == 0)
                        {
                            Console.WriteLine("No logs to process.");
                            break;
                        }
                        for(int i = 0; i < processedLogs.Count(); i++)
                        {
                            Console.WriteLine($"{processedLogs.ElementAt(i)}");
                        }
                        break;
                    case 4:
                        var undoneAction = commandproc.UndoLastAction();
                        Console.WriteLine($"Undone Action: {undoneAction}");
                        break;
                    case 5:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
