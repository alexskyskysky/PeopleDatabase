using Microsoft.AspNetCore.Mvc;
using PeopleDatabase.Data;
using PeopleDatabase.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PeopleDatabase.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase {
        private readonly ApplicationDbContext _context;
        public PersonsController(ApplicationDbContext context, ILogger<PersonsController> logger) {
            _context = context;
        }
        /*private List<Person> _peopleMock = new List<Person>
        {
            new Person { Id = 1, Name = "Иван", DateOfBirth = "1980-01-01", Phone = "1234567" },
            new Person { Id = 2,Name = "Юлия", DateOfBirth = "1985-01-01", Phone = "7891234" },
            new Person { Id = 3,Name = "Мария", DateOfBirth = "1995-01-01", Phone = "0123456789" }
        };

        [HttpGet]
        public ActionResult<List<Person>> GetPeople()
        {
            return _peopleMock;
        }*/
        [HttpGet]
        public async Task<IActionResult> Get() {
            return Ok(await _context.Persons.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Person person) {
            var existingPerson = await _context.Persons.FindAsync(id);
            if (existingPerson == null) {
                return NotFound();
            }

            existingPerson.Name = person.Name;
            existingPerson.DateOfBirth = person.DateOfBirth;
            existingPerson.Phone = person.Phone;

            await _context.SaveChangesAsync();

            return Ok(existingPerson);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Person person) {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return Ok(person);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var person = await _context.Persons.FindAsync(id);
            if (person == null) {
                return NotFound();
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return Ok(person);
        }
    }
}