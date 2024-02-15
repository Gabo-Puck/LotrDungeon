using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Models.AlterEntities
{
    public abstract class BaseWeapon : AlterState
    {
        protected int PowerAttack {get;set;}
        protected int StaminaUsage {get;set;}

        public BaseWeapon(int _PowerAttack, int _StaminaUsage){
            PowerAttack = _PowerAttack;
            StaminaUsage = _StaminaUsage;
        }
    }
}