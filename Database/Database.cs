using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Database
    {
        private readonly SqlConnection connection;

        public Database(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public List<Recipe> GetAllRecipes()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Recipes", connection);

            var reader = cmd.ExecuteReader();

            var recipeList = new List<Recipe>();

            while (reader.Read())
            {
                int id = int.Parse(reader["Id"].ToString());
                string name = reader["Name"].ToString();
                string summary = reader["Summary"].ToString();
                byte[] image = reader["Image"] as byte[];

                recipeList.Add(new Recipe()
                {
                    Id = id,
                    Name = name,
                    Summary = summary,
                    Image = image
                });
            }
            return recipeList;
        }

        public Recipe GetRecipeById(int id)
        {
            string query = "SELECT [Id], [Name], [Summary], [Image] FROM [RecipesDB].[dbo].[Recipes] WHERE [Id] = @Id";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int Id = int.Parse(reader["Id"].ToString());
                        string name = reader["Name"].ToString();
                        string summary = reader["Summary"].ToString();
                        byte[] image = reader["Image"] as byte[];

                        return new Recipe()
                        {
                            Id = id,
                            Name = name,
                            Summary = summary,
                            Image = image
                        };
                    }
                    else
                    {
                        return null;
                    }

                }

            }

        }
    }
}
