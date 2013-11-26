namespace Raisins.Client.Web.Migrations
{
    using System.Data.Entity.Migrations;
    using Raisins.Client.Web.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "Accounts",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    UserName = c.String(),
                    Password = c.String(),
                    Salt = c.String(),
                    AccountProfileID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("AccountProfiles", t => t.AccountProfileID, cascadeDelete: true)
                .Index(t => t.AccountProfileID);

            CreateTable(
                "Roles",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "AccountProfiles",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    IsLocal = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Beneficiaries",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Currencies",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    CurrencyCode = c.String(nullable: false),
                    Ratio = c.Decimal(nullable: false, precision: 18, scale: 2),
                    ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Payments",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Location = c.String(),
                    Email = c.String(),
                    Remarks = c.String(),
                    ClassID = c.Int(nullable: false),
                    Locked = c.Boolean(nullable: false),
                    BeneficiaryID = c.Int(nullable: false),
                    ExecutiveID = c.Int(),
                    CurrencyID = c.Int(nullable: false),
                    CreatedByID = c.Int(nullable: false),
                    AuditedByID = c.Int(),
                    //starts here
                    //CreatedDate=c.DateTime(nullable:false,defaultValue:DateTime.UtcNow),
                    //ModifiedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Beneficiaries", t => t.BeneficiaryID, cascadeDelete: true)
                .ForeignKey("Executives", t => t.ExecutiveID)
                .ForeignKey("Currencies", t => t.CurrencyID, cascadeDelete: true)
                .ForeignKey("Accounts", t => t.CreatedByID, cascadeDelete: true)
                .ForeignKey("Accounts", t => t.AuditedByID)
                .Index(t => t.BeneficiaryID)
                .Index(t => t.ExecutiveID)
                .Index(t => t.CurrencyID)
                .Index(t => t.CreatedByID)
                .Index(t => t.AuditedByID);
            //startshere
            //.Index(t => t.CreatedDate)
            //.Index(t => t.ModifiedDate);

            CreateTable(
                "Tickets",
                c => new
                {
                    ID = c.Long(nullable: false, identity: true),
                    TicketCode = c.String(),
                    Name = c.String(),
                    Payment_ID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Payments", t => t.Payment_ID)
                .Index(t => t.Payment_ID);

            CreateTable(
                "Executives",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Activities",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "AccountRoles",
                c => new
                {
                    Account_ID = c.Int(nullable: false),
                    Role_ID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Account_ID, t.Role_ID })
                .ForeignKey("Accounts", t => t.Account_ID, cascadeDelete: true)
                .ForeignKey("Roles", t => t.Role_ID, cascadeDelete: true)
                .Index(t => t.Account_ID)
                .Index(t => t.Role_ID);

            CreateTable(
                "AccountProfileBeneficiaries",
                c => new
                {
                    AccountProfile_ID = c.Int(nullable: false),
                    Beneficiary_ID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.AccountProfile_ID, t.Beneficiary_ID })
                .ForeignKey("AccountProfiles", t => t.AccountProfile_ID, cascadeDelete: true)
                .ForeignKey("Beneficiaries", t => t.Beneficiary_ID, cascadeDelete: true)
                .Index(t => t.AccountProfile_ID)
                .Index(t => t.Beneficiary_ID);

            CreateTable(
                "AccountProfileCurrencies",
                c => new
                {
                    AccountProfile_ID = c.Int(nullable: false),
                    Currency_ID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.AccountProfile_ID, t.Currency_ID })
                .ForeignKey("AccountProfiles", t => t.AccountProfile_ID, cascadeDelete: true)
                .ForeignKey("Currencies", t => t.Currency_ID, cascadeDelete: true)
                .Index(t => t.AccountProfile_ID)
                .Index(t => t.Currency_ID);

            CreateTable(
                "ActivityRoles",
                c => new
                {
                    Activity_ID = c.Int(nullable: false),
                    Role_ID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Activity_ID, t.Role_ID })
                .ForeignKey("Activities", t => t.Activity_ID, cascadeDelete: true)
                .ForeignKey("Roles", t => t.Role_ID, cascadeDelete: true)
                .Index(t => t.Activity_ID)
                .Index(t => t.Role_ID);

            //start

            //newusers
            using (var db = ObjectProvider.CreateDB())
            {
                Seeder.Seed(db);

                if (!Account.Exists("mendozg"))
                {
                    Account.CreateUser("mendozg", "mendozg!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Administrator")
                },
                        new AccountProfile() { Name = "Mendoza, Gen", IsLocal = false, Currencies = db.Currencies.ToList(), Beneficiaries = db.Beneficiaries.ToList() });
                }

                if (!Account.Exists("delacle"))
                {
                    Account.CreateUser("delacle", "delacle!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Administrator")
                },
                        new AccountProfile() { Name = "Dela Cruz, Lei", IsLocal = false, Currencies = db.Currencies.ToList(), Beneficiaries = db.Beneficiaries.ToList() });
                }

                if (!Account.Exists("pascuaa"))
                {
                    Account.CreateUser("pascuaa", "pascuaa!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Administrator")
                },
                        new AccountProfile() { Name = "Pascual, Arla", IsLocal = false, Currencies = db.Currencies.ToList(), Beneficiaries = db.Beneficiaries.ToList() });
                }

                //if (!Account.Exists("bolalie"))
                //{
                //    Account.CreateUser("bolalie", "bolalie!23",
                //        new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "Accountant")
                //},
                //        new AccountProfile() { Name = "Bolalin, Lian", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.ToList() });
                //}

                //if (!Account.Exists("angkiko"))
                //{
                //    Account.CreateUser("angkiko", "angkiko!23",
                //        new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "Accountant")
                //},
                //        new AccountProfile() { Name = "Angkiko, Youmi", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Saboteurs").ToList() });
                //}

                //if (!Account.Exists("bisaron"))
                //{
                //    Account.CreateUser("bisaron", "bisaron!23",
                //        new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "Accountant")
                //},
                //        new AccountProfile() { Name = "Bisa, Rona", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Remedy").ToList() });
                //}

                //if (!Account.Exists("santiaa"))
                //{
                //    Account.CreateUser("santiaa", "santiaa!23",
                //        new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "Accountant")
                //},
                //        new AccountProfile() { Name = "Santiago, Roanne", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "JNG Project").ToList() });
                //}

                //if (!Account.Exists("limcari"))
                //{
                //    Account.CreateUser("limcari", "limcari!23",
                //        new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "Accountant")
                //},
                //        new AccountProfile() { Name = "Lim, Terry", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Res Band").ToList() });
                //}

                //if (!Account.Exists("lucayar"))
                //{
                //    Account.CreateUser("lucayar", "lucayar!23",
                //        new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "User")
                //},
                //        new AccountProfile() { Name = "Lucaya, Rodney", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "Forever Wassaque").ToList() });
                //}

                //USERS

                if (!Account.Exists("cadizbl"))
                {
                    Account.CreateUser("cadizbl", "cadizbl!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Cadiz, Bless", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "AOPSmith").ToList() });
                }

                if (!Account.Exists("tagapag"))
                {
                    Account.CreateUser("tagapag", "tagapag!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Tagapan, Grace", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "AOPSmith").ToList() });
                }

                if (!Account.Exists("gabriem"))
                {
                    Account.CreateUser("gabriem", "gabriem!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Gabriel, Leslie", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "AOPSmith").ToList() });
                }

                if (!Account.Exists("corteza"))
                {
                    Account.CreateUser("corteza", "corteza!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Cortez, Aldrin", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "aQApella").ToList() });
                }

                if (!Account.Exists("pascuac"))
                {
                    Account.CreateUser("pascuac", "pascuac!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Pascual, Carmelyn", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "aQApella").ToList() });
                }

                if (!Account.Exists("perezan"))
                {
                    Account.CreateUser("perezan", "perezan!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Perez, Venerando", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "BANANA GANG").ToList() });
                }

                if (!Account.Exists("padernc"))
                {
                    Account.CreateUser("padernc", "padernc!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Padernal, John Carlos", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "BANANA GANG").ToList() });
                }

                if (!Account.Exists("leonorw"))
                {
                    Account.CreateUser("leonorw", "leonorw!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Leonor, William", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "MONSTROU").ToList() });
                }

                if (!Account.Exists("vidallu"))
                {
                    Account.CreateUser("vidallu", "vidallu!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Vidal, Lui", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "MONSTROU").ToList() });
                }

                if (!Account.Exists("quillas"))
                {
                    Account.CreateUser("quillas", "quillas!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Quillan, Shane", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "That's IT").ToList() });
                }


                if (!Account.Exists("bolalie"))
                {
                    Account.CreateUser("bolalie", "bolalie!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "Bolalin, Eliza", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "That's IT").ToList() });
                }


                if (!Account.Exists("decastt"))
                {
                    Account.CreateUser("decastt", "decastt!23",
                        new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "User")
                },
                        new AccountProfile() { Name = "De Castro, Teodore", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "That's IT").ToList() });
                }

                //ACCOUNTANTS

                if (!Account.Exists("linggay"))
                {
                    Account.CreateUser("linggay", "linggay!23",
                            new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                            new AccountProfile() { Name = "Ling, Gayle", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "MONSTROU").ToList() });
                }

                if (!Account.Exists("jaraban"))
                {
                    Account.CreateUser("jaraban", "jaraban!23",
                            new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                            new AccountProfile() { Name = "Jaraba, Noreen", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "MONSTROU").ToList() });
                }

                if (!Account.Exists("logicam"))
                {
                    Account.CreateUser("logicam", "logicam!23",
                            new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                            new AccountProfile() { Name = "Logica, Mia", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "aQApella").ToList() });
                }

                if (!Account.Exists("macalim"))
                {
                    Account.CreateUser("macalim", "macalim!23",
                            new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                            new AccountProfile() { Name = "Macalintal, Dianne", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "BANANA GANG").ToList() });
                }

                if (!Account.Exists("evangec"))
                {
                    Account.CreateUser("evangec", "evangec!23",
                            new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                            new AccountProfile() { Name = "Evangelista, Charry", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "AOPSmith").ToList() });
                }


                if (!Account.Exists("pardoja"))
                {
                    Account.CreateUser("pardoja", "pardoja!23",
                            new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                            new AccountProfile() { Name = "Pardo, Jazel", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "AARONics").ToList() });
                }


                if (!Account.Exists("candalm"))
                {
                    Account.CreateUser("candalm", "candalm!23",
                            new List<Role>()  
                { 
                    db.Roles.First(r => r.Name == "Accountant")
                },
                            new AccountProfile() { Name = "Candaliza, Marlo", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "That's IT").ToList() });
                }



                //new-users1
                //var account = db.Accounts.FirstOrDefault(a => a.UserName == "bolalie");

                //if (account != null)
                //{
                //    db.Accounts.Remove(account);
                //    db.SaveChanges();
                //}

                //account = db.Accounts.FirstOrDefault(a => a.UserName == "bisaron");

                //if (account != null)
                //{
                //    db.Accounts.Remove(account);
                //    db.SaveChanges();
                //}

                //account = db.Accounts.FirstOrDefault(a => a.UserName == "santiaa");

                //if (account != null)
                //{
                //    db.Accounts.Remove(account);
                //    db.SaveChanges();
                //}

                //if (!Account.Exists("bolalie"))
                //{
                //    Account.CreateUser("bolalie", "bolalie!23",
                //    new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "User")
                //},
                //    new AccountProfile() { Name = "Bolalin, Lian", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Remedy").ToList() });
                //}

                //if (!Account.Exists("santiaa"))
                //{
                //    Account.CreateUser("santiaa", "santiaa!23",
                //            new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "Accountant")
                //},
                //            new AccountProfile() { Name = "Santiago, Roanne", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Remedy").ToList() });
                //}

                //if (!Account.Exists("sorianm"))
                //{
                //    Account.CreateUser("sorianm", "sorianm!23",
                //            new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "User")
                //},
                //            new AccountProfile() { Name = "Soriano, Michelle", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "JNG Project").ToList() });
                //}

                //if (!Account.Exists("mendozg"))
                //{
                //    Account.CreateUser("mendozg", "mendozg!23",
                //        new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "Accountant")
                //},
                //        new AccountProfile() { Name = "Mendoza, Genecel", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "JNG Project").ToList() });
                //}

                //if (!Account.Exists("delosrd"))
                //{
                //    Account.CreateUser("delosrd", "delosrd!23",
                //            new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "User")
                //},
                //            new AccountProfile() { Name = "Delos Reyes, Danica", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Res Band").ToList() });
                //}


                //new-users2

                //if (!Account.Exists("remultj"))
                //{
                //    Account.CreateUser("remultj", "remultj!23",
                //    new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "User")
                //},
                //    new AccountProfile() { Name = "Remulta, Jared", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "Forever Wassaque").ToList() });
                //}

                //if (!Account.Exists("delacrl"))
                //{
                //    Account.CreateUser("delacrl", "delacrl!23",
                //    new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "User")
                //},
                //    new AccountProfile() { Name = "Dela Cruz, Lyndon", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "Forever Wassaque").ToList() });
                //}

                //if (!Account.Exists("perezan"))
                //{
                //    Account.CreateUser("perezan", "perezan!23",
                //    new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "User")
                //},
                //    new AccountProfile() { Name = "Perez, Andy", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "Forever Wassaque").ToList() });
                //}

                //new-activities

                var activity = db.Activities.SingleOrDefault(a => a.Name == "Home.Dashboard");

                if (activity == null)
                {
                    var dashboardActivity = new Activity() { Name = "Home.Dashboard", Roles = new List<Role>() { db.Roles.First(r => r.Name == "Administrator") } };

                    db.Activities.Add(dashboardActivity);
                    db.SaveChanges();
                }

                //update-users
                //account = db.Accounts.FirstOrDefault(a => a.UserName == "reyesce");

                //if (account != null)
                //{
                //    db.Accounts.Remove(account);
                //    db.SaveChanges();
                //}

                //Account.CreateUser("reyesce", "reyesce!23",
                //        new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "Accountant")
                //},
                //        new AccountProfile() { Name = "Reyes, Aubrey", IsLocal = true, Currencies = new List<Currency>(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "Forever Wassaque").ToList() });



                //new-user 3

                //if (!Account.Exists("ruetase"))
                //{
                //    Account.CreateUser("ruetase", "ruetase!23",
                //        new List<Role>()  
                //{ 
                //    db.Roles.First(r => r.Name == "User")
                //},
                //        new AccountProfile() { Name = "Ruetas, Elizabeth", IsLocal = true, Currencies = db.Currencies.Where(c => c.CurrencyCode == "PHP").ToList(), Beneficiaries = db.Beneficiaries.Where(b => b.Name == "The Res Band").ToList() });
                //}

            }





        }





        //down
        public override void Down()
        {
            DropIndex("ActivityRoles", new[] { "Role_ID" });
            DropIndex("ActivityRoles", new[] { "Activity_ID" });
            DropIndex("AccountProfileCurrencies", new[] { "Currency_ID" });
            DropIndex("AccountProfileCurrencies", new[] { "AccountProfile_ID" });
            DropIndex("AccountProfileBeneficiaries", new[] { "Beneficiary_ID" });
            DropIndex("AccountProfileBeneficiaries", new[] { "AccountProfile_ID" });
            DropIndex("AccountRoles", new[] { "Role_ID" });
            DropIndex("AccountRoles", new[] { "Account_ID" });
            DropIndex("Tickets", new[] { "Payment_ID" });
            DropIndex("Payments", new[] { "AuditedByID" });
            DropIndex("Payments", new[] { "CreatedByID" });
            DropIndex("Payments", new[] { "CurrencyID" });
            DropIndex("Payments", new[] { "ExecutiveID" });
            DropIndex("Payments", new[] { "BeneficiaryID" });
            DropIndex("Accounts", new[] { "AccountProfileID" });
            DropForeignKey("ActivityRoles", "Role_ID", "Roles");
            DropForeignKey("ActivityRoles", "Activity_ID", "Activities");
            DropForeignKey("AccountProfileCurrencies", "Currency_ID", "Currencies");
            DropForeignKey("AccountProfileCurrencies", "AccountProfile_ID", "AccountProfiles");
            DropForeignKey("AccountProfileBeneficiaries", "Beneficiary_ID", "Beneficiaries");
            DropForeignKey("AccountProfileBeneficiaries", "AccountProfile_ID", "AccountProfiles");
            DropForeignKey("AccountRoles", "Role_ID", "Roles");
            DropForeignKey("AccountRoles", "Account_ID", "Accounts");
            DropForeignKey("Tickets", "Payment_ID", "Payments");
            DropForeignKey("Payments", "AuditedByID", "Accounts");
            DropForeignKey("Payments", "CreatedByID", "Accounts");
            DropForeignKey("Payments", "CurrencyID", "Currencies");
            DropForeignKey("Payments", "ExecutiveID", "Executives");
            DropForeignKey("Payments", "BeneficiaryID", "Beneficiaries");
            DropForeignKey("Accounts", "AccountProfileID", "AccountProfiles");
            DropTable("ActivityRoles");
            DropTable("AccountProfileCurrencies");
            DropTable("AccountProfileBeneficiaries");
            DropTable("AccountRoles");
            DropTable("Activities");
            DropTable("Executives");
            DropTable("Tickets");
            DropTable("Payments");
            DropTable("Currencies");
            DropTable("Beneficiaries");
            DropTable("AccountProfiles");
            DropTable("Roles");
            DropTable("Accounts");
        }
    }
}
