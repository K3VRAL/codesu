using System;

using osuProgram.osu;

namespace osuProgram.codesu
{
    public static partial class programsu
    {
        public static void std()
        {
            stdObjects();
            if (GetArgsInfo.export || GetArgsInfo.all)
            {
                stdExport();
                return;
            }
            Console.WriteLine("osu!std does not currently have a supported programming language attached to it yet. Sorry.");
        }

        private static void stdObjects()
        {}

        private static void stdExport()
        {}
    }
}