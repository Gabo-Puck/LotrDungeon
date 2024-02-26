using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.AlterEntities
{
    public abstract class AlterState
    {
        protected int TURNS_TO_ACTIVATE {get;set;}= 0;
        protected int TURNS_ACTIVE {get;set;} = 1;
        public abstract (Stats attackerState, Stats defenderState) AlterEntityState(BaseEntity attacker, BaseEntity defender);
        public string Name {get;set;} = String.Empty;
        public virtual string Classifier {get;} = String.Empty;
        protected bool CheckIfCanUse()=> TURNS_TO_ACTIVATE == 0 && TURNS_ACTIVE >=1;
        protected void ConsumeTurn(){
            if(TURNS_ACTIVE > 0 && TURNS_TO_ACTIVATE == 0) TURNS_ACTIVE--;
            if(TURNS_TO_ACTIVATE > 0) TURNS_TO_ACTIVATE--;
        }

        public AlterState(string _Name){
            Name = _Name;
        }

        public virtual void PrintStats(){
            Console.WriteLine(this);
        }
        
        public override string ToString()
        {
            return @$"
                {Name}";
        }
    }
}