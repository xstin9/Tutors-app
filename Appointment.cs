using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thusong_Tutors
{
    [Table("Appointment")]
    public class Appointment
    {
       
      
        public DateTime Date { get; set; }
        [Column("Module")]
        [MaxLength(50)]
        public string Module { get; set; }
        [Column("Tutor_name")]
        [MaxLength(50)]
        public string Tutor_name { get; set; }
        [Column("User_ID")]
        [MaxLength(50)]
        public int UserID { get; set; }
        
    }
}
