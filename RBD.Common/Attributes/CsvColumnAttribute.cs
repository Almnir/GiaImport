using System;
using System.Globalization;
using RBD.Common.Enums;

namespace LINQtoCSV
{
    /// <summary>
    /// Summary description for CsvColumnAttribute
    /// </summary>
    [AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property, AllowMultiple = false)]
    public class CsvColumnAttribute : Attribute
    {
        public const int mc_DefaultFieldIndex = Int32.MaxValue;

        public string Name { get; set; }
        public bool CanBeNull { get; set; }
        public int FieldIndex { get; set; }
        public NumberStyles NumberStyle { get; set; }
        public string OutputFormat { get; set; }
		public RBDExtensions RBDExt { get; set; }

        public CsvColumnAttribute()
        {
        	RBDExt = RBDExtensions.None;
            Name = string.Empty;
            FieldIndex = mc_DefaultFieldIndex;
            CanBeNull = true;
            NumberStyle = NumberStyles.Any;
            OutputFormat = "G";
        }

        public CsvColumnAttribute(
                    string name, 
                    int fieldIndex, 
                    bool canBeNull,
                    string outputFormat,
                    NumberStyles numberStyle)
        {
            Name = name;
            FieldIndex = fieldIndex;
            CanBeNull = canBeNull;
            NumberStyle = numberStyle;
            OutputFormat = outputFormat;
        }
    }
}
