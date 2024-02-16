using LotrDungeon.AlterEntities;
using LotrDungeon.Exceptions;
using LotrDungeon.Menu;
using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon
{
    static public class MenuHandler{
        static MenuOption Accesories = new("Pick an accesory", PickAccesory);
        static MenuOption Weapons = new("Pick a weapon", PickWeapon);
        static MenuOption Defense = new("Pick a defense", PickDefense);
        static int HandleInput(){
            try{
                string? input = Console.ReadLine();
                return Int32.Parse(input);
            }catch(Exception){
                Console.WriteLine("Invalid option");
                return -1;
            }
        }

        static bool IsValidOption(int index, List<MenuOption> options){
            return index>=0 && index<=options.Count;
        }

        static void PrintMenuOption(int index, MenuOption option){
            Console.WriteLine($@"{index}) {option.Text}");
        }
        static void PrintMenu(List<MenuOption> options){
            for (int i = 0; i < options.Count; i++)
            {
                PrintMenuOption(i+1,options[i]);
            }
        }
        static BaseWeapon PickWeapon(BaseEntity entity){
            List<MenuOption> options = new();
            entity.Weapons.ForEach(w=>
                options.Add(
                    new(w.Name,null)
                )
            );
            options.Add(new("Leave",null));
            int input = -1;
            do{
                PrintMenu(options);
                input = HandleInput();
            }while(!IsValidOption(input,options));
            if(input==options.Count) return null;
            return entity.Weapons[input - 1];
        }
        static object PickDefense(BaseEntity entity){
            List<MenuOption> options = new();
            entity.Defense.ForEach(w=>
                options.Add(
                    new(w.Name,null)
                )
            );
            options.Add(new("Leave",null));
            int input = -1;
            do{
                PrintMenu(options);
                input = HandleInput();
            }while(!IsValidOption(input,options));
            if(input==options.Count) return null;
            return entity.Defense[input - 1];
        }
        static object PickAccesory(BaseEntity entity){
            List<MenuOption> options = new();
            entity.Accesories.ForEach(w=>
                options.Add(
                    new(w.Name,null)
                )
            );
            options.Add(new("Leave",null));
            int input = -1;
            do{
                PrintMenu(options);
                input = HandleInput();
            }while(!IsValidOption(input,options));
            if(input==options.Count) return null;
            return entity.Accesories[input - 1];
        }
        public static object PickAction(BaseEntity entity)
        {
            List<MenuOption> options = new(){
                Weapons,
                Defense,
                Accesories,
            };
            int input = -1;
            object result = null;
            while(result == null){
                do{
                    PrintMenu(options);
                    input = HandleInput();
                }while(!IsValidOption(input,options));
                result = options[input - 1].Func(entity);
            }
            return result;
        }
    }
    public class Game
    {
        int STAMINA_THRESHOLD = 50;
        Human player = new Human("Boromir",12,100,100,10,
        new(){
            new Lembda("Lembda",10,true),
            new Rum("Rum",10,true)
        },
        new(){
            new HeavyWeapon("Mace",10,10),
            new LightWeapon("Knife",10,10)
            },
        new(){
            new Armor("Mithril Chainmail",0.2f),
            new Shield("Oak Shield",10)
        }
        );
        BaseEntity? CurrentEnemy = new Orc("Poncho",10,100,100,10,[],[],[]);
        Stack<BaseEntity> EnemiesLeft = new();
        bool PlayerTurn {get;set;} = true;
        bool IsGameOver()=>EnemiesLeft.Count==0&&CurrentEnemy!=null&&CurrentEnemy.IsDead;

        void PullOutEnemy(){
            CurrentEnemy = EnemiesLeft.Take(1).First();
        }

        void Think(){
            if(CurrentEnemy.State.Stamina<STAMINA_THRESHOLD)
            {
                CurrentEnemy.defend(new Shield("Shield",20), player);
                return;
            }
            CurrentEnemy.attack(new HeavyWeapon("Mace",10,10), player);
        }

        public void Run(){
            while(!IsGameOver()){
                try{
                if(!PlayerTurn){
                    Think();
                    PlayerTurn = !PlayerTurn;
                }
                AlterState? action = (AlterState?)MenuHandler.PickAction(player);
                if(action is BaseWeapon){
                    player.attack((BaseWeapon)action, CurrentEnemy);
                }
                if(action is BaseDefense){
                    player.defend((BaseDefense)action, CurrentEnemy);

                }
                if(action is BaseAccesory){
                    player.consume((BaseAccesory)action, CurrentEnemy);                   
                    PlayerTurn = !PlayerTurn;
                }
                }catch(Exception ex){
                    if(ex is TurnException)
                        Console.WriteLine(ex.Message);
                    else
                        Console.WriteLine(ex);
                }finally{
                    PlayerTurn = !PlayerTurn;
                }
            }
        }
    }
}