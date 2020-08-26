using FeudalMP.Network;
using FeudalMP.Util;

namespace FeudalMP
{
    public sealed class ObjectBroker
    {
        private Logger Logger;

        // Brokerage Objects
        private NetworkService _NetworkService;
        public NetworkService NetworkService
        {
            get { return _NetworkService; }
            set
            {
                if (_NetworkService != null)
                {
                    throw new System.AccessViolationException("Not allowed to overwrite already initialized NetworkService within ObjectBroker");
                }
                _NetworkService = value;
            }
        }

        private static readonly System.Lazy<ObjectBroker>
            lazy =
            new System.Lazy<ObjectBroker>
                (() => new ObjectBroker());

        public static ObjectBroker Instance { get { return lazy.Value; } }

        private ObjectBroker()
        {
            Logger = new Logger(this.GetType().Name);
            Logger.Info("ObjectBroker Setup");
        }
    }
}