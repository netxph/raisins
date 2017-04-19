using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.ViewModels
{
    public class CheckModel
    {
        public CheckModel(int id, string name, bool check)
        {
            Id = id;
            Name = name;
            Checked = check;
        }
        public CheckModel(int id, string name)
        {
            Id = id;
            Name = name;
            Checked = false;
        }
        public CheckModel() { }
        public int Id{ get ; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }

        public void SetChecked(bool check)
        {
            Checked = check;
        }
    }
}