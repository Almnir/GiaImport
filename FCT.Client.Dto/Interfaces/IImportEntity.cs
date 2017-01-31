using System;
using System.Collections.Generic;
using FCT.Client.Dto.Interfaces;
using RBD.Client.Interfaces;
using RBD.Client.Services.Import.DataSource;
using RBD.Common.Enums;

namespace RBD.Client.Services.Import.Common.Entities.ImportEntities
{
    public interface IImportEntity
    {
        IDto IItem { get; set; }

        ImportGroup ImportGroup { get; }
        ExclusionType Exclusion { get; set; }
        DeletionType Deletion { get; set; }

        void ExcludeLeafs(ExclusionType exclusion, string message, bool boobled, bool exclude, Func<IImportEntity, bool> query);
        void ExcludeLeafs(ExclusionType exclusion, string message, bool boobled, bool exclude);
        void ProcessParents(ExclusionType exclusion, string message, bool exclude, Func<IImportEntity, bool> query);
        void ProcessParents(ExclusionType exclusion, string message, bool exclude, Func<IImportEntity, bool> query, bool lockInInterface);
        void ProcessParentsForced(ExclusionType exclusion, string message, bool exclude, Func<IImportEntity, bool> query);
        void ProcessParentsForced(ExclusionType exclusion, string message, bool exclude, Func<IImportEntity, bool> query, bool lockInInterface);

        Guid DtoId { get; set; }
        Guid IdInDb { get; set; }
        Guid GroupId { get; set; }
        string Name { get; }

        ImportSourceFile ImportSource { get; set; }

        bool IsNew { get; set; }
        bool IsDeleted { get; set; }
        bool Forced { get; set; }
        bool IsDeletedPermanently { get; set; }
        bool IsDeletedCorrection { get; set; }
        bool IsChanged { get; set; }
        bool IsBroken { get; set; }
        bool IsHidden { get; set; }
        bool IsLocked { get; set; }

        bool IsLockedInInterface { get; set; }

        bool IsCryticalFormat { get; set; }
        bool CryticalPlanningChanges { get; set; }
        bool IsObsolete { get; set; }
        bool IsExportedToPPE { get; set; }

        bool IsCodeNonUnique { get; set; }
        bool IsCodeChanged { get; set; }        
        bool DbEqualented { get; }

        bool HasErrors { get; }
        void AddError(string message, Exception exception);
        void AddWarning(string message, bool showInProgress);
        void AddInfo(string message);
        void RemoveInfo(string message);
        void AddCsvError(string message, int line);
        ICollection<string> GetDangerousMessages();

        string ErrorsParsed { get; }

        CrossmunitipalType CrossmunitipalType { get; set; }
        bool IsCrossmunitipal { get; }
        bool IsExcluded { get; set; }
        bool IsExcludedByExams { get; set; }

        bool IsExcludedByType { get; }
        bool IsExcludedByParent { get; set; }

        bool ReadyToImport { get; }
        bool ReadyToProcess { get; }
        bool ReadyToExclude { get; }

        bool HasHiddenDeletion { get; }


        bool IsDeletedByParent { get; }
        bool IsDeletedBySbor { get; }
        bool IsNotBrokenHidden { get; }
        bool IsMustBeLoad { get; }

        bool IsDisabled { get; }

        /// <summary>
        /// Обрабатываемый на 3-м шаге - искл объектов по типам объектов
        /// </summary>
        bool IsReadyForExclusionsByObjectsTypes { get; }
        bool IsAnyExcluded { get; }
        bool IsAnyBroken { get; }
        bool IsAnyDeleted { get; }

        int GiaUploadedItemsCount { get; set; }
    }
}
