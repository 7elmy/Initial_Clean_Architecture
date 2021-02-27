using Initial_Clean_Architecture.Data.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Runtime;
using Initial_Clean_Architecture.Data.Domain.Entities.Common;

namespace Initial_Clean_Architecture.Data.Domain.Entities
{
   public class AppUser : IdentityUser, ITrackableEntity
    {

        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string FamilyName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Log> Logs { get; set; }
    }
}
