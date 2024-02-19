using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotrDungeon.Menu
{
    public class OptionList<T>
    {
        private IEnumerable<BaseMenuOption<T>> menuOptions;

        public OptionList(IEnumerable<BaseMenuOption<T>> _menuOptions){
            menuOptions = _menuOptions;
        }

        public void PrintMenuOption(int index, BaseMenuOption<T> option){
            Console.WriteLine($@"{index}) {option.Text}");
        }
        public void PrintMenuOptions(){
            int index = 0;
            foreach (var item in menuOptions)
            {
                PrintMenuOption(index, item);
            }
        }
        public virtual BaseMenuOption<T> GetElement(int index){
            return menuOptions.ElementAt(index);
        }
    }
    
}