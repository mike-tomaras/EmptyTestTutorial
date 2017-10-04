using Autofac;

namespace Spike_TESTS.Tests.Integration.Infrastructure
{
    public class IntegrationTestForClass<T>
    {
        protected readonly IContainer _container;
        
        public IntegrationTestForClass()
        {
            _container = IoCConfig.RegisterDependencies();
        }

        public T ClassUnderTest => _container.Resolve<T>();
    }
}