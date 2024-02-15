using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon
{
    public class Game
    {
        Human player = new Human("Boromir",12,100,100,10,[],[],[]);
        Orc enemy = new Orc("Orc1",12,100,100,10,[],[],[]);
        public void Run(){
        var x = new Rum(1,true);
            for (int i = 0; i < 9; i++)
            {
                enemy.consume(player, x);
                // enemy.defend(player, new Armor());
                // player.attack(enemy, new LightWeapon(10,20));
                enemy.attack(player, new LightWeapon(10,20));
                // player.attack(enemy, new LightWeapon(10,20));
            }
        }
    }
}