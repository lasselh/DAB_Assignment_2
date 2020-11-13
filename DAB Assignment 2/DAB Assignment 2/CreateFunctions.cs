using System;
using System.Collections.Generic;
using System.Text;
using DAB2;

namespace DAB2
{
    class CreateFunctions
    {
        public void createCitizen()
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
        public void createTestCenter()
        {

        }
        public void createManagement()
        {

        }
        public void createTestCase()
        {

        }
        public void createLocation()
        {

        }
    }
}
