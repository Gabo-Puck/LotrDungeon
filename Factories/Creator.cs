using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotrDungeon.Factories
{
    public abstract class Creator<T>
    {
        public abstract T FactoryMethod();

    }
}