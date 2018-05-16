using System;
using System.Drawing;
using System.Linq;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace test
{
    public class EFCoreSQLiteTests
    {

        //no need for SQLite ref in this project. The data project uses it by default.
        private static Team CreateTeamAjax()
        {
            return new Team("AFC Ajax", "The Lancers", "1900", "Amsterdam Arena");
        }

       

        [Fact]
        public void CanStoreAndRetrieveHomeColors()
        {
            var team = CreateTeamAjax();
            team.SpecifyHomeUniformColors(Color.Blue, Color.Red, Color.Empty, Color.White, Color.Empty, Color.White);

            using (var context = new TeamContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Teams.Add(team);
                context.SaveChanges();
            }
        
            using (var context = new TeamContext())
            {
                var storedTeam = context.Teams.Include(t => t.HomeColors).FirstOrDefault();

                Assert.Equal(Color.Blue, storedTeam.HomeColors.ShirtPrimary);
            }
        }

    }
}