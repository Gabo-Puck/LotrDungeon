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
        public override string Classifier => "Light Weapon";

        double BASE_CRITIC_CHANCE = 0.2;
        int BASE_CRITIC_DAMAGE = 3;
        int CONSECUTIVE_ATTACKS = 2;

        public LightWeapon(string _Name, int _PowerAttack, int _StaminaUsage) : base(_Name, _PowerAttack, _StaminaUsage)
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
        bool IsCriticAttack() => Randomize.IsRNG(BASE_CRITIC_CHANCE);


        public override string ToString()
        {
            return @$"{base.ToString()}
                Critic chance: {BASE_CRITIC_CHANCE}
                Critic damage: {BASE_CRITIC_DAMAGE}
                Consecutive attacks: {CONSECUTIVE_ATTACKS}";
        }
    }
}