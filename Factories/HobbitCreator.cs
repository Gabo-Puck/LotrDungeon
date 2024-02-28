using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class HobbitCreator : Creator<Hobbit>
    {
        protected override List<string> Names {get;} = new (){
            "Frodo",
            "Sam",
            "Bilbo",
            "Merry",
            "Pippin"
        };

        public override Hobbit FactoryMethod()
        {
            
            var basicStats = GetBasicStats();
            EntitiesFactory<HeavyWeapon> heavyFactory = new();
            List<BaseWeapon> weapon = new();
            heavyFactory.GenerateEntities(2).ToList().ForEach(weapon.Add);
            EntitiesFactory<BaseDefense> baseFactory = new();
            List<BaseDefense> defense = new();
            baseFactory.GenerateEntities(2).ToList().ForEach(defense.Add);
            return new Hobbit(PickName(),basicStats,[],weapon,defense);
        }
    }
}