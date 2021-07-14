using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using osuProgram.osu;

namespace osuProgram.codesu
{
    public static partial class programsu
    {
        public static void ctb()
        {
            ctbObjects();
            if (GetArgsInfo.export || GetArgsInfo.all || GetArgsInfo.log)
            {
                datasu.External();
                if (!GetArgsInfo.run)
                {
                    return;
                }
            }
            ctbProcess();
        }

        private static void ctbObjects()
        {
            // i needs to equal to the beginning of [HitObjects] plus 1
            for (int i = GetMapInfo.GetItemLine("[HitObjects]"); i < GetCodesuInfo.lines.Count; i++)
            {
                try
                {
                    if (GetCodesuInfo.lines.Skip(i).First() == "" || GetCodesuInfo.lines.Skip(i).First().Contains("//"))
                    {
                        continue;
                    }
                    String[] amount = GetCodesuInfo.lines.Skip(i).First().Split(",");
                    if (amount.Length == 7)
                    {
                        if ((Int32.Parse(amount[3]) == 8 || Int32.Parse(amount[3]) == 12)
                        && (Int32.Parse(amount[0]) == 256 && Int32.Parse(amount[1]) == 192))
                        {
                            // Spinner added
                            GetCodesuInfo.AllHitObjects.Add(new GetObjectInfo
                            {
                                Object = GetCodesuInfo.lines.Skip(i).First(),
                                FileLine = i + 1,
                                OType = GetObjectInfo.Type.Spinner,
                                XVal = Int32.Parse(amount[0]),
                                YVal = Int32.Parse(amount[1]),
                                TVal = Int32.Parse(amount[2]),
                                NCombo = Int32.Parse(amount[3]),        // 12 is NCombo, 8 isn't
                                STVal = Int32.Parse(amount[5]),
                            });
                        }
                        else
                        {
                            Console.WriteLine("An error with the spinner: {0} at line {1}", GetCodesuInfo.lines.Skip(i).First(), i + 1);
                            return;
                        }
                    }
                    else if ((Int32.Parse(amount[3]) == 6 || Int32.Parse(amount[3]) == 2) 
                    && (Int32.Parse(amount[0]) >= 0 && Int32.Parse(amount[0]) <= 512)
                    && (Int32.Parse(amount[1]) >= 0 && Int32.Parse(amount[1]) <= 384))
                    {
                        // Slider added
                        GetCodesuInfo.AllHitObjects.Add(new GetObjectInfo
                        {
                            Object = GetCodesuInfo.lines.Skip(i).First(),
                            FileLine = i + 1,
                            OType = GetObjectInfo.Type.Slider,
                            XVal = Int32.Parse(amount[0]),
                            YVal = Int32.Parse(amount[1]),
                            TVal = Int32.Parse(amount[2]),
                            NCombo = Int32.Parse(amount[3]),            //  6 is NCombo, 2 isn't
                        });
                    }
                    else if ((Int32.Parse(amount[3]) == 5 || Int32.Parse(amount[3]) == 1)
                    && (Int32.Parse(amount[0]) >= 0 && Int32.Parse(amount[0]) <= 512)
                    && (Int32.Parse(amount[1]) >= 0 && Int32.Parse(amount[1]) <= 384))
                    {
                        // Circle added
                        GetCodesuInfo.AllHitObjects.Add(new GetObjectInfo
                        {
                            Object = GetCodesuInfo.lines.Skip(i).First(),
                            FileLine = i + 1,
                            OType = GetObjectInfo.Type.Normal,
                            XVal = Int32.Parse(amount[0]),
                            YVal = Int32.Parse(amount[1]),
                            TVal = Int32.Parse(amount[2]),
                            NCombo = Int32.Parse(amount[3]),            //  5 is NCombo, 1 isn't
                        });
                    }
                    else
                    {
                        Console.WriteLine("Error: {0} on line {1}", GetCodesuInfo.lines.Skip(i).First(), i + 1);
                        return;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("Error: Something went wrong at line {0}", i + 1);
                    return;
                }
            }
            GetCodesuInfo.AllHitObjects = GetCodesuInfo.AllHitObjects.OrderBy(a => a.TVal).ToList();

            int bracketcount = 0;
            foreach (var x in GetCodesuInfo.AllHitObjects)
            {
                if (x.YVal >= 64 && x.YVal < 128)
                {
                    switch (x.OType)
                    {
                        case GetObjectInfo.Type.Normal:
                                bracketcount++;
                            break;

                        case GetObjectInfo.Type.Slider:
                                bracketcount--;
                            break;

                        default:
                            Console.WriteLine("Error: There was an error with this Object Type: {0} and it's Y value: {1}", x.OType, x.YVal);
                            return;
                    }
                }
            }
            if (bracketcount != 0)
            {
                Console.WriteLine("Error: There is an unequal amount of lines {0}", bracketcount);
                return;
            }
        }

        public static void ctbProcess()
        {
            int tick = 0;
            int objct = 0;
            List<UInt16> memory = new List<UInt16>();
            for (int i = 0; i < 30000; i++)
            {
                memory.Add(0);
            }
            int memorypos = 0;
            int[] yloc = {0, 64, 128, 192, 256, 320, 384};
            string command = null;

            if (GetArgsInfo.logEVY)
            {
                Console.SetOut(datasu.esw);
                Console.SetError(datasu.esw);
            }
            else if (GetArgsInfo.logDBG)
            {
                Console.SetOut(datasu.dsw);
                Console.SetError(datasu.dsw);
            }
            while (objct < GetCodesuInfo.AllHitObjects.Count)
            {
                if (GetCodesuInfo.AllHitObjects[objct].YVal >= yloc[0] && GetCodesuInfo.AllHitObjects[objct].YVal < yloc[1])
                {
                    string inp = null;
                    if (GetArgsInfo.logEVY || GetArgsInfo.logDBG)
                    {
                        StreamWriter standardOutput = new(Console.OpenStandardOutput());
                        standardOutput.AutoFlush = true;
                        Console.SetOut(standardOutput);
                    }
                    //      inp,        V       inp;
                    switch (GetCodesuInfo.AllHitObjects[objct].OType)
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
                    if (GetArgsInfo.logEVY)
                    {
                        Console.SetOut(datasu.esw);
                        Console.SetError(datasu.esw);
                    }
                    else if (GetArgsInfo.logDBG)
                    {
                        Console.SetOut(datasu.dsw);
                        Console.SetError(datasu.dsw);
                    }
                }
                else if (GetCodesuInfo.AllHitObjects[objct].YVal >= yloc[1] && GetCodesuInfo.AllHitObjects[objct].YVal < yloc[2])
                {
                    //      jmp[        V       jmp]
                    int bracketcount = 1;
                    switch (GetCodesuInfo.AllHitObjects[objct].OType)
                    {
                        case GetObjectInfo.Type.Normal:
                            for (int i = objct + 1; i < GetCodesuInfo.AllHitObjects.Count; i++)
                            {
                                if ((GetCodesuInfo.AllHitObjects[i].YVal >= 85 && GetCodesuInfo.AllHitObjects[i].YVal < 170) && GetCodesuInfo.AllHitObjects[i].OType == GetObjectInfo.Type.Normal)
                                {
                                    bracketcount++;
                                }
                                else if ((GetCodesuInfo.AllHitObjects[i].YVal >= 85 && GetCodesuInfo.AllHitObjects[i].YVal < 170) && GetCodesuInfo.AllHitObjects[i].OType == GetObjectInfo.Type.Slider)
                                {
                                    bracketcount--;
                                }
                                if (bracketcount == 0)
                                {
                                    if (memory[memorypos] == 0)
                                    {
                                        datasu.WriteLine("jmp[\t-\tIs 0\tMemVal: {0}\tJumping past jmp]", memory[memorypos]);
                                        objct = i;
                                    }
                                    else
                                    {
                                        datasu.WriteLine("jmp[\t-\tNot 0\tMemVal: {0}\tNot jumping past jmp]", memory[memorypos]);
                                    }
                                    break;
                                }
                            }
                            command = "jmp[";
                            break;

                        case GetObjectInfo.Type.Slider:
                            for (int i = objct - 1; i >= 0; i--)
                            {
                                if ((GetCodesuInfo.AllHitObjects[i].YVal >= 85 && GetCodesuInfo.AllHitObjects[i].YVal < 170) && GetCodesuInfo.AllHitObjects[i].OType == GetObjectInfo.Type.Normal)
                                {
                                    bracketcount--;
                                }
                                else if ((GetCodesuInfo.AllHitObjects[i].YVal >= 85 && GetCodesuInfo.AllHitObjects[i].YVal < 170) && GetCodesuInfo.AllHitObjects[i].OType == GetObjectInfo.Type.Slider)
                                {
                                    bracketcount++;
                                }
                                if (bracketcount == 0)
                                {
                                    if (memory[memorypos] != 0)
                                    {
                                        datasu.WriteLine("jmp]\t-\tNot 0\tMemVal: {0}\tJumping to jmp[", memory[memorypos]);
                                        objct = i;
                                    }
                                    else
                                    {
                                        datasu.WriteLine("jmp]\t-\tIs 0\tMemVal: {0}\tNot jumping to jmp[", memory[memorypos]);
                                    }
                                    break;
                                }
                            }
                            command = "jmp]";
                            break;
                    }
                }
                else if (GetCodesuInfo.AllHitObjects[objct].YVal >= yloc[2] && GetCodesuInfo.AllHitObjects[objct].YVal < yloc[3])
                {
                    //  pnt<        V       pnt>
                    switch (GetCodesuInfo.AllHitObjects[objct].OType)
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
                else if (GetCodesuInfo.AllHitObjects[objct].YVal >= yloc[3] && GetCodesuInfo.AllHitObjects[objct].YVal < yloc[4])
                {
                    //  inc+        V       dec-        V       rnd~
                    switch (GetCodesuInfo.AllHitObjects[objct].OType)
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
                else if (GetCodesuInfo.AllHitObjects[objct].YVal >= yloc[4] && GetCodesuInfo.AllHitObjects[objct].YVal < yloc[5])
                {
                    //  mul*        V       div/
                    switch (GetCodesuInfo.AllHitObjects[objct].OType)
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
                else if (GetCodesuInfo.AllHitObjects[objct].YVal >= yloc[5] && GetCodesuInfo.AllHitObjects[objct].YVal <= yloc[6])
                {
                    //  out.        V       out:
                    datasu.Write("Output: ");
                    switch (GetCodesuInfo.AllHitObjects[objct].OType)
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
                    datasu.WriteLine();
                    datasu.Step(!GetArgsInfo.debug);
                }
                else
                {
                    Console.WriteLine("Error: Tick: {0} and Object: {1} at FileLine: {2} has Y value of {3}. Change this to be in range from {4} to {5}.", tick + 1, objct + 1, GetCodesuInfo.AllHitObjects[objct].FileLine, GetCodesuInfo.AllHitObjects[objct].YVal, yloc[0], yloc[yloc.Length-1]);
                    return;
                }
                datasu.WriteLine("Tick: {0}\tObject: {1}\tFileLine: {2}\tMemPos: {3}\tMemVal: {4}\tCommand: {5}", tick + 1, objct + 1, GetCodesuInfo.AllHitObjects[objct].FileLine, memorypos, memory[memorypos], command);
                datasu.Step(GetArgsInfo.debug);
                objct++;
                tick++;
                if (tick == 1000000)
                {
                    break;
                }
            }
            if (GetArgsInfo.logEVY)
            {
                datasu.everyStore.Add(datasu.esw.ToString());
                StreamWriter standardOutput = new(Console.OpenStandardOutput());
                standardOutput.AutoFlush = true;
                Console.SetOut(standardOutput);
            }
            else if (GetArgsInfo.logDBG)
            {
                datasu.debugStore.Add(datasu.dsw.ToString());
                StreamWriter standardOutput = new(Console.OpenStandardOutput());
                standardOutput.AutoFlush = true;
                Console.SetOut(standardOutput);
            }
        }
    }
}