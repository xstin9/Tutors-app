using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    [Table("User")]
    public class User
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        [Column("Name")]
        [MaxLength(50)]

        public string Name { get; set; }
        [MaxLength(50)]

        [Column("Surname")]
        public string Surname { get; set; }
        [MaxLength(50)]

        [Column("Email")]
        public string Email { get; set; }

        [Column("HashedPassword")]
        [MaxLength(50)]
        public string HashedPassword {  get; set; }
        [Column("Uni")]
        [MaxLength(50)]
        public string Uni { get; set; }

    }
}
