using System;
using System.Collections.Generic;
using RBD.Common.EventArgs;

namespace RBD.Common.Interfaces
{
	public interface IValidatorChecker
	{
        void CheckData();
        event EventHandler<CustomEventArgs<IList<int>>> HasError;
		void DoCorrectDB();
	}
}
