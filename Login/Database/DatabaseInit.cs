using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Login.Database
{
    public static class DatabaseInit
    {
        public static void INIT(System.IServiceProvider serviceProvider)
        {
            var context = new DatabaseContext(serviceProvider.GetRequiredService<DbContextOptions<DatabaseContext>>());
            context.Database.EnsureCreated();
            InsertDefault(context);
        }

        private static void InsertDefault(DatabaseContext dbContext)
        {
            AccountDefault(dbContext);
            MovieDefault(dbContext);

        }

        private static void AccountDefault(DatabaseContext dbContext)
        {
            if (!(dbContext.Account.Any()))
            {
                dbContext.Account.Add(new Models.Account
                {
                    Password = "ABC123",
                    UserName = "UserTEST1",
                    Email = "ABC123@gmail.com",
                    Role = "User"
                });
                dbContext.SaveChanges();

                dbContext.Account.Add(new Models.Account
                {
                    Password = "DEF123",
                    UserName = "AdminTEST1",
                    Email = "DEF123@gmail.com",
                    Role = "Administrator"
                });
                dbContext.SaveChanges();
            }
        }

        private static void MovieDefault(DatabaseContext dbContext)
        {
            if (!(dbContext.Movie.Any()))
            {
                dbContext.Movie.Add(
                    new Models.Movie
                    {
                        Title = "Avengers: Endgame",
                        Url = "https://m.media-amazon.com/images/M/MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzI2NzQ2NzM@._V1_SY1000_CR0,0,674,1000_AL_.jpg"
                    }
                );
                dbContext.SaveChanges();

                dbContext.Movie.Add(
                   new Models.Movie
                   {
                       Title = "Aladdin",
                       Url = "https://m.media-amazon.com/images/M/MV5BMjQ2ODIyMjY4MF5BMl5BanBnXkFtZTgwNzY4ODI2NzM@._V1_SY1000_CR0,0,674,1000_AL_.jpg"
                   }
               );
                dbContext.SaveChanges();

                dbContext.Movie.Add(
                   new Models.Movie
                   {
                       Title = "John Wick: Parabellum",
                       Url = "https://m.media-amazon.com/images/M/MV5BMDg2YzI0ODctYjliMy00NTU0LTkxODYtYTNkNjQwMzVmOTcxXkEyXkFqcGdeQXVyNjg2NjQwMDQ@._V1_SY1000_CR0,0,648,1000_AL_.jpg"
                   }
               );
                dbContext.SaveChanges();

                dbContext.Movie.Add(
                  new Models.Movie
                  {
                      Title = "Godzilla: King of the Monsters",
                      Url = "https://m.media-amazon.com/images/M/MV5BOGFjYWNkMTMtMTg1ZC00Y2I4LTg0ZTYtN2ZlMzI4MGQwNzg4XkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_SY1000_CR0,0,674,1000_AL_.jpg"
                  }
              );
                dbContext.SaveChanges();

            }
        }
    }
}