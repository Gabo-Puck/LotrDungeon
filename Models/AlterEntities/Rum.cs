using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Models.AlterEntities
{
    public class Rum : BaseAccesory
    {
        int BASE_DAMAGE_BONUS = 5;
        int BASE_SHIELD_BONUS = 5;
        public Rum(int _quantity, bool _consumible) : base(_quantity, _consumible)
        {
        }

        public override (Stats attackerState, Stats defenderState) AlterEntityState(BaseEntity attacker, BaseEntity defender)
        {
            Console.WriteLine("Getting drunk!!!");
            //calc attacker stats
            var attackerState = new Stats();
            attackerState.DamageBonus += BASE_DAMAGE_BONUS;
            attackerState.DefenseDebuff += BASE_SHIELD_BONUS;

            //cal defender stats
            var defenderState = new Stats();
            
            
            return (attackerState,defenderState);
        }
    }
}