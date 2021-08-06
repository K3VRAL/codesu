using System;
using System.Collections.Generic;
using System.Linq;

using osuProgram.osu;

namespace osuProgram.codesu
{
    public static partial class programsu
    {
        public static void taiko(bool log)
        {
            if (!log)
            {
                taikoObjects();
                if (GetArgsInfo.export || GetArgsInfo.all || GetArgsInfo.log)
                {
                    datasu.External();
                    if (!GetArgsInfo.run)
                    {
                        return;
                    }
                }
            }
            taikoProcess();
        }

        private static void taikoObjects()
        {
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
                                HSound = Int32.Parse(amount[4]),
                                STVal = Int32.Parse(amount[5]),
                                Command =
                                Int32.Parse(amount[4]) == 0 ? "Mirror\\" :
                                Int32.Parse(amount[4]) == 2 ? "Mirror/" :
                                Int32.Parse(amount[4]) == 4 ? "Crossing+" :
                                Int32.Parse(amount[4]) == 6 ? "Outputs\"\"" :
                                Int32.Parse(amount[4]) == 8 ? "Duplicates*" :
                                Int32.Parse(amount[4]) == 10 ? "Output$" :
                                Int32.Parse(amount[4]) == 12 ? "Redirect" :
                                Int32.Parse(amount[4]) == 14 ? "Teleport%" :
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
                            HSound = Int32.Parse(amount[4]),
                            Command =
                            Int32.Parse(amount[4]) == 0 ? "End&" :
                            Int32.Parse(amount[4]) == 2 ? "Horizontal-" :
                            Int32.Parse(amount[4]) == 4 ? "Left<" :
                            Int32.Parse(amount[4]) == 6 ? "Downwardsv" :
                            Int32.Parse(amount[4]) == 8 ? "Reflects)" :
                            Int32.Parse(amount[4]) == 10 ? "SetAddress#" :
                            Int32.Parse(amount[4]) == 12 ? "Control Flow~" :
                            Int32.Parse(amount[4]) == 14 ? "Operations{}" :
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
                            HSound = Int32.Parse(amount[4]),
                            Command =
                            Int32.Parse(amount[4]) == 0 ? "Start." :
                            Int32.Parse(amount[4]) == 2 ? "Verticle|" :
                            Int32.Parse(amount[4]) == 4 ? "Right>" :
                            Int32.Parse(amount[4]) == 6 ? "Upward^" :
                            Int32.Parse(amount[4]) == 8 ? "Reflects(" :
                            Int32.Parse(amount[4]) == 10 ? "SetAddress@" :
                            Int32.Parse(amount[4]) == 12 ? "Input?" :
                            Int32.Parse(amount[4]) == 14 ? "Operations[]" :
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

                GetCodesuInfo.AllHitObjects = GetCodesuInfo.AllHitObjects.OrderBy(a => a.TVal).ToList();

                foreach (var x in GetCodesuInfo.AllHitObjects)
                {
                    for (int y = 0; y < GetCodesuInfo.AllHitObjects.Count(); y++)
                    {
                        if ((x.XVal == GetCodesuInfo.AllHitObjects[y].XVal || x.YVal == GetCodesuInfo.AllHitObjects[y].YVal)
                            && (GetCodesuInfo.AllHitObjects[y - 1].Command != "Operations[]" || GetCodesuInfo.AllHitObjects[y - 1].Command != "Operations{}"))
                        {
                            Console.WriteLine("Error: There are overlapping objects: X: {0} Y: {1} Object: {2} with X: {3} Y: {4} Object: {5}", x.XVal, x.YVal, x.Object, GetCodesuInfo.AllHitObjects[y].XVal, GetCodesuInfo.AllHitObjects[y].YVal, GetCodesuInfo.AllHitObjects[y].Object);
                            return;
                        }
                    }
                    if (x.Command == null)
                    {
                        Console.WriteLine("Error: There was an error with this Object Type: {0} having a null Command because of HitSound value: {1}", x.OType, x.HSound);
                        return;
                    }
                }
            }
        }

        private static void taikoProcess()
        {
            int tick = 0;
            List<movingcell> mcell = new();
            // foreach (var obj in GetCodesuInfo.AllHitObjects)
            // {
            //     if (obj.Command == "Start.")
            //     {
            //         mcell.Add(new movingcell
            //         {
            //             x = obj.XVal,
            //             y = obj.YVal,
            //             currentlyOn = "Start.",
            //         });
            //     }
            // }
            mcell.Add(new movingcell
            {
                x = 0,
                y = 0,
                direction = 1,
                currentlyOn = "Start.",
            });
            mcell.Add(new movingcell
            {
                x = 0,
                y = 0,
                direction = 2,
                currentlyOn = "Start.",
            });
            mcell.Add(new movingcell
            {
                x = 0,
                y = 0,
                direction = -1,
                currentlyOn = "Start.",
            });
            mcell.Add(new movingcell
            {
                x = 0,
                y = 0,
                direction = -2,
                currentlyOn = "Start.",
            });

            while (true)
            {
                foreach (var mc in mcell)
                {
                    switch (mc.currentlyOn)
                    {
                        case "":
                            break;

                        default:
                            Console.WriteLine("Error");
                            return;
                    }

                    foreach (var obj in GetCodesuInfo.AllHitObjects)
                    {
                        if (mc.x == obj.XVal && mc.y == obj.YVal)
                        {

                        }
                    }

                    if (mc.currentlyOn == "")
                    {
                        mc.x += mc.direction % 2 != 0 ?
                            mc.direction > 0 ? 1 : -1
                        : mc.x;
                        mc.y += mc.direction % 2 == 0 ?
                            mc.direction > 0 ? 1 : -1
                        : mc.y;
                    }
                }
                if (tick == 5)
                {
                    return;
                }
                tick++;
            }
        }

        private class movingcell
        {
            public int x { get; set; }
            public int y { get; set; }
            public int direction { get; set; }      // 1 = ->, 2 = /\, -1 = <-, -2 = \/
            public string currentlyOn { get; set; }

        }
    }
}