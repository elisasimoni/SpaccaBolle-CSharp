using System;
using System.Collections.Generic;

namespace dev.spaccabolle.ui
{

	using Handler = dev.spaccabolle.Handler;

	public class UiManager
	{
		
		private Handler handler;

		private List<UIObject> objects;

		private const int BtnLimits = 600;


		public UiManager(Handler handler)
		{
			this.handler = handler;
			objects = new List<UIObject>();
		}
		
		public virtual void Tick()
		{
			foreach (UIObject o in objects)
			{
				o.Tick();
				if (o.y > BtnLimits)
				{
					o.y -= 2;
					o.bounds.y = (int) o.y;
				}
			}
		}
		
		public virtual void Render(Graphics g)
		{
			foreach (UIObject o in objects)
			{
				o.Render(g);
			}
		}

		
		public virtual void OnMouseMove(MouseEvent e)
		{
			foreach (UIObject o in objects)
			{
				o.OnMouseMove(e);
			}
		}
		
		public virtual void OnMouseRelease(MouseEvent e)
		{
			foreach (UIObject o in objects)
			{
				o.OnMouseRelease(e);
			}
		}
		
		public virtual void AddObject(UIObject o)
		{
			Console.WriteLine();
			objects.Add(o);
		}

		
		public virtual void RemoveObject(UIObject o)
		{
			objects.Remove(o);
		}

		
		public virtual Handler Handler
		{
			get
			{
				return handler;
			}
			set
			{
				handler = value;
			}
		}
		
		public virtual List<UIObject> Objects
		{
			get
			{
				return objects;
			}
			set
			{
				objects = value;
			}
		}


	}
}