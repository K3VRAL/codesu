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
                string[] file = null;
                bool debug = args.Any(i => i.Contains("-d"));
                bool ignore = args.Any(i => i.Contains("-i"));
                bool step = args.Any(i => i.Contains("-s"));
                bool all = args.Any(i => i.Contains("-a"));

                foreach (string x in args)
                {
                    if (x.Contains(".osu"))
                    {
                        file = x.Split(".");
                        break;
                    }
                }

                List<string> lines = new(File.ReadAllLines(args[0]));
                List<GetObjectInfo> AllHitObjects = new();
                GetMapInfo AllMapInfo = new();
                AllMapInfo.Path = lines;

                if (file[file.Length-1].Equals("osu"))
                {
                    // i needs to equal to the beginning of [HitObjects]
                    for (int i = AllMapInfo.GetItemLine("[HitObjects]"); i < lines.Count; i++)
                    {
                        if (lines.Skip(i).First() == "" || lines.Skip(i).First().Contains("//"))
                        {
                            if (!ignore)
                            {
                                Console.WriteLine("Warning: There was a found illegal line: {0} at line {1} possibly used for debugging. This will need to be removed when submitting.", lines.Skip(i).First(), i);
                            }
                            continue;
                        }

                        String[] amount = lines.Skip(i).First().Split(",");
                        if (amount.Length == 7)
                        {
                            if (Int32.Parse(amount[3]) == 12 && (Int32.Parse(amount[0]) == 256 && Int32.Parse(amount[1]) == 192))
                            {
                                // Spinner added
                                AllHitObjects.Add(new GetObjectInfo
                                {
                                    Object = lines.Skip(i).First(),
                                    OType = GetObjectInfo.Type.Spinner,
                                    XVal = Int32.Parse(amount[0]),
                                    YVal = Int32.Parse(amount[1]),
                                    TVal = Int32.Parse(amount[2])
                                });
                            }
                            else
                            {
                                Console.WriteLine("An error with the spinner: {0} at line {1}", lines.Skip(i).First(), i);
                                return;
                            }
                        }
                        else if (Int32.Parse(amount[3]) == 6 || Int32.Parse(amount[3]) == 2)
                        {
                            // TODO: Make checks if above and below limits with both x and y
                            // Slider added
                            AllHitObjects.Add(new GetObjectInfo
                            {
                                Object = lines.Skip(i).First(),
                                OType = GetObjectInfo.Type.Slider,
                                XVal = Int32.Parse(amount[0]),
                                YVal = Int32.Parse(amount[1]),
                                TVal = Int32.Parse(amount[2])
                            });
                        }
                        else if (Int32.Parse(amount[3]) == 5 || Int32.Parse(amount[3]) == 1)
                        {
                            // TODO: Make checks if above and below limits with both x and y
                            // Circle added
                            AllHitObjects.Add(new GetObjectInfo
                            {
                                Object = lines.Skip(i).First(),
                                OType = GetObjectInfo.Type.Normal,
                                XVal = Int32.Parse(amount[0]),
                                YVal = Int32.Parse(amount[1]),
                                TVal = Int32.Parse(amount[2])
                            });
                        }
                        else
                        {
                            Console.WriteLine("Error: {0} on line {1}", lines.Skip(i).First(), i);
                            return;
                        }
                    }

                    AllHitObjects = AllHitObjects.OrderBy(a => a.TVal).ToList();
                    int line = 0;
                    List<int> memory = new List<int>();
                    for (int i = 0; i < 30000; i++)
                        memory.Add(0);
                    int memorypos = 0;
                    int[] yloc = {0, 64, 128, 192, 256, 320, 384};
                    string command = null;
                    
                    if (all)
                    {
                        foreach (var x in AllHitObjects)
                        {
                            Console.WriteLine("Object: {0}\tX: {1}\tY: {2}\tTime: {3}\tFullLine: {4}", x.OType, x.XVal, x.YVal, x.TVal, x.Object);
                        }
                        return;
                    }

                    while (line < AllHitObjects.Count)
                    {
                        if (AllHitObjects[line].YVal >= yloc[0] && AllHitObjects[line].YVal < yloc[1])
                        {
                            // Input
                            switch (AllHitObjects[line].OType)
                            {
                                case GetObjectInfo.Type.Normal:
                                    try
                                    {
                                        Console.Write("Input (Digit): ");
                                        memory[memorypos] = Convert.ToInt32(Console.ReadLine());
                                        command = "inpDig";
                                    }
                                    catch (System.FormatException)
                                    {
                                        Console.WriteLine("Input with a digit.");
                                        return;
                                    }
                                    break;

                                case GetObjectInfo.Type.Slider:
                                    Console.Write("Input (ASCII): ");
                                    memory[memorypos] = Convert.ToChar(Console.ReadLine());
                                    command = "inpASC";
                                    break;
                            }
                        } else if (AllHitObjects[line].YVal >= yloc[1] && AllHitObjects[line].YVal < yloc[2])
                        {
                            // Jump
                            // TODO: Make it count the brackets precompiling and give an error if the brackets do not have the value of 0
                            int bracketcount = 0;
                            switch (AllHitObjects[line].OType)
                            {
                                case GetObjectInfo.Type.Normal:
                                    for (int i = line; i >= 0; i--)
                                    {
                                        if ((AllHitObjects[i].YVal >= 85 && AllHitObjects[i].YVal < 170) && AllHitObjects[i].OType == GetObjectInfo.Type.Normal)
                                        {
                                            bracketcount++;
                                        }
                                        else if ((AllHitObjects[i].YVal >= 85 && AllHitObjects[i].YVal < 170) && AllHitObjects[i].OType == GetObjectInfo.Type.Slider)
                                        {
                                            bracketcount--;
                                        }
                                        else if (bracketcount == 0)
                                        {
                                            if (memory[memorypos] == 0)
                                            {
                                                if (debug)
                                                {
                                                    Console.WriteLine("Looping since MemVal: {0} is 0", memory[memorypos]);
                                                }
                                                line = i;
                                            }
                                            break;
                                        }
                                    }
                                    command = "jmp[";
                                    break;

                                case GetObjectInfo.Type.Slider:
                                    for (int i = line; i >= 0; i--)
                                    {
                                        if ((AllHitObjects[i].YVal >= 85 && AllHitObjects[i].YVal < 170) && AllHitObjects[i].OType == GetObjectInfo.Type.Normal)
                                        {
                                            bracketcount--;
                                        }
                                        else if ((AllHitObjects[i].YVal >= 85 && AllHitObjects[i].YVal < 170) && AllHitObjects[i].OType == GetObjectInfo.Type.Slider)
                                        {
                                            bracketcount++;
                                        }
                                        else if (bracketcount == 0)
                                        {
                                            if (memory[memorypos] != 0)
                                            {
                                                if (debug)
                                                {
                                                    Console.WriteLine("Looping since MemVal: {0} is not 0", memory[memorypos]);
                                                }
                                                line = i;
                                            }
                                            break;
                                        }
                                    }
                                    command = "jmp]";
                                    break;
                            }
                        } else if (AllHitObjects[line].YVal >= yloc[2] && AllHitObjects[line].YVal < yloc[3])
                        {
                            // Pointer
                            switch (AllHitObjects[line].OType)
                            {
                                case GetObjectInfo.Type.Normal:
                                    memorypos--;
                                        if (memorypos < 0)
                                        {
                                            memorypos = memory.Count - 1;
                                        }
                                    command = "pnt<";
                                    break;

                                case GetObjectInfo.Type.Slider:
                                    memorypos++;
                                        if (memory.Count <= memorypos)
                                        {
                                            memorypos = 0;
                                        }
                                    command = "pnt>";
                                    break;
                            }
                        } else if (AllHitObjects[line].YVal >= yloc[3] && AllHitObjects[line].YVal < yloc[4])
                        {
                            // Inc/Dec
                            switch (AllHitObjects[line].OType)
                            {
                                case GetObjectInfo.Type.Normal:
                                    memory[memorypos]++;
                                    command = "inc+";
                                    break;

                                case GetObjectInfo.Type.Slider:
                                    memory[memorypos]--;
                                    command = "dec-";
                                    break;

                                case GetObjectInfo.Type.Spinner:
                                    memory[memorypos] = new Random().Next(0, 2);
                                    command = "rnd~";
                                    break;
                            }
                        } else if (AllHitObjects[line].YVal >= yloc[4] && AllHitObjects[line].YVal < yloc[5])
                        {
                            // Mul/Div
                            switch (AllHitObjects[line].OType)
                            {
                                case GetObjectInfo.Type.Normal:
                                    memory[memorypos] *= 2;
                                    command = "mul*";
                                    break;

                                case GetObjectInfo.Type.Slider:
                                    memory[memorypos] /= 2;
                                    command = "div/";
                                    break;
                            }
                        } else if (AllHitObjects[line].YVal >= yloc[5] && AllHitObjects[line].YVal <= yloc[6])
                        {
                            // Output
                            if (debug)
                            {
                                Console.Write("Output: ");
                            }
                            switch (AllHitObjects[line].OType)
                            {
                                case GetObjectInfo.Type.Normal:
                                    Console.Write(memory[memorypos]);
                                    command = "outDig";
                                    break;

                                case GetObjectInfo.Type.Slider:
                                    Console.Write((char)memory[memorypos]);
                                    command = "outASC";
                                    break;
                            }
                            if (debug)
                            {
                                Console.WriteLine();
                            }
                        } else {
                            Console.WriteLine("Error: Line {0} has Y value of {1}. Change this to be in range from {2} to {3}.", line + 1, AllHitObjects[line].YVal, yloc[0], yloc[yloc.Length-1]);
                            return;
                        }

                        if (debug)
                        {
                            Console.WriteLine("FileLine: {0}\tMemPos: {1}\tMemVal: {2}\tCommand: {3}", line + 1, memorypos, memory[memorypos], command);
                        }
                        if (step)
                        {
                            Console.ReadKey(true);
                        }
                        line++;
                    }
                    Console.WriteLine();
                }
                else if (!file[file.Length-1].Equals("osu"))
                {
                    Console.WriteLine("Error: Not a valid .osu file. Is a {0} file: {1}", file[file.Length-1], args[0]);
                }
            }
            else if (args.Length == 0)
            {
                Console.WriteLine("Seems as if there are too few arguments/files being inputted: {0} instead of 1.", args.Length);
            }
            else
            {
                Console.WriteLine("An error has occurred. Args: {0}", args);
            }
            return;
        }
    }
}