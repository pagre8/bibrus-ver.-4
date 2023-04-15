using BibrusServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace BibrusServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class tmpController : Controller
    {
        private readonly BibrusDbContext _context;

        public tmpController(BibrusDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            return _context.Students.ToList();
        }

    }
}
