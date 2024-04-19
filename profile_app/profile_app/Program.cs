using System;
using System.IO;
using Newtonsoft.Json;

namespace PersonInfoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Enter user information");
                Console.WriteLine("2. Display user information");
                Console.WriteLine("3. Exit");

                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        EnterUserInfo();
                        break;
                    case "2":
                        DisplayUserInfo();
                        break;
                    case "3":
                        Console.WriteLine("Exiting the program...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please select again.");
                        break;
                }
            }
        }

        static void EnterUserInfo()
        {
            Console.WriteLine("\nEnter user information:");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Height (in cm): ");
            double height = double.Parse(Console.ReadLine());

            Console.Write("Weight (in kg): ");
            double weight = double.Parse(Console.ReadLine());

            Console.Write("Field of Study: ");
            string fieldOfStudy = Console.ReadLine();

            Person person = new Person(firstName, lastName, age, height, weight, fieldOfStudy);

            string json = JsonConvert.SerializeObject(person, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("person.json", json);
            Console.WriteLine("User information saved successfully.\n");
        }

        static void DisplayUserInfo()
        {
            if (!File.Exists("person.json"))
            {
                Console.WriteLine("No user information found.");
                return;
            }

            string jsonFromFile = File.ReadAllText("person.json");
            Person loadedPerson = JsonConvert.DeserializeObject<Person>(jsonFromFile);

            Console.WriteLine("\nUser information:");
            Console.WriteLine($"First Name: {loadedPerson.FirstName}");
            Console.WriteLine($"Last Name: {loadedPerson.LastName}");
            Console.WriteLine($"Age: {loadedPerson.Age}");
            Console.WriteLine($"Height: {loadedPerson.Height} cm");
            Console.WriteLine($"Weight: {loadedPerson.Weight} kg");
            Console.WriteLine($"Field of Study: {loadedPerson.FieldOfStudy}");
            Console.WriteLine();
        }
    }

    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string FieldOfStudy { get; set; }

        public Person(string firstName, string lastName, int age, double height, double weight, string fieldOfStudy)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Height = height;
            Weight = weight;
            FieldOfStudy = fieldOfStudy;
        }
    }
}
