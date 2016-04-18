

using System;
using System.Collections.Generic;

namespace BlueConsole {
    public class Command : ICommand {

        public string Name { set; get; }
        public string Help { set; get; }
        public string Usage { get; set; }
        public Action<IList<string>> Callback { set; get; }

    }
}
