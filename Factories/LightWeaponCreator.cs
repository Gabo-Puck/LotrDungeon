using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class LightWeaponCreator : Creator<LightWeapon>
    {
        protected override int MAX_STAMINA => 20;
        protected override int MIN_STAMINA => 13;
        protected override int MAX_ATTACK => 15;
        protected override int MIN_ATTACK => 10;
        protected override List<string> Names {get;} = new(){
            "Knife",
            "Dagger",
            "Dawnbreaker",
            "Fists",
        };

        public override LightWeapon FactoryMethod()
        {
            Stats basicStats = GetBasicStats();
            return new LightWeapon(PickName(), basicStats.Damage, basicStats.Stamina);
        }
    }
}