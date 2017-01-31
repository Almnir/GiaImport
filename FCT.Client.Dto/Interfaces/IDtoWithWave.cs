namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithWave
    {
        int WaveCode { get; set; }
        WavesDto WaveDto { get; set; }
    }
}