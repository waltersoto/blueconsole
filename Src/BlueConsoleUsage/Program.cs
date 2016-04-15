using System;
using System.Collections.Generic;
using BlueConsole;

namespace BlueConsoleTest {
    class Program {
        static void Main(string[] args) {

            var bc = new Commander {
                Prompt = "MyShell",
                Commands = new List<Command>
                {
                    new Command
                    {
                        Name = "users",
                        Help = "Ex. \"users a\"",
                        Callback = list =>
                        {
                            Users(list.Count > 1?list[1]:"");
                        }
                    },
                    new Command
                    {
                        Name = "cities",
                        Help = "Ex. \"cities a\"",
                        Callback = list => { Cities(list.Count > 1?list[1]:""); }
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
