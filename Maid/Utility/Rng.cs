using System;
using System.Collections.Generic;
using System.Text;

namespace Maid.Utility
{
    public static class Rng
    {
        private static Random random = new Random();

        // Get a random element from the provided list
        public static T Choice<T>(IList<T> list)
        {
            return list[random.Next(0, list.Count)];
        }
    }
}
