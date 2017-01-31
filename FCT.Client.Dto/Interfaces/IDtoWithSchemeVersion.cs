namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithSchemeVersion
    {
        decimal SchemeVersionID { get; set; }
        SchemeVersionsDto SchemeVersionDto { get; set; }
    }
}