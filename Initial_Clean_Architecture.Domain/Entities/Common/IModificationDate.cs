using Initial_Clean_Architecture.Data.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Initial_Clean_Architecture.Data.Domain.Entities.Common
{
    public interface IModificationDate
    {
        DateTime ModificationDate { get; set; }
    }
}
