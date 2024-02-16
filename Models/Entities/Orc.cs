using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Exceptions;
using LotrDungeon.Models.AlterEntities;

namespace LotrDungeon.Models.Entities
{
    public class Orc : BaseEntity
    {
        int anger {get;set;} = 0;
        int MAX_ANGER = 5;

        public Orc(string _Name, int _Attack, int _Stamina, int _Health, int _DefenseStat, List<BaseAccesory> _Accesories, List<BaseWeapon> _Weapons, List<BaseDefense> _Defense) : base(_Name, _Attack, _Stamina, _Health, _DefenseStat, _Accesories, _Weapons, _Defense)
        {
        }

        public override int BASE_STAMINA_ATTACK => 10;

        public override void attack(BaseWeapon baseWeapon, BaseEntity enemy)
        {
            alterStates = new(){baseWeapon};

            (var ourState, var enemyState) = calculateState(enemy);
            
            ourState.Stamina -= BASE_STAMINA_ATTACK;
            enemyState.Damage += AttackPower + TmpState.DamageBonus + anger;

            if(!CheckIfCanAttack(ourState)) throw new Exception($"{Name} is too tired to attack");
            if(State.IsStun) {
                State.IsStun = false;
                throw new TurnException($"{Name} is stunned!");
            };

            enemy.ApplyState(enemyState);
            ApplyState(ourState);

            if(enemy.State.Health==0) setAnger(anger+1);

        }

        public void setAnger(int _anger){
            if(anger >= MAX_ANGER)
                anger = MAX_ANGER;
            anger = _anger;
        }

    }
}