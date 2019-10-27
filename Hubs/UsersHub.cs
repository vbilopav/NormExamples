using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NormExample.Data;

namespace NormExample.Hubs
{
    public class UsersHub : Hub
    {
        private readonly UsersService usersService;

        public UsersHub(UsersService usersService)
        {
            this.usersService = usersService;
        }

        public IAsyncEnumerable<User> Users() => usersService.GetUsersMapped3Async();

        public IAsyncEnumerable<User> UsersWithCancelationToken(CancellationToken cancellationToken) =>
            usersService.GetUsersMapped3WithDelayAsync().Select(u =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                return u;
            });
    }
}
