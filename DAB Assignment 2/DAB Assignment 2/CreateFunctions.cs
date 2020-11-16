using System;
using System.Collections.Generic;
using System.Text;
using DAB2;
using ConsoleTables;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;


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

            Console.WriteLine("Type in citizens sex: ");
            string sex = Console.ReadLine();

            Console.WriteLine("Type in citizens social security number (10 numbers): ");
            string SSN = Console.ReadLine();

            Console.Write("Type ID of the municipality the citizen lives in: ");
            int municipalityID = int.Parse(Console.ReadLine());

            var CitizenAdd = new Citizen() { FirstName = Firstname, LastName = Lastname, Age = age, Sex = sex, SocialSecurityNumber = SSN, MunicipalityID = municipalityID };

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

            var TestCenterAdd = new TestCenter() { TestCenterID = TestCenterID, Hours = hours, MunicipalityID = municipalityID };

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

            var TestCenterManagementAdd = new TestCenterManagement() { PhoneNumber = phonenumber, Email = email, TestCenterID = testcenterid };

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

            Console.Clear();
            Console.WriteLine("Type the status of the test (Not Tested, Not Ready, Ready): ");
            string status = Console.ReadLine();
            tcc.status = status;

            Console.Clear();
            Console.WriteLine("Type the date of the test (ddmmyy): ");
            string date = Console.ReadLine();
            tcc.date = date;

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

            var LocationAdd = new Location() { Address = address, MunicipalityID = municipalityid };

            db.Add(LocationAdd);
            db.SaveChanges();

            Console.WriteLine("Location succesfully added!\n");
        }
        public void createLocationCitizen(MyDBContext db)
        {
            Console.Clear();
            Console.WriteLine("Type in the address for the location:\n");
            string address = Console.ReadLine();

            Console.WriteLine("Type in the Municipality ID for the municipality the location is located in:\n");
            int municipalityid = int.Parse(Console.ReadLine());

            var LocationAdd = new Location() { Address = address, MunicipalityID = municipalityid };

            db.Add(LocationAdd);
            db.SaveChanges();

            Console.WriteLine("Location succesfully added!\n");
        }
        public void searchForCitizen(MyDBContext db)
        {
            using (var context = new MyDBContext())
            {
                Console.WriteLine("Type the name you want to find:\n");
                string tempname = Console.ReadLine();
                var citizen = context.Citizen.Where(c => c.FirstName.Contains(tempname)).ToList();
                var table = new ConsoleTable("Firstname", "Lastname", "Age", "Sex", "SSN");

                foreach (Citizen C in citizen)
                {
                    table.AddRow(C.FirstName, C.LastName, C.Age, C.Sex, C.SocialSecurityNumber);
                }

                table.Write();
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
                                  " 99: Exit\n");

                int tempInput = int.Parse(Console.ReadLine());

                List<TestCenterCitizen> tempresult_ = new List<TestCenterCitizen>();
                switch (tempInput)
                {
                    case 1:
                        Console.WriteLine("Chose the age-group to see active Covid cases for:\n");
                        var tempresult = context.TestCenterCitizen.Where(x => x.citizen.Age >= 0 && x.citizen.Age <= 10).ToList();
                        tempresult_ = tempresult;
                        break;
                    case 2:
                        Console.WriteLine("Chose the age-group to see active Covid cases for:\n");
                        tempresult = context.TestCenterCitizen.Where(x => x.citizen.Age >= 11 && x.citizen.Age <= 20).ToList();
                        tempresult_ = tempresult;
                        break;
                    case 3:
                        Console.WriteLine("Chose the age-group to see active Covid cases for:\n");
                        tempresult = context.TestCenterCitizen.Where(x => x.citizen.Age >= 21 && x.citizen.Age <= 30).ToList();
                        tempresult_ = tempresult;
                        break;
                    case 4:
                        Console.WriteLine("Chose the age-group to see active Covid cases for:\n");
                        tempresult = context.TestCenterCitizen.Where(x => x.citizen.Age >= 31 && x.citizen.Age <= 40).ToList();
                        tempresult_ = tempresult;
                        break;
                    case 5:
                        Console.WriteLine("Chose the age-group to see active Covid cases for:\n");
                        tempresult = context.TestCenterCitizen.Where(x => x.citizen.Age >= 41 && x.citizen.Age <= 50).ToList();
                        tempresult_ = tempresult;
                        break;
                    case 6:
                        Console.WriteLine("Chose the age-group to see active Covid cases for:\n");
                        tempresult = context.TestCenterCitizen.Where(x => x.citizen.Age >= 51 && x.citizen.Age <= 60).ToList();
                        tempresult_ = tempresult;
                        break;
                    case 7:
                        Console.WriteLine("Chose the age-group to see active Covid cases for:\n");
                        tempresult = context.TestCenterCitizen.Where(x => x.citizen.Age >= 61 && x.citizen.Age <= 70).ToList();
                        tempresult_ = tempresult;
                        break;
                    case 8:
                        Console.WriteLine("Chose the age-group to see active Covid cases for:\n");
                        tempresult = context.TestCenterCitizen.Where(x => x.citizen.Age >= 71 && x.citizen.Age <= 80).ToList();
                        tempresult_ = tempresult;
                        break;
                    case 9:
                        Console.WriteLine("Chose the age-group to see active Covid cases for:\n");
                        tempresult = context.TestCenterCitizen.Where(x => x.citizen.Age >= 81 && x.citizen.Age <= 90).ToList();
                        tempresult_ = tempresult;
                        break;
                    case 0:
                        Console.WriteLine("Chose the age-group to see active Covid cases for:\n");
                        tempresult = context.TestCenterCitizen.Where(x => x.citizen.Age >= 91 && x.citizen.Age <= 100).ToList();
                        tempresult_ = tempresult;
                        break;
                }


                var table = new ConsoleTable("Positive cases");
                int PositiveCases = 0;


                foreach (TestCenterCitizen C in tempresult_)
                {
                    if (C.result == true)
                        PositiveCases++;
                    else
                    {
                        // do nothing;
                    }
                }

                table.AddRow(PositiveCases);

                table.Write();
                Console.WriteLine("press any key to end");
                Console.ReadLine();
            }
        }
    }
}
