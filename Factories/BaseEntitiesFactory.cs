using System.Reflection;
using System.Security.Cryptography;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    public class BaseEntitiesFactory : IEntityFactory<BaseEntity>
    {
        public BaseEntity GenerateEntity()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var baseEntityType = typeof(Creator<BaseEntity>);
            var allBaseEntityType = new List<Type>();
            foreach(var type in assembly.GetTypes()){
                if(baseEntityType.IsAssignableFrom(type))
                    allBaseEntityType.Add(type);
            }
            var theRandomIndex = RandomNumberGenerator.GetInt32(0,allBaseEntityType.Count);
            var selectedType = allBaseEntityType[theRandomIndex];
            var selected = (Creator<BaseEntity>)Activator.CreateInstance(selectedType);
            return selected.FactoryMethod();
        }
        public IEnumerable<BaseEntity> GenerateEntities(int amount){
            List<BaseEntity> entities = new();
            for (int i = 0; i < amount; i++)
            {
                entities.Add(GenerateEntity());
            }
            return entities;
        }
    }
}