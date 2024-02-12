using LotrDungeon.Models.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon
{
    public class Game
    {
        Human player = new Human("Boromir",12,100,100,10,[],
        new () {
            new LightWeapon(10,20)
        }
        ,[]);
        Orc enemy = new Orc("Orc1",12,20,100,10,[],[],[]);
        
        public void Run(){
            for (int i = 0; i < 9; i++)
            {
                player.attack(enemy);
            }
        }
    }
}