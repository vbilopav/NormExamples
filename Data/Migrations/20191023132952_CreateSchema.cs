using Microsoft.EntityFrameworkCore.Migrations;

namespace NormExample.Data.Migrations
{
    public partial class CreateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            use NormExample
            GO

            drop table if exists dbo.NormUsers
            GO

            create table dbo.NormUsers
            (
                Id int not null primary key,
                Name nvarchar(64) not null,
                Email nvarchar(64) not null,
            )
            GO

            drop table if exists dbo.NormRoles
            GO

            create table dbo.NormRoles
            (
                Id int not null primary key,
                Name nvarchar(256) not null,
            )
            GO

            drop table if exists dbo.NormUserRoles
            GO

            create table dbo.NormUserRoles
            (
                UserId int not null,
                RoleId int not null,
                primary key (UserId, RoleId),
                foreign key (UserId) references dbo.NormUsers (Id),
                foreign key (RoleId) references dbo.NormRoles (Id)
            )
            GO
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
