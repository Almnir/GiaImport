using System;
using RBD.Common.Enums;

namespace RBD.Common.Interfaces
{
    public interface IConfigService
    {
        void AddLastLoggedUser(string username);
        string[] GetUserCollection();
        string AppConfigConnectionString { get; set; }
    	void AddExportExamSelected(int[] exams);
		int[] GetExportExamSelected { get; }

        int GetPagerValue(Type form);
        void SavePagerValue(Type getType, int pageSize);
		string ImportPath { get; set; }
		string ImportCSVPath { get; set; }
        void SetExportPath(string path, MainTreeType reciever);
        string GetExportPath(MainTreeType reciever);
		string ExportDistribPath { get; set; }
    	bool IsDistribGia9 { get; }
        bool PassMoscowMode(int regionCode);

        string FbsUser { get; set; }
        string FbsPassword { get; set; }

		ConnectionType ConnectionType { get; }

        bool AcceptPlanning { get; set; }
    	bool IsPlanningStage { get; }
		bool DebugMode { get; }
    	bool IsSecondWaveStage { get; }
    	bool IsConsolidationEnabled { get; }
    	DateTime? LastDateOfRegistrationOnMainWave { get; }
    	DateTime? LastDateOfRegistrationOnAdditionalWave { get; }
    	bool MssqlConnection { get; }
		bool IsSeatingStage { get; }

		string TechPortalAddress { get; set; }
    	string TechPortalUser { get; set; }
		string TechPortalPassword { get; set; }
		string UpdateStoragePath { get; set; }
    	bool AutomaticDownloadUpdate { get; set; }
		int UpdatePeriodicTime { get; set; }
		int UpdateAtTime { get; set; }
		bool UpdateAtTimeEnabled { get; set; }
		bool UpdateStartupEnabled { get; set; }
		bool UpdatePeriodicEnabled { get; set; }
		bool TechPortalEnabled { get; }
        bool ZayavkiRcoiEnabled { get; }
        bool NewMachineReportGenerator { get; set; }
        int PrintPullValue { get; set; }
        bool NotDuplexPrintCheck { get; set; }
        int GveMaxFileSize { get; }

        bool ExportWithSectionByArea(int regionCode);
        bool PrescheduleWaveAvailable(int regionCode);
        bool CertificateEnabled { get; }
        bool AbbyyAuthorization { get; }
        bool BigBrother { get; }
        DistribType DistribType { get; }
        bool HidePlanningWorkersOnVoiceExams { get; }
        bool PrintOpusBlankWithScale { get; }
        bool EnabledVoicePlanning { get; }
        bool GveReportVisible { get; }
    }
}
