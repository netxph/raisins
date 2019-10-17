using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Raisins.Client.ViewModels
{
    public class RaffleViewModel
    {
        private List<string> _winner;

        public List<string> Winner
        {
            get
            {
                /*if(_winner == null)
                {
                    return "---";
                }*/
                return _winner;
            }
            set
            {
                _winner = value;
            }
        }

        private List<string> _ticketCode;

        public List<string> TicketCode
        {
            get
            {
                /*if(string.IsNullOrEmpty(_ticketCode))
                {
                    return "(-----)";
                }*/
                return _ticketCode;
            }
            set { _ticketCode = value; }
        }
    }
}