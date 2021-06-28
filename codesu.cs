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

            if (GetMapInfo.GetItemLine("[HitObjects]") == -1)
            {
                Console.WriteLine("Error: There is no [HitObjects]. Please include this for the program to work.");
                return;
            }

            // i needs to equal to the beginning of [HitObjects]
            for (int i = GetMapInfo.GetItemLine("[HitObjects]"); i < lines.Count; i++)
            {
                try
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
                                FileLine = i + 1,
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
                            FileLine = i + 1,
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
                            FileLine = i + 1,
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
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("Error: Something went wrong at line {0}", i + 1);
                    return;
                }
            }
            
            AllHitObjects = AllHitObjects.OrderBy(a => a.TVal).ToList();
            int objct = 0;
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
                    Console.WriteLine("Object: {0}\tFileLine: {1}\tX: {2}\tY: {3}\tTime: {4}\tFullLine: {5}", x.OType, x.FileLine, x.XVal, x.YVal, x.TVal, x.Object);
                }
                return;
            }

            while (objct < AllHitObjects.Count)
            {
                if (AllHitObjects[objct].YVal >= yloc[0] && AllHitObjects[objct].YVal < yloc[1])
                {
                    string inp = null;
                    //      inp,        V       inp;
                    switch (AllHitObjects[objct].OType)
                    {
                        case GetObjectInfo.Type.Normal:
                            Console.Write("Input (Digit): ");
                            inp = Console.ReadLine();
                            try
                            {
                                memory[memorypos] = Convert.ToUInt16(inp);
                                command = "inp,";
                            }
                            catch (System.FormatException)
                            {
                                Console.WriteLine("Error: Invalid input: {0} to digit", inp);
                                return;
                            }
                            catch (System.OverflowException)
                            {
                                Console.WriteLine("Error: Too big of an input. Maxiumum input allowed is {0}: {1} to digit", UInt16.MaxValue, inp);
                                return;
                            }
                            break;

                        case GetObjectInfo.Type.Slider:
                            Console.Write("Input (ASCII): ");
                            inp = Console.ReadLine();
                            try
                            {
                                memory[memorypos] = Convert.ToUInt16(Convert.ToChar(inp));
                                command = "inp;";
                            }
                            catch (System.FormatException)
                            {
                                Console.WriteLine("Error: Invalid input: {0} to ASCII", inp);
                                return;
                            }
                            catch (System.OverflowException)
                            {
                                Console.WriteLine("Error: Too big of an input. Maxiumum input allowed is {0}: {1} to digit", UInt16.MaxValue, inp);
                                return;
                            }
                            break;
                    }
                }
                else if (AllHitObjects[objct].YVal >= yloc[1] && AllHitObjects[objct].YVal < yloc[2])
                {
                    //      jmp[        V       jmp]
                    // TODO: Make sure everything has been correctly implemented - looking great so far
                    // TODO: Make it count the brackets precompiling and give an error if the brackets do not have the value of 0
                    int bracketcount = 1;
                    switch (AllHitObjects[objct].OType)
                    {
                        case GetObjectInfo.Type.Normal:
                            for (int i = objct + 1; i < AllHitObjects.Count; i++)
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
                                        objct = i;
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
                            for (int i = objct - 1; i >= 0; i--)
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
                                        objct = i;
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
                }
                else if (AllHitObjects[objct].YVal >= yloc[2] && AllHitObjects[objct].YVal < yloc[3])
                {
                    //  pnt<        V       pnt>
                    switch (AllHitObjects[objct].OType)
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
                }
                else if (AllHitObjects[objct].YVal >= yloc[3] && AllHitObjects[objct].YVal < yloc[4])
                {
                    //  inc+        V       dec-        V       rnd~
                    switch (AllHitObjects[objct].OType)
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
                }
                else if (AllHitObjects[objct].YVal >= yloc[4] && AllHitObjects[objct].YVal < yloc[5])
                {
                    //  mul*        V       div/
                    switch (AllHitObjects[objct].OType)
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
                }
                else if (AllHitObjects[objct].YVal >= yloc[5] && AllHitObjects[objct].YVal <= yloc[6])
                {
                    //  out.        V       out:
                    if (GetArgsInfo.debug)
                    {
                        Console.Write("Output: ");
                    }
                    switch (AllHitObjects[objct].OType)
                    {
                        case GetObjectInfo.Type.Normal:
                            Console.Write(memory[memorypos]);
                            command = "out.";
                            break;

                        case GetObjectInfo.Type.Slider:
                            Console.Write((char)memory[memorypos]);
                            command = "out:";
                            break;
                    }
                    if (GetArgsInfo.debug)
                    {
                        Console.WriteLine();
                    }
                    if (GetArgsInfo.step)
                    {
                        Console.ReadKey(true);
                    }
                }
                else
                {
                    Console.WriteLine("Error: Object {0} at FileLine: {1} has Y value of {2}. Change this to be in range from {3} to {4}.", objct + 1, AllHitObjects[objct].FileLine, AllHitObjects[objct].YVal, yloc[0], yloc[yloc.Length-1]);
                    return;
                }

                if (GetArgsInfo.debug && GetArgsInfo.step)
                {
                    Console.WriteLine("Object: {0}\tFileLine: {1}\tMemPos: {2}\tMemVal: {3}\tCommand: {4}", objct, AllHitObjects[objct].FileLine, memorypos, memory[memorypos], command);
                    Console.ReadKey(true);
                }
                else if (GetArgsInfo.debug)
                {
                    Console.WriteLine("Object: {0}\tFileLine: {1}\tMemPos: {2}\tMemVal: {3}\tCommand: {4}", objct, AllHitObjects[objct].FileLine, memorypos, memory[memorypos], command);
                }
                objct++;
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