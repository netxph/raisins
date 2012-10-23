using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class Beneficiary
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public static List<Beneficiary> GetAll()
        {
            using (var db = DbFactory.Create())
            {
                return db.Beneficiaries.ToList();
            }
        }

        public static Beneficiary Find(int id = 0)
        {
            using (var db = DbFactory.Create())
            {
                return db.Beneficiaries.Find(id);
            }
        }

        public static Beneficiary Add(Beneficiary Beneficiary)
        {
            using (var db = DbFactory.Create())
            {
                db.Beneficiaries.Add(Beneficiary);
                db.SaveChanges();

                return Beneficiary;
            }
        }

        public static Beneficiary Edit(Beneficiary Beneficiary)
        {
            using (var db = DbFactory.Create())
            {
                db.Entry(Beneficiary).State = EntityState.Modified;
                db.SaveChanges();

                return Beneficiary;
            }
        }

        public static void Delete(int id)
        {
            using (var db = DbFactory.Create())
            {
                var Beneficiary = Find(id);

                db.Beneficiaries.Remove(Beneficiary);
                db.SaveChanges();
            }
        }

    }
}