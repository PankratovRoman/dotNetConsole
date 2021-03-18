using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseConsole
{

    class Command
    {
        public string Name;
        public string[] Parameters;

        public Command(string name, params string[] parameters)
        {
            Name = name;
            Parameters = parameters;
        }
    }

    class Program
    {
        // создаем словарь команд из экземпляров класса
        static Dictionary<string, Command> Commands = new Dictionary<string, Command>()
        {
            {"start", new Command("start", "engine", "pump") },
            {"power", new Command("power", "engine", "pump") },
            {"exit", new Command("exit") }
        };

        static void Main(string[] args)
        {
            Console.WriteLine(@"Welcome to Enterprise test console. Type ""help"" for help.");

            // запускаем главный цикл
            while (true)
            {
                Console.Write("Enter command: ");
                string command_input = Convert.ToString(Console.ReadLine()); // вводим команду
                string[] split_command = command_input.Split(' '); // бьем команду на массив строк
                string first_element = split_command[0]; // получаем саму команду

                // проверка на exit
                if (first_element == Commands["exit"].Name)
                {
                    Console.WriteLine("Terminated!");
                    break;
                }

                // проверка содержится ли команда в ключах словаря команд
                if (!Commands.ContainsKey(first_element))
                {
                    Console.WriteLine($"No command found /{first_element}/");
                    continue;
                }

                // получаю массив с отделением от него нулевого индекса = команда
                string[] getParamFromInput = split_command.Skip(1).ToArray();

                // получаю словарь из разделенных по ":" параметра и значения параметр 
                Dictionary<string, string> ParamsAndValues = new Dictionary<string, string>();
                foreach (var splitParamAndValue in getParamFromInput)
                {
                    var doSplit = splitParamAndValue.Split(":", StringSplitOptions.RemoveEmptyEntries);
                    if (doSplit.Length < 2)
                    {
                        Console.WriteLine("No param value");
                        continue;
                    }
                    ParamsAndValues.Add(doSplit[0], doSplit[1]);
                }

                // проверяю праметры команды в словаре команд
                var command = Commands[first_element];
                foreach (var param in ParamsAndValues.Keys) // кладу в param значения каждого ключа из словаря параметр:значение
                {
                    
                    if (!command.Parameters.Contains(param))
                    {
                        Console.WriteLine($"Incorrect parameter /{param}/");
                    }
                }

                Console.WriteLine();

            }


        }

    }
}