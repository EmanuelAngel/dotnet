using MySql.Data.MySqlClient;

namespace init.Models;

public class UserRepository
{
  readonly string ConnectionString = "Server=autorack.proxy.rlwy.net;Port=50288;Database=railway;User Id=root;Password=eWhQlWUBTVWOqIRgzkAiAkNthPuYoMze;SslMode=Preferred;";

  public List<User> FindMany()
  {
    List<User> users = [];

    using (MySqlConnection connection = new(ConnectionString))
    {
      var query = $@"
      SELECT
        id,
        name,
        email,
        role
      FROM users;
      ";

      using (MySqlCommand command = new(query, connection))
      {
        connection.Open();

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
          users.Add(new User
          {
            Id = reader.GetInt32("id"),
            Name = reader.GetString("name"),
            Email = reader.GetString("email"),
            Role = reader.GetString("role")
          });
        }

        connection.Close();
      }
    }

    return users;
  }
}