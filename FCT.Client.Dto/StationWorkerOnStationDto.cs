using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;
using RBD;
using RBD.Common.Enums;

namespace FCT.Client.Dto
{
	[Serializable][Description("Назначение работника в ППЭ")]
    [BulkTable("rbd_StationWorkerOnStation", "StationWorkerOnStation", RootTagName = "ArrayOfStationWorkerOnStationDto", ExportExclude = true)]
    public class StationWorkerOnStationDto : DtoCreateDateBase, IEquatable<StationWorkerOnStationDto>, IDtoWithStationWorker, IDtoWithStation, 
        IDtoWithWorkerPosition, IUidableDto, IDtoCollectorAccepter
	{
        [BulkColumn("Region")]
        [XmlElement]
        public override int Region { get; set; }

        [BulkColumn("StationWorkerOnStationID")]
	    public override Guid DtoID { get; set; }

        [BulkColumn("StationId")]
        [CsvColumn(Name = "Guid ППЭ", FieldIndex = 2)]
		public Guid Station { get; set; }

        [BulkColumn("StationWorkerId")]
		[CsvColumn(Name = "Guid работника ППЭ", FieldIndex = 1)]
		public Guid StationWorker { get; set; }

        [BulkColumn("WorkerType")]
        [CsvColumn(Name = "WorkerType", FieldIndex = 3)]
		[XmlIgnore] public WorkerType WorkerType { get; set; }

#if GiaDataCollect
        [XmlElement("SWorkerPosition")]
#endif
        [BulkColumn("SWorkerPositionID")]
        [CsvColumn(Name = "Код должности в ППЭ", FieldIndex = 4)]
		public int WorkerPositionCode { get; set; }

        [BulkColumn("CreateDate")]
        [CsvColumn(Name = "Дата-время создания", FieldIndex = 5, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate
        {
            get { return base.CreateDate; }
            set { base.CreateDate = value; }
        }

        [BulkColumn("UpdateDate")]
        [CsvColumn(Name = "Дата-время изменения", FieldIndex = 6, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime UpdateDate
        {
            get { return base.UpdateDate; }
            set { base.UpdateDate = value; }
        }

        #region XmlIgnore

	    public int WorkerTypeProperty
	    {
	        set { WorkerType = (WorkerType)value; } 
            get { return (int) WorkerType; }
	    }

        [XmlIgnore] public StationWorkersDto StationWorkerDto { get; set; }
        [XmlIgnore] public StationsDto StationDto { get; set; }
        [XmlIgnore] public SWorkerPositionsDto WorkerPositionDto { get; set; }

	    public int WorkerPositionId { get { return WorkerPositionDto.Return(x => x.SWorkerPositionID, -1); } set {} }

        [Description("Работник ППЭ")] 
        [XmlIgnore] public string StationWorkerName { get { return StationWorkerDto.Return(x => x.FIO, "---"); } }

        [Description("ППЭ")]
        [XmlIgnore] public string StationName { get { return StationDto.Return(x => x.ToString(), "---"); } }

        [Description("Должность")]
        [XmlIgnore] public string WorkerPositionName  { get { return WorkerPositionDto.Return(x => x.SWorkerPositionName, "---"); } }

        [Description("Тип регистрации")]
        [XmlIgnore] public string WorkerTypeName { get { return WorkerType == WorkerType.WorkerOnStation ? "ППЭ" : "Экзамен"; } }

        #endregion

        #region IEquatable<StationWorkerOnStationDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (StationWorkerOnStationDto)) return false;
            return Equals((StationWorkerOnStationDto) obj);
        }

        public bool Equals(StationWorkerOnStationDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.StationWorker.Equals(StationWorker) && 
                other.Station.Equals(Station) && 
                other.Region == Region;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result*37 + StationWorker.ToString().GetHashCode();
                result = result*37 + Station.ToString().GetHashCode();
                result = result*37 + Region.GetHashCode();
                return result;
            }
        }

	    public void Collect(IDtoDataCollector collector)
	    {
	        collector.Collect(this);
	    }

	    public override int CompareTo(object obj)
        {
            var other = obj as StationWorkerOnStationDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;

            result &= CheckChanges(other.Station.Equals(Station), "ППЭ");
            result &= CheckChanges(other.StationWorker.Equals(StationWorker), "Работник");
            result &= CheckChanges(Equals(other.WorkerType, WorkerType), "Тип работника");
            result &= CheckChanges(other.WorkerPositionCode == WorkerPositionCode, "Должность");
            result &= CheckChanges(other.Region == Region, "Регион");
            result &= CheckChanges(other.IsDeleted == IsDeleted, "Удалено");

            return result ? 0 : 1;               
        }

        #endregion

        #region GiaDataCollect Fields

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string UID
        {
            get { return string.Format("({0}, {1})", StationWorkerUID, StationUID); }
            set { }
        }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string StationUID { get; set; }

#if !GiaDataCollect
        [XmlIgnore]
#endif
        public string StationWorkerUID { get; set; }

        #endregion

        public override T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
	}
}
