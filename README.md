# Blue Console

Bolier plate .NET library to help you create a quick console application command list.

Ex.

```cs
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
```

It will listen for custom commands **users** and **cities** and it will execute the call back.

