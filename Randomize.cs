using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotrDungeon
{
    public class Randomize
    {
        public static bool IsRNG(int seed, int gap) => new Random().Next(0,seed + gap) > seed;
    }
}