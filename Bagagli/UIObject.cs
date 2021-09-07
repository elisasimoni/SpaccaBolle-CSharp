namespace dev.spaccabolle.ui
{

	
	public abstract class UIObject
	{

		
		protected internal float x, y;

		protected internal int larghezza, altezza;

		
		protected internal Rectangle bounds;

		
		protected internal bool sopra = false;

		
		public UIObject(float x, float y, int larghezza, int altezza)
		{
			this.x = x;
			this.y = y;
			this.larghezza = larghezza;
			this.altezza = altezza;
			bounds = new Rectangle((int) x, (int) y, larghezza, altezza);
		}

		public abstract void tick();

		
		public abstract void render(Graphics g);

		
		public abstract void onClick();

		public void onMouseMove(MouseEvent e)
		{
			if (bounds.contains(e.getX(), e.getY()))
			{
				sopra = true;
			}
			else
			{
				sopra = false;
			}
		}

		public void onMouseRelease(MouseEvent e)
		{
			if (sopra)
			{
				onClick();
			}
		}

		
		public float X
		{
			get
			{
				return x;
			}
			set
			{
				this.x = value;
			}
		}


		
		public float Y
		{
			get
			{
				return y;
			}
			set
			{
				y = value;
			}
		}
		public int Width
		{
			get
			{
				return larghezza;
			}
			set
			{
				larghezza = value;
			}
		}

		public int Height
		{
			get
			{
				return altezza;
			}
			set
			{
				altezza = value;
			}
		}


		
		public bool Hovering
		{
			get
			{
				return sopra;
			}
			set
			{
				sopra = value;
			}
		}


	}
}