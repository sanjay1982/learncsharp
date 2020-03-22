using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CalculatorLib.BLL
{
    public static class TypeExtensions
    {
        public static object GetInstance(this Type type, params object[] arguments)
        {
            var constructorArgumentTypes = arguments.Select(x => x.GetType()).ToArray();
            var constructor = type.GetConstructor(
                    BindingFlags.Instance | BindingFlags.Public,
                    null,
                    CallingConventions.HasThis,
                    constructorArgumentTypes,
                    new ParameterModifier[0]) ?? throw new InvalidOperationException();
            var constructorTypes = constructor.GetParameters().Select(x => x.ParameterType);
            var paramExpression = constructorTypes.Select((x, i) => Expression.Parameter(x, $"parameter{i}")).ToArray();

            var constructionCallExpression = Expression.New(constructor, paramExpression);
            var factory = Expression.Lambda(constructionCallExpression, paramExpression).Compile();
            return factory.DynamicInvoke(arguments);
        }
    }
}