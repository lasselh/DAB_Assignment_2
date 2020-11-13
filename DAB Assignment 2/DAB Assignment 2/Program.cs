﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
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
                    GenerateCitizens(100);
                    GenerateTestCenter(100);
                    break;

                case 2:
                    Console.Clear();
                    break;
            }

            choice = 0;

            do
            {
                Console.WriteLine("Choose your option... \n" +
                                  " 1: Create Citizen\n" +
                                  " 2: Create Testcenter\n" +
                                  " 3: Create Mangement\n" +
                                  " 4: Create Test Case\n" +
                                  " 5: Create Location\n");

                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        createCitizen();
                        break;

                    case 2:
                        createTestCenter();
                        break;

                    case 3:
                        createManagement();
                        break;

                    case 4:
                        createTestCase();
                        break;

                    case 5:
                        createLocation();
                        break;

                }

            } while (true);
        }

        public static void createCitizen()
        {
            Console.Clear();
            Console.WriteLine("Type in citizens firstname:\n");
            string Firstname = Console.ReadLine();

            Console.WriteLine("Type in citizens lastname:\n");
            string Lastname = Console.ReadLine();

            Console.WriteLine("Type in citizens age:\n");
            int age = Console.Read();

            Console.WriteLine("Type in citizens sex:\n");
            string sex = Console.ReadLine();

            Console.WriteLine("Type in citizens social security number (10 numbers)");
            string SSN = Console.ReadLine();

            Console.Write("Type ID of the municipality the citizen lives in:");
            int municipalityID = Console.Read();

            var CitizenAdd = new Citizen() { FirstName = Firstname, LastName = Lastname, Age = age, Sex = sex, SocialSecurityNumber = SSN, MunicipalityID = municipalityID };
        }
        public static void createTestCenter()
        {

        }
        public static void createManagement()
        {

        }
        public static void createTestCase()
        {

        }
        public static void createLocation()
        {

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
            var testcenter = new TestCenter();

            for (int i = 0; i < number; i++)
            {
                testcenter.TestCenterID = i;
                var temp = random.Next(Municipalities.Count);
                testcenter.MunicipalityID = Municipalities[temp];
                // hvad fuck er hours i TestCenter
                testcenter.Hours = random.Next(1, 1000);

                db.Add(testcenter);
                db.SaveChanges();
            }
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