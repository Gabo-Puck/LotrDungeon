using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Exceptions;
using LotrDungeon.Models.AlterEntities;

namespace LotrDungeon.Models.Entities
{
    public class Elve : BaseEntity
    {
        public override string Classifier => "Elve";
        public Elve(string _Name, int _Attack, int _Stamina, int _Health, int _DefenseStat, List<BaseAccesory> _Accesories, List<BaseWeapon> _Weapons, List<BaseDefense> _Defense) : base(_Name, _Attack, _Stamina, _Health, _DefenseStat, _Accesories, _Weapons, _Defense)
        {
        }
        public Elve(string _Name, Stats basicStats, List<BaseAccesory> _Accesories, List<BaseWeapon> _Weapons, List<BaseDefense> _Defense) : base(_Name, basicStats, _Accesories, _Weapons, _Defense)
        {
        }

        int luck {get;set;} = 3;
        public override int BASE_STAMINA_ATTACK => 3;
        public int CRITICAL_ATTACK_DAMAGE => 2;

        public override void attack(BaseWeapon baseWeapon, BaseEntity enemy)
        {
            alterStates = new(){baseWeapon};

            (var ourState, var enemyState) = calculateState(enemy);
            ourState.Stamina -= BASE_STAMINA_ATTACK;
            enemyState.Damage += AttackPower + TmpState.DamageBonus;

            if(!CheckIfCanAttack(ourState)) throw new TurnException($"{Name} is too tired to attack");
            if(State.IsStun) {
                State.IsStun = false;
                throw new TurnException($"{Name} is stunned!");
            };
            enemyState.Damage  += isCriticalAttack() ? CRITICAL_ATTACK_DAMAGE : 0;
            
            enemy.ApplyState(enemyState);
            ApplyState(ourState);

            TmpState = new Stats();
        }

        public bool isCriticalAttack(){
            bool isCritical = Randomize.IsRNG(luck, 3);
            if(isCritical)
                Console.WriteLine($"{Name} had a lucky strike");
            return isCritical;
        }

        
    }
}