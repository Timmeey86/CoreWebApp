using System;
using System.Collections.Generic;
using CoreWebApp.LogicLayer.Dtos;
using CoreWebApp.LogicLayer.Storage;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningDataController : ControllerBase
    {
        private readonly ILearningDataRepo _dataStorage;

        public LearningDataController(ILearningDataRepo dataStorage)
        {
            _dataStorage = dataStorage;
        }

        // GET: api/<LearningDataController>
        [HttpGet]
        public ActionResult<IEnumerable<LearningDataDto>> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/<LearningDataController>/5
        [HttpGet("{id}")]
        public ActionResult<LearningDataDto> Get(int id)
        {
            var learningData = _dataStorage.Retrieve(id);
            if(learningData == null)
            {
                return NotFound();
            }
            return _dataStorage.Retrieve(id);
        }

        // POST api/<LearningDataController>
        [HttpPost]
        public ActionResult Post([FromBody] LearningDataDto value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<LearningDataController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<LearningDataController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
