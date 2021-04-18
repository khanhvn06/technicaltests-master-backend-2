using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TestProgrammationConformit.Models
{
    public class Evenement
    {
        public long Id { get; set; }
        public string Titre { get; set; }

        [StringLength(100, ErrorMessage = "The Description value cannot exceed 100 characters. ")] public string Description { get; set; }

        public string Personne { get; set; }

        public IList<Commentaire> Commentaires { get; set; }
    }

    public class Commentaire
    {
        public long CommentaireId { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public int EvenementId { get; set; }

        public Evenement Evenement { get; set; }
    }
}
