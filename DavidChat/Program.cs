using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidChat
{
    class Message
    {
        public string Text;
        public User User;
        public DateTime Time;
        public Message(string text, User user, DateTime time)
        {
            Text = text;
            User = user;
            Time = time;
        }
    }
    class User
    {
        public string Name;
        public Guid Guid;
        public int PublicID;
        public ConsoleColor Color;

        public User(string name, ConsoleColor color, int publicID)
        {
            Name = name;
            Color = color;
            PublicID = publicID;
        }
        public User(string name, ConsoleColor color, Guid guid)
        {
            Name = name;
            Color = color;
            Guid = guid;
        }
    }
    class Room
    {
        public string Name;
        public string Password;
        public Guid Guid;

        public Room(string name, string password, Guid guid)
        {
            Name = name;
            Password = password;
            Guid = guid;
        }
    }
    class SQLManager
    {
        public User User;
        public Room Room;

        private string connectionString;
        private SqlConnection connection;

        public SQLManager(User user, string connectionString)
        {
            User = user;
            this.connectionString = connectionString;
            connection = new SqlConnection(connectionString);
        }

        public User[] GetUsers(Room room)
        {
            List<User> users = new List<User>();

            var command = new SqlCommand("client.GetUsers");
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@roomID", room.Guid));

            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                try
                {
                    DataTable table = new DataTable();
                    connection.Open();

                    adapter.Fill(table);

                    users = new List<User>();
                    foreach (DataRow row in table.Rows)
                    {
                        users.Add(new User(row.Field<string>("Name"), (ConsoleColor)int.Parse(row.Field<string>("Color")), Guid.Empty));
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return users.ToArray();
        }

        public bool CreateRoom()
        {
            var createCommand = new SqlCommand("client.CreateRoom");
            createCommand.CommandType = System.Data.CommandType.StoredProcedure;
            createCommand.Parameters.Add(new SqlParameter("@userID", User.Guid));
            createCommand.Parameters.Add(new SqlParameter("@password", User.Guid));

            bool result = false;
            using (SqlDataAdapter adapter = new SqlDataAdapter(createCommand))
            {
                try
                {
                    connection.Open();

                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    result = Guid.TryParse(table.Rows[0].Field<string>("RoomID"), out Room.Guid);

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return result;
        }

        public void LeaveRoom()
        {
            var roomsCommand = new SqlCommand("client.LeaveRoom");
            roomsCommand.CommandType = System.Data.CommandType.StoredProcedure;
            roomsCommand.Parameters.Add(new SqlParameter("@userID", User.Guid));

            try
            {
                connection.Open();
                roomsCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public string[] GetRooms()
        {
            var roomsCommand = new SqlCommand("client.GetRooms");
            roomsCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(roomsCommand);
            DataTable table = new DataTable();
            adapter.Fill(table);

            List<string> rooms = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                rooms.Add(row.Field<string>("Name"));
            }

            return rooms.ToArray();
        }

        public Message[] GetNewMessages(Dictionary<int, User> users)
        {
            List<Message> messages = new List<Message>();

            var updateCommand = new SqlCommand("client.GetMessages");
            updateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            updateCommand.Parameters.Add(new SqlParameter("@roomID", User.Guid));

            using (SqlDataAdapter adapter = new SqlDataAdapter(updateCommand))
            {
                try
                {
                    DataTable table = new DataTable();
                    connection.Open();

                    adapter.Fill(table);

                    messages = new List<Message>();
                    foreach (DataRow row in table.Rows)
                    {
                        messages.Add(new Message(row.Field<string>("Text"), users[row.Field<int>("PublicID")], DateTime.Parse(row.Field<string>("Time"))));
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    try
                    {

                    }
                    catch
                    {

                    }
                    Console.WriteLine(ex.ToString());
                }
            }
            return null;
        }
    }
    class Program
    {
        static User user;
        Room room;

        static void Main(string[] args)
        {

            Console.WriteLine("Enter name");
            user = new User(Console.ReadLine(), ConsoleColor.White, Guid.NewGuid());
            Console.WriteLine("guid: " + user.Guid);

            Console.WriteLine("-----------Enter favorite color------------");
            string[] colors = Enum.GetNames(typeof(ConsoleColor));
            for (int i = 1; i < colors.Length; i++)
            {
                Console.WriteLine($"Type {i} for {colors[i]}");
            }

            int favoriteColor;
            while (!int.TryParse(Console.ReadLine(), out favoriteColor))
            {
                Console.WriteLine("try again");
            }
            user.Color = (ConsoleColor)favoriteColor;


            while (true) //FINAL DO NOT DELETE
            {

            }
        }
    }
}
