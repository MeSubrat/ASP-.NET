using System;
using System.Security.Cryptography.X509Certificates;

namespace TicketManagementSystem
{
    internal class Program
    {
        public static void Menu()
        {
            Console.WriteLine("--- Ticket Management System ---");
            Console.WriteLine(
                "1. Create a ticket!\n" +
                "2. View All Tickets\n" +
                "3. Update a ticket\n" +
                "4. Search a Ticket\n" +
                "5. Delete a Ticket\n" +
                "6. Exit\n"
            );
        }
        public static void Main()
        {
            TicketOperations operations = new TicketOperations();
            try
            {
                while (true)
                {
                    Menu();
                    Console.Write("Enter Choice: ");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            operations.CreateTicket();
                            break;
                        case 2:
                            operations.ViewAllTicket();
                            break;
                        case 3:
                            operations.UpdateTicket();
                            break;
                        case 4:
                            operations.SearchTicket();
                            break;
                        case 5:
                            operations.DeleteTicket();
                            break;
                        case 6:
                            Environment.Exit(0);
                            break;
                        //Got help from AI!
                        default:
                            Console.WriteLine("Please enter a valid choice!");
                            break;
                    }
                }
            }
            catch(Exception exp)
            {
                Console.WriteLine($"Error: {exp}");
            }
        }
    }
}
