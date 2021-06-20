using System;
using System.Collections.Generic;
using System.Linq;

using osuProgram.osu;

namespace osuProgram.codesu
{
    public static class programsu
    {
        public static List<string> lines { get; set; }

        public static void ctb()
        {
            List<GetObjectInfo> AllHitObjects = new();

            // i needs to equal to the beginning of [HitObjects]
            for (int i = GetMapInfo.GetItemLine("[HitObjects]"); i < lines.Count; i++)
            {
                if (lines.Skip(i).First() == "" || lines.Skip(i).First().Contains("//"))
                {
                    if (!GetArgsInfo.ignore)
                    {
                        Console.WriteLine("Warning: Remove illegal line found at line {0} before submitting: {1}", i, lines.Skip(i).First());
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
            List<UInt16> memory = new List<UInt16>();
            for (int i = 0; i < 30000; i++)
                memory.Add(0);
            int memorypos = 0;
            int[] yloc = {0, 64, 128, 192, 256, 320, 384};
            string command = null;

            if (GetArgsInfo.all)
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
                    string inp = null;
                    // inpDig/ASC
                    switch (AllHitObjects[line].OType)
                    {
                        case GetObjectInfo.Type.Normal:
                            Console.Write("Input (Digit): ");
                            inp = Console.ReadLine();
                            try
                            {
                                memory[memorypos] = Convert.ToUInt16(inp);
                                command = "inpDig";
                            }
                            catch (System.FormatException)
                            {
                                Console.WriteLine("Error: Invalid input: {0} to digit", inp);
                                return;
                            }
                            break;

                        case GetObjectInfo.Type.Slider:
                            Console.Write("Input (ASCII): ");
                            inp = Console.ReadLine();
                            try
                            {
                                memory[memorypos] = Convert.ToUInt16(Convert.ToChar(inp));
                                command = "inpASC";
                            }
                            catch (System.FormatException)
                            {
                                Console.WriteLine("Error: Invalid input: {0} to ASCII", inp);
                                return;
                            }
                            break;
                    }
                } else if (AllHitObjects[line].YVal >= yloc[1] && AllHitObjects[line].YVal < yloc[2])
                {
                    // jmp[/]
                    // TODO: Make sure everything has been correctly implemented - looking great so far
                    // TODO: Make it count the brackets precompiling and give an error if the brackets do not have the value of 0
                    int bracketcount = 1;
                    switch (AllHitObjects[line].OType)
                    {
                        case GetObjectInfo.Type.Normal:
                            for (int i = line + 1; i < AllHitObjects.Count; i++)
                            {
                                if ((AllHitObjects[i].YVal >= 85 && AllHitObjects[i].YVal < 170) && AllHitObjects[i].OType == GetObjectInfo.Type.Normal)
                                {
                                    bracketcount++;
                                }
                                else if ((AllHitObjects[i].YVal >= 85 && AllHitObjects[i].YVal < 170) && AllHitObjects[i].OType == GetObjectInfo.Type.Slider)
                                {
                                    bracketcount--;
                                }
                                if (bracketcount == 0)
                                {
                                    if (memory[memorypos] == 0)
                                    {
                                        if (GetArgsInfo.debug)
                                        {
                                            Console.WriteLine("jmp[\t-\tIs 0\tMemVal: {0}\tJumping past jmp]", memory[memorypos]);
                                        }
                                        line = i;
                                    }
                                    else
                                    {
                                        if (GetArgsInfo.debug)
                                        {
                                            Console.WriteLine("jmp[\t-\tNot 0\tMemVal: {0}\tNot jumping past jmp]", memory[memorypos]);
                                        }
                                    }
                                    break;
                                }
                            }
                            command = "jmp[";
                            break;

                        case GetObjectInfo.Type.Slider:
                            for (int i = line - 1; i >= 0; i--)
                            {
                                if ((AllHitObjects[i].YVal >= 85 && AllHitObjects[i].YVal < 170) && AllHitObjects[i].OType == GetObjectInfo.Type.Normal)
                                {
                                    bracketcount--;
                                }
                                else if ((AllHitObjects[i].YVal >= 85 && AllHitObjects[i].YVal < 170) && AllHitObjects[i].OType == GetObjectInfo.Type.Slider)
                                {
                                    bracketcount++;
                                }
                                if (bracketcount == 0)
                                {
                                    if (memory[memorypos] != 0)
                                    {
                                        if (GetArgsInfo.debug)
                                        {
                                            Console.WriteLine("jmp]\t-\tNot 0\tMemVal: {0}\tJumping to jmp[", memory[memorypos]);
                                        }
                                        line = i;
                                    }
                                    else
                                    {
                                        if (GetArgsInfo.debug)
                                        {
                                            Console.WriteLine("jmp]\t-\tIs 0\tMemVal: {0}\tNot jumping to jmp[", memory[memorypos]);
                                        }
                                    }
                                    break;
                                }
                            }
                            command = "jmp]";
                            break;
                    }
                } else if (AllHitObjects[line].YVal >= yloc[2] && AllHitObjects[line].YVal < yloc[3])
                {
                    // pnt</>
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
                    // inc+/dec-/rnd~
                    switch (AllHitObjects[line].OType)
                    {
                        case GetObjectInfo.Type.Normal:
                            if (memory[memorypos] == UInt16.MaxValue)
                            {
                                memory[memorypos] = UInt16.MinValue;
                            }
                            else
                            {
                                memory[memorypos]++;
                            }
                            command = "inc+";
                            break;

                        case GetObjectInfo.Type.Slider:
                            if (memory[memorypos] == UInt16.MinValue)
                            {
                                memory[memorypos] = UInt16.MaxValue;
                            }
                            else
                            {
                                memory[memorypos]--;
                            }
                            command = "dec-";
                            break;

                        case GetObjectInfo.Type.Spinner:
                            memory[memorypos] = (UInt16)new Random().Next(UInt16.MinValue, UInt16.MaxValue);
                            command = "rnd~";
                            break;
                    }
                } else if (AllHitObjects[line].YVal >= yloc[4] && AllHitObjects[line].YVal < yloc[5])
                {
                    // mul*/div/
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
                    // outDig/ASC
                    if (GetArgsInfo.debug)
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
                    if (GetArgsInfo.debug)
                    {
                        Console.WriteLine();
                    }
                } else {
                    Console.WriteLine("Error: Line {0} has Y value of {1}. Change this to be in range from {2} to {3}.", line + 1, AllHitObjects[line].YVal, yloc[0], yloc[yloc.Length-1]);
                    return;
                }

                if (GetArgsInfo.debug)
                {
                    Console.WriteLine("FileLine: {0}\tMemPos: {1}\tMemVal: {2}\tCommand: {3}", line, memorypos, memory[memorypos], command);
                }
                if (GetArgsInfo.step)
                {
                    Console.ReadKey(true);
                }
                line++;
            }
            Console.WriteLine();
        }
        
        public static void std()
        {}
        
        public static void taiko()
        {}
        
        public static void mania()
        {}
    }
}