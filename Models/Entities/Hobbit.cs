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
    public class Hobbit : BaseEntity
    {
        public override string Classifier => "Hobbit";
        public Hobbit(string _Name, int _Attack, int _Stamina, int _Health, int _DefenseStat, List<BaseAccesory> _Accesories, List<BaseWeapon> _Weapons, List<BaseDefense> _Defense) : base(_Name, _Attack, _Stamina, _Health, _DefenseStat, _Accesories, _Weapons, _Defense)
        {
        }
        public Hobbit(string _Name, Stats basicStats, List<BaseAccesory> _Accesories, List<BaseWeapon> _Weapons, List<BaseDefense> _Defense) : base(_Name, basicStats, _Accesories, _Weapons, _Defense)
        {
        }

        double luck {get;set;} = 0.2;
        public override int BASE_STAMINA_ATTACK => 10;

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
            
            enemy.ApplyState(enemyState);
            ApplyState(ourState);

            TmpState = new Stats();
        }
        protected override float recieveDamage(int damage)
        {
            if(damage >0 && isAttackDodged()){
                return State.Health;    
            }
            return base.recieveDamage(damage);
        }
        public override void defend(BaseDefense defense, BaseEntity enemy)
        {
            alterStates.RemoveAll(_=>true);
            alterStates.Add(defense);
            (var ourState, var enemyState) = calculateState(enemy);
            getRested(BASE_STAMINA_ATTACK);
            TmpState = ourState;
        }
        

        public bool isAttackDodged(){
            bool isCritical = Randomize.IsRNG(luck);
            if(isCritical)
                Console.WriteLine($"{Name} miraculously sliped on his feet and dodged the attack!");
            return isCritical;
        }

        
    }
}