using Microsoft.Data.SqlClient;

namespace PGBDAdoDotNet
{
    internal class Program
    {
        const string ChinookConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chinook;Integrated Security=true;Encrypt=False";
        const string NorthwindConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=true;Encrypt=False";
        static void TestConnection(string connectionString)
        {
            // le using ici permet de s'assurer que la connexion est fermée correctement même en cas d'exception


            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            // Do work here; connection closed on following line.  

           // Console.WriteLine("Connexion ouverte avec succès ! Etat : " + connection.State);
            // fermeture de la connexion pour libérer les ressources
            using var command = new SqlCommand("SELECT COUNT(*) FROM Customers", connection);
            int currentCustomerCount = (int)command.ExecuteScalar();
            Console.WriteLine($"Nombre de clients dans la base de données : {currentCustomerCount}");

            connection.Close();

        }

        static int ArtistCount()
        {
            string connectionString = ChinookConnectionString;
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            try
            {
                using var command = new SqlCommand("SELECT COUNT(*) FROM Artist", connection);
                int currentArtistCount = (int)command.ExecuteScalar();
                return currentArtistCount;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'exécution de la requête : {ex.Message}");
                return -1; // Retourne -1 en cas d'erreur
            }
            finally
            {
                connection.Close();
            }
        }
        static void Main(string[] args)
        {
            //TestConnection(ChinookConnectionString);
            //TestConnection(NorthwindConnectionString);
            int artistCount = ArtistCount();
            Console.WriteLine($"Nombre d'artistes dans la base de données : {artistCount}");
        }

    
    }
}
