using System;

using osuProgram.osu;

namespace osuProgram.codesu
{
    public static partial class programsu
    {
        public static void taiko()
        {
            taikoObjects();
            if (GetArgsInfo.export || GetArgsInfo.all)
            {
                taikoExport();
                return;
            }
            Console.WriteLine("osu!taiko does not currently have a supported programming language attached to it yet. Sorry.");
        }

        private static void taikoObjects()
        {}

        private static void taikoExport()
        {}
    }
}