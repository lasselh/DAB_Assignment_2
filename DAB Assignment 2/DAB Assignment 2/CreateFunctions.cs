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
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("Type in citizens sex:\n");
            string sex = Console.ReadLine();

            Console.WriteLine("Type in citizens social security number (10 numbers):\n");
            string SSN = Console.ReadLine();

            Console.Write("Type ID of the municipality the citizen lives in:\n");
            int municipalityID = int.Parse(Console.ReadLine());

            var CitizenAdd = new Citizen() { FirstName = Firstname, LastName = Lastname, Age = age, Sex = sex, SocialSecurityNumber = SSN, MunicipalityID = municipalityID };

            Console.WriteLine("Citizen succesfully added!\n");
        }
        public void createTestCenter()
        {
            Console.Clear();
            Console.WriteLine("Type the ID for the testcenter:\n");
            int TestCenterID = int.Parse(Console.ReadLine());

            Console.WriteLine("Type the opening hours of the testcenter (fx. 8-16):\n");
            string hours = Console.ReadLine();

            Console.WriteLine("Type the MunicipalityID for the municipality in which the testcenter is:\n");
            int municipalityID = int.Parse(Console.ReadLine());

            var TestCenterAdd = new TestCenter() { TestCenterID = TestCenterID, Hours = hours, MunicipalityID = municipalityID };

            Console.WriteLine("TestCenter succesfully added!\n");
        }
        public void createManagement()
        {
            Console.Clear();
            Console.WriteLine("Type in TestCenterManagements PhoneNumber:(8 digits):\n");
            int phonenumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Type in TestCenterManagements Email:\n");
            string email = Console.ReadLine();

            Console.WriteLine("Type in TestCenterID for the TestCenter that the this Management will manage:\n");
            int testcenterid = int.Parse(Console.ReadLine());

            var TestCenterManagementAdd = new TestCenterManagement() { PhoneNumber = phonenumber, Email = email, TestCenterID = testcenterid };

            Console.WriteLine("TestCenterManagement succesfully added!\n");
        }
        public void createTestCase()
        {

        }
        public void createLocation()
        {
            Console.Clear();
            Console.WriteLine("Type in the address for the location:\n");
            string address = Console.ReadLine();

            Console.WriteLine("Type in the Municipality ID for the municipality the location is located in:\n");
            int municipalityid = int.Parse(Console.ReadLine());

            var LocationAdd = new Location() { Address = address, MunicipalityID = municipalityid };

            Console.WriteLine("Location succesfully added!\n");
        }
    }
}
