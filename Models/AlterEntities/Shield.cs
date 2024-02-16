using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Models.AlterEntities
{
    public class Shield : BaseDefense
    {
        public Shield(string _Name, float _BASE_DEFENSE) : base(_Name, _BASE_DEFENSE)
        {
        }

        public override (Stats attackerState, Stats defenderState) AlterEntityState(BaseEntity attacker, BaseEntity defender)
        {
            Console.WriteLine("Shield defense!!!");
            //calc attacker stats
            var attackerState = new Stats();
            attackerState.DamageMitigation += (int)BASE_DEFENSE;

            //cal defender stats
            var defenderState = new Stats();
            
            
            return (attackerState,defenderState);
        }
    }
}