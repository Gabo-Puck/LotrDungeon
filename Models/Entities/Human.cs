using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.AlterEntities;

namespace LotrDungeon.Models.Entities
{
    public class Human : BaseEntity
    {
        public Human(string _Name, int _Attack, int _Stamina, int _Health, int _DefenseStat, List<BaseAccesory> _Accesories, List<BaseWeapon> _Weapons, List<BaseDefense> _Defense) : base(_Name, _Attack, _Stamina, _Health, _DefenseStat, _Accesories, _Weapons, _Defense)
        {
        }

        int luck {get;set;} = 3;
        public override int BASE_STAMINA_ATTACK => 3;
        public int CRITICAL_ATTACK_DAMAGE => 2;

        public void attack(BaseEntity enemy)
        {
            alterStates.Add(Weapons[0]);

            (var ourState, var enemyState) = calculateState(enemy);

            if(!CheckIfCanAttack(ourState)) throw new Exception($"{Name} is too tired to attack");

            enemyState.Damage  += isCriticalAttack() ? CRITICAL_ATTACK_DAMAGE : 0;
            
            enemy.ApplyState(enemyState);
            ApplyState(ourState);

            alterStates.RemoveAll(_=>true);
        }

        public bool isCriticalAttack(){
            bool isCritical = Randomize.IsRNG(luck, 3);
            if(isCritical)
                Console.WriteLine($"{Name} had a lucky strike");
            return isCritical;
        }

        
    }
}