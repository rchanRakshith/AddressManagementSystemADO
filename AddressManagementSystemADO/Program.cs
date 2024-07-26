using System;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace AddressManagementSystemADO
{
    public class Program
    {
        
       
        public static void AddPerson()
        {
            try
            {

         
            Console.WriteLine("Enter First Name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Age:");
            int age = Convert.ToInt32(Console.ReadLine());
                Person person = new Person() { FirstName = firstName, LastName = lastName, Email = email, Age = age };
                person.AddPerson(person);
            }
            catch {
                Console.WriteLine("Input should be number");
            }

            

        }
        

         public static void spAddPerson()
        {
            try
            {


                Console.WriteLine("Enter First Name:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter Last Name:");
                string lastName = Console.ReadLine();
                Console.WriteLine("Enter Email:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter Age:");
                int age = Convert.ToInt32(Console.ReadLine());
                Person person = new Person() { FirstName = firstName, LastName = lastName, Email = email, Age = age };
                person.spAddPerson(person);
            }
            catch
            {
                Console.WriteLine("Input should be number");
            }



        }
        public static void GetallPersons()
        {
            int count = Person.GetPersonCount();
            if (count > 0)
            {
                DataTable allPersons = new Person().GetAllPersons();

                foreach (DataRow row in allPersons.Rows)
                {
                    Console.WriteLine($"{row["Firstname"]},{row["Lastname"]},{row["email"]},{row["Age"]}");
                }
            }
            else
                Console.WriteLine("Table is empty");
        }

        public static void GetCountOfSameStreetCityState()
        {
            DataTable AllAddressCount = new Address().GetCountOfSameStreetCityState();
            foreach (DataRow row in AllAddressCount.Rows)
            {
                int PersonCount = (int)row["PersonCount"];
                int PersonId = (int)row["PersonId"];
                int streetCount = (int)row["StreetCount"];
                int cityCount = (int)row["CityCount"];
                int stateCount = (int)row["StateCount"];

                Console.WriteLine($"PersonID:{PersonId}| PersonCount: {PersonCount}|Street: {streetCount}|City: {cityCount}|State: {stateCount}");
            }

        }

        public static void UpdatePerson()
        {
            GetallPersons();
            try
            {
                Console.WriteLine("Enter First Name:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter Last Name:");
                string lastName = Console.ReadLine();
                Console.WriteLine("Enter Email:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter Age:");
                int age = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the id to reflect the changes in the table");
                int id = Convert.ToInt32(Console.ReadLine());
                Person person = new Person() { FirstName = firstName, LastName = lastName, Email = email, Age = age };
                person.UpdatePerson(person, id);
            }
            catch
            {
                Console.WriteLine("Input should be number");
            }
            


        }

        public static void DeletePerson()
        {
            GetallPersons();
            try
            {


                Console.WriteLine("Enter the id to delete");
                int id = Convert.ToInt32(Console.ReadLine());
                Person person = new Person();
                person.DeletePerson(id);
            }
            catch
            {
                Console.WriteLine("Input should be number");
            }
        }
        public static void AddAddress()
        {
            try
            {
                Console.WriteLine("Enter PersonID:");
                int personId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Street:");
                string street = Console.ReadLine();
                Console.WriteLine("Enter city:");
                string city = Console.ReadLine();
                Console.WriteLine("Enter State:");
                string state = Console.ReadLine();
                Console.WriteLine("Enter Zipcode :");
                int zipcode = Convert.ToInt32(Console.ReadLine());
                Address address = new Address() { PersonId = personId, Street = street, City = city, State = state, ZipCode = zipcode };
                Console.WriteLine("Type 'yes' to set has a permanent address");
                string addressType = Console.ReadLine();
                if (addressType.ToLower() == "yes")
                {
                    address.Address_Type = 1;
                }


                address.AddAddress(address);
            }
            catch
            {
                Console.WriteLine("Input should be number");
            }

        }


        public static void GetallAddress()
        {
            int count = Address.GetAddressCount();
            if (count > 0)
            {


                DataTable allAddress = new Address().GetallAddress();

                foreach (DataRow row in allAddress.Rows)
                {
                    Console.WriteLine($"{row["Street"]}|{row["City"]}|{row["State"]}|{row["ZipCode"]}");
                }
            }
            else
                Console.WriteLine("Table is Empty");
        }
        public static void GetAddressOfPersons()
        {
            int count = Address.GetAddressCount();
            if (count > 0)
            {


                DataTable AddressOfPerson = new Address().GetAddressOfPersons();
                foreach (DataRow row in AddressOfPerson.Rows)
                {
                    string firstName = row["firstname"].ToString();
                    string lastName = row["lastname"].ToString();
                    string email = row["email"].ToString();
                    string age = row["age"].ToString();
                    string street = row["Street"].ToString();
                    string city = row["city"].ToString();
                    string state = row["state"].ToString();
                    int zipcode = (int)row["zipcode"];

                    Console.WriteLine($"Firstname:{firstName}| Lastname: {lastName}|Email: {email}|Age: {age}|Street: {street}|City:{city}|State: {state}|Zipcode:{zipcode}");
                }


            }
            else
                Console.WriteLine("Table is Empty");

        }

        public static void UpdateAddress()
        {
            GetallAddress();
            try
            {
                Console.WriteLine("Enter the Person ID");
                int personId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Street Name:");
                string StreetName = Console.ReadLine();
                Console.WriteLine("Enter City Name:");
                string CityName = Console.ReadLine();
                Console.WriteLine("Enter State Name:");
                string StateName = Console.ReadLine();
                Console.WriteLine("Enter ZipCode:");
                int Zipcode = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the Addres id to reflect the changes in the table");
                int Addressid = Convert.ToInt32(Console.ReadLine());
                Address address = new Address() { PersonId = personId, Street = StreetName, City = CityName, State = StateName, ZipCode = Zipcode };
                Console.WriteLine("Type 'yes' to set has a permanent address");
                string addressType = Console.ReadLine();
                if (addressType.ToLower() == "yes")
                {
                    address.Address_Type = 1;
                }
                address.UpdateAddress(address, Addressid);
            }
            catch
            {
                Console.WriteLine("Input should be number");
            }
        }

        public static void DeleteAddress()
        {
            GetallAddress();
            try
            {
                Console.WriteLine("Enter the Address id to delete");
                int id = Convert.ToInt32(Console.ReadLine());
                Address address = new Address();
                address.DeleteAddress(id);
            }
            catch
            {
                Console.WriteLine("Input should be number");
            }

        }

        public static void GetPersonsWithOrWithoutAddress()
        {
            int count = Address.GetAddressCount();
            if (count > 0)
            {


                DataTable AddressOfPerson = new Address().GetPersonsWithOrWithoutAddress();
                foreach (DataRow row in AddressOfPerson.Rows)
                {
                    string firstName = row["firstname"].ToString();
                    string lastName = row["lastname"].ToString();
                    string email = row["email"].ToString();
                    string age = row["age"].ToString();
                    string street = row["Street"] != DBNull.Value ? row["Street"].ToString() : "N/A";
                    string city = row["city"] != DBNull.Value ? row["city"].ToString() : "N/A";
                    string state = row["state"] != DBNull.Value ? row["state"].ToString() : "N/A";
                    string zipcode = row["zipcode"] != DBNull.Value ? row["zipcode"].ToString() : "N/A";


                    Console.WriteLine($"Firstname:{firstName}| Lastname: {lastName}|Email: {email}|Age: {age}|Street: {street}|City:{city}|State: {state}|Zipcode:{zipcode}");
                }

            }

            else
                Console.WriteLine("Table is Empty");
        }
        public static void GetAllPermanentAddressResidents()
        {
            int count = Address.GetAddressCount();
            if (count > 0)
            {


                DataTable allAddress = new Address().GetAllPermanentAddressResidents();

                foreach (DataRow row in allAddress.Rows)
                {
                    Console.WriteLine($"Firstname:{row["firstName"]}| Lastname: {row["lastName"]}|Email: {row["email"]}|Age: {row["age"]}|Street: {row["street"]}|City:{row["city"]}|State: {row["state"]}|Zipcode:{row["zipcode"]}");
                }
            }
            else
                Console.WriteLine("Table is Empty");

        }
        public static void GetAllTemparoryAddressResidents()
        {
            int count = Address.GetAddressCount();
            if (count > 0)
            {
                DataTable allAddress = new Address().GetAllTemparoryAddressResidents();

                foreach (DataRow row in allAddress.Rows)
                {
                    Console.WriteLine($"Firstname:{row["firstName"]}| Lastname: {row["lastName"]}|Email: {row["email"]}|Age: {row["age"]}|Street: {row["street"]}|City:{row["city"]}|State: {row["state"]}|Zipcode:{row["zipcode"]}");
                }
            }
            else
                Console.WriteLine("Table is Empty");

        }


        public static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("0 Exit");
                Console.WriteLine("1. Add Person");
                Console.WriteLine("2. Get All Persons");
                Console.WriteLine("3. Update Person");
                Console.WriteLine("4. Delete Person");
                Console.WriteLine("5. Add Address");
                Console.WriteLine("6. Get All Addresses");
                Console.WriteLine("7. Update Address");
                Console.WriteLine("8. Delete Address");
                Console.WriteLine("9. Get count of persons  belongs to same street,City,State");
                Console.WriteLine("10. Get Address for a perticular persons");
                Console.WriteLine("11. Get all the persons with or without address");
                Console.WriteLine("12. Permanent address list");
                Console.WriteLine("13. Temparory address list");
                Console.WriteLine("14. Add person using stored procedure");


                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 0: return;
                    case 1:AddPerson();
                        break;
                    case 2:
                        //GetallPersons();
                        Person.GetAllPersonsDapper();
                        break;
                    case 3: UpdatePerson();
                        break;
                    case 4: DeletePerson();
                        break;
                    case 5: AddAddress();
                        break;
                    case 6: GetallAddress();
                        break;
                    case 7: UpdateAddress();
                        break;
                    case 8: DeleteAddress();
                        break;
                    case 9: GetCountOfSameStreetCityState();
                        break;
                    case 10: GetAddressOfPersons();
                        break;
                    case 11: GetPersonsWithOrWithoutAddress();
                        break;
                    case 12:GetAllPermanentAddressResidents();
                        break;
                    case 13:GetAllTemparoryAddressResidents();
                        break;
                    case 14:spAddPerson();
                        break;
                    default:
                        Console.WriteLine("Invalid entry");
                        break;

                }
            }



        }

    }
    
    }



