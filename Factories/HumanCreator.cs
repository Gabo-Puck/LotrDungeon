using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class HumanCreator : Creator<BaseEntity>
    {
        public override Human FactoryMethod()
        {
            return new Human("Test",10,10,10,10,[],[],[]);
        }
    }
}