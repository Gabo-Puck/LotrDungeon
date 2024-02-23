using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class HeavyWeaponCreator : BaseAlterStateCreator<BaseWeapon>
    {
        protected override List<string> Names {get;} = new(){
            "Mace",
            "Axe",
        };

        public override HeavyWeapon FactoryMethod()
        {
            return new HeavyWeapon(PickName(),10,10);
        }
    }
}