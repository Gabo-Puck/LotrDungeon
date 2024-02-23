using System.Reflection;
using System.Security.Cryptography;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    /// <summary>
    /// This class creates entities through Reflection
    /// </summary>
    /// <typeparam name="T">T is a Creator class. This class is responsible for create an instance of Z</typeparam>
    /// <typeparam name="Z">Z is the entity's class that we want to generate</typeparam>
    public class EntitiesFactory<T,Z> : IEntityFactory<Z> where T : Creator<Z>
    {
        public Z GenerateEntity()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var baseEntityType = typeof(T);
            var allBaseEntityType = new List<Type>();
            foreach(var type in assembly.GetTypes()){
                if(baseEntityType.IsAssignableFrom(type) && !type.IsAbstract)
                    allBaseEntityType.Add(type);
            }
            var theRandomIndex = RandomNumberGenerator.GetInt32(0,allBaseEntityType.Count);
            var selectedType = allBaseEntityType[theRandomIndex];
            var selected = (T)Activator.CreateInstance(selectedType);
            return selected.FactoryMethod();
        }
        public IEnumerable<Z> GenerateEntities(int amount){
            List<Z> entities = new();
            for (int i = 0; i < amount; i++)
            {
                entities.Add(GenerateEntity());
            }
            return entities;
        }
    }
}