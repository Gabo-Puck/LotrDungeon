using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.AlterEntities
{
    public interface IAlterState
    {
        public (Stats attackerState, Stats defenderState) AlterState(BaseEntity attacker, BaseEntity defender);
    }
}