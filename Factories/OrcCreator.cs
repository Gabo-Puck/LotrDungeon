using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class OrcCreator : Creator<Orc>
    {
        protected override List<string> Names {get;} = new (){
            "Boldog",
            "Azog",
            "Gorgun"
        };

        public override Orc FactoryMethod()
        {
            
            var basicStats = GetBasicStats();
            EntitiesFactory<HeavyWeapon> heavyFactory = new();
            
            HeavyWeapon weapon = heavyFactory.GenerateEntity();
            EntitiesFactory<Armor> baseFactory = new();
            
            BaseDefense defense = baseFactory.GenerateEntity();
            return new Orc(PickName(),basicStats,[],[weapon],[defense]);
        }
    }
}