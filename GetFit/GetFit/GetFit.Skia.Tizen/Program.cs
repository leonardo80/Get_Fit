﻿using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace GetFit.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new GetFit.App(), args);
            host.Run();
        }
    }
}
