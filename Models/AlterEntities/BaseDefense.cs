using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Models.AlterEntities
{
    public abstract class BaseDefense : AlterState
    {
        protected float BASE_DEFENSE = 0.5f;
        public BaseDefense(string _Name, float _BASE_DEFENSE): base(_Name){
            BASE_DEFENSE = _BASE_DEFENSE;
        }
        public override abstract (Stats attackerState, Stats defenderState) AlterEntityState(BaseEntity attacker, BaseEntity defender);
    }
}