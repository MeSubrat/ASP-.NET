using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem
{
    internal class TicketStore
    {
        public static List<Ticket> Tickets { get; private set; } = new List<Ticket>();
    }
}
