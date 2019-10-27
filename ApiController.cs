using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NormExample.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NormExample
{
    [Route("api/")]
    public class ApiController : Controller
    {
        public ApiController(UsersService service)
        {
            Service = service;
        }

        public UsersService Service { get; }

        // GET: api/<controller>
        [HttpGet("1")]
        public IAsyncEnumerable<(int id, string userName, string email)> Get1()
        {
            return Service.GetUsersAsync();
        }

        // GET: api/<controller>
        [HttpGet("2")]
        public async IAsyncEnumerable<(int id, string userName, string email)> Get2()
        {
            await foreach (var user in Service.GetUsersAsync())
            {
                yield return user;
            }
        }


        // GET: api/<controller>
        [HttpGet("3")]
        public IAsyncEnumerable<User> Get3()
        {
            return Service.GetUsersMapped1Async();
        }

        // GET: api/<controller>
        [HttpGet("4")]
        public async IAsyncEnumerable<User> Get4()
        {
            await foreach (var user in Service.GetUsersMapped1Async())
            {
                yield return user;
            }
        }


        // GET: api/<controller>
        [HttpGet("5")]
        public IAsyncEnumerable<User> Get5()
        {
            return Service.GetUsersMapped3Async();
        }

        // GET: api/<controller>
        [HttpGet("6")]
        public async IAsyncEnumerable<User> Get6()
        {
            await foreach (var user in Service.GetUsersMapped3Async())
            {
                yield return user;
            }
        }

        // GET: api/<controller>
        [HttpGet("7")]
        public IAsyncEnumerable<User> Get7()
        {
            return Service.GetUsersMapped3WithDelayAsync();
        }


    }
}
