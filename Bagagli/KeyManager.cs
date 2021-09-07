using System;

namespace dev.spaccabolle.input
{

	public class KeyManager : KeyListener
	{

		private bool[] keys;

		public static bool Up,
			Down,
			Left,
			Right,
			Enter,
			Pause,
			Exit,
			Easy,
			Normal,
			Hard,
			Space,
			Home,
			Restart,
			Save,
			Yes,
			No;

		
		public KeyManager()
		{
			keys = new bool[256];
		}

		public void Tick()
		{
			up = keys[KeyEvent.VK_W];
			down = keys[KeyEvent.VK_S];
			left = keys[KeyEvent.VK_A];
			right = keys[KeyEvent.VK_D];
			enter = keys[KeyEvent.VK_ENTER];
			space = keys[KeyEvent.VK_SPACE];
			pause = keys[KeyEvent.VK_P]; 
			exit = keys[KeyEvent.VK_E]; 
			easy = keys[KeyEvent.VK_1]; 
			normal = keys[KeyEvent.VK_2]; 
			hard = keys[KeyEvent.VK_3]; 
			home = keys[KeyEvent.VK_H];
			save = keys[KeyEvent.VK_S];
			restart = keys[KeyEvent.VK_R];
			yes = keys[KeyEvent.VK_Y];
			no = keys[KeyEvent.VK_N];
		}

		public void KeyPressed(KeyEvent e)
		{
			keys[e.GetKeyCode()] = true;
		}
	
		public void KeyReleased(KeyEvent e)
		{
			keys[e.GetKeyCode()] = false;
		}

		public virtual void KeyTyped(KeyEvent e)
		{

		}

	}
}