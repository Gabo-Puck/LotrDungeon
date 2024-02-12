using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Models.AlterEntities
{
    public class Armor : BaseDefense
    {
        public override (Stats attackerState, Stats defenderState) AlterState(BaseEntity attacker, BaseEntity defender)
        {
            throw new NotImplementedException();
        }
    }
}