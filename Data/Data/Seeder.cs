using Core.DTOs;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public static class Seeder
    {
        public static void Seed(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Level>().HasData(
                new Level
                {
                    Id = 1,
                    Name = "Easy"
                },
                 new Level
                 {
                     Id = 2,
                     Name = "Medium"
                 },
                  new Level
                  {
                      Id = 3,
                      Name = "Hard"
                  }
                );

           modelBuilder.Entity<Language>().HasData(
                new Language
                {
                    Id = 1,
                    Name = "English"
                },
                 new Language
                 {
                     Id = 2,
                     Name = "Ukraine"
                 }
                );

            modelBuilder.Entity<AppText>().HasData(
             new AppText
             {
                 Id = 1,
                 Text = "bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla ",
                 LanguageId = 1,
                 LevelId = 1,
             },
             new AppText
             {
                 Id = 2,
                 Text = "бла бла бла бла бла бла бла бла бла бла бла бла бла бла бла ",
                 LanguageId = 2,
                 LevelId = 1,
             }
             );



        }







    }
}
