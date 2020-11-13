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
            Console.Clear();
            Console.WriteLine("Type the ID for the testcenter:\n");
            int TestCenterID = Console.Read();

            Console.WriteLine("Type the opening hours of the testcenter (fx. 8-16)");
            string hours = Console.ReadLine();

            Console.WriteLine("Type the MunicipalityID for the municipality in which the testcenter is");
            int municipalityID = Console.Read();

            var TestCenterAdd = new TestCenter() { TestCenterID = TestCenterID, Hours = hours, MunicipalityID = municipalityID };
        }
        public void createManagement()
        {
            Console.Clear();
            Console.WriteLine("Type in TestCenterManagements PhoneNumber:(8 digits)");
            int phonenumber = Console.Read();

            Console.WriteLine("Type in TestCenterManagements Email:");
            string email = Console.ReadLine();

            Console.WriteLine("Type in TestCenterID for the TestCenter that the this Management will manage:");
            int testcenterid = Console.Read();

            var TestCenterManagementAdd = new TestCenterManagement() { PhoneNumber = phonenumber, Email = email, TestCenterID = testcenterid };
        }
        public void createTestCase()
        {

        }
        public void createLocation()
        {
            Console.Clear();
            Console.WriteLine("Type in the address for the location:");
            string address = Console.ReadLine();

            Console.WriteLine("Type in the Municipality ID for the municipality the location is located in:");
            int municipalityid = Console.Read();

            var LocationAdd = new Location() { Address = address, MunicipalityID = municipalityid };
        }
    }
}
