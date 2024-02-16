using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.AlterEntities
{
    public abstract class BaseAccesory : AlterState
    {
        int quantity {get;set;} = 1;
        bool consumible {get;set;}

        protected BaseAccesory(string _Name,int _quantity, bool _consumible) : base(_Name)
        {
            quantity = _quantity;
            consumible = _consumible;
        }

        protected BaseAccesory(string _Name, bool _consumible, List<BaseAccesory> _d) : base(_Name)
        {
            consumible = _consumible;
        }

        public void Consume(){
            if(!consumible && quantity<=0) return;
            quantity--;
            
        }
        
        public bool CheckIfAny() => quantity > 0;
    }
}