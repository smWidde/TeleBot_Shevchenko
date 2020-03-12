using System;

using Telegram.Bot;

using Telegram.Bot.Args;

using System.Data.SQLite;

using Telegram.Bot.Types.ReplyMarkups;

using System.IO;



namespace TeleBot_Shevchenko

{

    class Program

    {



        static TelegramBotClient client;

        static string path = "botDB.sqlite";

        static SQLiteConnection connection = new SQLiteConnection($"Data Source = {path}");
        static void Main(string[] args)

        {

            if (!CheckExistDataBase(path))

                CreateDataBase(path);



            client = new TelegramBotClient("848578183:AAEyE9rbGZtyq4eunSdruS91Jj-gHn2F9Oc");

            client.OnMessage += getMsg;

            client.StartReceiving();



            Console.Read();

        }



        private static bool CheckExistDataBase(string path) => File.Exists(path);

        private static void CreateDataBase(string path)

        {

                if (connection.State != System.Data.ConnectionState.Closed)
                    connection.Close();
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Question" +

                    "([id] INTEGER PRIMARY KEY AUTOINCREMENT," +

                    "[text] VARCHAR(255) NOT NULL);", connection))

                {

                    try

                    {

                        command.ExecuteNonQuery();

                    }

                    catch (Exception ex)

                    {

                        Console.WriteLine(ex.Message);

                    }

                }

                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Answer" +

                    "([id] INTEGER PRIMARY KEY AUTOINCREMENT," +

                    "[text] VARCHAR(255) NOT NULL," +

                    "[QUESTION_ID] INTEGER REFERENCES Question(id));", connection))
                {

                    try

                    {

                        command.ExecuteNonQuery();

                    }

                    catch (Exception ex)

                    {

                        Console.WriteLine(ex.Message);

                    }

                }


        }
        private static void getMsg(object sender, MessageEventArgs e)

        {

            if (e.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)

                return;

            client.SendTextMessageAsync(e.Message.Chat.Id, "в разработке");

        }

    }

}