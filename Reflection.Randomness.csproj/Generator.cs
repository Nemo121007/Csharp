using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Randomness
{
    public interface ISet<T>
    {
        Generator<T> Set(IContinuousDistribution distribution);
    }

    public class FromDistribution : Attribute
    {
        private Type typeOfDistribution;
        private object[] args;

        public FromDistribution(Type typeOfDistribution, params object[] args)
        {
            this.typeOfDistribution = typeOfDistribution;
            this.args = args;
        }

        public IContinuousDistribution GetDistribution()
        {
            try
            {
                return (IContinuousDistribution)Activator.CreateInstance(typeOfDistribution, args);
            }
            catch
            {
                throw new ArgumentException(typeOfDistribution.Name);
            }
        }
    }

    public class Generator<T> : ISet<T>
    {
        private string property = "";
        public Dictionary<PropertyInfo, IContinuousDistribution> DictionaryOfProperties;
        public Generator()
        {
            DictionaryOfProperties = new Dictionary<PropertyInfo, IContinuousDistribution>();

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                if (property.GetCustomAttribute(typeof(FromDistribution), false) is FromDistribution attribute)
                {
                    DictionaryOfProperties.Add(property, attribute.GetDistribution());
                }
            }
        }

        public T Generate(Random random)
        {
            var links = new List<MemberBinding>();
            foreach (var element in DictionaryOfProperties)
            {
                var bind = Expression.Bind(element.Key, Expression.Constant(element.Value.Generate(random)));
                links.Add(bind);
            }
            return Expression.Lambda<Func<T>>
                (Expression.MemberInit(Expression.New(typeof(T).GetConstructor(new Type[0])), links))
                .Compile()();
        }

        public ISet<T> For(Expression<Func<T, double>> expression)
        {
            try
            {
                property = (expression.Body as MemberExpression).Member.Name;
                if (typeof(T).GetProperty(property) == null)
                    throw new ArgumentException();
                return new GeneratorWithoutFor<T>(this, property);
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        public Generator<T> Set(IContinuousDistribution distribution)
        {
            var properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name == property)
                {
                    DictionaryOfProperties[properties[i]] = distribution;
                    break;
                }
            }
            return this;
        }
    }

    public class GeneratorWithoutFor<T> : ISet<T>
    {
        private readonly string propertyName;
        private Generator<T> generator;
        public GeneratorWithoutFor(Generator<T> generator,
            string propertyName)
        {
            this.propertyName = propertyName;
            this.generator = generator;
        }

        public Generator<T> Set(IContinuousDistribution distribution)
        {
            var properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name == propertyName)
                {
                    generator.DictionaryOfProperties[properties[i]] = distribution;
                    break;
                }
            }
            return generator;
        }
    }
}
