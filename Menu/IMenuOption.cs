using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotrDungeon.Menu
{
    public abstract class BaseMenuOption<T>
    {
        public string Text { get; set; } = String.Empty;
        public T? Value {get;set;} = default(T);
        public BaseMenuOption(){
            
        }
        public BaseMenuOption(string _Text, T? _Value){
            Text = _Text;
            Value = _Value;
        }
        public abstract BaseMenuOption<T> GoNext(int index=1);
        public abstract BaseMenuOption<T> DoProcess();
        public abstract void PrintOption();
    }
    public abstract class BaseListMenuOption<T> : BaseMenuOption<T>
    {
        public IEnumerable<BaseMenuOption<T>> menuOptions { get; set; }
        public BaseListMenuOption(string _Text, T? _Value, IEnumerable<BaseMenuOption<T>> options) : base(_Text, _Value)
        {
            menuOptions = options;
        }
        public BaseListMenuOption() : base()
        {
            menuOptions = new List<MenuOption<T>>();
        }
        public override void PrintOption()
        {
            int index = 0;
            foreach (var item in menuOptions)
            {
                index++;
                PrintMenuOption(index, item);
            }
        }
        public virtual bool IsValidOption(int index){
            return index>=0 && index<menuOptions.Count();
        }

        public virtual void PrintMenuOption(int index, BaseMenuOption<T> option){
            Console.WriteLine($@"{index}) {option.Text}");
        }
    }
}