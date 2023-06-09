using System;

namespace PV.Core.Utility
{
    public static class Utilities
    {
        [Serializable]
        public struct SerializedKeyValuePair<KeyPair, ValuePair>
        {
            public KeyPair key;
            public ValuePair value;
        }
    }
}
