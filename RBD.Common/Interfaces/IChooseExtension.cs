namespace RBD.Common.Interfaces
{
	public interface IChooseExtension
	{
		bool Choose { get; set; }
	}

	public interface IChooseLockedExtension : IChooseExtension
	{
		bool ChooseLocked { get; }
	}
}
