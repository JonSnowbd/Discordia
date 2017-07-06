using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Maid.Utility
{
    public static class TriggerUtil
    {
        public static TriggerMethod ByName(string activator)
        {
            return (message) =>
            {
                return message.StartsWith(MaidCore.Prefix + activator);
            };
        }

        public static TriggerMethod ByRegex(Regex reg)
        {
            return (message) =>
            {
                return reg.IsMatch(message);
            };
        }
    }
}
