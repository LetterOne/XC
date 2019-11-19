using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XC.WebAPI.Context;

namespace XC.WebAPI.Controllers
{
    /// <summary>
    /// WebAPI 基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public abstract class AbstractController<T> : ControllerBase where T:class,new()
    {

        private readonly XCContext _context=new XCContext();



        // GET: api/Abstract
        [HttpGet]
        public virtual IEnumerable<T> Get()
        {
            return null;// await  _context.FindAsync<T>().ToAsyncEnumerable();
        }


        // GET: api/Abstract/5
        [HttpGet("{id}", Name = "Get")]
        public virtual string Get(int id)
        {
            return "value";
        }

        // POST: api/Abstract
        [HttpPost]
        public virtual void Post([FromBody] string value)
        {
        }

        // PUT: api/Abstract/5
        [HttpPut("{id}")]
        public virtual void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public virtual void Delete(int id)
        {
        }
    }
}
