using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Helper
{
    public static class Validator
    {

        public static bool IsAmountValid(decimal amount)
        {

            bool amountValid = false;

            if (amount > 0)
            {
                amountValid = true;
            }

            return amountValid;
        }

        public static bool IsAmountWithinRatio(decimal ratio, decimal amount)
        {

            bool withinRatio = false;

            if (amount % ratio == 0)
            {
                withinRatio = true;
            }

            return withinRatio;

        }

    }
}