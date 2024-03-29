using System.Linq;
using LotrDungeon.AlterEntities;
using LotrDungeon.Exceptions;
using LotrDungeon.Models.AlterEntities;

namespace LotrDungeon.Models.Entities
{
    public abstract class BaseEntity : AlterState
    {
        public Stats State {get;set;} = new Stats();
        public Stats TmpState {get;set;} = new Stats();
        public int AttackPower {get;}
        public string Name {get;}
        public bool IsDead {get; private set;} = false;
        public virtual int BASE_STAMINA_ATTACK {get;} = 3;
        public virtual int BASE_HEALTH {get;} = 100;
        public virtual int BASE_STAMINA {get;} = 100;
        public List<BaseAccesory> Accesories {get;set;}
        public List<BaseWeapon> Weapons {get;set;}
        public List<BaseDefense> Defense {get;set;}
        public List<AlterState> alterStates {get;set;} = new List<AlterState>();
        public BaseEntity(
            string _Name,
            int _Attack,
            int _Stamina,
            int _Health,
            int _DefenseStat,
            List<BaseAccesory> _Accesories,
            List<BaseWeapon> _Weapons,
            List<BaseDefense> _Defense
            ) : base(_Name){
            Name = _Name;
            AttackPower = _Attack;
            State.Defense = _DefenseStat;
            State.Stamina = _Stamina;
            State.Health = _Health;
            Accesories =_Accesories;
            Weapons =_Weapons;
            Defense = _Defense;
        }
        public BaseEntity(
            string _Name,
            Stats basicStats,
            List<BaseAccesory> _Accesories,
            List<BaseWeapon> _Weapons,
            List<BaseDefense> _Defense
            ) : base(_Name){
            Name = _Name;
            AttackPower = basicStats.Damage;
            State.Defense = basicStats.Defense;
            State.Stamina = basicStats.Stamina;
            State.Health = basicStats.Health;
            Accesories =_Accesories;
            Weapons =_Weapons;
            Defense = _Defense;
        }

        public virtual void attack(BaseWeapon weapon, BaseEntity enemy){
            alterStates.RemoveAll(_=>true);
            alterStates.Add(weapon);

            (var ourState, var enemyState) = calculateState(enemy);
            ourState.Stamina -= BASE_STAMINA_ATTACK;
            enemyState.Damage += AttackPower + TmpState.DamageBonus;
            if(!CheckIfCanAttack(ourState)) {
                throw new TurnException($"{Name} is too tired to attack");
            };
            if(State.IsStun) {
                State.IsStun = false;
                throw new TurnException($"{Name} is stunned!");
            };

            ApplyState(ourState);
            enemy.ApplyState(enemyState);
            TmpState = new Stats();

        }

        public virtual void defend(BaseDefense defense, BaseEntity enemy){
            alterStates.RemoveAll(_=>true);
            alterStates.Add(defense);
            (var ourState, var enemyState) = calculateState(enemy);
            getRested(BASE_STAMINA_ATTACK);
            TmpState = ourState;
        }

        public virtual void consume(BaseAccesory accesory, BaseEntity enemy){
            alterStates.RemoveAll(_=>true);
            alterStates.Add(accesory);
            (var ourState, var enemyState) = calculateState(enemy);
            ApplyState(ourState);
            accesory.Consume();
            if(!accesory.CheckIfAny()) Accesories.Remove(accesory);

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
            var enemyState = new Stats();
            foreach (var item in alterStates)
            {
                (var attacker, var defender) = item.AlterEntityState(this, enemy);
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
            recieveDefenseDebuff(state.DefenseDebuff);
            recieveDamage(state.Damage);
            recieveStamina(state.Stamina);
            recieveHealth(state.Health);
            recieveDamageBonus(state.DamageBonus);
            State.IsStun = state.IsStun;
            PrintStats();
        }

        public override string ToString()
        {
            string stat = "";
            if(IsDead)
                stat += $@"{Name} is already dead!";
            stat += @$"
                {base.ToString()}
                {State}";
            return stat;
        }

        public string PrintItems(){
            string itemsAccesories = PrintItemList(Accesories);
            string itemsWeapons = PrintItemList(Weapons);
            string itemsDefense = PrintItemList(Defense);
            var item = @$"
                Items
            {itemsWeapons}
            {itemsDefense}
            {itemsAccesories}
            ";
            return item;
        }

        string PrintItemList(IEnumerable<AlterState> alterStates){
            string itemsAccesories = $@"";
            foreach (var item in alterStates)
            {
                itemsAccesories += $@"{item}";
            }
            return itemsAccesories;
        }

        protected virtual void recieveDamageBonus(int _damageBonus){
            TmpState.DamageBonus = _damageBonus;
        }
        protected virtual void recieveDefenseDebuff(int _defenceDebuff){
            TmpState.DefenseDebuff = _defenceDebuff;
        }
        protected virtual void recieveHealth(float _health){
            float health = State.Health + _health;
            if(health <= 0)
                State.Stamina = 0;
            if(health >= BASE_HEALTH)
                State.Health = BASE_HEALTH;
            else
                State.Health = health;
            
        }

        int calcDefense()=>
            TmpState.DefenseDebuff>State.Defense ? 0 : State.Defense - TmpState.DefenseDebuff;
        
        protected virtual void recieveStamina(int _stamina){
            int stamina = State.Stamina + _stamina;
            if(stamina <= 0)
                State.Stamina = 0;
            if(stamina >= BASE_STAMINA)
                State.Stamina = BASE_STAMINA;
            else
                State.Stamina = stamina;
            
        }
        protected virtual float recieveDamage(int damage){
            float _damage = damage;
            if(this.TmpState.DamageMitigation > 0 && this.TmpState.DamageMitigation < 1)
            {
                _damage = (float)damage*TmpState.DamageMitigation;
            }else if(this.TmpState.DamageMitigation >= 1)
            {
                _damage = damage - this.TmpState.DamageMitigation;
            }   
            _damage -= calcDefense();
            if(_damage < 0 ) _damage = 0;

            if(_damage>=State.Health){
                State.Health = 0;
                IsDead = true;
            }else
                State.Health -= _damage;

            return State.Health;
        }
        public override (Stats attackerState, Stats defenderState) AlterEntityState(BaseEntity attacker, BaseEntity defender)
        {
            throw new NotImplementedException();
        }
        
    }
    
}