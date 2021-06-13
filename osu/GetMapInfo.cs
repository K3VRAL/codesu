using System;
using System.Collections.Generic;

namespace osuProgram.osu
{
    public class GetMapInfo
    {
        public List<string> Path { get; set; }
        public int GetItemLine(String item)
        {
            int lineindex = 0;
            foreach (var line in Path)
            {
                if (line.Contains(item))
                    return lineindex + 1;
                lineindex++;
            }
            return -1;
        }
    }
}
