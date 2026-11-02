using konyvtarakAPI.DTOs;
using konyvtarakAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace konyvtarakAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelepulesController : ControllerBase
    {
        //dependency injection
        private KonyvtarakContext _context;
        public TelepulesController(KonyvtarakContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var telepules = _context.Telepuleseks.ToList();
                if(telepules == null)
                {
                    return NotFound("Nincs ilyen adat");
                }
                return Ok(telepules);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var telepules = _context.Telepuleseks.FirstOrDefault(t => t.Id == id);
                if (telepules == null)
                {
                    return NotFound("Nincs ilyen adat");
                }
                return Ok(telepules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }

        [HttpPost("Post")]
        public IActionResult PostTelepulesek(Telepulesek ujTelepules)
        {
            try
            {
                _context.Telepuleseks.Add(ujTelepules);
                _context.SaveChanges();
                return Ok("Sikeres adatfeltöltés");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }

        [HttpPut("Put/{id}")]
        public IActionResult PutTelepulesek(int id, Telepulesek modositottTelepules)
        {
            try
            {
                var telepules = _context.Telepuleseks.FirstOrDefault(t => t.Id == id);
                _context.ChangeTracker.Clear();
                if (telepules == null)
                {
                    return NotFound("Nincs ilyen adat");
                }
                _context.Telepuleseks.Update(modositottTelepules);
                _context.SaveChanges();
                return Ok("Sikeres adatmódosítás");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteTelepulesek(int id)
        {
            try
            {
                var telepules = _context.Telepuleseks.FirstOrDefault(t => t.Id == id);
                _context.ChangeTracker.Clear();
                if (telepules == null)
                {
                    return NotFound("Nincs ilyen adat");
                }
                _context.Telepuleseks.Remove(telepules);
                _context.SaveChanges();
                return Ok("Sikeres adatmódosítás");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("GetbyNevResz/{nevResz}")]
        public IActionResult GetByNevResz(string nevResz)
        {
            try
            {
                List<Telepulesek> talalat = _context.Telepuleseks.Where(t => t.TelepNev.Contains(nevResz)).ToList();
                if (talalat == null)
                {
                    return NotFound("Nincs ilyen adat");
                }
                return Ok(talalat);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("TelepKonyvtar")]
        public IActionResult TelepKonyvtar()
        {
            try
            {
                var telepules = _context.Telepuleseks.Include(t => t.Konyvtaraks).ToList();
                if (telepules == null)
                {
                    return NotFound("Nincs ilyen adat");
                }
                return Ok(telepules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("TelepulesKonyvtarai/{telepNev}")]
        public IActionResult GetTelepKonyvtarNevMegye(string telepNev)
        {
            try
            {
                List<TelepulesKonyvtarai> talalat = _context.Telepuleseks.
                    Include(t => t.Konyvtaraks).
                    Include(t => t.Megye).
                    Where(t => t.TelepNev == telepNev).
                    Select(k => new TelepulesKonyvtarai
                    {
                        TelepNev = k.TelepNev,
                        KonyvtarNev = k.Konyvtaraks.Select(k => k.KonyvtarNev).FirstOrDefault(),
                        MegyeNev = k.Megye.MegyeNev
                    }).
                    ToList();
                if (talalat == null)
                {
                    return NotFound("Nincs ilyen adat");
                }
                return Ok(talalat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }

        //async függvények
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var telepules = await _context.Telepuleseks.ToListAsync();
                if (telepules == null)
                {
                    return NotFound("Nincs ilyen adat");
                }
                return Ok(telepules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("GetByIdAsync/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var telepules = await _context.Telepuleseks.FirstOrDefaultAsync(t => t.Id == id);
                if (telepules == null)
                {
                    return NotFound("Nincs ilyen adat");
                }
                return Ok(telepules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }

        [HttpPost("PostAsync")]
        public async Task<IActionResult> PostTelepulesekAsync(Telepulesek ujTelepules)
        {
            try
            {
                _context.Telepuleseks.Add(ujTelepules);
                await _context.SaveChangesAsync();
                return Ok("Sikeres adatfeltöltés");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }

        [HttpPut("PutAsync/{id}")]
        public async Task<IActionResult> PutTelepulesekAsync(int id, Telepulesek modositottTelepules)
        {
            try
            {
                var telepules = await _context.Telepuleseks.FirstOrDefaultAsync(t => t.Id == id);
                _context.ChangeTracker.Clear();
                if (telepules == null)
                {
                    return NotFound("Nincs ilyen adat");
                }
                _context.Telepuleseks.Update(modositottTelepules);
                await _context.SaveChangesAsync();
                return Ok("Sikeres adatmódosítás");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }

        [HttpDelete("DeleteAsync/{id}")]
        public async Task<IActionResult> DeleteTelepulesekAsync(int id)
        {
            try
            {
                var telepules = await _context.Telepuleseks.FirstOrDefaultAsync(t => t.Id == id);
                _context.ChangeTracker.Clear();
                if (telepules == null)
                {
                    return NotFound("Nincs ilyen adat");
                }
                _context.Telepuleseks.Remove(telepules);
                await _context.SaveChangesAsync();
                return Ok("Sikeres adatmódosítás");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }

    }
}
