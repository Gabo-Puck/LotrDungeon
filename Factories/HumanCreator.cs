using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class HumanCreator : Creator<Human>
    {
        protected override List<string> Names {get;} = new(){
            "Boromir",
            "Faramir",
            "Aragorn",
            "Gabo"
        };
        public override Human FactoryMethod()
        {

            var basicStats = GetBasicStats();
            EntitiesFactory<BaseWeapon> heavyFactory = new();

            BaseWeapon weapon = heavyFactory.GenerateEntity();
            EntitiesFactory<BaseDefense> baseFactory = new();

            BaseDefense defense = baseFactory.GenerateEntity();
            return new Human(PickName(),basicStats,[],[weapon],[defense]);
        }
    }
}