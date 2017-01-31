using System;
using System.ComponentModel;
using System.Xml.Serialization;
using FCT.Client.Dto.Interfaces;
using LINQtoCSV;

namespace FCT.Client.Dto
{
    [Serializable]
    [Description("Схема проведения экзамена по Химии")]
    public class RegionSettingsDto : DtoCreateDateBase, IEquatable<RegionSettingsDto>
    {
        [CsvColumn(Name = "Уникальный идентификатор настройки", FieldIndex = 1)]
        public override Guid DtoID { get; set; }

        [CsvColumn(Name = "Имя настройки", FieldIndex = 2)]
        public string SettingName { get; set; }

        [CsvColumn(Name = "Значение настройки", FieldIndex = 3)]
        public string Value { get; set; }

        [XmlElement]
        [CsvColumn(Name = "Код региона", FieldIndex = 4)]
        public override int Region { get; set; }

        [CsvColumn(Name = "Время создания настройки", FieldIndex = 5, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime CreateDate
        {
            get { return base.CreateDate; }
            set { base.CreateDate = value; }
        }

        [CsvColumn(Name = "Время изменения настройки", FieldIndex = 6, OutputFormat = "dd.MM.yyyy HH:mm:ss")]
        public override DateTime UpdateDate
        {
            get { return base.UpdateDate; }
            set { base.UpdateDate = value; }
        }

        #region IEquatable<RegionsDto> Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(RegionSettingsDto)) return false;
            return Equals((RegionSettingsDto)obj);
        }

        public bool Equals(RegionSettingsDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return 
                other.Region == Region &&
                other.SettingName == SettingName;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result * 37 + Region.GetHashCode();
                result = result * 37 + SettingName.GetHashCode();
                return result;
            }
        }

        /// <summary>
        /// Сравнение объектов по полям
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            var other = obj as RegionSettingsDto;
            if (other == null) return -1;

            ClearChanges();

            bool result = true;
            result &= CheckChanges(other.Region == Region, "Код региона");
            result &= CheckChanges(StringEquals(other.SettingName, SettingName), "Имя настройки");
            result &= CheckChanges(StringEquals(other.Value, Value), "Значение настройки");
            return result ? 0 : 1;
        }

        #endregion
    }
}
