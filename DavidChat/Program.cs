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

        public User(string name, ConsoleColor color)
        {
            Name = name;
            Color = color;
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
        private Dictionary<int, User> users;
        Dictionary<int, Message> messages;

        public SQLManager(User user, string connectionString)
        {
            User = user;


            messages = new Dictionary<int, Message>();
            users = new Dictionary<int, User>();
            this.connectionString = connectionString;
            connection = new SqlConnection(connectionString);
            Register();
        }

        public bool Register()
        {
            var joinCommand = new SqlCommand("client.Register", connection);
            joinCommand.CommandType = System.Data.CommandType.StoredProcedure;
            joinCommand.Parameters.Add(new SqlParameter("@name", User.Name));
            joinCommand.Parameters.Add(new SqlParameter("@color", (int)User.Color));

            bool result = false;
            using (SqlDataAdapter adapter = new SqlDataAdapter(joinCommand))
            {
                try
                {
                    connection.Open();

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    User.Guid = table.Rows[0].Field<Guid>("UserID");

                    connection.Close();
                    result = true;
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Console.WriteLine(ex.ToString());
                    result = false;
                }
            }
            return result;
        }

        public void GetUsers()
        {
            var command = new SqlCommand("client.GetUsers", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@roomID", Room.Guid));

            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                try
                {
                    DataTable table = new DataTable();
                    connection.Open();

                    adapter.Fill(table);

                    users = new Dictionary<int, User>();
                    foreach (DataRow row in table.Rows)
                    {
                        users.Add(row.Field<int>("PublicID"), new User(row.Field<string>("Name"), (ConsoleColor)row.Field<int>("Color")));
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public bool JoinRoom(string name, string password)
        {
            var joinCommand = new SqlCommand("client.JoinRoom", connection);
            joinCommand.CommandType = System.Data.CommandType.StoredProcedure;
            joinCommand.Parameters.Add(new SqlParameter("@userID", User.Guid));
            joinCommand.Parameters.Add(new SqlParameter("@roomName", name));
            joinCommand.Parameters.Add(new SqlParameter("@password", password));

            bool result = false;
            using (SqlDataAdapter adapter = new SqlDataAdapter(joinCommand))
            {
                try
                {
                    connection.Open();

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    Room = new Room(name, password, table.Rows[0].Field<Guid>("RoomID"));

                    connection.Close();
                    result = true;
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Console.WriteLine(ex.ToString());
                    result = false;
                }
            }
            return result;
        }

        public bool CreateRoom(string name, string password)
        {
            var createCommand = new SqlCommand("client.CreateRoom", connection);
            createCommand.CommandType = System.Data.CommandType.StoredProcedure;
            createCommand.Parameters.Add(new SqlParameter("@userID", User.Guid));
            createCommand.Parameters.Add(new SqlParameter("@name", name));
            createCommand.Parameters.Add(new SqlParameter("@password", password));

            bool result = false;
            using (SqlDataAdapter adapter = new SqlDataAdapter(createCommand))
            {
                try
                {
                    connection.Open();

                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    Room = new Room(name, password, table.Rows[0].Field<Guid>("RoomID"));

                    connection.Close();
                    result = true;
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Console.WriteLine(ex.ToString());
                    result = false;
                }
            }
            return result;
        }

        public void LeaveRoom()
        {
            var roomsCommand = new SqlCommand("client.LeaveRoom", connection);
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
                connection.Close();
                Console.WriteLine(ex.ToString());
            }
        }

        public string[] GetRooms()
        {
            var roomsCommand = new SqlCommand("client.GetRooms", connection);
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

        public bool SendMessage(string message)
        {
            var roomsCommand = new SqlCommand("client.SendMessage", connection);
            roomsCommand.CommandType = System.Data.CommandType.StoredProcedure;
            roomsCommand.Parameters.Add(new SqlParameter("@userID", User.Guid));
            roomsCommand.Parameters.Add(new SqlParameter("@roomID", Room.Guid));
            roomsCommand.Parameters.Add(new SqlParameter("@text", message));

            try
            {
                connection.Open();
                roomsCommand.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                connection.Close();
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public Message[] GetNewMessages()
        {
            var updateCommand = new SqlCommand("client.GetMessages", connection);
            updateCommand.CommandType = System.Data.CommandType.StoredProcedure;
            updateCommand.Parameters.Add(new SqlParameter("@roomID", Room.Guid));

            using (SqlDataAdapter adapter = new SqlDataAdapter(updateCommand))
            {
                try
                {
                    DataTable table = new DataTable();
                    connection.Open();

                    adapter.Fill(table);

                    var newMessages = new List<Message>();
                    foreach (DataRow row in table.Rows)
                    {
                        if (!users.ContainsKey(row.Field<int>("PublicID")))
                        {
                            connection.Close();
                            GetUsers();
                        }
                        if(!messages.ContainsKey(row.Field<int>("MessageID")))
                        {
                            var msg = new Message(row.Field<string>("Text"), users[row.Field<int>("PublicID")], row.Field<DateTime>("Time"));

                            messages.Add(row.Field<int>("MessageID"), msg);
                            newMessages.Add(msg);
                        }
                    }

                    connection.Close();
                    return newMessages.ToArray();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Console.WriteLine(ex.ToString());
                }
            }
            return new Message[0];
        }
    }
    class Program
    {
        static User user;
        static SQLManager manager;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter name");
            user = new User(Console.ReadLine(), ConsoleColor.White, Guid.NewGuid());

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
            Console.ForegroundColor = user.Color;

            manager = new SQLManager(user, "Data Source = GMRMLTV; Initial Catalog = DavidChat; User ID = sa; Password = GreatMinds110;");
            Console.WriteLine("guid: " + user.Guid);

            string line = "";
            while (true)
            {
                //join room
                while (manager.Room == null)
                {
                    Console.WriteLine("Would you like to Create or Join a room");
                    string input = Console.ReadLine();
                    if (input.ToLower().Contains("create"))
                    {
                        Console.Write("Name: ");
                        string name = Console.ReadLine().Trim();
                        Console.Write("Password: ");
                        string password = Console.ReadLine().Trim();
                        if (manager.CreateRoom(name, password))
                        {
                            Console.WriteLine($"Room {name} joined successfully");
                        }
                        else
                        {
                            Console.WriteLine("Name already taken. Try again");
                        }
                    }
                    else
                    {
                        string name = "";
                        bool exists = false;
                        while (!exists)
                        {
                            Console.WriteLine("------------Rooms-------------");
                            string[] rooms = manager.GetRooms();
                            foreach (string room in rooms)
                            {
                                Console.WriteLine(room);
                            }

                            Console.Write("Name: ");
                            name = Console.ReadLine().Trim();
                            foreach (string room in rooms)
                            {
                                if (room.Trim() == name)
                                {
                                    exists = true;
                                    break;
                                }
                            }
                            if (!exists)
                            {
                                Console.WriteLine("Room does not exist");
                                rooms = manager.GetRooms();
                            }
                        }
                        Console.Write("Password: ");
                        string password = Console.ReadLine().Trim();

                        if (manager.JoinRoom(name, password))
                        {
                            Console.WriteLine($"Room {name} joined successfully");
                        }
                        else
                        {
                            Console.WriteLine("Invalid password. Try again");
                        }
                    }
                }

                Message[] messages = manager.GetNewMessages();
                foreach (var message in messages)
                {
                    Console.ForegroundColor = message.User.Color;
                    Console.Write(message.User.Name.Trim() + ": ");
                    Console.WriteLine(message.Text);
                }
                Console.ForegroundColor = user.Color;

                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.Backspace:
                            string temp = "";
                            for (int i = 0; i < line.Length - 1; i++)
                            {
                                temp += line[i];
                            }
                            
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(new string(' ', Console.WindowWidth - 1));
                            Console.SetCursorPosition(0, Console.CursorTop);

                            Console.Write(user.Name + ": ");
                            line = temp;
                            Console.Write(line);

                            break;

                        case ConsoleKey.Enter:
                            manager.SendMessage(line);
                            Console.WriteLine("");
                            line = user.Name + ": ";

                            break;
                            
                        default:
                            line += (char)key;
                            for (int i = 0; i < Console.BufferWidth; i++)
                            {
                                Console.Write(" ");
                                Console.Write((char)ConsoleKey.Enter);
                            }
                            Console.Write(user.Name + ": ");
                            Console.Write(line);

                            break;
                    }
                }
            }
        }

    }
}
