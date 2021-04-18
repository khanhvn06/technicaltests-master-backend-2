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
    public class CommentairesController : ControllerBase
    {
        private readonly TodoContext _context;

        public CommentairesController(TodoContext context)
        {
            _context = context;
        }

        [Route("~/api/GetCommentaire")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commentaire>>> GetCommentaires()
        {
            return await _context.Commentaires.ToListAsync();
        }

        [Route("~/api/GetCommentaire/{id}")]
        [HttpGet]
        public async Task<ActionResult<Commentaire>> GetCommentaire(long id)
        {
            var commentaire = await _context.Commentaires.FindAsync(id);

            if (commentaire == null)
            {
                return NotFound();
            }

            return commentaire;
        }

        [Route("~/api/UpdateCommentaire/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutCommentaire(long id, Commentaire commentaire)
        {
            if (id != commentaire.CommentaireId)
            {
                return BadRequest();
            }

            _context.Entry(commentaire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentaireExists(id))
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

        [Route("~/api/AddCommentaire")]
        [HttpPost]
        public async Task<ActionResult<Commentaire>> PostCommentaire(Commentaire commentaire)
        {
           
            _context.Commentaires.Add(commentaire);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommentaire", new { id = commentaire.CommentaireId }, commentaire);
        }

        [Route("~/api/DeleteCommentaire/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCommentaire(long id)
        {
            var commentaire = await _context.Commentaires.FindAsync(id);
            if (commentaire == null)
            {
                return NotFound();
            }

            _context.Commentaires.Remove(commentaire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentaireExists(long id)
        {
            return _context.Commentaires.Any(e => e.CommentaireId == id);
        }
    }
}
