using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.AlterEntities
{
    public abstract class BaseAccesory : IAlterState
    {
        public List<BaseAccesory> d;
        int quantity {get;set;} = 1;
        bool consumible {get;set;}

        protected BaseAccesory(int _quantity, bool _consumible, List<BaseAccesory> _d)
        {
            d = _d;
            quantity = _quantity;
            consumible = _consumible;
        }

        protected BaseAccesory(bool _consumible, List<BaseAccesory> _d)
        {
            d = _d;
            consumible = _consumible;
        }

        protected void Consume(BaseEntity attacker, BaseEntity defender){
            if(!consumible && quantity<=0) return;
            quantity--;
            (Stats attackerState, Stats defenderState) = AlterState(attacker, defender);
            attacker.ApplyState(attackerState);
            defender.ApplyState(defenderState);
            if(quantity==0)
                d.Remove(this);
        }

        public abstract (Stats attackerState, Stats defenderState) AlterState(BaseEntity attacker, BaseEntity defender);
        
    }
}