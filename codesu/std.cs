using osuProgram.osu;

namespace osuProgram.codesu
{
    public static partial class programsu
    {
        public static void std(bool log)
        {
            if (!log)
            {
                stdObjects();
                if (GetArgsInfo.export || GetArgsInfo.all || GetArgsInfo.log)
                {
                    datasu.External();
                    if (!GetArgsInfo.run)
                    {
                        return;
                    }
                }
            }
            stdProcess();
        }

        private static void stdObjects()
        {}

        private static void stdProcess()
        {}
    }
}