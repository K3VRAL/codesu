using System;
using System.IO;
using System.Collections.Generic;

using osuProgram.codesu;
using osuProgram.osu;

namespace osuProgram
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                string file = null;

                foreach (string x in args)
                {
                    if (x.Contains(".osu"))
                    {
                        file = x;
                    }

                    switch (x)
                    {
                        case "-d":
                            GetArgsInfo.debug = true;
                            break;

                        case "-i":
                            GetArgsInfo.ignore = true;
                            break;

                        case "-s":
                            GetArgsInfo.step = true;
                            break;

                        case "-a":
                            GetArgsInfo.all = true;
                            break;
                    }
                }
                
                if (file == "" || file == null || string.IsNullOrEmpty(file) || string.IsNullOrWhiteSpace(file) || file.Length == 0)
                {
                    Console.WriteLine("Error: No relevant file was inputted: {0}", string.Join(" ", args));
                    return;
                }

                string[] osufile = file.Split(".");
                if (osufile[osufile.Length - 1] != "osu")
                {
                    Console.WriteLine("Error: File inputted is not of an .osu file. Check and make sure what you have inputted is correct: {0} with {1}", file, osufile[osufile.Length - 1]);
                    return;
                }
                
                List<string> lines = null;
                try
                {
                    lines = new(File.ReadAllLines(file));
                }
                catch (System.IO.FileNotFoundException)
                {
                    Console.WriteLine("Error: File not found: {0}", file);
                    return;
                }

                programsu.lines = lines;
                // TODO: if (args is ctb or anything else)
                programsu.ctb();
            }
            else if (args.Length == 0)
            {
                Console.WriteLine("Error: Seems as if there are too few arguments/files being inputted: {0} instead of 1.", args.Length);
            }
            else
            {
                Console.WriteLine("Error in general: args: {0}", args);
            }
        }
    }
}