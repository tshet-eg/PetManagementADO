using System;
using System.Configuration;
using System.Data.SqlClient;

namespace PetManagementDemo
{
    class PetType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        PetType type = null;

        string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = null;
        SqlCommand cmd = null;

        public void InsertPetType(string id, string name)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"insert into PetType values(@id, @name)", conn);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("name", name);
                    conn.Open();
                    int rowsEffected=cmd.ExecuteNonQuery();
                    if (rowsEffected > 0)
                    {
                        Console.WriteLine("inserted successfully");
                    }
                    else
                        Console.WriteLine("insertion error!!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!" + ex.Message);
            }
        }

        public void UpdatePetType(PetType petType)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"update petType set typename=@typename where id=@id", conn);
                    cmd.Parameters.AddWithValue ("id", petType.Id);
                    cmd.Parameters.AddWithValue("typename", petType.Name);
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    if (rowsEffected > 0)
                    {
                        Console.WriteLine("Data updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Updation failed");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!" + ex.Message);
            }
        }

        public void DeletePetType(string id)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"delete from petType where id=@id", conn);
                    cmd.Parameters.AddWithValue("id", id);
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    if (rowsEffected > 0)
                    {
                        Console.WriteLine("Data deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Deletion failed");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!" + ex.Message);
            }
        }

        public PetType GetPetType(string id)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"select * from petType where id=@id", conn);
                    cmd.Parameters.AddWithValue("id", id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        return null;
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            type = new PetType()
                            {
                                Id = id,
                                Name = reader["typeName"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!", ex);
            }
            return type;
        }

        public void DisplayPetTypeTable()
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand("select * from petType", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No data in the table !!");
                    }
                    else
                    {

                        while (reader.Read())
                        {
                            Console.WriteLine($"Pet type ID: {reader["id"]}\tPet type: {reader["typeName"]}\t ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!", ex);
            }
        }
    }
}
