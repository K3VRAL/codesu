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
            if (GetArgsInfo.export || GetArgsInfo.all)
            {
                ctbExport();
                return;
            }
            int objct = 0;
            List<UInt16> memory = new List<UInt16>();
            for (int i = 0; i < 30000; i++)
                memory.Add(0);
            int memorypos = 0;
            int[] yloc = {0, 64, 128, 192, 256, 320, 384};
            string command = null;

            while (objct < GetCodesuInfo.AllHitObjects.Count)
            {
                if (GetCodesuInfo.AllHitObjects[objct].YVal >= yloc[0] && GetCodesuInfo.AllHitObjects[objct].YVal < yloc[1])
                {
                    string inp = null;
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
                }
                else if (GetCodesuInfo.AllHitObjects[objct].YVal >= yloc[1] && GetCodesuInfo.AllHitObjects[objct].YVal < yloc[2])
                {
                    //      jmp[        V       jmp]
                    // TODO: Make sure everything has been correctly implemented - looking great so far
                    // TODO: Make it count the brackets precompiling and give an error if the brackets do not have the value of 0
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
                    if (GetArgsInfo.debug)
                    {
                        Console.Write("Output: ");
                    }
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
                    Console.WriteLine("Error: Object {0} at FileLine: {1} has Y value of {2}. Change this to be in range from {3} to {4}.", objct + 1, GetCodesuInfo.AllHitObjects[objct].FileLine, GetCodesuInfo.AllHitObjects[objct].YVal, yloc[0], yloc[yloc.Length-1]);
                    return;
                }

                if (GetArgsInfo.debug && GetArgsInfo.step)
                {
                    Console.WriteLine("Object: {0}\tFileLine: {1}\tMemPos: {2}\tMemVal: {3}\tCommand: {4}", objct, GetCodesuInfo.AllHitObjects[objct].FileLine, memorypos, memory[memorypos], command);
                    Console.ReadKey(true);
                }
                else if (GetArgsInfo.debug)
                {
                    Console.WriteLine("Object: {0}\tFileLine: {1}\tMemPos: {2}\tMemVal: {3}\tCommand: {4}", objct, GetCodesuInfo.AllHitObjects[objct].FileLine, memorypos, memory[memorypos], command);
                }
                objct++;
            }
            Console.WriteLine();
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
                        if ((Int32.Parse(amount[3]) == 8 || Int32.Parse(amount[3]) == 12) && (Int32.Parse(amount[0]) == 256 && Int32.Parse(amount[1]) == 192))
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
                    else if (Int32.Parse(amount[3]) == 6 || Int32.Parse(amount[3]) == 2)
                    {
                        // TODO: Make checks if above and below limits with both x and y
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
                    else if (Int32.Parse(amount[3]) == 5 || Int32.Parse(amount[3]) == 1)
                    {
                        // TODO: Make checks if above and below limits with both x and y
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
        }

        private static void ctbExport()
        {
            if (GetArgsInfo.all)
            {
                foreach (var x in GetCodesuInfo.AllHitObjects)
                {
                    Console.WriteLine("Object: {0}\tFileLine: {1}\tX: {2}\tY: {3}\tTime: {4}\tFullLine: {5}", x.OType, x.FileLine, x.XVal, x.YVal, x.TVal, x.Object);
                }
                if (!GetArgsInfo.export)
                {
                    return;
                }
            }
            if (GetArgsInfo.export)
            {
                if (GetArgsInfo.expANC)
                {
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
                File.Create(GetCodesuInfo.file).Close();
                using (StreamWriter sw = new(GetCodesuInfo.file))
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
                Console.WriteLine("Export: Done!");
                return;
            }
        }
    }
}