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
        private readonly ILearningDataRepo _learningDataRepo;

        public LearningDataController(ILearningDataRepo learningDataRepo)
        {
            _learningDataRepo = learningDataRepo;
        }

        // GET: api/<LearningDataController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_learningDataRepo.RetrieveAll());
        }

        // GET api/<LearningDataController>/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var learningData = _learningDataRepo.Retrieve(id);
            if(learningData == null)
            {
                return NotFound();
            }
            return Ok(_learningDataRepo.Retrieve(id));
        }

        // POST api/<LearningDataController>
        [HttpPost]
        public IActionResult Post([FromBody] LearningDataDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var learningDataId = _learningDataRepo.Add(value);
            var newObject = _learningDataRepo.Retrieve(learningDataId);
            return CreatedAtAction(nameof(Get), new { id = newObject.Id }, newObject);
        }

        // PUT api/<LearningDataController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LearningDataDto value)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(_learningDataRepo.Retrieve(id) == null)
            {
                return NotFound();
            }

            value.Id = id;
            _learningDataRepo.Update(value);
            return Ok();
        }

        // DELETE api/<LearningDataController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var learningData = _learningDataRepo.Retrieve(id);
            if(learningData == null)
            {
                return NotFound();
            }

            _learningDataRepo.Remove(learningData);
            return Ok();
        }
    }
}
