using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using RBD.Common.Enums;

namespace FCT.Client.Dto
{
    [Serializable][Description("Шаблоны поиска")]
    public class SearchTemplatesDto : DtoBase, IEquatable<SearchTemplatesDto>
    {
        public Guid SearchTemplateId { get; set; }
		public SearchTemplateType SearchTemplateType { get; set; }
        public string SearchTemplateCode { get; set; }
        public string SearchTemplateName { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid? UpdateUserId { get; set; }
        public MainTreeType MainTreeType { get; set; }
        public byte[] Template { get; set; }

        #region IEquatable<OrganizationRolesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SearchTemplatesDto)) return false;
            return Equals((SearchTemplatesDto) obj);
        }

        public bool Equals(SearchTemplatesDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.SearchTemplateId.Equals(SearchTemplateId);
        }

        public override int GetHashCode()
        {
            unchecked { return SearchTemplateId.GetHashCode(); } 
        }

        public override int CompareTo(object obj)
        {
            var other = obj as SearchTemplatesDto;
            if (other == null) return -1;

            ClearChanges();

            var result = true;

            result &= CheckChanges(other.SearchTemplateType == SearchTemplateType, "SearchTemplateType");
            result &= CheckChanges(StringEquals(other.SearchTemplateCode, SearchTemplateCode), "SearchTemplateCode");
            result &= CheckChanges(StringEquals(other.SearchTemplateName, SearchTemplateName), "SearchTemplateName");
            result &= CheckChanges(other.UpdateDate == UpdateDate, "UpdateDate");
            result &= CheckChanges(other.UpdateUserId == UpdateUserId, "UpdateUserId");
            result &= CheckChanges(other.MainTreeType == MainTreeType, "MainTreeType");
            result &= CheckChanges(other.Template == Template, "Template");
            
            return result ? 0 : 1;
        }

        #endregion
    }
}
