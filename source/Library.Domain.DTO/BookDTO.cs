using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDCore.Domain;

namespace Library.Domain.DTO
{
    public class BookDTO
    {

        public System.Int64 BookId { get; internal set; }
        public System.DateTime CreatedAt { get; set; }
        public EnumBookStatus BookStatusId { get; set; }
        public string Name { get; set; }
        public long OwnerId { get; set; }
        public string Author { get; set; }
        public EnumGenre? Genre { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDTO Owner { get; set; }

    }
}
