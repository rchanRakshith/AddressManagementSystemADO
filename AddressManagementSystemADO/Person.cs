using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Data;
using Dapper;
using System.Linq;

namespace AddressManagementSystemADO
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }


        static string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void AddPerson(Person person)
        {
    
            using(SqlConnection con=new SqlConnection(ConString))
            {
                string query = "insert into Person(FirstName,LastName,Email,Age) values(@FirstName,@LastName,@Email,@Age)";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@FirstName", person.FirstName);
                command.Parameters.AddWithValue("@LastName", person.LastName);
                command.Parameters.AddWithValue("@Email", person.Email);
                command.Parameters.AddWithValue("@Age", person.Age);
                con.Open();
                try
                {
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        DataTable allPersons = new Person().GetAllPersons();

                        foreach (DataRow row in allPersons.Rows)
                        {
                            if (row["email"].ToString() ==  person.Email){
                                Console.WriteLine($"Insertion Successfull and the id is {row["personId"]}");
                                break;
                            }
                        }
                       
                    }
                  
                }
                catch {
                    Console.WriteLine("Email must be unique");
                }
               
                

            }
        }

        public void spAddPerson(Person person)
        {

            using (SqlConnection con = new SqlConnection(ConString))
            {

                SqlCommand command = new SqlCommand()
                {
                    CommandText = "spCreatePerson",
                    Connection = con,
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@FirstName",person.FirstName);
                command.Parameters.AddWithValue("@LastName",person.LastName);
                command.Parameters.AddWithValue("@Email",person.Email);
                command.Parameters.AddWithValue("@Age",person.Age);
                   
               SqlParameter personId = new SqlParameter()
                {
                   ParameterName="@PersonId",
                   SqlDbType = SqlDbType.Int,
                   Direction = ParameterDirection.Output,
                };
                command.Parameters.Add(personId);
               
                con.Open();
                try
                {
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                     
                      Console.WriteLine($"Insertion Successfull and the id is {personId.Value.ToString()}");
                           
                       

                    }

                }
                catch
                {
                    Console.WriteLine("Email must be unique");
                }



            }
        }

        public static int GetPersonCount()
        {
            int rowCount;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string sqlQuery = "Select COUNT(*) from person";
                SqlCommand command = new SqlCommand(sqlQuery, con);
                con.Open();
                rowCount = (int)command.ExecuteScalar();
            }
            return rowCount;

            
        }
        public DataTable GetAllPersons()
        {
            SqlConnection con = new SqlConnection(ConString);
            DataTable dt = new DataTable();
            string sqlQuery = "SELECT * FROM Person";
            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, ConString))
            {
                adapter.Fill(dt);
            }
            return dt;

        }

        public static void GetAllPersonsDapper()
        {
            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                var persons = con.Query<Person>("SELECT * FROM Person").ToList();
               foreach (Person person in persons)
                {
                    Console.WriteLine($"FirstName:{person.FirstName}|LastName:{person.LastName}|Email:{person.Email}|Age:{person.Age}");
                }
            }
        }

        public void UpdatePerson(Person person,int id)
        {
            using (SqlConnection con = new SqlConnection(ConString)) { 
 
                string query = "Update person set Firstname=@Firstname,LastName=@LastName,Email=@Email,Age=@Age where PersonId=@PersonId";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@FirstName", person.FirstName);
                command.Parameters.AddWithValue("@LastName", person.LastName);
                command.Parameters.AddWithValue("@Email", person.Email);
                command.Parameters.AddWithValue("@Age", person.Age);
                command.Parameters.AddWithValue("@PersonId",id);
                con.Open();
                try
                {
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                        Console.WriteLine("Updation Successfull");
                    else
                        Console.WriteLine("Invalid Person ID");
                }
                catch
                {
                    Console.WriteLine("Email must be unique");
                }

            }
        }
        public void DeletePerson(int id)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string query = "Delete from Person where PersonId=@PersonId";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@PersonId",id);
                con.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("Deletion Successfull");
                else
                    Console.WriteLine("Invalid Person ID");


            }
        }
      
  
    }
}
