using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class HumanCreator : BaseEntityCreator
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
            EntitiesFactory<BaseAlterStateCreator<BaseWeapon>,BaseWeapon> factory = new();
            var x = factory.GenerateEntity();
            return new Human(PickName(),basicStats,[],[],[]);
        }
    }
}