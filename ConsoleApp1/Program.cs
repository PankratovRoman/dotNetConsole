using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseConsole
{

    class Command
    {
        public string Name;
        public string HelpText;
        public string[] Parameters;

        public Command(string name, string helpText, params string[] parameters)
        {
            Name = name;
            Parameters = parameters;
            HelpText = string.Format(helpText, name, string.Join(", ", parameters));
        }
    }

    class Program
    {
        // создаем словарь команд из экземпляров класса
        static Dictionary<string, Command> Commands = new Dictionary<string, Command>()
        {
            {"help", new Command("help","A completely useless command. But how can I refuse the Master?") },
            {"start", new Command("start", "Command \"{0}\" gives the command to start one of the modules. Expected parameters: {1}", "engine", "pump") },
            {"power", new Command("power", "Command \"{0}\" gives the command to increase or decrease power. Expected parameters: {1}", "engine", "pump") },
            {"exit", new Command("exit", "Shut down app") }
        };

        static void Main(string[] args)
        {
            Console.WriteLine(@"Welcome to Enterprise test console. Type ""help"" for help.");

            // запускаем главный цикл
            while (true)
            {
                Console.Write("Enter command: ");
                var command_input = Convert.ToString(Console.ReadLine()); // вводим команду
                string[] split_command = command_input.Split(' '); // бьем команду на массив строк
                var first_element = split_command[0]; // получаем саму команду
                // проверка содержится ли команда в ключах словаря команд
                if (!Commands.ContainsKey(first_element))
                {
                    Console.WriteLine($"No command found \"{first_element}\"");
                    continue;
                }
                var command = Commands[first_element];
                // проверка на help
                if (first_element == Commands["help"].Name)
                {
                    if (split_command.Length < 2)
                    {
                        Console.WriteLine("Expected parameters:");
                        continue;
                    }
                    var forHelpCommand = split_command[1];
                    if (!Commands.ContainsKey(forHelpCommand))
                    {
                        Console.WriteLine($"No command found \"{forHelpCommand}\"");
                        continue;
                    }
                    Console.WriteLine(Commands[forHelpCommand].HelpText);
                    continue;
                }
                // проверка на exit
                if (first_element == Commands["exit"].Name)
                {
                    Console.WriteLine("Terminated!");
                    break;
                }     
                // получаю массив с отделением от него нулевого индекса = команда
                string[] getParamFromInput = split_command.Skip(1).ToArray();

                // получаю словарь из разделенных по ":" параметра и значения параметр и проверяю праметры команды в словаре команд
                Dictionary<string, string> paramsAndValues = new Dictionary<string, string>();


                foreach (var splitParamAndValue in getParamFromInput)
                {
                    var doSplit = splitParamAndValue.Split(":", StringSplitOptions.RemoveEmptyEntries);

                    if (!command.Parameters.Contains(doSplit[0]))
                    {
                        Console.WriteLine($"Incorrect parameter \"{doSplit[0]}\"");
                        break;
                    }

                    if (doSplit.Length < 2)
                    {
                        Console.WriteLine("No param value");
                        break;
                    }
                    paramsAndValues.Add(doSplit[0], doSplit[1]);
                }



            }


        }

    }
}
