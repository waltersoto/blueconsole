using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BlueConsole {
    public class Commander {

        public Commander() : this(ConsoleColor.Blue, ConsoleColor.White) { }
        public Commander(ConsoleColor backgroundColor, ConsoleColor foregroundColor) {
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            Commands = new List<Command>();
            StartNotes = new List<string>();
            Prompt = "Commander";
            Title = "Command Listener 1.0";
            Divider = "=========================================";
        }

        public void Clear() {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine(Divider);
            Console.WriteLine();
        }

        public void Help() {

            ShowHelp(DefaultValues.EXIT, DefaultValues.EXIT_HELP);
            ShowHelp(DefaultValues.CLEAR, DefaultValues.CLEAR_HELP);
            ShowHelp(DefaultValues.CLS, DefaultValues.CLS_HELP);
            ShowHelp(DefaultValues.HELP, DefaultValues.HELP_HELP);
            foreach (var cmd in Commands) {
                ShowHelp(cmd.Name, cmd.Help);
            }
        }

        public void ShowHelp(string name, string help) {
            Console.WriteLine("{0}: {1}", name, help);
            Console.WriteLine();
        }

        public void Listen() {
            var running = true;
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
            Clear();
            if (StartNotes.Any()) {
                foreach (var note in StartNotes) {
                    Console.WriteLine(note);
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            while (running) {

                Console.Write("{0}> ", Prompt);
                var cmd = Console.ReadLine();
                Console.WriteLine();

                if (string.IsNullOrEmpty(cmd)) continue;

                var parameters = GetParameters(cmd);

                if (!parameters.Any()) continue;
                var name = parameters.FirstOrDefault();
                var showHelp = false;

                if (parameters.Count > 1) {
                    if (!string.IsNullOrEmpty(parameters[1])) {
                        if (parameters[1].Equals(DefaultValues.DISPLAY_HELP) ||
                            parameters[1].Equals(DefaultValues.DISPLAY_HELP_ALT)) {
                            showHelp = true;
                        }
                    }
                }
                if (string.IsNullOrEmpty(name)) continue;

                switch (name.Trim().ToLower()) {
                    case DefaultValues.EXIT:
                        if (showHelp) {
                            ShowHelp(DefaultValues.EXIT, DefaultValues.EXIT_HELP);
                        } else {
                            running = false;
                        }
                        break;
                    case DefaultValues.CLEAR:
                        if (showHelp) {
                            ShowHelp(DefaultValues.CLEAR, DefaultValues.CLEAR_HELP);
                        } else {
                            Clear();
                        }
                        break;
                    case DefaultValues.CLS:
                        if (showHelp) {
                            ShowHelp(DefaultValues.CLS, DefaultValues.CLS_HELP);
                        } else {
                            Clear();
                        }
                        break;
                    case DefaultValues.HELP:
                        Help();
                        break;
                    default:

                        if (
                            Commands.Any(
                                m => m.Name.Trim().Equals(name, StringComparison.InvariantCultureIgnoreCase))) {

                            var command = Commands.FirstOrDefault(m => m.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                            if (command != null && showHelp) {
                                ShowHelp(command.Name, command.Help);
                            } else {
                                command?.Callback?.Invoke(parameters);
                            }

                        }
                        break;
                }


            }
        }

        private static IList<string> GetParameters(string cmd)
                        => Regex.Matches(cmd, @"[\""].+?[\""]|[^ ]+")
                                .Cast<Match>()
                                .Select(m => m.Value.TrimStart('"').TrimEnd('"'))
                                .ToList();

        public string Prompt { set; get; }
        public ConsoleColor BackgroundColor { set; get; }
        public ConsoleColor ForegroundColor { set; get; }
        public IList<Command> Commands { set; get; }
        public string Title { set; get; }
        public string Divider { set; get; }
        public IList<string> StartNotes { set; get; }

    }
}
