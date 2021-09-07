using System;
using System.Collections.Generic;
using System.Text;
using System.Io;
using System.Drawing;

namespace Sofia_Bagagli_C_Sharp
{
    class StateGame : State
    {
        private static const int SCARTO = 100;
        private static const int CANNON_X = (Launcher.GAME_WIDTH/2)-(Assets.cannon.GetWidth()/2);        
        private static const int CANNON_Y = (Launcher.GAME_HEIGHT/2)+Assets.cannon.GetHeight()+SCARTO;
        private static const int EASY = 1;
        private static const int NORMAL = 2;
        private static const int HARD = 3;
        private static const int INITIAL_DRAGON = 200;
        private static const int EASY_DRAGON = 200;
        private static const int NORMAL_DRAGON = 300;
        private static const int HARD_DRAGON = 410;
        private static const int LIMITS = 15;
        public  static int xDragon = 500, yDragon = 200, yDragonVictory = 450, yMove = -1;
	    public static int yDragonLimits = 200, yDragonVictoryLimits = 200;
	    public static bool exit = false;
        public static bool pause = false;
	    public File.Create(level);
	    public Map map;    
        public static Cannon cannon;
        public static CollectBall collectBall;
        public static CollectBall collectBallMap;		    
        public static Pause paused;
        private DrawImage imageDraw;

        public StateGame (Handler handler) : base (handler)
        {
            base.handler;
            collectBall = new CollectBall();
			collectBallMap = new CollectBall();
			CollectBall.point = 0;
			Score.tempFlyngPoint = 0;
			CollectBall.flyngPoint = 0;
			cannon = new Cannon(CANNON_X, CANNON_Y, Assets.cannon.GetWidth(), Assets.cannon.GetHeight(),collectBall);
			map = new Map(0, Ball.LEFT_BOUNCE,collectBallMap,level);			
			paused = new Pause();
			imageDraw = new DrawImage();

        }

        private void IfPause() 
		{
		    if(KeyManager.pause && !CollectBall.gameOver) 
			{
	    	 	pause = true;
		    }
			if (pause) 
			{
			   if(KeyManager.easy) 
			   {
				   cannon.difficult = EASY;   		
				   yDragon = EASY_DRAGON;
				   yDragonLimits = EASY_DRAGON;
			   }
			   if(KeyManager.normal) 
			   {
				   cannon.difficult = NORMAL;   		
				   yDragon = NORMAL_DRAGON;
				   yDragonLimits = NORMAL_DRAGON;
				}
			   if(KeyManager.hard) 
			   {
				   cannon.difficult = HARD;
				   yDragon = HARD_DRAGON;
				   yDragonLimits = HARD_DRAGON;
				}
		   }
		}
		
		private void IfExit() 
		{
			if(KeyManager.exit && !CollectBall.gameOver && !CollectBall.victory) 
			{
				exit = true;
			}
			else if (KeyManager.exit)
			{
				System.Exit(0);
			}			   
		    if(exit) 
			{
			   if(KeyManager.yes) 
			   {
				   System.Exit(0);
			   }
			   else if(KeyManager.no) 
			   {
				   exit = false;
			   }
		   }
		}

		private void IfGameOver() 
		{
			if (CollectBall.gameOver) 
			{
			   if(KeyManager.restart) 
			   {
				   State.SetState(handler.GetGame().menuState);
                   CollectBall.point=0;
                   Cannon.firstShot=0;  //restart of the game		
				   StateMenu.run = false;
			   }
		   	}
		}
	
		private void GetInput() 
		{
		   IfPause();   //pause game
		   IfExit();	//exit game
		   IfGameOver(); //if game over restart game
		   
		   if(KeyManager.save && Cannon.firstShot==1) 
		   {			   
			   Display.SaveFile((StateMenu) handler.GetGame().menuState);
		   }

		   if(KeyManager.enter) 
		   {
			   pause = false;
			   yDragon = INITIAL_DRAGON;
			   yDragonLimits = INITIAL_DRAGON;
		   }
		}

		private void MoveIcon() 
		{
			if((yDragon < yDragonLimits-LIMITS || yDragon > yDragonLimits)) 
			{
		          yMove*=-1;
		    }
		     yDragon+=yMove;
		}

		public void Tick() 
		{
			GetInput();
			MoveIcon();
			cannon.Tick();
		    collectBallMap.Tick();
		    collectBall.Tick();
    
		}

		public void Render(Graphics g) 
		{
			imageDraw.Render(g);
		}
    }
}