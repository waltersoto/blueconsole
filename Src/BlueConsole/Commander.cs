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
            Prompt = "Command";
            PromptSign = ">";
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

            ShowHelp(DefaultValues.EXIT, DefaultValues.EXIT_HELP, DefaultValues.EXIT_USAGE);
            ShowHelp(DefaultValues.CLEAR, DefaultValues.CLEAR_HELP, DefaultValues.CLEAR_USAGE);
            ShowHelp(DefaultValues.CLS, DefaultValues.CLS_HELP, DefaultValues.CLS_USAGE);
            ShowHelp(DefaultValues.HELP, DefaultValues.HELP_HELP, DefaultValues.HELP_USAGE);
            foreach (var cmd in Commands) {
                ShowHelp(cmd.Name, cmd.Help, cmd.Usage);
            }
        }

        public void ShowHelp(string name, string help, string usage) {
            Console.WriteLine("{0}: {1}", name, help);
            if (!string.IsNullOrEmpty(usage)) {
                Console.WriteLine(usage);
            }

            Console.WriteLine();
        }

        public void Listen() {
            var running = true;
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
            Clear();
            if (StartNotes != null && StartNotes.Any()) {
                foreach (var note in StartNotes) {
                    Console.WriteLine(note);
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            while (running) {

                Console.Write("{0}{1} ", Prompt, PromptSign);
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
                            ShowHelp(DefaultValues.EXIT, DefaultValues.EXIT_HELP, DefaultValues.EXIT_USAGE);
                        } else {
                            running = false;
                        }
                        break;
                    case DefaultValues.CLEAR:
                        if (showHelp) {
                            ShowHelp(DefaultValues.CLEAR, DefaultValues.CLEAR_HELP, DefaultValues.CLEAR_USAGE);
                        } else {
                            Clear();
                        }
                        break;
                    case DefaultValues.CLS:
                        if (showHelp) {
                            ShowHelp(DefaultValues.CLS, DefaultValues.CLS_HELP, DefaultValues.CLS_USAGE);
                        } else {
                            Clear();
                        }
                        break;
                    case DefaultValues.HELP:
                        Help();
                        break;
                    default:
                        if (Commands.Any(
                                m => m.Name.Trim().Equals(name, StringComparison.InvariantCultureIgnoreCase))) {

                            var command = Commands.FirstOrDefault(m => m.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                            if (command != null && showHelp) {
                                ShowHelp(command.Name, command.Help, command.Usage);
                            } else {
                                var list = new List<string>();
                                if (parameters.Count > 1) {
                                    list.AddRange(parameters.Skip(1));
                                }
                                command?.Callback?.Invoke(list);
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
        public string PromptSign { set; get; }
        public ConsoleColor BackgroundColor { set; get; }
        public ConsoleColor ForegroundColor { set; get; }
        public IList<ICommand> Commands { set; get; }
        public string Title { set; get; }
        public string Divider { set; get; }
        public IList<string> StartNotes { set; get; }

    }
}
