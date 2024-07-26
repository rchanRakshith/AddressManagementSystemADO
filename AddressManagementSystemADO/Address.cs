using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressManagementSystemADO
{
    public class Address
    {
        public int AddressId { get; set; }
        public int PersonId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }

        public int Address_Type {  get; set; }
        

        static string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public void AddAddress(Address address)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string query = "insert into Address(PersonId,Street,City,State,Zipcode,Address_Type) values(@PersonId,@Street,@City,@State,@Zipcode,@Address_Type)";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@PersonId", address.PersonId);
                command.Parameters.AddWithValue("@Street", address.Street);
                command.Parameters.AddWithValue("@City", address.City);
                command.Parameters.AddWithValue("@State", address.State);
                command.Parameters.AddWithValue("@Zipcode", address.ZipCode);
                command.Parameters.AddWithValue("@Address_Type", address.Address_Type);


                con.Open();
                try
                {
                    int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            DataTable allAddress = new Address().GetallAddress();
                            int max = 0;
                        foreach (DataRow row in allAddress.Rows)
                            {
                            
                            if ((int)row["addressId"]> max){
                                max = (int)row["addressId"];
                            }
                            }
                        Console.WriteLine($"Address added successfully and your address ID  is {max} ");

                    }
                 
                }
                catch(Exception e) 
                {
                    Console.WriteLine(e);

                }
            }




        }
        public static int GetAddressCount()
        {
            int rowCount;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string sqlQuery = "Select COUNT(*) from address";
                SqlCommand command = new SqlCommand(sqlQuery, con);
                con.Open();
                rowCount = (int)command.ExecuteScalar();
            }
            return rowCount;


        }
        public DataTable GetallAddress()
        {
            DataTable dt = new DataTable();
            string sqlQuery = "SELECT * FROM Address";
            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, ConString))
            {
                adapter.Fill(dt);
            }
            return dt;

        }

        public void UpdateAddress(Address address, int addressid)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            {

                string query = "Update address set PersonId=@PersonId,Street=@Street,City=@City,State=@State,Zipcode=@Zipcode Address_Type=@Address_Type where AddressId=@AddressId";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@Street",address.Street);
                command.Parameters.AddWithValue("@City",address.City);
                command.Parameters.AddWithValue("@State",address.State);
                command.Parameters.AddWithValue("@Zipcode",address.ZipCode);
                command.Parameters.AddWithValue("@AddressId", addressid);
                command.Parameters.AddWithValue("@PersonId", address.PersonId);
                command.Parameters.AddWithValue("@Address_Type", address.Address_Type);


                con.Open();
                int result = command.ExecuteNonQuery();
                Console.WriteLine(result);
                if (result > 0)
                    Console.WriteLine("Updation Successfull");
                else
                    Console.WriteLine("Updation Unsuccessfull");

            }
        }

        public void DeleteAddress(int id)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string query = "Delete from address where AddressId=@AddressId";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@AddressId", id);
                con.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("Deletion Successfull");
                else
                    Console.WriteLine("Deletion Unsuccessfull");


            }
        }
        public  DataTable GetCountOfSameStreetCityState()
        {

            DataTable dt = new DataTable();
            string sqlQuery = "select PersonId,street,city,PersonId ,count(PersonId) over(partition by PersonId) as PersonCount,count(Street) over(partition by Street) as StreetCount,count(City) over(partition by City) as CityCount, count(State) over(partition by State) as StateCount from Address ";
            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, ConString))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetAddressOfPersons()
        {
            DataTable dt = new DataTable();
            string sqlQuery = "select p.firstname,p.lastname,p.email,p.age,a.street,a.city,a.state,a.zipcode from Person p INNER JOIN Address a on p.PersonId=a.PersonId";
            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, ConString))
            {
                adapter.Fill(dt);
            }
            return dt;
        }
        public DataTable GetPersonsWithOrWithoutAddress()
        {
            DataTable dt = new DataTable();
            string sqlQuery = "select *from person full outer join address on Person.PersonId=address.PersonId";
            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, ConString))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetAllPermanentAddressResidents()
        {
            DataTable dt = new DataTable();
            string sqlQuery = "select p.firstname,p.lastname,p.email,p.age,a.street,a.city,a.state,a.zipcode from Person p INNER JOIN Address a on p.PersonId=a.PersonId and address_Type=1";
            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, ConString))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetAllTemparoryAddressResidents()
        {
            DataTable dt = new DataTable();
            string sqlQuery = "select p.firstname,p.lastname,p.email,p.age,a.street,a.city,a.state,a.zipcode from Person p INNER JOIN Address a on p.PersonId=a.PersonId and address_Type=0";
            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, ConString))
            {
                adapter.Fill(dt);
            }
            return dt;
        }


    }
}
