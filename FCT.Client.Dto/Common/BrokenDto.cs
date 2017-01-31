using FCT.Client.Dto.Interfaces;

namespace FCT.Client.Dto.Common
{
    public class BrokenDto
    {
        public DtoBase Dto { get; set; }
        public string ErrorMessage { get; set; }
        public ExcludeTypeEnum ExcludeType { get; set; }

        public BrokenDto(DtoBase dto, ExcludeTypeEnum t, string message, params object[] messageArgs)
        {
            Dto = dto;
            ErrorMessage = string.Format(message, messageArgs);
            ExcludeType = t;
        }

        public override string ToString()
        {
            var uidable = Dto as IUidableDto;
            if (uidable != null) return string.Format("{0} ({1}) - {2}", Dto.DtoName, uidable.UID, ErrorMessage);
            return string.Format("{0} - {1}", Dto.DtoName, ErrorMessage);
        }

        public enum ExcludeTypeEnum
        {
            Broken,
            Excluded
        }
    }
}
