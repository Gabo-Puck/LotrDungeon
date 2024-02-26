using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class ArmorCreator : Creator<Armor>
    {
        protected override int MAX_DEFENSE => 100;
        protected override int MIN_DEFENSE => 0;
        protected override List<string> Names {get;} = new(){
            "Mithril Chainmail",
            "Gondor Chestplate",
        };

        public override Armor FactoryMethod()
        {
            Stats basicStats = GetBasicStats();
            return new Armor(PickName(), ((float)basicStats.Defense)/100f);
        }
    }
}