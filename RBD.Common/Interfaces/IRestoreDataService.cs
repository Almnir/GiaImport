using System;
using RBD.Common.EventArgs;

namespace RBD.Common.Interfaces
{
	public interface IRestoreDataService
	{
		void Restore(int item);

		event EventHandler<CustomEventArgs<string>> Message;
		event EventHandler RestoreSuccess;
		event EventHandler RestoreFailed;
	}
}
