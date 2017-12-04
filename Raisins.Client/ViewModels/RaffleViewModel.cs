using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Raisins.Client.ViewModels
{
    public class RaffleViewModel
    {
        private string _winner;

        public string Winner
        {
            get
            {
                if(string.IsNullOrEmpty(_winner))
                {
                    return "---";
                }
                return _winner;
            }
            set
            {
                _winner = value;
            }
        }

        private string _ticketCode;

        public string TicketCode
        {
            get
            {
                if(string.IsNullOrEmpty(_ticketCode))
                {
                    return "(-----)";
                }
                return _ticketCode;
            }
            set { _ticketCode = value; }
        }
    }
}