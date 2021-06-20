using System;

using osuProgram.codesu;

namespace osuProgram.osu
{
    public static class GetMapInfo
    {
        public static int GetItemLine(String item)
        {
            int lineindex = 0;
            foreach (var line in programsu.lines)
            {
                if (line.Contains(item))
                    return lineindex + 1;
                lineindex++;
            }
            return -1;
        }
    }
}
