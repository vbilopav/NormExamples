using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace NormExample.Data.Migrations
{
    public partial class InsertData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            insert into NormRoles (Id, Name) values(1, 'User');
            GO
            insert into NormRoles (Id, Name) values(2, 'SuperUser');
            GO
            insert into NormRoles (Id, Name) values(3, 'Admin');
            GO
            insert into NormRoles (Id, Name) values(4, 'SuperAdmin');
            ");

            for(var i = 0; i < 1000; i++)
            {
                if (i > 0)
                {
                    var email = $"{GenerateName(3, 12, i)}@{GenerateName(3, 9, i + 1)}.{GenerateName(2, 2, i + 2)}";
                    var name = $"{GenerateName(5, 15, i +3)}";
                    migrationBuilder.Sql($"insert into NormUsers (Id, Name, Email) values({i}, '{name}', '{email}');");

                    var r = new Random(DateTime.Now.Millisecond * (i + 10));
                    for (var j = 1; j <= r.Next(1, 5); j++)
                    {
                        migrationBuilder.Sql($"insert into NormUserRoles (UserId, RoleId) values({i}, {j});");
                    }
                }
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }

        private static string GenerateName(int min, int max, int seed)
        {
            var rnd = new Random(DateTime.Now.Millisecond * seed);
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            var Name = "";
            Name += consonants[rnd.Next(consonants.Length)];
            Name += vowels[rnd.Next(vowels.Length)];
            int b = 2;
            var len = rnd.Next(min, max);
            while (b < len)
            {
                Name += consonants[rnd.Next(consonants.Length)];
                b++;
                Name += vowels[rnd.Next(vowels.Length)];
                b++;
            }

            return Name;
        }
    }
}
