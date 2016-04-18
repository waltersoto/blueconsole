using System;
using System.Collections.Generic;
using System.Linq;
using BlueConsole;

namespace BlueConsoleTest {
    class Program {
        static void Main(string[] args) {

            var bc = new Commander {
                Prompt = "MyShell",
                Commands = new List<ICommand>
                {
                    new Command
                    {
                        Name = "users",
                        Help = "Ex. \"users a\"",
                        Callback = list =>
                        {
                            Users(list.FirstOrDefault() ?? "");
                        }
                    },
                    new Command
                    {
                        Name = "cities",
                        Help = "Ex. \"cities a\"",
                        Callback = list => { Cities(list.FirstOrDefault() ?? ""); }
                    }
                }
            };

            bc.Listen();
        }

        private static void Users(string letter) {
            Console.WriteLine("Users with letter {0}", letter);
            Console.WriteLine();
        }

        private static void Cities(string country) {
            Console.WriteLine("Cities in {0}", country);
            Console.WriteLine();
        }
    }
}
