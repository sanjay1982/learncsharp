using System;
using System.Linq;
using System.Reflection;

namespace CalculatorLib.BLL.Functions
{
    public class TypeMethod : SingleArgumentFunction
    {
        private readonly MethodInfo[] _methodInfos;
        private readonly Type _type;

        private object _instance;

        public TypeMethod(Type type, MethodInfo[] methodInfos) : base($" {methodInfos.First().Name} ",
            methodInfos.First().GetParameters().Length)
        {
            _type = type;
            _methodInfos = methodInfos;
        }

        private object TypeInstance => _instance = _instance ?? _type.GetInstance();

        protected override object Calculate(double[] arguments)
        {
            var methodInfo =
                _methodInfos.FirstOrDefault(x => x.GetParameters().All(y => y.ParameterType == typeof(double))) ??
                _methodInfos.FirstOrDefault(x =>
                    x.GetParameters().All(y => y.ParameterType.IsAssignableFrom(typeof(double)))) ??
                _methodInfos.FirstOrDefault(x =>
                    x.GetParameters().All(y => y.ParameterType.IsAssignableFrom(typeof(int))));
            if (methodInfo == null) return 0.0;

            var index = 0;
            var prams = methodInfo.GetParameters()
                .Select(x => Convert.ChangeType(arguments[index++], x.ParameterType)).ToArray();
            return methodInfo.Invoke(methodInfo.IsStatic ? null : TypeInstance, prams);
        }

        protected override object Calculate(long[] arguments)
        {
            var methodInfo =
                _methodInfos.FirstOrDefault(x => x.GetParameters().All(y => y.ParameterType == typeof(long))) ??
                _methodInfos.FirstOrDefault(x =>
                    x.GetParameters().All(y => y.ParameterType.IsAssignableFrom(typeof(long)))) ??
                _methodInfos.FirstOrDefault(x =>
                    x.GetParameters().All(y => y.ParameterType.IsAssignableFrom(typeof(int))));
            if (methodInfo == null) return 0.0;

            var index = 0;
            var prams = methodInfo.GetParameters()
                .Select(x => Convert.ChangeType(arguments[index++], x.ParameterType)).ToArray();
            return methodInfo.Invoke(methodInfo.IsStatic ? null : TypeInstance, prams);
        }
    }
}