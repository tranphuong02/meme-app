using Core.Common;
using Core.DI;

namespace STS.Framework.DI
{
    public static class EngineContext
    {
        public static IEngine Current
        {
            get { return Singleton<Engine>.Instance; }
        }
    }
}
