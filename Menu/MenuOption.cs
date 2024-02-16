using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Menu
{
    public class MenuOption
    {
        public string Text {get;set;} = String.Empty;
        public Func<BaseEntity,object> Func {get;set;}
        public MenuOption(string _Text,Func<BaseEntity,object> _Func){
            Text = _Text;
            Func = _Func;
        }
    }
}