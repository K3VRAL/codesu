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
        {}

        private static void taikoProcess()
        {}
    }
}