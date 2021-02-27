using System;
using System.Collections.Generic;
using System.Text;

namespace Initial_Clean_Architecture.Data.Domain.Entities.Common
{
    public interface ITrackableEntity : ICreationDate, IModificationDate, IIsDeleted
    {

    }
}
