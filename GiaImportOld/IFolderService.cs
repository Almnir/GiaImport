using System;
using System.IO;
using RBD.Client.BL;
using RBD.Common.Enums;

namespace RBD.Client.Interfaces
{
	public interface IFolderService
	{
		DirectoryInfo TempFolder { get; }
        DirectoryInfo TempReportImageFolder { get; }
        DirectoryInfo TempImageFolder { get; }
		DirectoryInfo ValidatorFolder { get; }
		//DirectoryInfo CurrentExportTempFolder(KeyCode key);
		DirectoryInfo CurrentExportDictionaryFolder();
		DirectoryInfo CSVExportDictionaryFolder();
		DirectoryInfo CSVExportEntityFolder(string entity);
		DirectoryInfo SettingsFolder { get; }
	    FileInfo UserSettingsFile { get; }
	    void SetBaseName(string baseName);

		string VersionFileName { get; }
		string DictionaryFileName { get; }
		string KeyFileName { get; }

		//string KeyFileFolder(KeyCode key);
		string DictionaryDataFileFolder { get; }
		string LocalBackupFolder { get; }
        string CSVExportDestFileName(Guid ppeId, int code, string examDate, DistribType distribType);
		//string DestinationFile(string baseFolder, KeyCode key);
  //      string DestinationFile(string baseFolder, KeyCode key, Guid? areaId);
  //      string DestinationFileName(string baseFolder, KeyCode key, DistribType distribType);
  //      string DestinationFileName(string baseFolder, KeyCode key, Guid? areaId, DistribType distribType);
		string TempFolderPath { get; }
		string FCTFolderPath { get; }
		string GiaLicense { get; }
		string SborDataFileName { get; }
		//string SborDataFileFolder(KeyCode keyCode);
		//string PlanningDataFileFolder(KeyCode keyCode);
		string VersionFileFolder { get; }
		string VersionImportFileFolder { get; }
		void ClearTempFolder();
		bool VersionFileExists { get; }
		string UpdateUpdaterVersion { get; }
		string PlanningDataEntityFileName { get; }
		void CreateDistributive(string distributivePath);
		FileInfo CreateTempFile();
		void DeleteTempFile(string FullFileName);

		void ClearUpdateFolder();
		bool SaveFile(string fileName, byte[] data);
		bool CanWriteAppPath();
	    string StationReportPath { get; }
	    void ClearStationReportPath();
        FileInfo GetNewTempFileName();
	    void ClearTempImageFolder();
	}
}