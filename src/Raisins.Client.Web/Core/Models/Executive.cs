using System.ComponentModel.DataAnnotations;

namespace Raisins.Client.Web.Models
{
    public class Executive
    {

        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

    }
}
