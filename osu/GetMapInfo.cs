using System;

namespace osuProgram.osu
{
    public static class GetMapInfo
    {
        public static int GetItemLine(String item)
        {
            int lineindex = 0;
            foreach (var line in GetCodesuInfo.lines)
            {
                if (line.Contains(item))
                    return lineindex + 1;
                lineindex++;
            }
            return -1;
        }
    }
}
