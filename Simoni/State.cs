using System.IO;
using System.Drawing;

namespace dev.spaccabolle.states
{

	using Handler = dev.spaccabolle.Handler;
	public abstract class State
	{

		private static State currentState = null;

		public static FileStream level;

		public static State State
		{
			set
			{
				currentState = value;
			}
			get
			{
				return currentState;
			}
		}

		protected internal Handler handler;

		public State(Handler handler)
		{
			this.handler = handler;
		}

		public abstract void Tick();

		public abstract void Render(System.Drawing g);

	}
}
