using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetManagementDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string name, typeId= null;
            int id;
            PetType type = new PetType();
            Pet pet = new Pet();
            OwnerType owner = new OwnerType();
            int mainChoice = 0;
            int choice = 0;
            do
            {
                Console.WriteLine("\n----------------MENU----------------");
                Console.WriteLine("1. Pet type operations");
                Console.WriteLine("2. Owner operations");
                Console.WriteLine("3. Pet details operations");
                Console.WriteLine("------------------------------------");
                Console.Write("Input your choice: ");
                mainChoice = int.Parse(Console.ReadLine());
                switch (mainChoice)
                {
                    case 1:
                        do
                        {
                            Console.WriteLine("\n----------------MENU----------------");
                            Console.WriteLine("1. Insert pet type");
                            Console.WriteLine("2. Update a pet type");
                            Console.WriteLine("3. Delete a pet type");
                            Console.WriteLine("4. Display a pet type");
                            Console.WriteLine("5. Display pet type table");
                            Console.WriteLine("6. Exit");
                            Console.WriteLine("------------------------------------");
                            Console.Write("Input your choice: ");
                            choice = int.Parse(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    Console.Write("\nPet type name: ");
                                    name = Console.ReadLine();
                                    Console.Write("Type ID: ");
                                    typeId = Console.ReadLine();
                                    type.InsertPetType(typeId, name);
                                    break;
                                case 2:
                                    Console.Write("Enter type id: ");
                                    typeId = Console.ReadLine();
                                    type.GetPetType(typeId);
                                    Console.Write("ENter new pet type: ");
                                    string typeName = Console.ReadLine();
                                    type = new PetType()
                                    {
                                        Id = typeId,
                                        Name = typeName,
                                    };
                                    type.UpdatePetType(type);
                                    break;
                                case 3:
                                    Console.Write("\n Enter type id you want to delete: ");
                                    typeId = Console.ReadLine();
                                    type.DeletePetType(typeId);
                                    break;
                                case 4:
                                    Console.Write("\n Enter type id you want to view: ");
                                    typeId = Console.ReadLine();
                                    PetType typeObj= type.GetPetType(typeId);
                                    if (typeObj != null)
                                    {
                                        Console.WriteLine($" Pet type ID: {typeObj.Id}\n Pet type: {typeObj.Name}\n ");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid ID!!");
                                    }
                                    break;
                                case 5:
                                    type.DisplayPetTypeTable();
                                    break;
                                case 6:
                                    break;
                                default:
                                    Console.WriteLine("enter correct choice!!");
                                    break;
                            }
                        } while (choice != 6);
                        break;
                    case 2:
                        do
                        {
                            Console.WriteLine("\n----------------MENU----------------");
                            Console.WriteLine("1. Insert new owner");
                            Console.WriteLine("2. Update an owner detail");
                            Console.WriteLine("3. Delete an owner");
                            Console.WriteLine("4. Display an owner details");
                            Console.WriteLine("5. Display owner table");
                            Console.WriteLine("6. List of pets per owner");
                            Console.WriteLine("7. Count of pets per owner");
                            Console.WriteLine("8. Exit");
                            Console.WriteLine("------------------------------------");
                            Console.Write("Input your choice: ");
                            choice = int.Parse(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    Console.Write("\nOwner name: ");
                                    name = Console.ReadLine();
                                    owner.InsertOwner(name);
                                    break;
                                case 2:
                                    Console.Write("Enter owner id: ");
                                    id = int.Parse(Console.ReadLine());
                                    Console.Write("Enter new owner name: ");
                                    name = Console.ReadLine();
                                    owner = new OwnerType()
                                    {
                                        Id = id,
                                        Name = name,
                                    };
                                    owner.UpdateOwner(owner);
                                    break;
                                case 3:
                                    Console.Write("\n Enter owner id you want to delete: ");
                                    id = int.Parse(Console.ReadLine());
                                    owner.DeleteOwner(id);
                                    break;
                                case 4:
                                    Console.Write("\n Enter owner id you want to view: ");
                                    id = int.Parse(Console.ReadLine());
                                    OwnerType ownerObj = owner.DisplayOwner(id);
                                    if (ownerObj != null)
                                    {
                                        Console.WriteLine($" Owner ID: {ownerObj.Id}\n Owner name: {ownerObj.Name}\n ");
                                    }
                                    else
                                        Console.WriteLine("Invalid ID!!");
                                    break;
                                case 5:
                                    owner.DisplayOwnerTable();
                                    break;
                                case 6:
                                    owner.GetOwnerAndPet();
                                    break;
                                case 7:
                                    owner.PetCountPerOwner();
                                    break;
                                case 8:
                                    break;
                                default:
                                    Console.WriteLine("enter correct choice!!");
                                    break;
                            }
                        }while(choice != 8);
                        break;

                    case 3:
                        do
                        {
                            int updateChoice = 0;
                            Console.WriteLine("\n----------------MENU----------------");
                            Console.WriteLine("1. Insert new Pet");
                            Console.WriteLine("2. Update a pet detail");
                            Console.WriteLine("3. Delete a pet detail");
                            Console.WriteLine("4. Delete pets by owner");
                            Console.WriteLine("5. Delete pets by category");
                            Console.WriteLine("6. Display a pet details");
                            Console.WriteLine("7. Display pet table");
                            Console.WriteLine("8. Display pet by owner");
                            Console.WriteLine("9. Display pet by category");
                            Console.WriteLine("10. Exit");
                            Console.WriteLine("------------------------------------");
                            Console.Write("Input your choice: ");
                            choice = int.Parse(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    Console.Write("\nPet name: ");
                                    name = Console.ReadLine();
                                    Console.Write("\nPet dob: ");
                                    string dob = Console.ReadLine();
                                    Console.Write("\nPet owner ID: ");
                                    int ownerId = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("\nPet type id: ");
                                    typeId = Console.ReadLine();
                                    pet.InsertPet(name, dob, ownerId, typeId);
                                    break;
                                case 2:
                                        Console.Write("Enter pet id: ");
                                        id = int.Parse(Console.ReadLine());
                                        Pet petDetails = pet.DisplayPet(id);
                                    do 
                                    { 
                                        Console.WriteLine("\n----------------MENU----------------");
                                        Console.WriteLine("1. Update pet name");
                                        Console.WriteLine("2. Update pet date of birth");
                                        Console.WriteLine("3. Update pet owner");
                                        Console.WriteLine("4. Update pet type");
                                        Console.WriteLine("5. Exit");
                                        Console.WriteLine("------------------------------------");
                                        Console.Write("Input your choice: ");
                                        updateChoice = int.Parse(Console.ReadLine());
                                        switch (updateChoice)
                                        {
                                            case 1:
                                                Console.Write("enter new name: ");
                                                string newName = Console.ReadLine();
                                                pet = new Pet()
                                                {
                                                    Id = petDetails.Id,
                                                    Name = newName,
                                                    DOB = petDetails.DOB,
                                                    OwnerId = petDetails.OwnerId,
                                                    TypeId = petDetails.TypeId,
                                                };
                                                pet.EditPet(pet);
                                                break;
                                            case 2:
                                                Console.Write("enter new dob: ");
                                                DateTime newDob = Convert.ToDateTime(Console.ReadLine());
                                                pet = new Pet()
                                                {
                                                    Id = petDetails.Id,
                                                    Name = petDetails.Name,
                                                    DOB = newDob.ToString(),
                                                    OwnerId = petDetails.OwnerId,
                                                    TypeId = petDetails.TypeId,
                                                };
                                                pet.EditPet(pet);
                                                break;
                                            case 3:
                                                Console.Write("enter new owner id: ");
                                                int newOwnerId = Convert.ToInt32(Console.ReadLine());
                                                pet = new Pet()
                                                {
                                                    Id = petDetails.Id,
                                                    Name = petDetails.Name,
                                                    DOB = petDetails.DOB,
                                                    OwnerId = newOwnerId,
                                                    TypeId = petDetails.TypeId,
                                                };
                                                pet.EditPet(pet);
                                                break;
                                            case 4:
                                                Console.Write("enter new type id: ");
                                                string newTypeId = Console.ReadLine();
                                                pet = new Pet()
                                                {
                                                    Id = petDetails.Id,
                                                    Name = petDetails.Name,
                                                    DOB = petDetails.DOB,
                                                    OwnerId = petDetails.OwnerId,
                                                    TypeId = newTypeId,
                                                };
                                                pet.EditPet(pet);
                                                break;
                                            case 5:
                                                break;
                                            default:
                                                Console.WriteLine("enter correct choice!!");
                                                break;
                                        }
                                    } while (updateChoice != 5);
                                    break;
                                case 3:
                                    Console.Write("\n Enter pet id you want to delete: ");
                                    id = int.Parse(Console.ReadLine());
                                    pet.DeletePet(id);
                                    break;
                                case 4:
                                    Console.Write("\n Enter owner id you want to clear: ");
                                    id = int.Parse(Console.ReadLine());
                                    pet.DeletePetByOwner(id);
                                    break;
                                case 5:
                                    Console.Write("\n Enter category id:");
                                    typeId = Console.ReadLine();
                                    pet.DeletePetByType(typeId);
                                    break;
                                case 6:
                                    Console.Write("\n Enter pet id you want to view: ");
                                    id = int.Parse(Console.ReadLine());
                                    pet.DisplayPet(id);
                                    break;
                                case 7:
                                    pet.DisplayPetTable();
                                    break;
                                case 8:
                                    Console.Write("\n Enter owner id:");
                                    id = int.Parse(Console.ReadLine());
                                    pet.GetPetByOwner(id);
                                    break;
                                case 9:
                                    Console.Write("\n Enter pet type id:");
                                    typeId = Console.ReadLine();
                                    pet.GetPetByType(typeId);
                                    break;
                                case 10:
                                    break;
                                default:
                                    Console.WriteLine("enter correct choice!!");
                                    break;
                            }
                        } while (choice != 10);
                        break;
                }
            }while (mainChoice != 6);
        }
    }
}
