using System;
using System.ComponentModel;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable][Description("Роли организаторов вне аудитории")]
    public class OrganizationRolesDto : DtoBase, IEquatable<OrganizationRolesDto>
    {
        public override Guid DtoID
        {
            get { return OrgRoleID; }
            set { OrgRoleID = value; }
        }

        [Description("Удалено")]
		public override bool IsDeleted { get; set; }

        public int Region { get; set; }
        public Guid OrgRoleID { get; set; }
        
		[CsvColumn(Name = "Код", FieldIndex = 1)]
        [Description("Код")]
		public int OrgRoleCode { get; set; }

		[CsvColumn(Name = "Наименование", FieldIndex = 2)]
        [Description("Наименование")]
		public string OrgRoleName { get; set; }

        #region IEquatable<OrganizationRolesDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (OrganizationRolesDto)) return false;
            return Equals((OrganizationRolesDto) obj);
        }

	    public bool Equals(OrganizationRolesDto other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;
	        return other.OrgRoleID == OrgRoleID;
	    }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
                int result = 17;
                result = result*37 + OrgRoleID.ToString().GetHashCode();
                return result;	            
	        }
        }

        public override int CompareTo(object obj)
        {
            var other = obj as OrganizationRolesDto;
            if (other == null) return -1;

            ClearChanges();

            var result = true;

            result &= CheckChanges(other.OrgRoleID.Equals(OrgRoleID), "Id");
            result &= CheckChanges(other.IsDeleted.Equals(IsDeleted), "Удалено");
            result &= CheckChanges(other.OrgRoleCode == OrgRoleCode, "Код");
            result &= CheckChanges(StringEquals(other.OrgRoleName, OrgRoleName), "Наименование");
            result &= CheckChanges(other.Region == Region, "Регион");

            return result ? 0 : 1;
        }

        #endregion
    }
}
