using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    /// <summary>
    /// Creator class to create any class of "AlterState"
    /// This class mostly was developed to support Weapons/Defense/Accesories
    /// But AlterState also could be an BaseEntity due to hierarchy, so probably i'm 
    /// going to refactor all existing BaseCreator stuff to use this class to tie all logic
    /// into one class
    /// </summary>
    /// <typeparam name="T">Class that "FactoryMethod" is going to return</typeparam>
    public abstract class BaseAlterStateCreator<T> : Creator<T> where T : AlterState
    {
        protected abstract IEnumerable<string> Names {get;}
        protected int MAX_HEALTH = 100;
        protected int MAX_STAMINA = 100;
        protected int MAX_ATTACK = 20;
        protected int MAX_DEFENSE = 10;
        protected int MIN_HEALTH = 50;
        protected int MIN_STAMINA = 50;
        protected int MIN_ATTACK = 2;
        protected int MIN_DEFENSE = 5;
        public abstract override T FactoryMethod();
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