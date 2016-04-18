using System;
using System.Collections.Generic;

namespace BlueConsole {
    public interface ICommand {

        string Name { set; get; }
        string Help { set; get; }
        string Usage { set; get; }
        Action<IList<string>> Callback { set; get; }

    }
}
