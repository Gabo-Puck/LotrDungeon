using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class ElveCreator : Creator<Elve>
    {
        protected override List<string> Names {get;} = new (){
            "Amras",
            "Argon",
            "Carantir"
        };

        public override Elve FactoryMethod()
        {
            
            var basicStats = GetBasicStats();
            EntitiesFactory<HeavyWeapon> heavyFactory = new();
            List<BaseWeapon> weapon = new();
            heavyFactory.GenerateEntities(2).ToList().ForEach(weapon.Add);
            EntitiesFactory<Armor> baseFactory = new();
            List<BaseDefense> defense = new();
            baseFactory.GenerateEntities(2).ToList().ForEach(defense.Add);
            return new Elve(PickName(),basicStats,[],weapon,defense);
        }
    }
}