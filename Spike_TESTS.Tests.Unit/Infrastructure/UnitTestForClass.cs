using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Spike_TESTS.Tests.Unit.Infrastructure
{
    [TestFixture]
    public abstract class UnitTestForClass<T> 
    {
        public static T ClassUnderTest { get; set; }
        private static Dictionary<string, object> _dic;
        protected static Fixture Fixture;

        [SetUp]
        public void Init()
        {
            InitializeFixture();

            _dic = new Dictionary<string, object>();

            var ctors = typeof(T).GetConstructors();
            var ctor = ctors[0];

            foreach (var param in ctor.GetParameters())
            {
                if (param.ParameterType.Name == new bool().GetType().Name)
                {
                    _dic.Add(param.ParameterType.Name, false);
                    continue;
                }

                try
                {
                    _dic.Add(param.ParameterType.Name, DynamicMock(param.ParameterType));
                }
                catch (ArgumentException e)
                {
                    throw new Exception($"This is probably because the ClassUnderTest has a dependency injected more than once :{param.ParameterType.Name}.", e);
                }
            }

            try
            {
                ClassUnderTest = (T)Activator.CreateInstance(typeof(T), _dic.Values.Select(Convert).ToArray());
            }
            catch (MissingMethodException)
            {
                throw new Exception("This is probably because you have mocked a dependency that your ClassUnderTest doesn't have in the constructor.");
            }
        }

        private static void InitializeFixture()
        {
            Fixture = new Fixture();

            Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => Fixture.Behaviors.Remove(b));
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        protected static Mock<M> MockOf<M>() where M : class
        {
            try
            {
                return (Mock<M>)_dic[typeof(M).Name];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("This is probably because you're mocking something that isn't needed in the constructor");
            }
        }

        private static object DynamicMock(Type type)
        {
            return typeof(Mock<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });
        }

        private static object Convert(object ob)
        {
            if (ob.GetType().Name == new bool().GetType().Name)
                return false;

            return ob.GetType().GetProperties().First(f => f.Name == "Object" && f.ReflectedType != typeof(object)).GetValue(ob, new object[] { });
        }
    }
}