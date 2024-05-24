using System;
using System.Data.SqlClient;
using Dapper;

namespace DatabaseApp
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


                ShowAllCustomers(connection);
                ShowAllCustomerEmails(connection);
                ShowAllInterests(connection);
                ShowAllPromotionItems(connection);
                ShowAllCities(connection);
                ShowAllCountries(connection);


                ShowCustomersFromCity(connection, "Київ");
                ShowCustomersFromCountry(connection, "Україна");
                ShowPromotionsForCountry(connection, "Україна");
            }
        }

        static void ShowAllCustomers(SqlConnection connection)
        {
            var customers = connection.Query<string>("SELECT FullName FROM Customers");
            Console.WriteLine("Покупці:");
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        static void ShowAllCustomerEmails(SqlConnection connection)
        {
            var emails = connection.Query<string>("SELECT Email FROM Customers");
            Console.WriteLine("Email покупців:");
            foreach (var email in emails)
            {
                Console.WriteLine(email);
            }
        }

        static void ShowAllInterests(SqlConnection connection)
        {
            var interests = connection.Query<string>("SELECT DISTINCT Interest FROM Interests");
            Console.WriteLine("Список розділів:");
            foreach (var interest in interests)
            {
                Console.WriteLine(interest);
            }
        }

        static void ShowAllPromotionItems(SqlConnection connection)
        {
            var promotionItems = connection.Query<string>("SELECT DISTINCT PromotionItem FROM PromotionItems");
            Console.WriteLine("Список акційних товарів:");
            foreach (var item in promotionItems)
            {
                Console.WriteLine(item);
            }
        }

        static void ShowAllCities(SqlConnection connection)
        {
            var cities = connection.Query<string>("SELECT DISTINCT City FROM Customers");
            Console.WriteLine("Міста:");
            foreach (var city in cities)
            {
                Console.WriteLine(city);
            }
        }

        static void ShowAllCountries(SqlConnection connection)
        {
            var countries = connection.Query<string>("SELECT DISTINCT Country FROM Customers");
            Console.WriteLine("Країни:");
            foreach (var country in countries)
            {
                Console.WriteLine(country);
            }
        }

        static void ShowCustomersFromCity(SqlConnection connection, string city)
        {
            var customers = connection.Query<string>("SELECT FullName FROM Customers WHERE City = @City", new { City = city });
            Console.WriteLine($"Покупці з міста {city}:");
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        static void ShowCustomersFromCountry(SqlConnection connection, string country)
        {
            var customers = connection.Query<string>("SELECT FullName FROM Customers WHERE Country = @Country", new { Country = country });
            Console.WriteLine($"Покупці з країни {country}:");
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        static void ShowPromotionsForCountry(SqlConnection connection, string country)
        {
            var promotions = connection.Query<int>("SELECT PromotionID FROM Promotions WHERE Country = @Country", new { Country = country });
            Console.WriteLine($"Акції для країни {country}:");
            foreach (var promotion in promotions)
            {
                Console.WriteLine(promotion);
            }
        }
    }
}