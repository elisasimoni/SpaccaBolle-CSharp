namespace dev.spaccabolle.ui
{
	public class UIImageButton : UIObject
	{
		private BufferedImage[] images;

		public ClickListener clicker;

		public UIImageButton(float x, float y, int width, int height, BufferedImage[] images, ClickListener clicker) : base(x, y, width, height)
		{
	
			this.immagini = images;
			this.clicker = clicker;
		}

		public virtual void Tick()
		{
		}

		public virtual void Render(Graphics g)
		{
			if (sopra)
			{
				g.DrawImage(images[1], (int) x, (int) y, larghezza, altezza, null);
			}
			else
			{
				g.DrawImage(images[0], (int) x, (int) y, larghezza, altezza, null);
			}
		}

		public virtual void OnClick()
		{
			clicker.OnClick();
		}

	}
}