using System;

using osuProgram.osu;

namespace osuProgram.codesu
{
    public static partial class programsu
    {
        public static void mania()
        {
            maniaObjects();
            if (GetArgsInfo.export || GetArgsInfo.all)
            {
                maniaExport();
                return;
            }
            Console.WriteLine("osu!mania does not currently have a supported programming language attached to it yet. Sorry.");
        }

        private static void maniaObjects()
        {}

        private static void maniaExport()
        {}
    }
}