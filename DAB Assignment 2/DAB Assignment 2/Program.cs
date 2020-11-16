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
            int choice2;
            bool finished = false;
            bool finishedSearch = false;


            // program start
            Console.WriteLine("Make a choice! \n" +
                              " 1: Add Denmark municipality and random dummy data\n" +
                              " 2: Empty database");
            choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    gf.ParseMunicipality(db);
                    gf.GenerateTestCenter(db, 100);
                    gf.GenerateCitizens(db, 100);
                    gf.AddCitizenToTestCenter(db, 100);
                    gf.GenerateLocation(db, 100);
                    gf.AddCitizenToLocation(db, 100);
                    gf.GenerateTestCenterManagement(db, 100);
                    break;

                case 2:
                    Console.Clear();
                    break;
            }

            choice = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose an option... \n" +
                                  " 1: Create Citizen\n" +
                                  " 2: Create Test Center\n" +
                                  " 3: Create Mangement\n" +
                                  " 4: Create Test Case\n" +
                                  " 5: Create Location\n" +
                                  " 6: Create LocationCitizen\n" +
                                  " 7: Search the database\n" +
                                  " 0: Exit");

                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        cf.createCitizen(db);
                        break;

                    case 2:
                        cf.createTestCenter(db);
                        break;

                    case 3:
                        cf.createManagement(db);
                        break;

                    case 4:
                        cf.createTestCase(db);
                        break;

                    case 5:
                        cf.createLocation(db);
                        break;
                    case 6:
                        cf.createLocationCitizen(db);
                        break;

                    case 7:
                        Console.Clear();
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("What do you want to search by? \n" +
                                              " 1: Search by name\n" +
                                              " 2: Search by age groups\n" +
                                              " 3: Search by gender\n" +
                                              " 4: Search by municipality\n" +
                                              " 0: Exit search");

                            choice2 = Convert.ToInt32(Console.ReadLine());
                            switch (choice2)
                            {
                                case 1:
                                    cf.searchForCitizen(db);
                                    break;
                                case 2:
                                    cf.searchforAge(db);
                                    break;
                                case 3:
                                    cf.searchforSex(db);
                                    break;
                                case 4:
                                    cf.SearchForMunincipality(db);
                                    break;
                                case 0:
                                    finishedSearch = true;
                                    break;
                            }
                        } while (finishedSearch == false);
                        break;
                    case 0:
                        finished = true;
                        break;
                }

            } while (finished == false);
        }
    }
}