using System;
using FCT.Client.Dto.Common;

namespace RBD.Client.Interfaces
{
	public interface IKeyCode
	{
		int RegionCode { get; set;}
		GovernmentKeyDto MOYO { get; set; }
		SchoolKeyDto OY { get; set; }
		
        Guid Region { get; set; }
        Guid MOYOId { get; }
        Guid OYId { get; }

		bool IsRcoi { get; }
		bool IsMoyo { get; }
        bool IsSchool { get; }
        bool IsEmpty { get; }

		string GiaLicense { get; }
        bool IsGiaDistrib { get; }
	    bool IsZoo { get; }
		bool Load(IKeyCode key);
	}
}
