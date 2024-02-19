using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotrDungeon.Factories
{
    public abstract class Creator<T>
    {
        public int x {get;set;}
        public abstract T FactoryMethod();

    }
}