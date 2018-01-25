using System;
using Xunit;

namespace test
{
    public class DomainClassTests
    {
        [Fact]
        public void CanCreateNewTeam()
        {
            var team=new Team("AFC Ajax", "The Lancers", "1900","Amsterdam Arena");
            

        }

    }
}
