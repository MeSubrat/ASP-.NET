using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem
{
    internal interface ITicketOperations
    {
        public void CreateTicket();
        public void DeleteTicket();
        public void UpdateTicket();
        public void ViewAllTicket();
        public void SearchTicket();
    }
}
