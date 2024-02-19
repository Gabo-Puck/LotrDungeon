using LotrDungeon.AlterEntities;
using LotrDungeon.Exceptions;
using LotrDungeon.Factories;
using LotrDungeon.Menu;
using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;
using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Timers;
namespace LotrDungeon
{
    public class MenuHandler{
        Game game;
        MenuOption<AlterState> Accesories;
        MenuOption<AlterState> Weapons;
        MenuOption<AlterState> Defenses;
        OnlyViewOption<AlterState> Enemies;
        MenuOption<AlterState> InitialMenu;
        LeaveOption<AlterState> LeaveOption;
        public MenuHandler(Game _game){
            UpdateOptions(_game);
        }

        public void UpdateOptions(Game _game){
            game = _game;
            InitialMenu = new MenuOption<AlterState>();
            LeaveOption = new LeaveOption<AlterState>("Leave",null,InitialMenu);
            Accesories = InitMenuOptions("Pick an accesory", game.player.Accesories);
            Defenses = InitMenuOptions("Pick a defense", game.player.Defense);
            Weapons = InitMenuOptions("Pick a weapon", game.player.Weapons);
            Enemies = InitViewOnlyOptions("Scout enemies", game.EnemiesLeft, InitialMenu);
            var List = new List<BaseMenuOption<AlterState>>();
            List.Add(Weapons);
            List.Add(Defenses);
            List.Add(Accesories);
            List.Add(Enemies);
            InitialMenu.menuOptions = List;
        }
        private MenuOption<AlterState> InitMenuOptions(string Text, IEnumerable<AlterState> options){
            var menuOptions = new List<BaseMenuOption<AlterState>>();
            foreach(var accesories in options){
                menuOptions.Add((
                    new MenuOption<AlterState>(accesories.Name, accesories,[])
                ));
            }
            menuOptions.Add(LeaveOption);
            var option = new MenuOption<AlterState>(Text ,null, menuOptions);
            return option;
        }
        private OnlyViewOption<AlterState> InitViewOnlyOptions(string Text, IEnumerable<AlterState> options, BaseMenuOption<AlterState> next){
            var menuOptions = new List<BaseMenuOption<AlterState>>();
            int index = 0;
            foreach(var accesories in options){
                menuOptions.Add((
                    new OnlyViewOption<AlterState>($"{accesories.Name} ({accesories.Classifier}) {(index== 0 ?"- Current":"")}", null, [], next)
                ));
                index++;
            }
            var option = new OnlyViewOption<AlterState>(Text ,null, menuOptions, next);
            return option;
        }
        int HandleInput(){
            try{
                string? input = Console.ReadLine();
                return Int32.Parse(input);
            }catch(Exception){
                Console.WriteLine("Invalid option");
                return -1;
            }
        }

        public object PickAction(IEnumerable<BaseEntity> EnemiesLeft)
        {
            int input = -1;
            object result = null;
            BaseMenuOption<AlterState> actualOption = InitialMenu;
            while(result == null){
                actualOption.PrintOption();
                input = HandleInput();
                try{
                    actualOption = actualOption.GoNext(input - 1);
                    actualOption = actualOption.DoProcess();
                }catch(Exception ex){
                    Console.WriteLine(ex.Message);
                }
                if(actualOption.Value != null)
                    result = actualOption.Value;
            }
            return result;
        }
    }
    public class Game
    {
        /// <summary>
        /// Enemy spawn rate in seconds
        /// </summary>
        static double ENEMY_SPAWN_RATE = 5;
        int STAMINA_THRESHOLD = 50;
        public Human player = new Human("Boromir",12,100,1,10,
        new(){
            new Lembda("Lembda",3,true),
            new Rum("Rum",10,true)
        },
        new(){
            new HeavyWeapon("Mace",1000,10),
            new LightWeapon("Knife",10,10)
            },
        new(){
            new Armor("Mithril Chainmail",0.2f),
            new Shield("Oak Shield",10)
        }
        );
        
        MenuHandler menu;
        BaseEntity? CurrentEnemy = new Orc("Poncho",30,100,100,10,[],[],[]);
        public Queue<BaseEntity> EnemiesLeft = new();
        bool PlayerTurn {get;set;} = true;

        public Game(){
            menu = new MenuHandler(this);
            EnemiesLeft.Enqueue(CurrentEnemy);
        }

        bool IsGameOver()=>EnemiesLeft.Count==0&&CurrentEnemy!=null&&CurrentEnemy.IsDead||player.IsDead;

        void PullOutEnemy(){
            if(EnemiesLeft.TryDequeue(out BaseEntity? e)&&EnemiesLeft.TryPeek(out BaseEntity? currentEnemy)){
                CurrentEnemy = currentEnemy;
            }
        }

        void Think(){
            PlayerTurn = true;
            if(CurrentEnemy.State.Stamina<STAMINA_THRESHOLD)
            {
                CurrentEnemy.defend(new Shield("Shield",20), player);
                return;
            }
            CurrentEnemy.attack(new HeavyWeapon("Mace",10,10), player);
        }
        void HandlePlayerTurn(){
                Console.WriteLine($"You are fighting: {CurrentEnemy.Name} ({CurrentEnemy.Classifier})");
                AlterState? action = (AlterState?)menu.PickAction(EnemiesLeft);
                if(action is BaseAccesory){
                    player.consume((BaseAccesory)action, CurrentEnemy);                   
                    PlayerTurn = true;
                    return;
                }
                if(action is BaseWeapon){
                    player.attack((BaseWeapon)action, CurrentEnemy);
                }
                if(action is BaseDefense){
                    player.defend((BaseDefense)action, CurrentEnemy);
                }
                PlayerTurn = false;
        }

        private void SpawnEnemies(int amount){
            BaseEntitiesFactory factory = new();
            var entity = factory.GenerateEntities(amount);
            foreach (var item in entity)
            {
                EnemiesLeft.Enqueue(item);
            }
        }

        private int CalcEnemies(double seconds){
            double enemies = seconds/ENEMY_SPAWN_RATE;
            return (int)enemies;
        }


        public void Run(){
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool RestartCount = false;
            while(!IsGameOver()){
                if(RestartCount){
                    stopwatch.Restart();
                    RestartCount = false;
                }
                try{
                if(!PlayerTurn){
                    Think();
                }else{
                    HandlePlayerTurn();
                }
                
                }catch(Exception ex){
                    if(ex is TurnException)
                        Console.WriteLine(ex.Message);
                    else
                        Console.WriteLine(ex);
                }finally{
                    if(CurrentEnemy.IsDead){
                        stopwatch.Stop();
                        int enemies = CalcEnemies(stopwatch.Elapsed.TotalSeconds);
                        Console.WriteLine($"You have killed: {CurrentEnemy.Name}!");
                        Console.WriteLine($"Have appeared: {enemies} enemies");
                        SpawnEnemies(enemies);
                        PullOutEnemy();
                        RestartCount = true;
                    }
                menu.UpdateOptions(this);
                }
            }
            if(player.IsDead){
                Console.WriteLine("The hero has fallen...");
                return;
            }
            if(EnemiesLeft.Count == 0){
                Console.WriteLine("You won!");
                return;
            }
            Console.WriteLine("We've won, but at what cost...");
        }
    }
}