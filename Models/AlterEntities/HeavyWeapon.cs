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
        public override string Classifier => "Heavy Weapon";
        int BASE_STUN_CHANCE = 3;

        public HeavyWeapon(string _Name, int _PowerAttack, int _StaminaUsage) : base(_Name, _PowerAttack, _StaminaUsage)
        {
        }

        public override (Stats attackerState, Stats defenderState) AlterEntityState(BaseEntity attacker, BaseEntity defender)
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

        
        public override string ToString()
        {
            return @$"{base.ToString()}
                Stun change: {BASE_STUN_CHANCE}";
        }
    }
}