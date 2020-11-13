using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using DAB2;

namespace DAB2
{
    class Program
    {
        
        static MyDBContext db = new MyDBContext();

        static void Main(string[] args)
        {
            GenerateFunctions gf = new GenerateFunctions();
            CreateFunctions cf = new CreateFunctions();

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
                    gf.ParseMunicipality(db);
                    gf.GenerateTestCenter(db,100);
                    gf.GenerateCitizens(db,100);
                    break;

                case 2:
                    Console.Clear();
                    break;
            }


            // Mikkel skriv under den her linje så vi ikke fucking hinanden op 
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
                        cf.createCitizen();
                        break;

                    case 2:
                        cf.createTestCenter();
                        break;

                    case 3:
                        cf.createManagement();
                        break;

                    case 4:
                        cf.createTestCase();
                        break;

                    case 5:
                        cf.createLocation();
                        break;

                }

            } while (true);
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