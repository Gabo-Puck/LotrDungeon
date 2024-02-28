using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotrDungeon
{
    public class Randomize
    {
        private static Random random = new Random();
        public static bool IsRNG(double prob) => random.NextDouble() < prob; // Comprobar si está fuera del rango y si cumple con la probabilidad
    }
}