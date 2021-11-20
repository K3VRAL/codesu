using System;
using System.Collections.Generic;

using osuProgram.codesu;

namespace osuProgram.osu
{
    public static class GetCodesuInfo
    {
        public static List<string> lines { get; set; }
        public static string file { get; set; }
        public static int mode { get; set; }
        public static List<GetObjectInfo> AllHitObjects = new();
        public static void runMode(bool log)
        {
            switch (GetCodesuInfo.mode)
            {
                case 0:
                    programsu.std(log);
                    break;

                case 1:
                    programsu.taiko(log);
                    break;

                case 2:
                    programsu.ctb(log);
                    break;

                case 3:
                    programsu.mania(log);
                    break;

                default:
                    Console.WriteLine("Error: \"Mode\" does not have a correct or valid integer to target a specified gamemode: {0}", string.Join(" ", mode));
                    return;
            }
        }
    }
}