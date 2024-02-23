using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class OrcCreator : BaseEntityCreator
    {
        protected override List<string> Names {get;} = new (){
            "Boldog",
            "Azog",
            "Gorgun"
        };

        public override Orc FactoryMethod()
        {
            var basicStats = GetBasicStats();
            return new Orc(PickName(),basicStats,[],[],[]);
        }
    }
}