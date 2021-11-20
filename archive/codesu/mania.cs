using osuProgram.osu;

namespace osuProgram.codesu
{
    public static partial class programsu
    {
        public static void mania(bool log)
        {
            if (!log)
            {
                maniaObjects();
                if (GetArgsInfo.export || GetArgsInfo.all || GetArgsInfo.log)
                {
                    datasu.External();
                    if (!GetArgsInfo.run)
                    {
                        return;
                    }
                }
            }
            maniaProcess();
        }

        private static void maniaObjects()
        {}

        private static void maniaProcess()
        {}
    }
}