

using System;
using System.Collections.Generic;

namespace BlueConsole {
    public class Command {
        public string Name { set; get; }
        public string Help { set; get; }
        public Action<IList<string>> Callback { set; get; }
    }
}
