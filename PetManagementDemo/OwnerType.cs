using System.Configuration;
using System.Data.SqlClient;
using System;
using System.Data;

namespace PetManagementDemo
{
    class OwnerType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        OwnerType owner = null;

        string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = null;
        SqlCommand cmd = null;

        public void InsertOwner(string name)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    //cmd = new SqlCommand($"insert into ownerDetails(name) values(@name)", conn);
                    cmd=new SqlCommand("spInsertOwner1", conn);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ownerName", name);
                    SqlParameter ownerIdParam=new SqlParameter("@ownerId", SqlDbType.Int);
                    ownerIdParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(ownerIdParam);
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    if (rowsEffected > 0)
                    {
                        Console.WriteLine("Data inserted successfully.");
                        int ownerId = (int)ownerIdParam.Value;
                        Console.WriteLine($"Owner id is {ownerId}");
                    }
                    else
                    {
                        Console.WriteLine("Insertion failed");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!" + ex.Message);
            }
        }

        public void UpdateOwner(OwnerType owner)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"update ownerDetails set name=@name where ownerId={owner.Id}", conn);
                    cmd.Parameters.AddWithValue("name", owner.Name);
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
        public void DeleteOwner(int id)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"delete from ownerDetails where ownerId={id}", conn);
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

        public OwnerType DisplayOwner(int id)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"select * from ownerDetails where ownerId={id}", conn);
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
                            owner = new OwnerType()
                            {
                                Id = id,
                                Name = reader["name"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!", ex);
            }
            return owner;
        }

        public void DisplayOwnerTable()
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand("select * from ownerDetails", conn);
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
                            Console.WriteLine($"Owner ID: {reader["ownerId"]}\tOwner name: {reader["name"]}\t ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!", ex);
            }
        }

        public void GetOwnerAndPet()
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand("select ownerD.name as 'Owner', pet.name as 'petName', petT.typeName as 'Category' from ownerDetails ownerD left outer join petDetails pet on ownerD.ownerId=pet.OwnerId left outer join petType petT on pet.petTYpeId=petT.id", conn);
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
                            var pName= reader["petName"]!= DBNull.Value?reader["petName"].ToString():"N/A";
                            var pCategory= reader["Category"] != DBNull.Value ? reader["Category"].ToString() : "N/A";
                            Console.WriteLine($"Owner : {reader["owner"]}\tPetname: {pName}\t Pet Category: {pCategory} ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!", ex);
            }
        }

        public void PetCountPerOwner()
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand("select ownerD.name as 'Owner', count(petD.ownerId) as 'Total Pets' \r\nfrom petDetails petD \r\nright outer join OwnerDetails ownerD on ownerD.ownerId=petD.OwnerId group by ownerD.name", conn);
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
                            Console.WriteLine($"Owner : {reader["owner"]}\t Pet count: {reader["Total Pets"]}\t ");
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
