using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Models.AlterEntities
{
    public class LightWeapon : BaseWeapon
    {
        int BASE_CRITIC_CHANCE = 3;
        int BASE_CRITIC_DAMAGE = 3;
        int CONSECUTIVE_ATTACKS = 2;

        public LightWeapon(int _PowerAttack, int _StaminaUsage) : base(_PowerAttack, _StaminaUsage)
        {
        }

        public override (Stats attackerState, Stats defenderState) AlterEntityState(BaseEntity attacker, BaseEntity defender)
        {
            Console.WriteLine("Knife attack!!!");
            //calc attacker stats
            var attackerState = new Stats();
            attackerState.Stamina -= StaminaUsage;

            //cal defender stats
            var defenderState = new Stats();
            int damage = 0;
            for (int i = 0; i < CONSECUTIVE_ATTACKS; i++)
            {
                damage += IsCriticAttack() ? PowerAttack + BASE_CRITIC_DAMAGE : PowerAttack;   
            }
            
            defenderState.Damage += damage;
            
            return (attackerState,defenderState);
        }
        bool IsCriticAttack() => Randomize.IsRNG(BASE_CRITIC_CHANCE,BASE_CRITIC_CHANCE+4);

    }
}