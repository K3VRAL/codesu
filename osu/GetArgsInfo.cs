namespace osuProgram.osu
{
    public static class GetArgsInfo
    {
        // Ignore warnings
        public static bool ignore { get; set; }
        
        // Takes one step per line printed
        public static bool step { get; set; }
        
        // Runs regardless if all, export or log is true
        public static bool run { get; set; }
        
        // Shows more info about each executed line
        public static bool debug { get; set; }
        
        // Shows all objects and their info
        public static bool all { get; set; }

        // Allows for exporting options
        public static bool export { get; set; }
        // If exporting should have all objects be a new combo
        public static bool expANC { get; set; }
        // If exporting should have "Mode: NUMB" and "[Hitobjects]"
        public static bool expMAH { get; set; }

        // Allows for logging options
        public static bool log { get; set; }
        // If logging should log everything printed
        public static bool logEVY { get; set; }
        // If logging should log everything debugged
        public static bool logDBG { get; set; }
        // If logging should log all objects listed
        public static bool logALL { get; set; }
    }
}