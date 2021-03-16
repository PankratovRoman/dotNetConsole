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
        static Dictionary<string, Command> Commands = new Dictionary<string, Command>
        {
            {"start", new Command("start", "engine", "pump") },
            {"power", new Command("power", "engine", "pump") }
        };
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter command: ");
                string command_input = Convert.ToString(Console.ReadLine()); // вводим команду
                string[] split_command = command_input.Split(' '); // бьем команду на массив строк
                string first_element = split_command[0];
                if (first_element == "exit")
                {
                    Console.WriteLine("Terminated!");
                    break; 
                }
                if (!Commands.ContainsKey(first_element))
                {
                    Console.WriteLine($"No command {first_element}!");
                    continue;
                }

                string[] getParamFromInput = split_command.Skip(1).ToArray();
                // !!равнозначно
                //string[] getParamFromInput = new string[split_command.Length-1];  
                //for (var j = 0; j < getParamFromInput.Length; j++)
                //{
                //    getParamFromInput[j] = split_command[j+1];
                //}
                // !!равнозначно
                //List<string> get_param_from_input = new List<string>(); // создаем новый список
                //for (int j = 1; j < split_command.Length; j++) // отделяем команду - первый элемент массива, а оставшуюся часть массива отправляем ччилить в список из параметров
                //{
                //get_param_from_input.Add(split_command[j].ToString());
                //}

                //Console.WriteLine(first_element + " ");
                foreach (string el in getParamFromInput)
                {
                Console.WriteLine(el + " ");
                }
            

            }


        }

    }
}