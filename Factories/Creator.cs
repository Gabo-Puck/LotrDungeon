using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;

namespace LotrDungeon.Factories
{
    public abstract class Creator<T> : Creator where T : AlterState
    {
        public override abstract AlterState FactoryMethod();

    }
    public abstract class Creator{

        protected abstract IEnumerable<string> Names {get;}
        protected virtual int MAX_HEALTH => 100;
        protected virtual int MAX_STAMINA => 100;
        protected virtual int MAX_ATTACK => 20;
        protected virtual int MAX_DEFENSE => 10;
        protected virtual int MIN_HEALTH => 50;
        protected virtual int MIN_STAMINA => 50;
        protected virtual int MIN_ATTACK => 2;
        protected virtual int MIN_DEFENSE => 5;
        public abstract AlterState FactoryMethod();
        
        protected string PickName(){
            int index = RandomNumberGenerator.GetInt32(Names.Count());
            return Names.ElementAt(index);
        }

        protected Stats GetBasicStats(){
             Stats basicStats = new();
             basicStats.Health = RandomNumberGenerator.GetInt32(MIN_HEALTH, MAX_HEALTH);
             basicStats.Defense = RandomNumberGenerator.GetInt32(MIN_DEFENSE, MAX_DEFENSE);
             basicStats.Stamina = RandomNumberGenerator.GetInt32(MIN_STAMINA, MAX_STAMINA);
             basicStats.Damage = RandomNumberGenerator.GetInt32(MIN_ATTACK, MAX_ATTACK);
             return basicStats;
        }

    }
}