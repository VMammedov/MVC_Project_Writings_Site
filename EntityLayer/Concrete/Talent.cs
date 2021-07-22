using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Talent
    {
        [Key]
        public int TalentID { get; set; }

        [Required]
        [StringLength(50)]
        public string TalentName { get; set; }

        [StringLength(250)]
        public string TalentDetail { get; set; }

        [Required]
        public int TalentPoint { get; set; }

        public bool TalentStatus { get; set; }
    }
}
