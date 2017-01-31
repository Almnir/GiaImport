using System;
using System.IO;
using RBD.Common.Enums;

namespace RBD.Client.Services.Import.DataSource
{
    /// <summary>
    /// Источник импорта - файл, его характеристики
    /// </summary>
    public class ImportSourceFile
    {
        public ImportSourceFile(Guid id, string name, SourceType type)
        {
            Id = id;
        	Name = name;
            SourceType = type;
        }

		public ImportSourceFile(Guid id, FileInfo file, SourceType type)
			: this(id, file.Name, type)
		{
			File = file;
		}

        /// <summary>
        /// Идентифиукатор ресурса
        /// </summary>
        public Guid Id { get; set; }
        public SourceType SourceType { get; set; }

    	/// <summary>
    	/// Наименование источника данных
    	/// </summary>
    	//public string Name { get; set; }
		public string Name { get; set; }

        /// <summary>
        /// Файл поврежден и не мог быть десериализован
        /// </summary>
        public bool IsBroken { get; set; }

		/// <summary> Файл импорта </summary>
		public FileInfo File { get; set; }
    }
}
