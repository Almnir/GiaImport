using RBD.Client.Interfaces;
using RBD.Client.Services.Import.Common.Entities.ImportEntities;
using RBD.Common.Enums;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDto : IDtoProcessorAccepter
    {
        bool IsDeleted { get; set; }
        string SourceTypeName { get; set; }
        string DtoName { get; }
    }
}
