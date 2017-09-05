﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        public static List<Animal> GetAnimals()
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var result = from r in context.Animals select r;
            return result.ToList();
        }
        public static void AddAnimal()
        {
            Console.WriteLine("What is the animals name");
            string name = Console.ReadLine();
            Animal animal = new Animal();
            animal.name = name;
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            context.Animals.InsertOnSubmit(animal);
            context.SubmitChanges();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {

            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            Employee employee = (from data in context.Employees where data.email == email && data.employeeNumber == employeeNumber select data).First();
            return employee;
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var employee = (from data in context.Employees where data.userName == userName && data.pass == password select data).First();
            return employee;
        }

        public static IQueryable<USState> GetStates()
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var states = from r in context.USStates select r;
            return states;
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            context.Employees.InsertOnSubmit(employee);
            context.SubmitChanges();
        }

        internal static int? GetBreed()
        {
            Breed breed = new Breed();
            breed.Species1.species = UserInterface.GetStringData("species","the animal's");
            breed.breed1 = UserInterface.GetStringData("breed", "the animal's");
            breed.pattern = UserInterface.GetStringData("pattern", "the animial's");
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            context.Breeds.InsertOnSubmit(breed);
            var currentBreed = (from data in context.Breeds where data.breed1 == breed.breed1 && data.Species1.species == breed.Species1.species && data.pattern == breed.pattern select data).First();
            return currentBreed.ID;
        }

        public static IQueryable<Client> RetrieveClients()
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clients = from r in context.Clients select r;
            return clients;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var employee = (from data in context.Employees where data.userName == userName select data).First();
            if (employee == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void AddNewClient(string firstName, string lastName, string userName, string password, string email, string streetAddress, int zipCode, int stateNumber)
        {
            Client client = new Client();
            int address = GetClientAddressKey(streetAddress, zipCode, stateNumber);
            client.email = email;
            client.firstName = firstName;
            client.lastName = lastName;
            client.userName = userName;
            client.pass = password;
            client.userAddress = address;
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            context.Clients.InsertOnSubmit(client);
            context.SubmitChanges();
        }

        internal static void RemoveEmployee(string lastName, string employeeNumber)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            Employee employee = (from data in context.Employees where data.lastName == lastName && data.employeeNumber == int.Parse(employeeNumber) select data).First();
            if (employee != null)
            {
                context.Employees.DeleteOnSubmit(employee);
                context.SubmitChanges();
            }
        }

        internal static void AddNewEmployee(string firstName, string lastName, string employeeNumber, string email)
        {
            Employee employee = new Employee();
            employee.employeeNumber = int.Parse(employeeNumber);
            employee.firsttName = firstName;
            employee.lastName = lastName;
            employee.email = email;
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            context.Employees.InsertOnSubmit(employee);
            context.SubmitChanges();
        }

        public static Client GetClient(string username, string password)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var client = (from user in context.Clients where user.userName == username && user.pass == password select user).ToList();
            return (Client)client[0];
            
        }
        public static int GetClientAddressKey(string streetAddress, int zipCode, int stateNumber)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            int addressNumber;
            var addressObject = from address in context.UserAddresses where address.addessLine1 == streetAddress && address.zipcode == zipCode && address.usState == stateNumber select address.ID;
            if(addressObject.ToList().Count > 0)
            {
                addressNumber = addressObject.ToList()[0];
            }
            else
            {
                UserAddress address = new UserAddress();
                address.zipcode = zipCode;
                address.addessLine1 = streetAddress;
                address.usState = stateNumber;
                context.UserAddresses.InsertOnSubmit(address);
                context.SubmitChanges();
                var addressKey = from location in context.UserAddresses where location.addessLine1 == streetAddress && location.zipcode == zipCode && location.usState == stateNumber select address.ID;
                addressNumber = addressKey.ToList()[0];
            }
            return addressNumber;
        }

        public static void UpdateFirstName(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().firstName = client.firstName;
            context.SubmitChanges();
        }
        public static void UpdateLastName(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().lastName = client.lastName;
            context.SubmitChanges();
        }
        public static void UpdateAddress(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().UserAddress1.zipcode= client.UserAddress1.zipcode;
            clientData.First().UserAddress1.addessLine1 = client.UserAddress1.addessLine1;
            clientData.First().UserAddress1.usState = client.UserAddress1.usState;
            context.SubmitChanges();
        }
        public static void UpdateEmail(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().email = client.email;
            context.SubmitChanges();
        }
        public static void UpdateUsername(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().userName = client.userName;
            context.SubmitChanges();
        }
    }
}
