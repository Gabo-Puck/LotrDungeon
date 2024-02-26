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
    public class LembdaCreator : Creator<Lembda>
    {
        protected override List<string> Names {get;} = new(){
            "Lembda",
            "Potatoes",
        };

        public override Lembda FactoryMethod()
        {
            int Quantity = RandomNumberGenerator.GetInt32(1, 6);
            return new Lembda(PickName(), Quantity, true);
        }
    }
}