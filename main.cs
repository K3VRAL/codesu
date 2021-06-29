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

                        case "-e":
                            GetArgsInfo.export = true;
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

                GetCodesuInfo.lines = lines;
                GetCodesuInfo.file = file + ".exported";
                string[] getLine = null;
                try
                {
                    getLine = lines[GetMapInfo.GetItemLine("Mode:")-1].Split(' ');
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Error: \"Mode\" from .osu file does not exist. Please include it so the items can be processed in a specific programming language.");
                    return;
                }

                if (GetArgsInfo.export)
                {
                    string temp = null;
                    Console.WriteLine("Export Options:");
                    while (true)
                    {
                        Console.Write("Do you want all objects to have a New Combo attribute? (Y/N) ");
                        temp = Console.ReadLine().ToUpper();
                        if (temp == "" || temp == null || string.IsNullOrEmpty(temp) || string.IsNullOrWhiteSpace(temp) || temp.Length == 0 || (temp != "Y" && temp != "N"))
                        {
                            Console.WriteLine("The input must either be a Y(es) or N(o). Please correctly input the right letters.");
                        }
                        else
                        {
                            GetArgsInfo.expANC = temp == "Y";
                            break;
                        }
                    }
                }

                switch (getLine[getLine.Length-1])
                {
                    case "0":
                        Console.Write("osu!std");
                        break;

                    case "1":
                        Console.Write("osu!taiko");
                        break;

                    case "2":
                        programsu.ctb();
                        return;

                    case "3":
                        Console.Write("osu!mania");
                        break;

                    default:
                        Console.WriteLine("Error: \"Mode\" does not have a correct or valid integer to target a specified gamemode: {0}", string.Join(" ", getLine));
                        return;
                }
                Console.WriteLine(" does not currently have a supported programming language attached to it yet. Sorry.");
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