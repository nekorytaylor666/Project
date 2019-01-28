using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ExamWork
{
    class Program
    {
        
        static string GenerateCode()
        {
            string code;

            Random rand = new Random();
            code = rand.Next(9999).ToString();

            return code;
        }
        static bool ValidNumber(string phoneNumber)
        {
            long phoneNumberInt;
            bool successPhoneNumber = Int64.TryParse(phoneNumber, out phoneNumberInt);
            return successPhoneNumber;
        }


        static bool ValidationBySms(string code, string login="")
        {
           
            for (int tryCountsInputNumber = 5; tryCountsInputNumber > 0; tryCountsInputNumber--)
            {
                Console.Clear();
                Console.Write("Enter phone number: +7 ");
                string phoneNumber = "7" + Console.ReadLine();
                if (ValidNumber(phoneNumber))
                {
                    try
                    {
                        ISmsSender smsSender = new MessageSender();
                        smsSender.SendMessage("+" + phoneNumber, $"Hello {login}! It is yours verification code: {code}");
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Press enter to return to menu.");
                        return false;
                    }
                   
                    for (int tryCountsInputCode = 5; tryCountsInputCode > 0; tryCountsInputCode--)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter your verification code below: ");
                        string userInput = Console.ReadLine();
                        if (String.Equals(code, userInput))
                        {
                            Console.Clear();
                            Console.WriteLine("Congratz! You are in system your system password is: " + code);
                            Console.ReadLine();
                            return true;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Error input! Tries left: {tryCountsInputCode}! Press enter to input!");
                            Console.ReadLine();
                        }
                    }
                    return false;
                }
                else
                {
                    Console.WriteLine($"Enter valid number! Tries left: {tryCountsInputNumber}");
                    Console.Read();
                }
            }
            return false;
           
        }

        static void Main(string[] args)
        {
            using (UserContext db = new UserContext())
            {
                var users = db.Users.ToList();
                
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1 - Регистрация ");
                    Console.WriteLine("2 - Вход ");
                    Console.WriteLine("3 - Выход ");
                    string choose = Console.ReadLine();
                    int chooseInt;
                    bool success = int.TryParse(choose, out chooseInt);

                    if (success)
                    {
                        switch (chooseInt)
                        {
                            case 1:

                                Console.Clear();
                                Console.WriteLine("Input your login below: ");
                                string name = Console.ReadLine();
                                string code = GenerateCode();
                                if (ValidationBySms(code))
                                {
                                    Console.Clear();
                                    Console.WriteLine("GG!");
                                    try
                                    {

                                        User newUser = new User { Name = name, Password = code, IsConfirmed = true };
                                        if (!users.Contains(newUser))
                                        { 
                                            db.Users.Add(newUser);
                                            db.SaveChanges();
                                            Console.WriteLine("Пользователь зарегистрирован!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Вы уже в базе!");
                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }

                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("Sorry! We can't registrate you try later! Press enter to return to menu!");
                                    Console.ReadLine();
                                }

                                break;
                            case 2:
                                
                                Console.WriteLine("Input your login below: ");
                                string login = Console.ReadLine();
                                var user = users.Find(us=>us.Name == login);
                                if (user != null)
                                {
                                    for (int tryCountsInputCode = 5; tryCountsInputCode > 0; tryCountsInputCode--)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Enter your verification code below: ");
                                        string userInput = Console.ReadLine();
                                        if (String.Equals(user.Password, userInput))
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Congratz! You are in system");
                                            Console.ReadLine();
                                            break;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"Error input! Tries left: {tryCountsInputCode}! Press enter to input!");
                                            Console.ReadLine();
                                        }
                                    }

                                    if (user != null)
                                    {
                                        string newCode = GenerateCode();

                                        if (ValidationBySms(newCode))
                                        {
                                            users.Find(us => us.Name == login).Password = newCode;
                                            users.Find(us => us.Name == login).IsConfirmed = true;
                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Sorry! We can't registrate you try later! Press enter to return to menu!");
                                            users.Find(us => us.Name == login).IsConfirmed = false;
                                            Console.ReadLine();
                                        }

                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Такого логина нет!");
                                    Console.ReadLine();
                                }
                                Console.Clear();
                                break;
                            case 3:
                                Environment.Exit(0);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Enter 1-3!");
                        Console.Read();
                    }
                }
            }
        }
    }
}

