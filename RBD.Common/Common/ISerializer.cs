using System;
using System.IO;

namespace RBD.Client.Interfaces
{
	public interface ISerializer
	{
	    void Serialize<T>(string filePath, T data) where T : class;
	    byte[] Serialize<T>(T data) where T : class;
        string Serialize<T>(T[] dtos) where T : class;
	    T Deserialize<T>(string filePath) where T : class;
	    T DeserializeFromText<T>(string text) where T : class;
	    T Deserialize<T>(byte[] bytes) where T : class;
	    T Deserialize<T>(string filePath, bool isEncryptionUsed) where T : class;
	    void Serialize<T>(string filePath, T data, bool isEncryptionUsed) where T : class;
        object Deserialize(MemoryStream ms, Type dtoType);
	}
}
