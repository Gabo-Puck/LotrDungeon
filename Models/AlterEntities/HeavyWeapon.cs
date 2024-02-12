using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Models.AlterEntities
{
    public class HeavyWeapon : BaseWeapon
    {
        int BASE_STUN_CHANCE = 3;

        public HeavyWeapon(int _PowerAttack, int _StaminaUsage) : base(_PowerAttack, _StaminaUsage)
        {
        }

        public override (Stats attackerState, Stats defenderState) AlterState(BaseEntity attacker, BaseEntity defender)
        {
            Console.WriteLine("Maze attack!!!");
            //calc attacker stats
            var attackerState = new Stats();
            attackerState.Stamina -= StaminaUsage;

            //cal defender stats
            var defenderState = new Stats();
            defenderState.Damage += PowerAttack;
            defenderState.IsStun = IsPowerfulEnough();
            return (attackerState,defenderState);
        }

        bool IsPowerfulEnough() => Randomize.IsRNG(BASE_STUN_CHANCE,BASE_STUN_CHANCE+4);
    }
}