using System;

namespace osuProgram.osu
{
    public class GetObjectInfo
    {
        public enum Type
        {
            Normal,
            Slider,
            Spinner
        }

        public string Object { get; set; }
        public int FileLine { get; set; }
        public Type OType { get; set; }
        public int XVal { get; set; }
        public int YVal { get; set; }
        public int TVal { get; set; }
    }
}
