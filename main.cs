using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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

                // Going through each argument inputted; storing an osu file path and arguments
                foreach (string x in args)
                {
                    if (x.Contains("osu"))
                    {
                        file = x;
                    }

                    if (x.Contains("-"))
                    {
                        char[] split = x.ToCharArray();
                        foreach (char y in split.Skip(1))
                        {
                            switch (y)
                            {
                                case 'd':
                                    GetArgsInfo.debug = true;
                                    break;

                                case 'i':
                                    GetArgsInfo.ignore = true;
                                    break;

                                case 's':
                                    GetArgsInfo.step = true;
                                    break;

                                case 'a':
                                    GetArgsInfo.all = true;
                                    break;

                                case 'e':
                                    GetArgsInfo.export = true;
                                    break;

                                case 'l':
                                    GetArgsInfo.log = true;
                                    break;
                                
                                case 'r':
                                    GetArgsInfo.run = true;
                                    break;

                                default:
                                    Console.WriteLine("Error: argument inputted does not exist: {0}", y);
                                    return;
                            }
                        }
                    }
                }
                
                // If no file was inputted
                if (file == "" || file == null || string.IsNullOrEmpty(file) || string.IsNullOrWhiteSpace(file) || file.Length == 0)
                {
                    Console.WriteLine("Error: No relevant file was inputted: {0}", string.Join(" ", args));
                    return;
                }

                // Another check making sure that an osu file was inputted
                string[] osufile = file.Split(".");
                if (osufile[osufile.Length - 1] != "osu")
                {
                    Console.WriteLine("Error: File inputted is not of an .osu file. Check and make sure what you have inputted is correct: {0} with {1}", file, osufile[osufile.Length - 1]);
                    return;
                }
                
                // Reading all lines of osu files
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
                
                // Sets osu file
                GetCodesuInfo.lines = lines;
                GetCodesuInfo.file = file;
                
                // If "[HitObjects]" exists
                if (GetMapInfo.GetItemLine("[HitObjects]") == -1)
                {
                    Console.WriteLine("Error: There are no [HitObjects]. Please include this for the program to work.");
                    return;
                }

                // Using "Mode" to choose a programming language
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

                // Exporting and Logging options
                boolRelated expANC = new();
                boolRelated expMAH = new();
                boolRelated logEVY = new();
                boolRelated logDBG = new();
                boolRelated logALL = new();
                List<boolRelated> boOptions = new();
                List<string> doOptions = new();
                string temp = null;
                bool YoN;
                if (GetArgsInfo.export)
                {
                    doOptions.Add("Do you want all objects to have a New Combo attribute?");
                    boOptions.Add(expANC);
                    doOptions.Add("Do you want to include \"Mode: 2\" and \"[HitObjects]\"?");
                    boOptions.Add(expMAH);
                }
                if (GetArgsInfo.log)
                {
                    doOptions.Add("Do you want to log everything printed?");
                    boOptions.Add(logEVY);
                    doOptions.Add("Do you want to log everything debugged?");
                    boOptions.Add(logDBG);
                    doOptions.Add("Do you want to log all objects listed?");
                    boOptions.Add(logALL);
                }
                for (int i = 0; i < doOptions.Count(); i++)
                {
                    if (GetArgsInfo.export && i == 0)
                    {
                        Console.WriteLine("Export Options:");
                    }
                    else if ((GetArgsInfo.export && GetArgsInfo.log && i == 2)
                    || (!GetArgsInfo.export && GetArgsInfo.log && i == 0))
                    {
                        Console.WriteLine("Log Options:");
                    }
                    while (true)
                    {
                        Console.Write("{0} (seperate file) (Y/N) ", doOptions[i]);
                        temp = Console.ReadLine().ToUpper();
                        YoN = temp != "Y" && temp != "N" && !(string.IsNullOrEmpty(temp) || string.IsNullOrWhiteSpace(temp));
                        if (YoN)
                        {
                            Console.WriteLine("The input must either be a Y(es) or N(o). Please correctly input the right letters.");
                        }
                        else
                        {
                            boOptions[i].enabled = temp != "N";;
                            break;
                        }
                    }
                }
                GetArgsInfo.expANC = expANC.enabled;
                GetArgsInfo.expMAH = expMAH.enabled;
                GetArgsInfo.logEVY = logEVY.enabled;
                GetArgsInfo.logDBG = logDBG.enabled;
                GetArgsInfo.logALL = logALL.enabled;

                GetCodesuInfo.mode = Int32.Parse(getLine[getLine.Length-1]);

                // Getting rid of comments (in newlines) or empty newlines
                for (int i = GetMapInfo.GetItemLine("[HitObjects]"); i < GetCodesuInfo.lines.Count; i++)
                {
                    try
                    {
                        if (GetCodesuInfo.lines.Skip(i).First() == "" || GetCodesuInfo.lines.Skip(i).First().Contains("//"))
                        {
                            if (GetArgsInfo.export)
                            {
                                if (!GetArgsInfo.ignore)
                                {
                                    Console.WriteLine("Export: Ignoring illegal GetCodesuInfo.lines found at line {0}", i + 1);
                                }
                            }
                            else if (!GetArgsInfo.ignore)
                            {
                                Console.WriteLine("Warning: Remove illegal line found at line {0} before submitting: {1}", i + 1, GetCodesuInfo.lines.Skip(i).First());
                            }
                            continue;
                        }
                    }
                    catch (System.IndexOutOfRangeException)
                    {
                        Console.WriteLine("Error: Something went wrong at line {0}", i + 1);
                        return;
                    }
                }

                // Running programming language
                GetCodesuInfo.runMode(false);
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

        public class boolRelated
        {
            public bool enabled { get; set; }
        }
    }
}