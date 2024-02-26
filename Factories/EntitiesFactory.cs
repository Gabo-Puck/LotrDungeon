using System.Reflection;
using System.Security.Cryptography;
using LotrDungeon.AlterEntities;
using LotrDungeon.Models.Entities;

namespace LotrDungeon.Factories
{
    /// <summary>
    /// This class creates entities through Reflection
    /// </summary>
    /// <typeparam name="T">T is a Creator class. This class is responsible for create an instance of Z</typeparam>
    /// <typeparam name="Z">Z is the entity's class that we want to generate</typeparam>
    public class EntitiesFactory<Z> : IEntityFactory<Z>  where Z : AlterState
    {
        public Z GenerateEntity()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var returnType = typeof(Z);
            var allBaseEntityType = new List<Type>();
            foreach(var type in assembly.GetTypes()){
                if(
                    type.IsClass && 
                    !type.IsAbstract && 
                    type.BaseType != null && 
                    type.BaseType.IsGenericType && 
                    IsSubclassOfRawGeneric(type, typeof(Creator<>)) &&
                    returnType.IsAssignableFrom(type.BaseType.GetGenericArguments().First())
                    )
                    allBaseEntityType.Add(type);
            }
            
            if(allBaseEntityType.Count<=0)
                throw new Exception($"There are no valid 'Creator' implementations to instantiate a {typeof(Z)}");
            var theRandomIndex = RandomNumberGenerator.GetInt32(0,allBaseEntityType.Count);
            var selectedType = allBaseEntityType[theRandomIndex];
            var selected = (Creator)Activator.CreateInstance(selectedType);
            return (Z)selected.FactoryMethod();
        }
        bool IsSubclassOfRawGeneric(Type toCheck, Type generic)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var current = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == current)
                    return true;
                toCheck = toCheck.BaseType;
            }
            return false;
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