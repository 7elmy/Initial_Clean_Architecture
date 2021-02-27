using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Initial_Clean_Architecture.Data.Domain.Interfaces
{
    public interface IDbConfig
    {
        void InstallDbConfig(ModelBuilder modelBuilder);
    }
}
