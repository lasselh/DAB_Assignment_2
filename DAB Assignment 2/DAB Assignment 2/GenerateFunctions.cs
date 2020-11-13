using System;
using System.Collections.Generic;
using System.Text;
using DAB2;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.IO;

namespace DAB2
{
    class GenerateFunctions
    {
        ////////////////////////////////////////////////
        // Hjælpefunktioner hugget fra Henriks github //
        ////////////////////////////////////////////////
        static string[] Firstnames = new string[] {
            "Hans", "Jens", "Henrik", "Jesper", "Morten", "Mathias", "Alice", "Bob", "Mette", "Lene", "Hanne", "Lise", "Lise", "Mikkel", "Lasse", "Mathias"
        };
        static string[] Lastnames = new string[] {
            "Hansen", "Jensen", "Olsen", "Mortensen", "Frederiksen", "Larsen", "Vigen"
        };
        static string[] Genders = new string[] { "female", "male" };
        static Random random = new Random();
        public string getDate() {
            return setZeroes(random.Next(28) + 1);
        }
        public string getMonth() {
            return setZeroes(random.Next(12) + 1);
        }
        public string getYear(int age) {
            var birthYear = 2020 - age;
            return birthYear.ToString().Substring(2);
        }
        public string getControl() {
            return setZeroes(random.Next(10000), 4);
        }
        public string setZeroes(int i, int length = 2) {
            return ("" + i).PadLeft(length, '0');
        }
        ////////////////////////////////////////////////
        ////////// Vores funktioner under //////////////
        ////////////////////////////////////////////////
        

        public List<int> Municipalities = new List<int>();
        public void ParseMunicipality(MyDBContext db)
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

        /// <summary>
        /// Generates set amount of citizens. Default 100.
        /// </summary>
        /// <param name="db">DatabaseContext to add to</param>
        /// <param name="number">Amount of citizens to generate</param>
        public void GenerateCitizens(MyDBContext db, int number = 100)
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

        public void GenerateTestCenter(MyDBContext db, int number = 100)
        {
            for (int i = 1; i < (number + 1); i++)
            {
                var testcenter = new TestCenter();
                testcenter.TestCenterID = i;
                var temp = random.Next(Municipalities.Count);
                testcenter.MunicipalityID = Municipalities[temp];
                // hvad fuck er hours i TestCenter
                testcenter.Hours = "8-20";

                db.Add(testcenter);
                db.SaveChanges();
            }
        }

        public static void AddCitizenToTestCenter(MyDBContext db, string ssn, int testcenterid, bool result, string status, string date)
        {
            var cit = db.Citizen.Find(ssn);
            var tcr = db.TestCenter.Find(testcenterid);

            var tcc = new TestCenterCitizen();
            tcc.SocialSecurityNumber = cit.SocialSecurityNumber;
            tcc.TestCenterID = tcr.TestCenterID;

        }
    }
}
