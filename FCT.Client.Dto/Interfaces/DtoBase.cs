using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using RBD;
using RBD.Client.Services.Import.Common.Entities.ImportEntities;
using RBD.Common.Enums;

namespace FCT.Client.Dto.Interfaces
{
    public abstract class DtoBase : IDto, IComparable, ICloneable
    {
        #region Properties

        public virtual Guid DtoID { get; set; }
        [XmlIgnore] public virtual Guid IdInDb { get; set; }
        [XmlIgnore] public virtual bool IsNew { get; set; }
        [XmlIgnore] public List<string> Errors { get; set; }

        List<string> _changes = new List<string>();
        [XmlIgnore] public List<string> Changes { get { return _changes; } }

        [XmlIgnore] public virtual ImportGroup ImportGroup { get { return ImportGroup.Sbor; } }

        #endregion

        public DtoBase()
        {
            DtoID = Guid.NewGuid();
            Errors = new List<string>();
        }

        public bool CheckChanges(bool equals, string fieldName)
        {
            if (!equals) Changes.Add(fieldName);
            return equals;
        }

        public void ClearChanges()
        {
            Changes.Clear();
        }

        public string GetChanges()
        {
            if (Changes.Count == 0) return string.Empty;
            return "Изменения в полях:\n\t\t- " + string.Join("\n\t\t- ", Changes.ToArray());
        }

        public bool StringEquals(string obj1, string obj2)
        {
            if (string.IsNullOrEmpty(obj1) && string.IsNullOrEmpty(obj2))
                return true;

            obj1 = obj1 ?? string.Empty;
            obj2 = obj2 ?? string.Empty;

            return obj1.Trim().Equals(obj2.Trim(), StringComparison.InvariantCultureIgnoreCase);
        }

        #region Механизм исключения

        public delegate void ExcludeDelegate(ExclusionType exclusion,
            string message, bool boobled, bool exclude, Func<IImportEntity, bool> query);
        public event ExcludeDelegate OnExclude = delegate { };
        public event Action<ExclusionType, string, bool, Func<IImportEntity, bool>> OnExcludeEntity = delegate { };

        public virtual void ExcludeLeafs(ExclusionType exclusion,
            string message, bool boobled, bool exclude, bool procesLeafs, Func<IImportEntity, bool> query)
        {
            /* Если исключаем по dto - поднимаем сообщение вверх до IImportEntity */
            if (boobled)
                OnExcludeEntity(exclusion, message, exclude, query);

            /* Пробрасываем дальше */
            if (procesLeafs)
                OnExclude(exclusion, message, true, exclude, query);
        }

        public virtual void ExcludeLeafs(ExclusionType exclusion,
            string message, bool boobled, bool exclude, Func<IImportEntity, bool> query)
        {
            ExcludeLeafs(exclusion, message, boobled, exclude, true, query);
        }

        public virtual void ExcludeLeafs(ExclusionType exclusion,
            string message, bool boobled, bool exclude)
        {
            ExcludeLeafs(exclusion, message, boobled, exclude, true, null);
        }

        #endregion;

        [Description("Удалено")]
        [XmlIgnore] public virtual bool IsDeleted { get; set; }

        [Description("Источник данных")]
        [XmlIgnore] public virtual string SourceTypeName { get; set; }

        [XmlIgnore] public string DtoName { get { return GetType().GetDescription(); } }

        public virtual int CompareTo(object obj) { return 0; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public virtual T Visit<T>(IDtoProcessVisitor<T> visitor)
        {
            throw new NotImplementedException("IDtoProcessVisitor.Visit");
        }
    }
}
