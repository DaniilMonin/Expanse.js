using Expanse.Services;
using Xunit;

namespace Expanse.Tests.Services
{
    public class StartTest
    {
        [Fact]
        public void Start()
        {
            Bootstrapper.Load().Start(new string[] {});
        }
             
    }
}