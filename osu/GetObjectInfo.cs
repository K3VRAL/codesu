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
        public int NCombo { get; set; }
        public int HSound { get; set; }
        public int STVal { get; set; }
        public string Command { get; set; }
    }
}
