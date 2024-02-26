using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class HeavyWeaponCreator : Creator<HeavyWeapon>
    {
        protected override int MAX_STAMINA => 20;
        protected override int MIN_STAMINA => 13;
        protected override int MAX_ATTACK => 30;
        protected override int MIN_ATTACK => 20;
        protected override List<string> Names {get;} = new(){
            "Mace",
            "Axe",
        };

        public override HeavyWeapon FactoryMethod()
        {
            Stats basicStats = GetBasicStats();
            return new HeavyWeapon(PickName(), basicStats.Damage, basicStats.Stamina);
        }
    }
}