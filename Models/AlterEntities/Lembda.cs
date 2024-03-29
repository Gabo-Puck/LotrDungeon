using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Models.AlterEntities
{
    public class Lembda : BaseAccesory
    {
        public override string Classifier => "Lembda";
        int BASE_HEALTH_BONUS = 5;
        public Lembda(string _Name, int _quantity, bool _consumible) : base(_Name, _quantity, _consumible)
        {
        }

        public override (Stats attackerState, Stats defenderState) AlterEntityState(BaseEntity attacker, BaseEntity defender)
        {
            Console.WriteLine("Eating lembda!!!");
            //calc attacker stats
            var attackerState = new Stats();
            attackerState.Health += BASE_HEALTH_BONUS;

            //cal defender stats
            var defenderState = new Stats();
            
            
            return (attackerState,defenderState);
        }

        public override string ToString()
        {
            return @$"{base.ToString()}
                Health Points: {BASE_HEALTH_BONUS}";
        }
    }
}