using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Raisins.Client.Web.Models
{
    public class MailQueue
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Content { get; set; }

        public static MailQueue Push(MailQueue mail)
        {
            using(var db = ObjectProvider.CreateDB())
            {
                db.MailQueues.Add(mail);
                db.SaveChanges();

                return mail;
            }
        }
    }
}
