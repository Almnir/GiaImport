using System.Drawing;

namespace RBD.Common.EventArgs
{
    public class CancelHandler : System.EventArgs
    {
        public CancelHandler(bool argument)
        {
            Cancel = argument;
        }

        public bool Cancel { get; set; }
    }

    public class CustomEventArgs<T> : System.EventArgs
    {
        public CustomEventArgs(T argument)
        {
            Argument = argument;
        }

        public T Argument { get; private set; }
    }

    public class CustomEventArgsSecondArgument<T, U> : CustomEventArgs<T>
    {
        public CustomEventArgsSecondArgument(T argument, U secondArgument)
            : base(argument)
        {
            SecondArgument = secondArgument;
        }

        public U SecondArgument { get; private set; }
    }

    public class ExportEvent : System.EventArgs
    {
        public ExportEvent(int row, int process)
        {
            Row = row;
            Process = process;
        }

        public int Row { get; private set; }
        public int Process { get; private set; }
    }

    public class ExportColorEvent : System.EventArgs
    {
        public ExportColorEvent(int row, Color color)
        {
            Row = row;
            Color = color;
        }

        public int Row { get; private set; }
        public Color Color { get; private set; }
    }

    public class CustomHandlerOutValue<T, U> : CustomHandlerOutValue<U>
    {
        public CustomHandlerOutValue(T argument)
        {
            Argument = argument;
        }

        public T Argument { get; private set; }
    }

    public class CustomHandlerOutValue<U> : System.EventArgs
    {
        public CustomHandlerOutValue()
        {
            Result = default(U);
        }

        public U Result { get; set; }
    }

    public class CustomEventArgsMessage<T> : CustomEventArgs<T>
    {
        public CustomEventArgsMessage(T argument, string name, int count) : base(argument)
        {
            Name = name;
            Count = count;
        }

        public string Name { get; private set; }
        public int Count { get; private set; }
    }

    public delegate T ReturnStringValue<T>(object sender, System.EventArgs args);

	public class ResultEventArgs : System.EventArgs
	{
		public ResultEventArgs()
		{
			Result = true;
		}
		public bool Result { get; set; }

		public static new ResultEventArgs Empty = new ResultEventArgs();
}

	public class CustomEventArgs : System.EventArgs
	{
		public CustomEventArgs(object value)
		{
			Value = value;
		}
		public object Value { get; set; }
	}

}