using Microsoft.EntityFrameworkCore;
using Norm.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NormExample.Data
{

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class UsersService
    {
        private readonly DbConnection _connection;

        public UsersService(ApplicationDbContext dbContext)
        {
            _connection = dbContext.Database.GetDbConnection();
        }

        public IEnumerable<(int id, string userName, string email)> GetUsers() => 
            _connection.Read<int, string, string>("select Id, Name, Email from NormUsers");

        public IEnumerable<User> GetUsersMapped1() =>
            _connection.Read<int, string, string>("select Id, Name, Email from NormUsers").Select(t =>
            {
                var (id, userName, email) = t;
                return new User { Id = id, UserName = userName, Email = email };
            });

        public IEnumerable<User> GetUsersMapped2() =>
            _connection.Read("select Id, Name, Email from NormUsers").Select<User>();

        public IAsyncEnumerable<(int id, string userName, string email)> GetUsersAsync() => 
            _connection.ReadAsync<int, string, string>("select Id, Name, Email from NormUsers");

        
        public IAsyncEnumerable<(int id, string userName, string email)> GetUsersWithDelayAsync() =>
            _connection.ReadAsync<int, string, string>("select Id, Name, Email from NormUsers").SelectAwait(async t =>
            {
                await Task.Delay(100);
                return t;
            });

        public IAsyncEnumerable<User> GetUsersMapped1Async() => 
            _connection.ReadAsync<int, string, string>("select Id, Name, Email from NormUsers").Select(t => 
                new User { Id = t.Item1, UserName = t.Item2, Email = t.Item3 });

        public IAsyncEnumerable<User> GetUsersMapped2Async() =>
            _connection.ReadAsync<int, string, string>("select Id, Name, Email from NormUsers").Select(t =>
            {
                var (id, userName, email) = t;
                return new User { Id = id, UserName = userName, Email = email };
            });

        public IAsyncEnumerable<User> GetUsersMapped3Async() =>
            _connection.ReadAsync("select Id, Name as UserName, Email from NormUsers").Select<User>();

        public IAsyncEnumerable<User> GetUsersMapped3WithDelayAsync() =>
            _connection.ReadAsync("select Id, Name as UserName, Email from NormUsers").Select<User>().SelectAwait(async u =>
            {
                await Task.Delay(10);
                return u;
            });

        public IAsyncEnumerable<(int id, string userName, string email, IAsyncEnumerable<string> roles)> GetUsersAndRolesAsync() =>
            _connection.ReadAsync<int, string, string, string>(@"
                select u.Id, u.Name, Email, r.Name as Role
                from NormUsers u
                    left outer join NormUserRoles ur on u.Id = ur.UserId
                    left outer join NormRoles r on ur.RoleId = r.Id
                ")
                .GroupBy(u =>
                {
                    var (id, userName, email, _) = u;
                    return (id, userName, email);
                }).Select(g => (
                    g.Key.id,
                    g.Key.userName,
                    g.Key.email,
                    g.Select(r =>
                    {
                        var (_, _, _, role) = r;
                        return role;
                    })
                )); 

       
    }
}
