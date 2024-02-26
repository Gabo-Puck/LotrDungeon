using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class RumCreator : Creator<Rum>
    {
        protected override List<string> Names {get;} = new(){
            "Old wine",
            "Rum",
        };

        public override Rum FactoryMethod()
        {
            int Quantity = RandomNumberGenerator.GetInt32(1, 6);
            return new Rum(PickName(), Quantity, true);
        }
    }
}