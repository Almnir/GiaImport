using System;
using System.ComponentModel;
using FCT.Client.Dto.Common;
using RBD;

namespace FCT.Client.Dto.Interfaces
{
    public abstract class DtoCreateDateBase : RegionDtoBase
    {
        private DateTime _createDate;
        [BulkColumn]
        [Description("Дата создания")]
        public virtual DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = DateTime.SpecifyKind(value.NotMillisecondsDateTime(), DateTimeKind.Unspecified); }
        }

        private DateTime _updateDate;
        [BulkColumn]
        [Description("Дата изменения")]
        public virtual DateTime UpdateDate
        {
            get { return _updateDate; }
            set { _updateDate = DateTime.SpecifyKind(value.NotMillisecondsDateTime(), DateTimeKind.Unspecified); }
        }

        private DateTime _importCreateDate;
        [BulkColumn]
        public virtual DateTime ImportCreateDate
        {
            get { return _importCreateDate; }
            set { _importCreateDate = DateTime.SpecifyKind(value.NotMillisecondsDateTime(), DateTimeKind.Unspecified); }
        }

        private DateTime _importUpdateDate;
        [BulkColumn]
        public virtual DateTime ImportUpdateDate
        {
            get { return _importUpdateDate; }
            set { _importUpdateDate = DateTime.SpecifyKind(value.NotMillisecondsDateTime(), DateTimeKind.Unspecified); }
        }
    }
}
