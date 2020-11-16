using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DAB2;
using ConsoleTables;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.VisualBasic;


namespace DAB2
{
    class CreateFunctions
    {
        public void createCitizen(MyDBContext db)
        {
            Console.Clear();
            Console.WriteLine("Type in citizens firstname: ");
            string Firstname = Console.ReadLine();

            Console.WriteLine("Type in citizens lastname: ");
            string Lastname = Console.ReadLine();

            Console.WriteLine("Type in citizens age: ");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("Type in citizens sex (male/female): ");
            string sex = Console.ReadLine();

            Console.WriteLine("Type in citizens social security number (10 numbers): ");
            string SSN = Console.ReadLine();

            Console.Write("Type ID of the municipality the citizen lives in: ");
            int municipalityID = int.Parse(Console.ReadLine());

            var CitizenAdd = new Citizen()
            {
                FirstName = Firstname, LastName = Lastname, Age = age, Sex = sex, SocialSecurityNumber = SSN,
                MunicipalityID = municipalityID
            };

            db.Add(CitizenAdd);
            db.SaveChanges();

            Console.WriteLine("Citizen succesfully added!\n");
        }

        public void createTestCenter(MyDBContext db)
        {
            Console.Clear();
            Console.WriteLine("Type the ID for the testcenter: ");
            int TestCenterID = int.Parse(Console.ReadLine());

            Console.WriteLine("Type the opening hours of the testcenter (fx. 8-16): ");
            string hours = Console.ReadLine();

            Console.WriteLine("Type the MunicipalityID for the municipality in which the testcenter is: ");
            int municipalityID = int.Parse(Console.ReadLine());

            var TestCenterAdd = new TestCenter()
                {TestCenterID = TestCenterID, Hours = hours, MunicipalityID = municipalityID};

            db.Add(TestCenterAdd);
            db.SaveChanges();

            Console.WriteLine("TestCenter succesfully added!\n");
        }

        public void createManagement(MyDBContext db)
        {
            Console.Clear();
            Console.WriteLine("Type in TestCenterManagements PhoneNumber:(8 digits): ");
            int phonenumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Type in TestCenterManagements Email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Type in TestCenterID for the TestCenter that the this Management will manage: ");
            int testcenterid = int.Parse(Console.ReadLine());

            var TestCenterManagementAdd = new TestCenterManagement()
                {PhoneNumber = phonenumber, Email = email, TestCenterID = testcenterid};

            db.Add(TestCenterManagementAdd);
            db.SaveChanges();

            Console.WriteLine("TestCenterManagement succesfully added!\n");
        }

        public void createTestCase(MyDBContext db)
        {
            Console.Clear();
            Console.WriteLine("Type the SocialSecurityNumber of the citizen: ");
            string temp1 = Console.ReadLine();
            string ssn = temp1.Trim();

            Console.Clear();
            Console.WriteLine("Type the TestCenterID of the TestCenter where the test was made: ");
            string temp2 = Console.ReadLine();
            int testcenterid = int.Parse(temp2);

            var cit = db.Citizen.Find(ssn);
            var tcr = db.TestCenter.Find(testcenterid);

            var tcc = new TestCenterCitizen();
            tcc.SocialSecurityNumber = cit.SocialSecurityNumber;
            tcc.TestCenterID = tcr.TestCenterID;

            Console.Clear();
            Console.WriteLine("Type the date of the test (ddmmyy): ");
            string date12 = Console.ReadLine();
            tcc.date = date12;

            Console.Clear();
            Console.WriteLine("Type the status of the test (Not Tested, Not Ready, Ready): ");
            string status = Console.ReadLine();
            tcc.status = status;

            Console.Clear();
            Console.WriteLine("Type the test result: \n" +
                              "'P' for positive\n" +
                              "'N' for negative/unknown\n");
            string temp = Console.ReadLine();
            int check = 0;
            do
            {
                if (temp == "P")
                {
                    tcc.result = true;
                    check = 1;

                    using (var context = new MyDBContext())
                    {
                        var tmpresult = context.LocationCitizen.Where(x => x.SocialSecurityNumber == ssn).ToList();

                        foreach (LocationCitizen loccit in tmpresult)
                        {
                            var adress = loccit.Address;
                            var tmp2 = context.LocationCitizen.Where(x => x.Address == adress).ToList();

                            string day1 = loccit.date.Substring(0, 2);
                            string month1 = loccit.date.Substring(2, 2);
                            string year1 = "20" + loccit.date.Substring(4, 2);
                            DateTime teststring = new DateTime(int.Parse(year1), int.Parse(month1), int.Parse(day1));

                            // Får den nuværende dato
                            DateTime dt = DateTime.Now;

                            Console.WriteLine(dt);

                            DateTime dt1;
                            DateTime dt2;
                            DateTime dt3;
                            // Trækker 3 dage fra for at se om den er aktiv
                            dt1 = dt.AddDays(-3);
                            dt2 = dt.AddDays(-2);
                            dt3 = dt.AddDays(-1);

                            // Fjerner timer/minutter/sekunder fra DateTime
                            string stringdt1 = dt1.ToShortDateString();
                            dt1 = Convert.ToDateTime(stringdt1);
                            string stringdt2 = dt2.ToShortDateString();
                            dt2 = Convert.ToDateTime(stringdt2);
                            string stringdt3 = dt3.ToShortDateString();
                            dt3 = Convert.ToDateTime(stringdt3);

                            var table = new ConsoleTable("People who MIGHT be infected");

                            foreach (LocationCitizen C in tmp2)
                            {
                                // Formaterer Citizen test casens dato til DateTime og sammenligner med dt
                                string day = C.date.Substring(0, 2);
                                string month = C.date.Substring(2, 2);
                                string year = "20" + C.date.Substring(4, 2);
                                DateTime citdt = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                                float compare = teststring.CompareTo(citdt);
                                float compare1 = dt1.CompareTo(citdt);
                                float compare2 = dt2.CompareTo(citdt);
                                float compare3 = dt3.CompareTo(citdt);

                                Console.WriteLine($"citdt er: {citdt}");

                                Console.WriteLine($"teststring er: {teststring}");

                                Console.WriteLine($"dt1 er: {dt1}");
                                Console.WriteLine(compare1);

                                Console.WriteLine($"dt2 er: {dt2}");
                                Console.WriteLine(compare2);

                                Console.WriteLine($"dt3 er: {dt3}");
                                Console.WriteLine(compare3);

                                if (compare <= 0)
                                {
                                    if (C.SocialSecurityNumber != ssn)
                                    {
                                        table.AddRow(C.SocialSecurityNumber);
                                    }
                                }
                            }
                            table.Write();
                            Console.WriteLine("Press any key to end");
                            Console.ReadLine();
                        }
                    }
                }
                else if (temp == "N")
                {
                    tcc.result = false;
                    check = 1;
                }
                else
                {
                    Console.WriteLine("Please type a valid result: ");
                    temp = Console.ReadLine();
                    check = 0;
                }
            } while (check == 0);

            db.Add(tcc);
            db.SaveChanges();

            Console.WriteLine("Test Case succesfully added!\n");
        }

        public void createLocation(MyDBContext db)
        {
            Console.Clear();
            Console.WriteLine("Type in the address for the location: ");
            string address = Console.ReadLine();

            Console.WriteLine("Type in the Municipality ID for the municipality the location is located in: ");
            int municipalityid = int.Parse(Console.ReadLine());

            var LocationAdd = new Location() {Address = address, MunicipalityID = municipalityid};

            db.Add(LocationAdd);
            db.SaveChanges();

            Console.WriteLine("Location succesfully added!\n");
        }

        public void createLocationCitizen(MyDBContext db)
        {
            Console.Clear();
            Console.WriteLine("Type in the address for the location: ");
            string address = Console.ReadLine();

            Console.WriteLine("Type in the SocialSecurityNumber for the citizen: ");
            string ssn = (Console.ReadLine());

            Console.WriteLine("Type in the date ");
            string date = (Console.ReadLine());

            var LocationCitizenAdd = new LocationCitizen() {Address = address, SocialSecurityNumber = ssn, date = date};

            db.Add(LocationCitizenAdd);
            db.SaveChanges();

            Console.WriteLine("Location succesfully added!\n");
        }

        public void searchForCitizen(MyDBContext db)
        {
            using (var context = new MyDBContext())
            {
                Console.WriteLine("Type the name you want to find:");
                string tempname = Console.ReadLine();
                var citizen = context.Citizen.Where(c => c.FirstName.Contains(tempname)).ToList();
                var table = new ConsoleTable("Firstname", "Lastname", "Age", "Sex", "SSN");

                foreach (Citizen C in citizen)
                {
                    table.AddRow(C.FirstName, C.LastName, C.Age, C.Sex, C.SocialSecurityNumber);
                }

                table.Write();
                Console.WriteLine("Press any key to end");
                Console.ReadLine();
            }
        }

        public void searchforAge(MyDBContext db)
        {
            using (var context = new MyDBContext())
            {
                Console.WriteLine("Choose an age group to show amount of positive cases \n" +
                                  " 1: 0-10\n" +
                                  " 2: 11-20\n" +
                                  " 3: 21-30\n" +
                                  " 4: 31-40\n" +
                                  " 5: 41-50\n" +
                                  " 6: 51-60\n" +
                                  " 7: 61-70\n" +
                                  " 8: 71-80\n" +
                                  " 9: 81-90\n" +
                                  " 0: 91-100\n" +
                                  " 99: Exit");

                int tempInput = int.Parse(Console.ReadLine());

                List<TestCenterCitizen> tempresult_ = new List<TestCenterCitizen>();
                switch (tempInput)
                {
                    case 1:
                        Console.WriteLine("Positive cases for age group 0-10");
                        var tempresult = context.TestCenterCitizen
                            .Where(x => x.citizen.Age >= 0 && x.citizen.Age <= 10)
                            .ToList();
                        tempresult_ = tempresult;
                        break;
                    case 2:
                        Console.WriteLine("Positive cases for age group 11-20");
                        tempresult = context.TestCenterCitizen
                            .Where(x => x.citizen.Age >= 11 && x.citizen.Age <= 20)
                            .ToList();
                        tempresult_ = tempresult;
                        break;
                    case 3:
                        Console.WriteLine("Positive cases for age group 21-30");
                        tempresult = context.TestCenterCitizen
                            .Where(x => x.citizen.Age >= 21 && x.citizen.Age <= 30)
                            .ToList();
                        tempresult_ = tempresult;
                        break;
                    case 4:
                        Console.WriteLine("Positive cases for age group 31-40");
                        tempresult = context.TestCenterCitizen
                            .Where(x => x.citizen.Age >= 31 && x.citizen.Age <= 40)
                            .ToList();
                        tempresult_ = tempresult;
                        break;
                    case 5:
                        Console.WriteLine("Positive cases for age group 41-50");
                        tempresult = context.TestCenterCitizen
                            .Where(x => x.citizen.Age >= 41 && x.citizen.Age <= 50)
                            .ToList();
                        tempresult_ = tempresult;
                        break;
                    case 6:
                        Console.WriteLine("Positive cases for age group 51-60");
                        tempresult = context.TestCenterCitizen
                            .Where(x => x.citizen.Age >= 51 && x.citizen.Age <= 60)
                            .ToList();
                        tempresult_ = tempresult;
                        break;
                    case 7:
                        Console.WriteLine("Positive cases for age group 61-70");
                        tempresult = context.TestCenterCitizen
                            .Where(x => x.citizen.Age >= 61 && x.citizen.Age <= 70)
                            .ToList();
                        tempresult_ = tempresult;
                        break;
                    case 8:
                        Console.WriteLine("Positive cases for age group 71-80");
                        tempresult = context.TestCenterCitizen
                            .Where(x => x.citizen.Age >= 71 && x.citizen.Age <= 80)
                            .ToList();
                        tempresult_ = tempresult;
                        break;
                    case 9:
                        Console.WriteLine("Positive cases for age group 81-90");
                        tempresult = context.TestCenterCitizen
                            .Where(x => x.citizen.Age >= 81 && x.citizen.Age <= 90)
                            .ToList();
                        tempresult_ = tempresult;
                        break;
                    case 0:
                        Console.WriteLine("Positive cases for age group 91-100");
                        tempresult = context.TestCenterCitizen
                            .Where(x => x.citizen.Age >= 91 && x.citizen.Age <= 100)
                            .ToList();
                        tempresult_ = tempresult;
                        break;
                }

                var table = new ConsoleTable("Positive cases");
                int PositiveCases = 0;


                foreach (TestCenterCitizen C in tempresult_)
                {
                    if (C.result == true)
                        PositiveCases++;
                }

                table.AddRow(PositiveCases);

                table.Write();
                Console.WriteLine("Press any key to end");
                Console.ReadLine();
            }
        }

        public void searchforSex(MyDBContext db)
        {
            List<TestCenterCitizen> _tempresult = new List<TestCenterCitizen>();
            using (var context = new MyDBContext())
            {
                Console.WriteLine("Type the gender you want to search by (male/female): ");
                string sex = Console.ReadLine();

                switch (sex)
                {
                    case "male":
                        Console.WriteLine("Positive cases for men");
                        var tempsex = context.TestCenterCitizen
                            .Where(c => c.citizen.Sex == "male")
                            .ToList();
                        _tempresult = tempsex;
                        break;
                    case "female":
                        Console.WriteLine("Positive cases for women");
                        tempsex = context.TestCenterCitizen
                            .Where(c => c.citizen.Sex == "female")
                            .ToList();
                        _tempresult = tempsex;
                        break;
                }

                var table = new ConsoleTable("Positive cases");
                int PositiveCases = 0;

                foreach (TestCenterCitizen C in _tempresult)
                {
                    if (C.result == true)
                        PositiveCases++;
                }

                table.AddRow(PositiveCases);
                table.Write();
                Console.WriteLine("Press any key to end");
                Console.ReadLine();
            }
        }

        public void SearchForMunincipality(MyDBContext db)
        {
            List<TestCenterCitizen> _tmpresult = new List<TestCenterCitizen>();
            using (var context = new MyDBContext())
            {
                Console.WriteLine("Type the municipalityId you want to see: ");
                int mid = int.Parse(Console.ReadLine());

                Console.WriteLine($"Positive cases for munincipality {mid}");
                var tempres = context.TestCenterCitizen
                    .Where(tcc => tcc.citizen.MunicipalityID == mid).ToList();

                _tmpresult = tempres;

                var table = new ConsoleTable("Active positive cases");
                int PositiveCases = 0;

                // Får den nuværende dato
                DateTime dt = DateTime.Now;
                // Trækker 14 dage fra for at se om den er aktiv
                dt = dt.AddDays(-14);
                // Laver det om til en string, formarterer det osv.
                //string date = dt.ToShortDateString();
                //date = date.Replace("-", "");
                //string date1 = date.Substring(0,4);
                //string date2 = date.Substring(6, 2);
                //date = date1 + date2;

                foreach (TestCenterCitizen C in _tmpresult)
                {
                    // Formaterer Citizen test casens dato til DateTime og sammenligner med dt
                    string day = C.date.Substring(0, 2);
                    string month = C.date.Substring(2, 2);
                    string year = "20" + C.date.Substring(4, 2);
                    DateTime citdt = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                    float compare = dt.CompareTo(citdt);

                    if (C.result == true && compare <= 0)
                        PositiveCases++;
                }

                table.AddRow(PositiveCases);
                table.Write();
                Console.WriteLine("Press any key to end");
                Console.ReadLine();
            }
        }
    }
}
