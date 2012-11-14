using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Raisins.Client.Web.Models
{
    public class Executive
    {

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }


        public static List<Executive> GetAll()
        {
            using (var db = ObjectProvider.CreateDB())
            {
                return db.Executives.ToList();
            }
        }
    }
}
