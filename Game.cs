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
        OnlyViewOption<AlterState> CheckStats;
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
            CheckStats = new("Stats", null, [
                new OnlyViewOption<AlterState>(game.player.ToString(),null,[],InitialMenu)
            ], InitialMenu);
            var List = new List<BaseMenuOption<AlterState>>();
            List.Add(Weapons);
            List.Add(Defenses);
            List.Add(Accesories);
            List.Add(Enemies);
            List.Add(CheckStats);
            InitialMenu.menuOptions = List;
        }
        private MenuOption<AlterState> InitMenuOptions(string Text, IEnumerable<AlterState> options){
            var menuOptions = new List<BaseMenuOption<AlterState>>();
            foreach(var accesories in options){
                MenuOption<AlterState> x = new();
                var n = new List<BaseMenuOption<AlterState>>(){
                    new MenuOption<AlterState>("Use", accesories, []),
                    new OnlyViewOption<AlterState>("Stats", null, [
                        new OnlyViewOption<AlterState>(accesories.ToString(),null,[], x)
                    ], x),
                    LeaveOption
                };
                x.Text = accesories.Name;
                x.Value = null;
                x.menuOptions = n;
                menuOptions.Add(x);
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
    
    public class MenuPickCharacter{
        const int OPTIONS = 10;
        MenuOption<BaseEntity> Options;
        public MenuPickCharacter(){
            EntitiesFactory<BaseEntity> entitiesFactory = new();
            MenuOption<BaseEntity> menuOption = new();
            List<MenuOption<BaseEntity>> menuOptions = new();
            Options = menuOption;
            List<BaseEntity> entities = (List<BaseEntity>)entitiesFactory.GenerateEntities(OPTIONS);
            foreach(BaseEntity entity in  entities){
                MenuOption<BaseEntity> option = new();
                option.Text = $"{entity.Name} - ({entity.Classifier})";
                option.Value = null;
                option.menuOptions = new List<BaseMenuOption<BaseEntity>>(){
                    new MenuOption<BaseEntity>("Use",entity,[]),
                    new OnlyViewOption<BaseEntity>("Stats",null,[
                        new OnlyViewOption<BaseEntity>($@"{entity}
                        {entity.PrintItems()}",null,[],option)
                    ],option),
                    new LeaveOption<BaseEntity>("Leave",null, Options)
                };
                menuOptions.Add(option);
            }
            menuOption.Text = "";
            menuOption.menuOptions = menuOptions;

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

        public BaseEntity PickAction()
        {
            int input = -1;
            BaseEntity result = null;
            BaseMenuOption<BaseEntity> actualOption = Options;
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
        public BaseEntity player;
        
        MenuHandler menu;
        MenuPickCharacter menuPickCharacter;
        BaseEntity? CurrentEnemy;
        public Queue<BaseEntity> EnemiesLeft = new();
        bool PlayerTurn {get;set;} = true;

        public Game(){
            var factory2 = new EntitiesFactory<Elve>();
            factory2.GenerateEntity();
            menuPickCharacter = new MenuPickCharacter();
            BaseEntity _player = menuPickCharacter.PickAction();
            player = _player;
            var factory = new EntitiesFactory<BaseEntity>();
            BaseEntity _CurrentEnemy = factory.GenerateEntity();
            EnemiesLeft.Enqueue(_CurrentEnemy);
            CurrentEnemy = EnemiesLeft.Peek();
            menu = new MenuHandler(this);
        }

        bool IsGameOver()=>EnemiesLeft.Count==0&&CurrentEnemy!=null&&CurrentEnemy.IsDead||player.IsDead;

        void PullOutEnemy(){
            if(EnemiesLeft.TryDequeue(out BaseEntity? e)&&EnemiesLeft.TryPeek(out BaseEntity? currentEnemy)){
                CurrentEnemy = currentEnemy;
            }
        }

        void Think(){
            PlayerTurn = true;
            Console.WriteLine($"{CurrentEnemy.Name} - ({CurrentEnemy.Classifier}) turn");
            if(CurrentEnemy.State.Stamina<STAMINA_THRESHOLD)
            {
                CurrentEnemy.defend(CurrentEnemy.Defense[0], player);
                return;
            }
            CurrentEnemy.attack(CurrentEnemy.Weapons[0], player);
        }
        void HandlePlayerTurn(){
                Console.WriteLine($"You are fighting: {CurrentEnemy.Name} ({CurrentEnemy.Classifier})");
                AlterState? action = (AlterState?)menu.PickAction(EnemiesLeft);
                if(action is BaseAccesory){
                    player.consume((BaseAccesory)action, CurrentEnemy);                   
                    PlayerTurn = true;
                    return;
                }
                PlayerTurn = false;
                Console.WriteLine("Your turn");
                if(action is BaseWeapon){
                    player.attack((BaseWeapon)action, CurrentEnemy);
                }
                if(action is BaseDefense){
                    player.defend((BaseDefense)action, CurrentEnemy);
                }
        }

        private void SpawnEnemies(int amount){
            EntitiesFactory<BaseEntity> factory = new();
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
                    if(ex is TurnException){
                        Console.WriteLine(ex.Message);
                    }
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