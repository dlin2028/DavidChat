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
        public ConsoleColor Color;

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
        private List<User> Users;
        public Room Room;
        
        private string connectionString;
        private SqlConnection connection;

        public SQLManager(User user, string connectionString)
        {
            User = user;
            this.connectionString = connectionString;
            connection = new SqlConnection(connectionString);
        }

        public bool JoinRoom(ref Room room, string password)
        {
            var joinCommand = new SqlCommand("user.JoinRoom");
            joinCommand.CommandType = System.Data.CommandType.StoredProcedure;
            joinCommand.Parameters.Add(new SqlParameter("@roomID", room.Guid));
            joinCommand.Parameters.Add(new SqlParameter("@password", password));

            using (SqlDataAdapter adapter = new SqlDataAdapter(joinCommand))
            {
                DataTable table = new DataTable();
                connection.Open();

                adapter.Fill(table);

                Users = new List<User>();
                foreach (DataRow row in table.Rows)
                {
                    Users.Add(new User(row.Field<string>("Name"), (ConsoleColor)int.Parse(row.Field<string>("Color")), Guid.Parse(row.Field<string>("Time"))));
                }

                connection.Close();
            }
            return Users.Count != 0;
        }

        public bool CreateRoom()
        {
            var createCommand = new SqlCommand("user.CreateRoom");
            createCommand.CommandType = System.Data.CommandType.StoredProcedure;
            createCommand.Parameters.Add(new SqlParameter("@userID", User.Guid));
            createCommand.Parameters.Add(new SqlParameter("@password", User.Guid));

        }

        public void LeaveRoom()
        {
            var roomsCommand = new SqlCommand("user.LeaveRoom");
            roomsCommand.CommandType = System.Data.CommandType.StoredProcedure;
            roomsCommand.Parameters.Add(new SqlParameter("@userID", User.Guid));
        }

        public string[] GetRooms()
        {

            var roomsCommand = new SqlCommand("user.GetRooms");
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
        public string[] GetNewMessages()
        {
            var updateCommand = new SqlCommand("user.Update");
            updateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            updateCommand.Parameters.Add(new SqlParameter("@userID", User.Guid));
            
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
            while(!int.TryParse(Console.ReadLine(), out favoriteColor))
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
