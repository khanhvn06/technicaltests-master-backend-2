using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProgrammationConformit.Models;

namespace TestProgrammationConformit.Controllers
{
  



    [Route("api/[controller]")]
    [ApiController]
    public class EvenementsController : ControllerBase
    {
        private readonly TodoContext _context;

        public EvenementsController(TodoContext context)
        {
            _context = context;
        }

        private bool TodoItemExists(long id) =>
                _context.Evenements.Any(e => e.Id == id);

        private static EvenementDTO ItemToDTO(Evenement todoItem) =>
            new EvenementDTO
            {
                Id = todoItem.Id,
                Titre = todoItem.Titre,
                Description = todoItem.Description,
                Personne = todoItem.Personne,
                
            };





        [Route("~/api/GetEvenement")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvenementDTO>>> GetEvenements()
        {
            return await _context.Evenements.Select(x=> ItemToDTO(x)).ToListAsync();
        }

       
        [Route("~/api/GetEvenement/{id}")]
        [HttpGet]
        public async Task<ActionResult<EvenementDTO>> GetEvenement(long id)
        {
            var evenement = await _context.Evenements.FindAsync(id);

            if (evenement == null)
            {
                return NotFound();
            }

            return ItemToDTO(evenement);
        }

        
        
        [Route("~/api/UpdateEvenement/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutEvenement(long id, EvenementDTO evenementDTO)
        {
            if (id != evenementDTO.Id)
            {
                return BadRequest();
            }

            //_context.Entry(evenement).State = EntityState.Modified;
            var evenement = await _context.Evenements.FindAsync(id);
            if (evenement == null)
            {
                return NotFound();
            }


            evenement.Titre = evenementDTO.Titre;
            evenement.Description = evenementDTO.Description;
            evenement.Personne = evenementDTO.Personne;
          

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvenementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

    
        [Route("~/api/AddEvenement")]
        [HttpPost]
        public async Task<ActionResult<EvenementDTO>> PostEvenement(EvenementDTO evenementDTO)
        {

            var evenement = new Evenement
            {
                Titre = evenementDTO.Titre,
                Description = evenementDTO.Description,
                Personne = evenementDTO.Personne,
               
            };

            _context.Evenements.Add(evenement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvenement", new { id = evenement.Id }, ItemToDTO(evenement));
        }

        [Route("~/api/DeleteEvenement/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEvenement(long id)
        {
            var evenement = await _context.Evenements.FindAsync(id);
            if (evenement == null)
            {
                return NotFound();
            }

            _context.Evenements.Remove(evenement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvenementExists(long id)
        {
            return _context.Evenements.Any(e => e.Id == id);
        }
    }
}
