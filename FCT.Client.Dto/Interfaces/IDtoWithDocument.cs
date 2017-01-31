namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithDocument
    {
        int DocumentTypeCode { get; set; }
        DocumentTypesDto DocumentTypeDto { get; set; }
    }
}
