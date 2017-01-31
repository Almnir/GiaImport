using System;

namespace RBD.Common.Interfaces
{
	public interface IAuditInformation
	{
		DateTime CreateDate { get; set; }
		DateTime UpdateDate { get; set; }
	}
}
