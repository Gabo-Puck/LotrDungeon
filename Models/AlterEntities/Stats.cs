namespace LotrDungeon.AlterEntities
{
    public class Stats
    {
        public int Stamina {get;set;}
        public int Health {get;set;}
        public int Defense {get;set;}
        public int Damage {get;set;}
        public bool IsStun {get;set;}
        public override string ToString()
        {
            return $@"
                Health: {Health}
                Stamina: {Stamina}
                Defense: {Defense}
                Stun: {IsStun}
            ";
        }
        public void sum(Stats stats){
            Stamina += stats.Stamina;
            Health += stats.Health;
            Defense += stats.Defense;
            Damage += stats.Damage;
            IsStun = stats.IsStun;
        }

    }
}