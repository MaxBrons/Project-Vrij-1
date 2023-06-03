using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace PV.Utility
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
