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
        public Room Room;

        private string connectionString;

        public SQLManager(User user, string connectionString)
        {
            User = user;
            this.connectionString = connectionString;
        }

        public bool JoinRoom(ref Room room, string password, int maxMessages = 50)
        {
            var joinCommand = new SqlCommand("user.JoinRoom");
            joinCommand.CommandType = System.Data.CommandType.StoredProcedure;
            joinCommand.Parameters.Add(new SqlParameter("@roomID", room.Guid));
            joinCommand.Parameters.Add(new SqlParameter("@password", password));

            SqlDataAdapter adapter = new SqlDataAdapter(joinCommand);
            DataTable table = new DataTable();
            adapter.Fill(table);

            List<User> users = new List<User>();
            foreach (DataRow row in table.Rows)
            {
                users.Add(new User(row.Field<string>("Name"), (ConsoleColor)int.Parse(row.Field<string>("Color")), Guid.Parse(row.Field<string>("Time"))));
            }

            return users.Count != 0;
        }
        public string[] GetRooms()
        {
            var roomsCommand = new SqlCommand("user.GetRooms");
            roomsCommand.CommandType = System.Data.CommandType.StoredProcedure;

            return null;
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
        Guid roomID;
        Room room;

        static void Main(string[] args)
        {

            Console.WriteLine("Enter name");
            user = new User(Console.ReadLine(), ConsoleColor.White, Guid.NewGuid());
            Console.WriteLine("guid: " + user.Guid);






            while (true) //FINAL DO NOT DELETE
            {

            }
        }
    }
}
