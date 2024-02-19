using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class OrcCreator : Creator<BaseEntity>
    {
        public override Orc FactoryMethod()
        {
            return new Orc("Test",10,10,10,10,[],[],[]);
        }
    }
}