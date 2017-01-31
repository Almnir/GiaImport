using RBD.Common.Enums;

namespace RBD.Common.Interfaces
{
    public interface IControlMode
    {
        void SwitchToMode(ControlMode mode);
		ControlMode CurrentMode { get; }
    }
}
