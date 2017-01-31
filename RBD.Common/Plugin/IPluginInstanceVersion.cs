using System;
using RBD.Common.Enums;

namespace RBD.Common.Plugin
{
    public interface IPluginInstanceVersion
    {
        Guid Rcoi { get; set; }
        Guid MOUO { get; set; }
        Guid SCHOOL { get; set; }
        ConnectionType ConnectionType { get; set; }
        int Region { get; set; }
    }
}