using System;
using System.Collections.Generic;
using System.Text;
using System.Io;
using System.Drawing;

namespace Sofia_Bagagli_C_Sharp
{
    class StateMenu : State
	{
		private static const int yLogoLimits = 30, xDragonLimits = 345, yDragonLimits = 445;
		private static const int LIMITS = 15;
		public static int yMovelogo = -430;
		public static int dimDragon = 0;
		public static int xDragon = 800;
		public static int yDragon = 445;
		public static UIManager uiManager;
		public static bool run = false;
		public static bool load = false;
		public static bool home = false;
		public static bool isLoadGame = false;
		public static  FileStream loadGame;
		public static  FileStream saveGame = new File.Create(filePath+"/../src/res/map/save.txt");
		private int yMove = -1;
		private int YMoveButton = 840;
		public static System.IO.FileStream filePath(saveGame);

		public StateMenu(Handler handler) : base (handler)
		{
			uiManager = new UIManager(handler);
			handler.GetMouseManager().SetUIManager(uiManager);

			uiManager.AddObject(new UIImageButton(50, YMoveButton, 220, 150, Assets.btn_start, new ClickListener() 
			{
				public void OnClick() 
				{
					State.SetState(handler.GetGame().gameState);
					run = true;
				}
			}));
			
			uiManager.AddObject(new UIImageButton(40, 640, 200, 90, Assets.trasparent, new ClickListener() 
			{
				public void OnClick() 
				{
					if(run) 
					{
						if(Cannon.firstShot==1) 
						{
							Display.SaveFile((StateMenu) handler.GetGame().menuState);
						}
						
					}
				}
			}));

			uiManager.AddObject(new UIImageButton(330, 640, 200, 90, Assets.trasparent, new ClickListener() 
			{
				public void OnClick() 
				{
					if(run) 
					{
						StateGame.pause = true;
					}
				}
			}));

			uiManager.AddObject(new UIImageButton(600, 640, 200, 90, Assets.trasparent, new ClickListener() 
			{
				public void OnClick() 
				{
					if(run) 
					{
						StateGame.exit = true;
					}
				}
			}));

			uiManager.AddObject(new UIImageButton(210, 510, 90, 90, Assets.trasparent, new ClickListener() 
			{
				public void OnClick() 
				{
					if(StateGame.pause) 
					{
						State.SetState(handler.GetGame().menuState);
						StateGame.pause = false;
						Cannon.firstShot=0;  //restart of the game		
						CollectBall.point=0;
						Score.SetZero(CollectBall.score);
						run = false;
						home = true;
					}
				}
			}));

			uiManager.AddObject(new UIImageButton(353, 510, 90, 90, Assets.trasparent, new ClickListener() 
			{
				public void OnClick() 
				{
					if(StateGame.pause) 
					{
						StateGame.pause = false;
					}
				}
			}));

			uiManager.AddObject(new UIImageButton(495, 510, 90, 90, Assets.trasparent, new ClickListener() 
			{
				public void OnClick() 
				{
					if(StateGame.pause) 
					{
						StateGame.exit = true;
					}
				}
			}));

			uiManager.AddObject(new UIImageButton(308, YMoveButton, 220, 150, Assets.btn_load, new ClickListener() 
			{
						public void OnClick() 
						{
							if(!run) 
							{
								load = true;
								try
								{
									Thread.Sleep(3000);
									load=false;
								}
								catch(Exception e){}   
							}
						}
			}));

			uiManager.AddObject(new UIImageButton(566, YMoveButton, 220, 150, Assets.btn_exit, new ClickListener() 
			{
						public void OnClick() 
						{
							if(!run) 
							{
								System.Exit(0);
							}
						}
			}));
		}

		private void MoveLogo() 
		{
			yMovelogo+=2;
		}

		private void MoveDragon() 
		{
			if(xDragon > xDragonLimits) 
			{
				xDragon-=2;
			}
			else 
			{
				if(yDragon < yDragonLimits-LIMITS || yDragon > yDragonLimits+LIMITS) 
				{
					yMove*=-1;
				}
				yDragon+=yMove;
			}
			if(dimDragon<150)
			{
				dimDragon++;
			}
		}

		public void Tick() 
		{
			uiManager.Tick();
			if(yMovelogo < yLogoLimits) 
			{
				MoveLogo();
			}
			MoveDragon();
		}

		public void Render(Graphics g) 
		{
			g.DrawImage(Assets.dark_background, 0, 0, Launcher.GAME_WIDTH, Launcher.GAME_HEIGHT, null);
			g.DrawImage(Assets.logo, 70, StateMenu.yMovelogo, 650, 650, null);
			g.DrawImage(Assets.dragon, StateMenu.xDragon, StateMenu.yDragon, StateMenu.dimDragon, StateMenu.dimDragon, null);
			StateMenu.uiManager.Render(g);
			if(load) 
			{
				g.DrawImage(Assets.demo, 0, 0, Launcher.GAME_WIDTH, Launcher.GAME_HEIGHT, null);
			}

		}
	}
}
