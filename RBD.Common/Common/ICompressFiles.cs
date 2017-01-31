using System.IO;

namespace RBD.Client.Interfaces
{
	public interface ICompressFiles
	{
		byte[] Compress(string[] fileNames, string destinationFolder);
		byte[] Compress(string[] fileNames, string destinationFolder, string destinationFile);
		byte[] Compress(string[] fileNames, string destinationFolder, string destinationFile, string[] filesToKeep);
		bool UnZipFiles(string zipPathAndFile, string outputFolder);
	    bool UnZipFiles(Stream zipStream, string outputFolder);
	}
}
