using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class ShieldCreator : Creator<Shield>
    {
        protected override int MAX_DEFENSE => 15;
        protected override int MIN_DEFENSE => 7;
        protected override List<string> Names {get;} = new(){
            "Boromir's Shield",
            "Oakwood shield",
            "Orc's corpse"
        };

        public override Shield FactoryMethod()
        {
            Stats basicStats = GetBasicStats();
            return new Shield(PickName(), basicStats.Defense);
        }
    }
}