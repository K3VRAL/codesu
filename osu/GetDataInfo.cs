using System;
using System.IO;
using System.Collections.Generic;

using osuProgram.codesu;

namespace osuProgram.osu
{
    public static partial class datasu
    {
        public static List<string> everyStore = new();
        public static List<string> debugStore = new();
        private static List<string> allStore = new();
        public static StringWriter esw = new();
        public static StringWriter dsw = new();
        private static StringWriter asw = new();

        // Doing things with all, export, and/or log
        public static void External()
        {
            if (GetArgsInfo.all || GetArgsInfo.logALL)
            {
                if (GetArgsInfo.logALL)
                {
                    Console.SetOut(asw);
                    Console.SetError(asw);
                }
                foreach (var x in GetCodesuInfo.AllHitObjects)
                {
                    Console.WriteLine("Object: {0}\tFileLine: {1}\tX: {2}\tY: {3}\tTime: {4}\tFullLine: {5}", x.OType, x.FileLine, x.XVal, x.YVal, x.TVal, x.Object);
                    datasu.Step(true);
                }
                if (GetArgsInfo.logALL)
                {
                    allStore.Add(asw.ToString());
                    StreamWriter standardOutput = new(Console.OpenStandardOutput());
                    standardOutput.AutoFlush = true;
                    Console.SetOut(standardOutput);
                }
            }

            if (GetArgsInfo.export)
            {
                if (GetArgsInfo.export && GetArgsInfo.expANC)
                {
                    Console.WriteLine("Sorting before Exporting");
                    foreach (var x in GetCodesuInfo.AllHitObjects)
                    {
                        switch (x.OType)
                        {
                            case GetObjectInfo.Type.Normal:
                                x.NCombo = 5;
                                break;

                            case GetObjectInfo.Type.Slider:
                                x.NCombo = 6;
                                break;

                            case GetObjectInfo.Type.Spinner:
                                x.NCombo = 12;
                                break;

                            default:
                                Console.WriteLine("Error: New Combo {0} from Object {1} at line {2}", x.NCombo, x.Object, x.FileLine);
                                return;
                        }
                    }
                }
                Console.WriteLine("Exporting");
                File.Create(GetCodesuInfo.file + ".exported").Close();
                using (StreamWriter sw = new(GetCodesuInfo.file + ".exported"))
                {
                    if (GetArgsInfo.expMAH)
                    {
                        sw.WriteLine("Mode: 2\n[HitObjects]");
                    }
                    foreach (var x in GetCodesuInfo.AllHitObjects)
                    {
                        string[] temp = x.Object.Split(",");
                        if (x.OType == GetObjectInfo.Type.Normal)
                        {
                            sw.WriteLine(x.XVal + "," + x.YVal + "," + x.TVal + "," + x.NCombo + "," + temp[4] + "," + temp[5]);
                        }
                        else if (x.OType == GetObjectInfo.Type.Slider)
                        {
                            sw.WriteLine(x.XVal + "," + x.YVal + "," + x.TVal + "," + x.NCombo + "," + temp[4] + "," + temp[5] + "," + temp[6] + "," + temp[7]);
                        }
                        else if (x.OType == GetObjectInfo.Type.Spinner)
                        {
                            sw.WriteLine(x.XVal + "," + x.YVal + "," + x.TVal + "," + x.NCombo + "," + temp[4] + "," + x.STVal + "," + temp[6]);
                        }
                        else
                        {
                            sw.WriteLine("Error: Exporting had an error so fix issue regarding the New Combo {0} from Object {1} at line {2}", x.NCombo, x.Object, x.FileLine);
                            Console.WriteLine("Error: Exporting had an error in New Combo {0} from Object {1} at line {2}", x.NCombo, x.Object, x.FileLine);
                            return;
                        }
                    }
                }
                Console.WriteLine("Done");
            }

            if (GetArgsInfo.log)
            {
                if (GetArgsInfo.logEVY)
                {
                    Console.WriteLine("Logging every");
                    programsu.ctbProcess();
                    File.Create(GetCodesuInfo.file + ".every.log").Close();
                    using (StreamWriter sw = new(GetCodesuInfo.file + ".every.log"))
                    {
                        foreach (var x in everyStore)
                        {
                            sw.WriteLine(x);
                        }
                    }
                    Console.WriteLine("Done");
                    GetArgsInfo.logEVY = false;
                }
                if (GetArgsInfo.logDBG)
                {
                    Console.WriteLine("Logging debug");
                    bool temp = GetArgsInfo.debug;
                    GetArgsInfo.debug = true;
                    programsu.ctbProcess();
                    File.Create(GetCodesuInfo.file + ".debug.log").Close();
                    using (StreamWriter sw = new(GetCodesuInfo.file + ".debug.log"))
                    {
                        foreach (var x in debugStore)
                        {
                            sw.WriteLine(x);
                        }
                    }
                    Console.WriteLine("Done");
                    GetArgsInfo.debug = temp;
                    GetArgsInfo.logDBG = false;
                }
                if (GetArgsInfo.logALL)
                {
                    Console.WriteLine("Logging all");
                    File.Create(GetCodesuInfo.file + ".all.log").Close();
                    using (StreamWriter sw = new(GetCodesuInfo.file + ".all.log"))
                    {
                        foreach (var x in allStore)
                        {
                            sw.WriteLine(x);
                        }
                    }
                    Console.WriteLine("Done");
                    GetArgsInfo.logALL = false;
                }
            }
        }

        public static void WriteLine()
        {
            if (GetArgsInfo.debug)
            {
                Console.WriteLine();
            }
        }

        public static void WriteLine(string input, params object[] args)
        {
            if (GetArgsInfo.debug)
            {
                Console.WriteLine(input, args);
            }
        }

        public static void Write(string input, params object[] args)
        {
            if (GetArgsInfo.debug)
            {
                Console.Write(input, args);
            }
        }

        public static void Step(bool debug)
        {
            if (GetArgsInfo.step && debug)
            {
                Console.ReadKey(true);
            }
        }
    }
}