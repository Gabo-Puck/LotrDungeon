using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Models.AlterEntities
{
    public class Armor : BaseDefense
    {
        public override (Stats attackerState, Stats defenderState) AlterEntityState(BaseEntity attacker, BaseEntity defender)
        {
            Console.WriteLine("Chainmail defense!!!");
            //calc attacker stats
            var attackerState = new Stats();
            attackerState.DamageMitigation += BASE_DEFENSE;

            //cal defender stats
            var defenderState = new Stats();
            
            
            return (attackerState,defenderState);
        }

    }
}