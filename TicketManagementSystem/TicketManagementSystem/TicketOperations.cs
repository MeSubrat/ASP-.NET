using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem
{
    internal class TicketOperations : ITicketOperations
    {
        static string title="", description="";
        static void getTicketDetails()
        {
            Console.Write("Enter Ticket title: ");
            title = Console.ReadLine();
            Console.Write("Enter Ticket Description: ");
            description = Console.ReadLine();
        }
        public void CreateTicket()
        {
            TicketOperations obj = new TicketOperations();
            try
            {
                getTicketDetails();
                if (title == null || description == null)
                {
                    throw new Exception("Title or Description can't be null");
                }
                Ticket ticket = new Ticket(title, description);
                TicketStore.Tickets.Add(ticket);
                Console.WriteLine($"Ticket Created successfully!\nGenerated ticket id: {ticket.TicketId}");
            }
            catch(Exception exp)
            {
                Console.WriteLine($"Error: {exp}");
            }
        }
        public void DeleteTicket()
        {
            try
            {
                Console.WriteLine("Enter the Ticket Id: ");
                string ticketId = Console.ReadLine().Trim();
                if (ticketId == null)
                {
                    throw new Exception("ticketId can't be null!");
                }
                var ticket = TicketStore.Tickets.Find(t => t.TicketId == ticketId);
                if (ticket == null)
                {
                    throw new Exception("Ticket doesn't Exist!");
                }
                Console.WriteLine($"{ticket.TicketId} is Deleted Successfully!");
                TicketStore.Tickets.Remove(ticket);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"Error: {exp}");
            }
        }

        public void UpdateTicket()
        {
            try
            {
                Console.WriteLine("Enter the Ticket Id: ");
                string ticketId = Console.ReadLine().Trim();
                if (ticketId == null)
                {
                    throw new Exception("ticketId can't be null!");
                }
                var ticket = TicketStore.Tickets.Find(t=>t.TicketId == ticketId);
                if (ticket == null)
                {
                    throw new Exception("Ticket not found!!");
                }
                else
                {
                    getTicketDetails();
                    ticket.Title = title;
                    ticket.Description = description;
                }
                Console.WriteLine("Ticket Updated successfully!");
            }
            catch(Exception exp)
            {
                Console.WriteLine($"Error: {exp}");
            }
        }
        public void ViewAllTicket()
        {
            if (TicketStore.Tickets.Count == 0)
            {
                Console.WriteLine("No tickets are present! Please Create some!");
            }
            foreach(var ticket in TicketStore.Tickets)
            {
                ticket.ShowDetails();
            }
        }
        public void SearchTicket()
        {
            try
            {
                Console.WriteLine("Enter the Ticket Id: ");
                string ticketId = Console.ReadLine().Trim();
                if (ticketId == null)
                {
                    throw new Exception("ticketId can't be null!");
                }
                var ticket = TicketStore.Tickets.Find(t => t.TicketId == ticketId);
                if (ticket == null)
                {
                    throw new Exception("Ticket not found!!");
                }
                ticket.ShowDetails(ticketId);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"Error: {exp}");
            }
        }

    }
}
