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
        public override abstract (Stats attackerState, Stats defenderState) AlterEntityState(BaseEntity attacker, BaseEntity defender);
    }
}