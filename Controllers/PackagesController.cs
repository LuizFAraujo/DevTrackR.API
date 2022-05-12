using DevTrackR.API.Entities;
using DevTrackR.API.Models;
using DevTrackR.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevTrackR.API.Controllers
{
    [ApiController]
    [Route("api/packages")]
    public class PackagesController : ControllerBase
    {
        private readonly DbMemContext _context;

        // Construtor // Injeção de Dependência
        public PackagesController(DbMemContext context)
        {
            _context = context;
        }


        // GET api/packages
        [HttpGet]
        public IActionResult GetAll()
        {
            // var packages = new List<Package> {
            //     new Package("Pacote 1", 1.3M),
            //     new Package("Pacote 2", 0.2M),
            //  };

            var packages = _context.Packages;

            return Ok(packages);
        }


        // GET api/packages/(um código aqui)
        [HttpGet("{code}")]
        public IActionResult GetByCode(string code)
        {
            // var package = new Package("Pacote 2", 0.2M);

            var package = _context
                .Packages
                .SingleOrDefault(p => p.Code == code);

            if (package == null)
            {
                return NotFound();
            }

            return Ok(package);
        }


        // POST api/packages
        [HttpPost]
        public IActionResult Post(AddPackageInputModel model)
        {
            if (model.Title.Length < 10)
            {
                return BadRequest("Tamanho do título é menor que 10 caracteres!");
            }

            var package = new Package(model.Title, model.Weight);

            _context.Packages.Add(package);

            return CreatedAtAction(
                "GetByCode",
                new { code = package.Code },
                package);
        }


        // Cadastrar uma atualização
        // POST api/packages/(um código aqui)/updates
        [HttpPost("{code}/updates")]
        public IActionResult PostUpdate(string code, AddPackageUpdateInputModel model)
        {
            // var package = new Package("Pacote 1", 1.2M);

            var package = _context
                .Packages
                .SingleOrDefault(p => p.Code == code);

            if (package == null)
            {
                return NotFound();
            }

            package.AddUpdate(model.Status, model.Delivered);

            return NoContent();
        }


        // Atualização de um recurso
        // PUT api/packages/(um código aqui)
        // [HttpPut("{code}")]
        // public IActionResult Put(string code)
        // {
        //     return Ok();
        // }

    }
}