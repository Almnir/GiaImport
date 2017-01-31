namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithRegion
    {
        int Region { get; set; }
        RegionsDto RegionDto { get; set; }
    }
}