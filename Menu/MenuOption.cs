using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Menu
{
    public class MenuOption<T> : BaseListMenuOption<T>
    {
        public IEnumerable<BaseMenuOption<T>> menuOptions { get; set; }
        public MenuOption(string _Text, T? _Value, IEnumerable<BaseMenuOption<T>> options) : base(_Text, _Value, options)
        {
            menuOptions = options;
        }
        public MenuOption() : base()
        {
            menuOptions = new List<MenuOption<T>>();
        }
        public override BaseMenuOption<T> GoNext(int index = 0)
        {
            if(!IsValidOption(index)) throw new Exception("Invalid option");
            return menuOptions.ElementAt(index);
        }
        bool IsValidOption(int index){
            return index>=0 && index<menuOptions.Count();
        }

        public void PrintMenuOption(int index, BaseMenuOption<T> option){
            Console.WriteLine($@"{index}) {option.Text}");
        }
        public override BaseMenuOption<T> DoProcess()
        {
            return this;
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
    }
    public class LeaveOption<T> : BaseMenuOption<T>
    {
        protected BaseMenuOption<T> menuOption{get;set;}
        public LeaveOption(string _Text, T? _Value, BaseMenuOption<T> _menuOption) : base(_Text, _Value)
        {
            menuOption = _menuOption;
        }

        public override BaseMenuOption<T> GoNext(int index){
            return menuOption;
        }

        public override BaseMenuOption<T> DoProcess()
        {
            return menuOption;
        }

        public override void PrintOption()
        {
            Console.WriteLine("");
        }
    }
    public class OnlyViewOption<T> : BaseListMenuOption<T>
    {
        protected BaseMenuOption<T> menuOption{get;set;}
        public OnlyViewOption(string _Text, T _Value, IEnumerable<BaseMenuOption<T>> _Options, BaseMenuOption<T> _menuOption):base(_Text,_Value, _Options){
            menuOption = _menuOption;
        }
        public override void PrintMenuOption(int i, BaseMenuOption<T> item)
        {
            Console.WriteLine($"{item.Text}");
        }
        public override BaseMenuOption<T> DoProcess()
        {
            PrintOption();
            return menuOption;
        }
        public override BaseMenuOption<T> GoNext(int index){
            return menuOption;
        }
    }
}