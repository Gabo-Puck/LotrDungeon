namespace LotrDungeon.AlterEntities
{
    public class Stats
    {
        public int Stamina {get;set;}
        public float Health {get;set;}
        public int Defense {get;set;}
        public int Damage {get;set;}
        public bool IsStun {get;set;}
        public float DamageMitigation {get;set;}
        public int DamageBonus {get;set;}
        public int DefenseDebuff {get;set;}
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
            DamageMitigation += stats.DamageMitigation;
            DefenseDebuff += stats.DefenseDebuff;
            DamageBonus += stats.DamageBonus;
            IsStun = stats.IsStun;
        }

    }
}