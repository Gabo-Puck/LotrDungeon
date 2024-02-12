using LotrDungeon.AlterEntities;
using LotrDungeon.Models.AlterEntities;

namespace LotrDungeon.Models.Entities
{
    public abstract class BaseEntity
    {
        public Stats State {get;set;} = new Stats();
        public int AttackPower {get;}
        public string Name {get;}
        public bool IsDead {get; private set;}
        public virtual int BASE_STAMINA_ATTACK {get;} = 3;
        public virtual int BASE_HEALTH {get;} = 100;
        public virtual int BASE_STAMINA {get;} = 100;
        public List<BaseAccesory> Accesories {get;set;}
        public List<BaseWeapon> Weapons {get;set;}
        public List<BaseDefense> Defense {get;set;}
        public List<IAlterState> alterStates {get;set;} = new List<IAlterState>();
        public BaseEntity(
            string _Name,
            int _Attack,
            int _Stamina,
            int _Health,
            int _DefenseStat,
            List<BaseAccesory> _Accesories,
            List<BaseWeapon> _Weapons,
            List<BaseDefense> _Defense
            ){
            Name = _Name;
            AttackPower = _Attack;
            State.Defense = _DefenseStat;
            State.Stamina = _Stamina;
            State.Health = _Health;
            Accesories =_Accesories;
            Weapons =_Weapons;
            Defense = _Defense;
        }

        public virtual void attack(BaseEntity enemy){
            alterStates.Add(Weapons[0]);

            (var ourState, var enemyState) = calculateState(enemy);

            if(!CheckIfCanAttack(ourState)) throw new Exception($"{Name} is too tired to attack");

            enemy.ApplyState(enemyState);
            ApplyState(ourState);
        }

        public virtual void defend(){
            getRested(BASE_STAMINA_ATTACK);
        }

        public virtual bool CheckIfCanAttack(Stats stats){
            int Cost = 0;
            if(stats.Stamina<=0){
                Cost += Math.Abs(stats.Stamina);
            }else{
                Cost -= stats.Stamina;
            }
            return State.Stamina > Cost;
        }

        protected (Stats ourState, Stats enemyState) calculateState(BaseEntity enemy){
            var ourState = new Stats();
            ourState.Stamina -= BASE_STAMINA_ATTACK;
            var enemyState = new Stats();
            enemyState.Damage += AttackPower;
            foreach (var item in alterStates)
            {
                (var attacker, var defender) = item.AlterState(this, enemy);
                ourState.sum(attacker);
                enemyState.sum(defender);
            }
            return (ourState,enemyState);
        }

        public virtual void getRested(int stamina){
            if(State.Stamina < BASE_STAMINA)
                State.Stamina += stamina;
            
        }

        public void ApplyState(Stats state){
            recieveDamage(state.Damage);
            recieveStamina(state.Stamina);
            State.IsStun = state.IsStun;
            printStats();
        }
        protected virtual void recieveStamina(int _stamina){
            int stamina = State.Stamina + _stamina;
            if(stamina <= 0)
                State.Stamina = 0;
            if(stamina >= BASE_STAMINA)
                State.Stamina -= BASE_STAMINA;
            State.Stamina = stamina;
            
        }
        protected virtual int recieveDamage(int damage){
            if(damage>State.Health){
                State.Health = 0;
                IsDead = true;
            }else
                State.Health -= damage;
            return State.Health;
        }
        public virtual void printStats(){
            if(IsDead)
                Console.WriteLine($@"{Name} is already dead!");

            Console.WriteLine(@$"
                {Name}
                {State}
            ");
        }
    }
}