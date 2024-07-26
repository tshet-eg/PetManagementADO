using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace PetManagementDemo
{
    class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public int OwnerId {  get; set; }
        public string TypeId {  get; set; }
        Pet pet = null;

        string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = null;
        SqlCommand cmd = null;
        public void InsertPet(string name, string dob, int ownerId, string typeId)
        {
            try
            {
                using(conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand("spInsertPet", conn);
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", name); 
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    cmd.Parameters.AddWithValue("@ownerId", ownerId);
                    cmd.Parameters.AddWithValue("@categoryId", typeId);
                    SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int);
                    idParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(idParam);
                    conn.Open();
                    int rowsEffected=cmd.ExecuteNonQuery();
                    if (rowsEffected > 0)
                    {
                        Console.WriteLine("Data inserted successfully.");
                        int id = (int)idParam.Value;
                        Console.WriteLine($"Pet id is: {id}");
                    }
                    else
                    {
                        Console.WriteLine("Insertion failed");
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("something went wrong!!" +ex.Message);
            }

        }

        public void EditPet(Pet pet)
        {
            try
            {
                using(conn=new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"update petDetails set name=@name, dob=@dob, ownerId={pet.OwnerId}, petTypeId=@typeId where id={pet.Id}", conn);
                    cmd.Parameters.AddWithValue("name", pet.Name);
                    cmd.Parameters.AddWithValue("dob", pet.DOB);
                    cmd.Parameters.AddWithValue("typeId", pet.TypeId);
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
                Console.WriteLine("something went wrong!!", ex);
            }

        }

        public void DeletePet(int id)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"delete from petDetails where id={id}", conn);
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    if (rowsEffected > 0)
                    {
                        Console.WriteLine("Data deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid pet Id!!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!", ex);
            }
        }

        public void DeletePetByOwner(int ownerId)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"delete from petDetails where ownerId={ownerId}", conn);
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    if (rowsEffected > 0)
                    {
                        Console.WriteLine("Data deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No pets registered under given owner id");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!", ex);
            }
        }

        public void DeletePetByType(string typeId)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"delete from petDetails where petTypeId=@typeId", conn);
                    cmd.Parameters.AddWithValue("typeId", typeId);
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    if (rowsEffected > 0)
                    {
                        Console.WriteLine("Data deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No pets data of given category");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!", ex);
            }
        }

        public Pet DisplayPet(int id)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"select * from petDetails where id={id}", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("Invalid ID!!");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            pet = new Pet()
                            {
                                Id = id,
                                Name = reader["name"].ToString(),
                                DOB = reader["dob"].ToString(),
                                OwnerId = (int)reader["ownerId"],
                                TypeId = reader["PetTypeId"].ToString()
                            };

                            Console.WriteLine($" Pet ID: {id}\n Pet name: {pet.Name}\n Pet DOB: {pet.DOB}\n Owner: {pet.OwnerId}\n Pet type: {pet.TypeId}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!", ex);
            }
            return pet;
        }

        public void DisplayPetTable()
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand("select pet.Id as 'petId', pet.name as 'petName', pet.DoB as 'petDOB', PetType.TypeName as 'petCategory', ownerD.name as 'owner' from petDetails pet left outer join ownerDetails ownerD on pet.ownerId=ownerD.ownerId inner join PetType on pet.PetTypeId= PetType.Id", conn);
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
                            Console.WriteLine($"Pet ID: {reader["petId"]}\tPet name: {reader["petName"]}\t Pet DOB: {reader["petDoB"]}\t Pet type: {reader["PetCategory"]}\t Owner: {reader["owner"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!", ex);
            }
        }

        public void GetPetByOwner(int ownerId)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"select pet.Id as 'petId', pet.name as 'Pet Name', pet.DoB as 'DOB', pType.typeName as 'category', ownerD.name as 'owner' from OwnerDetails ownerd inner join petDetails pet on pet.OwnerId= ownerD.OwnerId\r\ninner join PetType pType\r\non pet.PetTypeId=pType.Id\r\nwhere ownerD.ownerId={ownerId}", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No data under this id !!");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Pet ID: {reader["petId"]}\tPet name: {reader["Pet Name"]}\t Pet DOB: {reader["dob"]}\t Pet type: {reader["category"]}\t Owner: {reader["owner"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong!!", ex);
            }
        }

        public void GetPetByType(string typeId)
        {
            try
            {
                using (conn = new SqlConnection(connString))
                {
                    cmd = new SqlCommand($"select pet.Id as 'petId', pet.name as 'Pet Name', pet.DoB as 'DOB', pType.typeName as 'category', ownerD.name as 'owner' from petType pType inner join petDetails pet on pet.petTypeId= pType.id\r\ninner join ownerDetails ownerD\r\non pet.ownerID=ownerD.ownerId\r\nwhere pType.id=@typeId", conn);
                    cmd.Parameters.AddWithValue("typeId", typeId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No data under this id !!");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Pet ID: {reader["petId"]}\tPet name: {reader["Pet Name"]}\t Pet DOB: {reader["dob"]}\t Owner: {reader["owner"]}\t Pet type: {reader["category"]}");
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
