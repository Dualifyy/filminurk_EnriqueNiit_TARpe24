using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Dto
{
    public class ApplicationUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool ProfileType { get; set; }
        public List<Guid>? FavouriteListIDs { get; set; }
        public List<Guid>? CommentIDs { get; set; }
        public string? AvatarImageID { get; set; }
        public string DisplayName { get; set; }

        /* 2 õpilase poolt väljamõeldud andmevälja, tüüp on optionaliga */
        public DateTime? Birthdate { get; set; }
        public string? FavouriteMovie { get; set; }

        public int? Reputation { get; set; } = 0;
        public string? Signature { get; set; }
    }
}