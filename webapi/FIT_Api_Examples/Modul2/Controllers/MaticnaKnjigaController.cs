using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul2.ViewModels;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.Models;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FIT_Api_Examples.Modul2.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class MaticnaKnjigaController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MaticnaKnjigaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<MaticnaKnjiga> Add([FromBody] UpisVM x)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            if(!x.obnova && _dbContext.MaticnaKnjiga.Count(m=> m.godinaStudija==x.godinaStudija && m.student.id==x.studentId)>0)
            {
                return BadRequest("Pokusavate upisati opet istu godinu");
            }

            var maticna = new MaticnaKnjiga()
            {
                student = _dbContext.Student.Find(x.studentId),
                godinaStudija = x.godinaStudija,
                cijena = x.cijena,
                datumUpisa = x.datumUpisa,
                akademskaGodina = _dbContext.AkademskaGodina.Find(x.akademskaGodinaId),
                evidentirao=HttpContext.GetLoginInfo().korisnickiNalog,
                obnova=x.obnova,
            };
            _dbContext.MaticnaKnjiga.Add(maticna);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<List<MaticnaKnjiga>> GetById(int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");


            return _dbContext.MaticnaKnjiga.Include(x=> x.evidentirao).Include(x=>x.akademskaGodina).Where(x => x.student.id == id).ToList();
        }

        [HttpPost]
        public ActionResult<MaticnaKnjiga> Ovjeri([FromBody] OvjeraVM m)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            var ovjeri = _dbContext.MaticnaKnjiga.Where(x => x.id == m.id).FirstOrDefault();
            ovjeri.datumOvjere = m.datumOvjere;
            ovjeri.napomena= m.napomena;

            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
