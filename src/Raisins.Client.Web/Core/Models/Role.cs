using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.Web.Models
{
    public class Role
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsAdmin()
        {
            if (ID == 1) return true;
            else return false;
        }


    }
}
