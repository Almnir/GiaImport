using System;
using System.Collections.Generic;
using RBD.Client.Services.Import.Common.Entities.ImportEntities;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDuplicated : IImportEntity
    {
        Guid GroupId { get; set; }
        string GroupTitle { get; set; }
        bool IsMainEntity { get; set; }
        bool IsDuplicate { get; set; }
        List<IDuplicated> Duplicates { get; set; }
    }
}
