using System;

namespace FCT.Client.Dto.Interfaces
{
    public interface IDtoWithAddressDtoOnly
    {
        AddressDto AddressDto { get; set; }
    }
    
    public interface IDtoWithAddress : IDtoWithAddressDtoOnly
    {
        Guid Address { get; set; }
    }

    public interface IDtoWithAddressNullable : IDtoWithAddressDtoOnly
    {
        Guid? Address { get; set; }
    }
}