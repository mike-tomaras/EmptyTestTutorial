using Machine.Specifications;
using Shouldly;

namespace Spike_TESTS.Tests.e2e
{
    public class when_navigating_to_the_home_page : BaseTest
    {
        //the context is always established once. 
        //Observations are observations. 
        //They should not mutate state. 
        //So, there is no reason to execute the context more than once.
        //Forces small / per case classes
        Establish context = () =>
        {
            
        };

        Because of = () =>
        {
            
        };

        private It should_go_to_the_home_page = () =>
        {
            
        };

        //It should_show_the_list_of_users

        //It should_show_users_in_the_list
    }
}
