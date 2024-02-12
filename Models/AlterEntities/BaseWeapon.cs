using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Models.AlterEntities
{
    public abstract class BaseWeapon : IAlterState
    {
        protected int PowerAttack {get;set;}
        protected int StaminaUsage {get;set;}
        public abstract (Stats attackerState, Stats defenderState) AlterState(BaseEntity attacker, BaseEntity defender);

        public BaseWeapon(int _PowerAttack, int _StaminaUsage){
            PowerAttack = _PowerAttack;
            StaminaUsage = _StaminaUsage;
        }
    }
}