using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem
{
    public enum TicketStatus
    {
        Pending,
        Open,
        Inprogress,
        Resolved
    }
    
    internal class Ticket
    {
        public static string GenerateTicketId()
        {
            return "TKT" + "-" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        public string TicketId { get; private set; } 
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public TicketStatus Status { get; set; } = TicketStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public Ticket(string title, string description)
        {
            TicketId = GenerateTicketId();
            Title = title;
            Description = description;
        }

        public void ShowDetails()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"TicketId     : {TicketId}");
            Console.WriteLine($"Title     : {Title}");
            Console.WriteLine($"Desc      : {Description}");
            Console.WriteLine($"Status    : {Status}");
            Console.WriteLine($"Created At: {CreatedAt}");
            Console.WriteLine("---------------------------------\n");
        }
        public void ShowDetails(string ticketId)
        {
            var ticket = TicketStore.Tickets.Find(t => t.TicketId == ticketId);
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"TicketId     : {ticket.TicketId}");
            Console.WriteLine($"Title     : {ticket.Title}");
            Console.WriteLine($"Desc      : {ticket.Description}");
            Console.WriteLine($"Status    : {ticket.Status}");
            Console.WriteLine($"Created At: {ticket.CreatedAt}");
            Console.WriteLine("---------------------------------\n");
        }
    }
}
