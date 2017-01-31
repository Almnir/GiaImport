namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithSubject
    {
        int SubjectCode { get; set; }
        SubjectsDto SubjectDto { get; set; }
    }
}