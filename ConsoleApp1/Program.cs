using System;
using System.Data.SqlClient;
using Dapper;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=127.0.0.1;Initial Catalog=ShopDB;User ID=user;Password=23324rcop;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine("Спроба підключення до бази даних...");
                try
                {
                    connection.Execute("SELECT 1");
                    Console.WriteLine("Підключено до бази даних!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка підключення до бази даних: {ex.Message}");
                }

                // Task 1: Insert Functionality
                InsertNewCustomer(connection, "John Doe", "john.doe@example.com", "Kyiv", "Ukraine");
                InsertNewCountry(connection, "France");
                InsertNewCity(connection, "Paris", "France");
                InsertNewInterest(connection, "Books");
                InsertNewPromotionItem(connection, "Discounted Books");

                // Task 2: Update Functionality
                UpdateCustomer(connection, 1, "John Doe", "john.doe@example.com", "Lviv", "Ukraine");
                UpdateCountry(connection, "France", "French Republic");
                UpdateCity(connection, "Paris", "Lyon", "France");
               

                // Task 3: Deletion Functionality
                DeleteCustomer(connection, 1);
                DeleteCountry(connection, "France");
                

                // Task 4: Additional Display Functionality
                ShowCitiesInCountry(connection, "Ukraine");
                
            }
        }

        static void InsertNewCustomer(SqlConnection connection, string fullName, string email, string city, string country)
        {
            connection.Execute("INSERT INTO Customers (FullName, Email, City, Country) VALUES (@FullName, @Email, @City, @Country)",
                new { FullName = fullName, Email = email, City = city, Country = country });
            Console.WriteLine("New customer added successfully.");
        }

        static void InsertNewCountry(SqlConnection connection, string countryName)
        {
            connection.Execute("INSERT INTO Countries (CountryName) VALUES (@CountryName)", new { CountryName = countryName });
            Console.WriteLine("New country added successfully.");
        }

        static void InsertNewCity(SqlConnection connection, string cityName, string country)
        {
            connection.Execute("INSERT INTO Cities (CityName, Country) VALUES (@CityName, @Country)", new { CityName = cityName, Country = country });
            Console.WriteLine("New city added successfully.");
        }

        static void InsertNewInterest(SqlConnection connection, string interest)
        {
            connection.Execute("INSERT INTO Interests (Interest) VALUES (@Interest)", new { Interest = interest });
            Console.WriteLine("New interest added successfully.");
        }

        static void InsertNewPromotionItem(SqlConnection connection, string item)
        {
            connection.Execute("INSERT INTO PromotionItems (PromotionItem) VALUES (@Item)", new { Item = item });
            Console.WriteLine("New promotion item added successfully.");
        }

        static void UpdateCustomer(SqlConnection connection, int customerId, string fullName, string email, string city, string country)
        {
            connection.Execute("UPDATE Customers SET FullName = @FullName, Email = @Email, City = @City, Country = @Country WHERE CustomerID = @CustomerID",
                new { CustomerID = customerId, FullName = fullName, Email = email, City = city, Country = country });
            Console.WriteLine("Customer information updated successfully.");
        }

        static void UpdateCountry(SqlConnection connection, string oldCountryName, string newCountryName)
        {
            connection.Execute("UPDATE Countries SET CountryName = @NewCountryName WHERE CountryName = @OldCountryName",
                new { OldCountryName = oldCountryName, NewCountryName = newCountryName });
            Console.WriteLine("Country information updated successfully.");
        }

        static void UpdateCity(SqlConnection connection, string cityName, string newCityName, string country)
        {
            connection.Execute("UPDATE Cities SET CityName = @NewCityName WHERE CityName = @CityName AND Country = @Country",
                new { CityName = cityName, NewCityName = newCityName, Country = country });
            Console.WriteLine("City information updated successfully.");
        }

        static void DeleteCustomer(SqlConnection connection, int customerId)
        {
            connection.Execute("DELETE FROM Customers WHERE CustomerID = @CustomerID", new { CustomerID = customerId });
            Console.WriteLine("Customer deleted successfully.");
        }

        static void DeleteCountry(SqlConnection connection, string countryName)
        {
            connection.Execute("DELETE FROM Countries WHERE CountryName = @CountryName", new { CountryName = countryName });
            Console.WriteLine("Country deleted successfully.");
        }

        static void ShowCitiesInCountry(SqlConnection connection, string country)
        {
            var cities = connection.Query<string>("SELECT CityName FROM Cities WHERE Country = @Country", new { Country = country });
            Console.WriteLine($"Cities in {country}:");
            foreach (var city in cities)
            {
                Console.WriteLine(city);
            }
        }

        
    }
}
