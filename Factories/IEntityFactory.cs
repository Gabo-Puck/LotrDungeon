using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotrDungeon.Factories
{
    public interface IEntityFactory<T>
    {
        public T GenerateEntity();
    }
}