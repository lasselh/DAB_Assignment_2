using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace DAB2
{
    class Program
    {
        static MyDBContext db = new MyDBContext();

        static List<int> Municipalities = new List<int>();

        static void Main(string[] args)
        {
            int choice;
            Console.WriteLine("Make a choice! \n" +
                              " 1: Add Denmark municipality and random dummy citizens\n" +
                              " 2: Empty database\n" +
                              "Enter number: ");
            choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    ParseMunicipality();
                    GenerateTestCenter(100);
                    GenerateCitizens(100);
                    break;

                case 2:
                    Console.Clear();
                    break;
            }

            //do
            //{
            //    Console.WriteLine("Choose your option... \n" +
            //                      " 1: Create Citizen\n" +
            //                      " 2: Create Testcenter/Mangement\n" +
            //                      " 3: Create Test Case\n" +
            //                      " 4: Create Location\n");
            //} while (true);
        }



        /////////////////////////////////////////////
        /////////////////////////////////////////////
        // HJÆLPE FUNKTIONER DER DANNER DUMMY DATA //
        /////////////////////////////////////////////
        /////////////////////////////////////////////
        private static void ParseMunicipality()
        {
            var dk = new Nation();
            string country = "Danmark";
            dk.nationName = country;
            db.Add(dk);
            db.SaveChanges();

            string line = "";
            var reader = new StreamReader("./Municipality_test_pos.csv");
            reader.ReadLine(); // Skip first

            while ((line = reader.ReadLine()) != null)
            {
                var data = line.Split(";");
                var population = float.Parse(data[4].Trim());

                var mun = new Municipality();
                mun.MunicipalityID = int.Parse(data[0].Trim());
                mun.Name = data[1].Trim();
                mun.Population = population;
                mun.nationName = country;

                Municipalities.Add(int.Parse(data[0].Trim()));

                db.Add(mun);
                db.SaveChanges();
            }
            reader.Close();
        }

        static string[] Firstnames = new string[] 
        {
            "Hans", "Jens", "Henrik", "Jesper", "Morten", "Mathias", "Alice", "Bob", "Mette", "Lene", "Hanne", "Lise", "Lise", "Mikkel", "Lasse", "Mathias"
        };

        static string[] Lastnames = new string[] 
        {
            "Hansen", "Jensen", "Olsen", "Mortensen", "Frederiksen", "Larsen", "Vigen"
        };

        static string[] Genders = new string[] { "female", "male" };

        static Random random = new Random();

        public static void GenerateCitizens(int number = 100)
        {
            for (var i = 0; i < number; i++)
            {
                var first = Firstnames[random.Next(Firstnames.Length)];
                var last = Lastnames[random.Next(Lastnames.Length)];
                var gender = Genders[random.Next(Genders.Length)];
                var age = random.Next(100);
                var ssn = $"{getDate()}{getMonth()}{getYear(age)}{getControl()}";
                var temp = random.Next(Municipalities.Count);
                var mid = Municipalities[temp];

                var cit = new Citizen();
                cit.FirstName = first;
                cit.LastName = last;
                cit.Sex = gender;
                cit.Age = age;
                cit.SocialSecurityNumber = ssn;
                cit.MunicipalityID = mid;

                db.Add(cit);
                db.SaveChanges();
            }
        }

        public static string getDate()
        {
            return setZeroes(random.Next(28) + 1);
        }
        public static string getMonth()
        {
            return setZeroes(random.Next(12) + 1);
        }
        public static string getYear(int age)
        {
            var birthYear = 2020 - age;
            return birthYear.ToString().Substring(2);
        }
        public static string getControl()
        {
            return setZeroes(random.Next(10000), 4);
        }

        public static string setZeroes(int i, int length = 2)
        {
            return ("" + i).PadLeft(length, '0');
        }

        public static void GenerateTestCenter(int number)
        {
            for (int i = 1; i < (number+1); i++)
            {
                var testcenter = new TestCenter();
                testcenter.TestCenterID = i;
                var temp = random.Next(Municipalities.Count);
                testcenter.MunicipalityID = Municipalities[temp];
                // hvad fuck er hours i TestCenter
                testcenter.Hours = "random.Next(1, 1000)";

                db.Add(testcenter);
                db.SaveChanges();
            }
        }

        public static void AddCitizenToTestCenter(string ssn, int testcenterid)
        {
            var cit = db.Citizen.Find(ssn);
            var tcr = db.TestCenter.Find(testcenterid);
        }
    }
}







//var cit = new Citizen();
//cit.FirstName = "Mikke";
//cit.LastName = "bøsse";
//cit.SocialSecurityNumber = 1234;
//cit.MunicipalityID = 1234;
//cit.Age = 12;
//cit.Sex = "Male";
//cit.municipality = new Municipality();
//cit.locationCitizens = new List<LocationCitizen>();
//cit.testCenterCitizens = new List<TestCenterCitizen>();

//db.Add(cit);
//db.SaveChanges();