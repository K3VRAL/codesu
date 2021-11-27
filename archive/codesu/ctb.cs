using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using osuProgram.osu;

namespace osuProgram.codesu
{
    public static partial class programsu
    {
        public static void ctb(bool log)
        {
            if (!log)
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
            }
            ctbProcess();
        }
        
        private static int[] yloc = {0, 64, 128, 192, 256, 320, 384};
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
                                Command =
                                Int32.Parse(amount[1]) >= yloc[3] && Int32.Parse(amount[1]) < yloc[4] ? "rnd~" :
                                null,
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
                            Command =
                            Int32.Parse(amount[1]) >= yloc[0] && Int32.Parse(amount[1]) < yloc[1] ? "inp;" :
                            Int32.Parse(amount[1]) >= yloc[1] && Int32.Parse(amount[1]) < yloc[2] ? "jmp]" :
                            Int32.Parse(amount[1]) >= yloc[2] && Int32.Parse(amount[1]) < yloc[3] ? "pnt>" :
                            Int32.Parse(amount[1]) >= yloc[3] && Int32.Parse(amount[1]) < yloc[4] ? "dec-" :
                            Int32.Parse(amount[1]) >= yloc[4] && Int32.Parse(amount[1]) < yloc[5] ? "div/" :
                            Int32.Parse(amount[1]) >= yloc[5] && Int32.Parse(amount[1]) <= yloc[6] ? "out:" :
                            null,
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
                            Command =
                            Int32.Parse(amount[1]) >= yloc[0] && Int32.Parse(amount[1]) < yloc[1] ? "inp," :
                            Int32.Parse(amount[1]) >= yloc[1] && Int32.Parse(amount[1]) < yloc[2] ? "jmp[" :
                            Int32.Parse(amount[1]) >= yloc[2] && Int32.Parse(amount[1]) < yloc[3] ? "pnt<" :
                            Int32.Parse(amount[1]) >= yloc[3] && Int32.Parse(amount[1]) < yloc[4] ? "inc+" :
                            Int32.Parse(amount[1]) >= yloc[4] && Int32.Parse(amount[1]) < yloc[5] ? "mul*" :
                            Int32.Parse(amount[1]) >= yloc[5] && Int32.Parse(amount[1]) <= yloc[6] ? "out." :
                            null,
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
                if (x.YVal >= yloc[1] && x.YVal < yloc[2])
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
                if (x.Command == null)
                {
                    Console.WriteLine("Error: There was an error with this Object Type: {0} having a null Command because of Y value: {1}", x.OType, x.YVal);
                    return;
                }
            }
            if (bracketcount != 0)
            {
                Console.WriteLine("Error: There is an unequal amount of lines {0}", bracketcount);
                return;
            }
        }

        private static void ctbProcess()
        {
            int tick = 0;
            int objct = 0;
            List<UInt16> memory = new();
            for (int i = 0; i < UInt16.MaxValue; i++)
            {
                memory.Add(0);
            }
            int memorypos = 0;

            if (GetArgsInfo.logEVY)
            {
                Console.SetOut(datasu.psw);
                Console.SetError(datasu.psw);
            }
            else if (GetArgsInfo.logDBG)
            {
                Console.SetOut(datasu.dsw);
                Console.SetError(datasu.dsw);
            }
            while (objct < GetCodesuInfo.AllHitObjects.Count)
            {
                switch(GetCodesuInfo.AllHitObjects[objct].Command)
                {
                    case "inp,":
                        string inpD = null;
                        if (GetArgsInfo.logEVY || GetArgsInfo.logDBG)
                        {
                            StreamWriter standardOutput = new(Console.OpenStandardOutput());
                            standardOutput.AutoFlush = true;
                            Console.SetOut(standardOutput);
                        }

                        Console.Write("Input (Digit): ");
                        inpD = Console.ReadLine();
                        try
                        {
                            memory[memorypos] = Convert.ToUInt16(inpD);
                        }
                        catch (System.FormatException)
                        {
                            Console.WriteLine("Error: Invalid input: {0} to digit", inpD);
                            return;
                        }
                        catch (System.OverflowException)
                        {
                            Console.WriteLine("Error: Too big of an input. Maxiumum input allowed is {0}: {1} to digit", UInt16.MaxValue, inpD);
                            return;
                        }

                        if (GetArgsInfo.logEVY)
                        {
                            Console.SetOut(datasu.psw);
                            Console.SetError(datasu.psw);
                        }
                        else if (GetArgsInfo.logDBG)
                        {
                            Console.SetOut(datasu.dsw);
                            Console.SetError(datasu.dsw);
                        }
                        break;

                    case "inp;":
                        string inpA = null;
                        if (GetArgsInfo.logEVY || GetArgsInfo.logDBG)
                        {
                            StreamWriter standardOutput = new(Console.OpenStandardOutput());
                            standardOutput.AutoFlush = true;
                            Console.SetOut(standardOutput);
                        }

                        Console.Write("Input (ASCII): ");
                        inpA = Console.ReadLine();
                        try
                        {
                            memory[memorypos] = Convert.ToUInt16(Convert.ToChar(inpA));
                        }
                        catch (System.FormatException)
                        {
                            Console.WriteLine("Error: Invalid input: {0} to ASCII", inpA);
                            return;
                        }
                        catch (System.OverflowException)
                        {
                            Console.WriteLine("Error: Too big of an input. Maxiumum input allowed is {0}: {1} to digit", UInt16.MaxValue, inpA);
                            return;
                        }

                        if (GetArgsInfo.logEVY)
                        {
                            Console.SetOut(datasu.psw);
                            Console.SetError(datasu.psw);
                        }
                        else if (GetArgsInfo.logDBG)
                        {
                            Console.SetOut(datasu.dsw);
                            Console.SetError(datasu.dsw);
                        }
                        break;

                    case "jmp[":
                        int bracketcountR = 1;
                        for (int i = objct + 1; i < GetCodesuInfo.AllHitObjects.Count; i++)
                        {
                            if (GetCodesuInfo.AllHitObjects[i].Command == "jmp[")
                            {
                                bracketcountR++;
                            }
                            else if (GetCodesuInfo.AllHitObjects[i].Command == "jmp]")
                            {
                                bracketcountR--;
                            }
                            if (bracketcountR == 0)
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
                        break;

                    case "jmp]":
                        int bracketcountL = 1;
                        for (int i = objct - 1; i >= 0; i--)
                        {
                            if (GetCodesuInfo.AllHitObjects[i].Command == "jmp[")
                            {
                                bracketcountL--;
                            }
                            else if (GetCodesuInfo.AllHitObjects[i].Command == "jmp]")
                            {
                                bracketcountL++;
                            }
                            if (bracketcountL == 0)
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
                        break;

                    case "pnt<":
                        memorypos--;
                        if (memorypos < 0)
                        {
                            memorypos = memory.Count - 1;
                        }
                        break;

                    case "pnt>":
                        memorypos++;
                        if (memory.Count <= memorypos)
                        {
                            memorypos = 0;
                        }
                        break;

                    case "inc+":
                        if (memory[memorypos] == UInt16.MaxValue)
                        {
                            memory[memorypos] = UInt16.MinValue;
                        }
                        else
                        {
                            memory[memorypos]++;
                        }
                        break;

                    case "dec-":
                        if (memory[memorypos] == UInt16.MinValue)
                        {
                            memory[memorypos] = UInt16.MaxValue;
                        }
                        else
                        {
                            memory[memorypos]--;
                        }
                        break;

                    case "rnd~":
                        memory[memorypos] = (UInt16)new Random().Next(UInt16.MinValue, UInt16.MaxValue);
                        break;

                    case "mul*":
                        memory[memorypos] *= 2;
                        break;

                    case "div/":
                        memory[memorypos] /= 2;
                        break;

                    case "out.":
                        datasu.Write("Output: ");
                        Console.Write(memory[memorypos]);
                        datasu.WriteLine();
                        datasu.Step(!GetArgsInfo.debug);
                        break;

                    case "out:":
                        datasu.Write("Output: ");
                        Console.Write((char)memory[memorypos]);
                        datasu.WriteLine();
                        datasu.Step(!GetArgsInfo.debug);
                        break;

                    case null:
                        Console.WriteLine("Error: Object {0} in line {1} has command {2}", GetCodesuInfo.AllHitObjects[objct].Object, GetCodesuInfo.AllHitObjects[objct].FileLine + 1, GetCodesuInfo.AllHitObjects[objct].Command);
                        return;

                    default:
                        Console.WriteLine("Error: Tick: {0} and Object: {1} at FileLine: {2} has Y value of {3}. Change this to be in range from {4} to {5}.", tick + 1, objct + 1, GetCodesuInfo.AllHitObjects[objct].FileLine, GetCodesuInfo.AllHitObjects[objct].YVal, yloc[0], yloc[yloc.Length-1]);
                        return;
                }
                datasu.WriteLine("Tick: {0}\tObject: {1}\tFileLine: {2}\tMemPos: {3}\tMemVal: {4}\tCommand: {5}", tick + 1, objct + 1, GetCodesuInfo.AllHitObjects[objct].FileLine, memorypos, memory[memorypos], GetCodesuInfo.AllHitObjects[objct].Command);
                datasu.Step(GetArgsInfo.debug);
                objct++;
                tick++;
            }
            if (GetArgsInfo.logEVY)
            {
                datasu.printStore.Add(datasu.psw.ToString());
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