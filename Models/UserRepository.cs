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

  public User? FindOne(int id)
  {
    User? user = null;

    using (MySqlConnection connection = new(ConnectionString))
    {
      var query = $@"
        SELECT
          id,
          name,
          email,
          role
        FROM users
        WHERE id = @id;
      ";

      using (MySqlCommand command = new(query, connection))
      {
        command.Parameters.AddWithValue("@id", id);

        connection.Open();

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
          user = new User
          {
            Id = reader.GetInt32("id"),
            Name = reader.GetString("name"),
            Email = reader.GetString("email"),
            Role = reader.GetString("role")
          };
        }

        connection.Close();
      }
    }

    return user;
  }

  public int Create(User user)
  {
    int res = -1;

    using (MySqlConnection connection = new(ConnectionString))
    {
      var query = $@"
        INSERT INTO users
        (name, email, role)
        VALUES
        (@name, @email, @role);
        SELECT LAST_INSERT_ID();
      ";

      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        command.Parameters.AddWithValue("@name", user.Name);
        command.Parameters.AddWithValue("@email", user.Email);
        command.Parameters.AddWithValue("@role", user.Role);

        connection.Open();
        res = Convert.ToInt32(command.ExecuteScalar());
        connection.Close();
      }
    }

    return res;
  }

  public int Edit(int id, User user)
  {
    return -1;
  }
}