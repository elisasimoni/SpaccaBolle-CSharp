using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace dev.spaccabolle
{
   public class Game : Runnable
    {
        private Display display;
        private int width;
        private int height;
        public string title;
        private bool running = false;
        private Thread thread;
        private BufferStrategy bs;
        private Graphics g;

        public State gameState;
        public State menuState;
        public State gameOverState;

        private KeyManager keyManager;
        private MouseManager mouseManager;

        private Handler handler;

        public Game(String title, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.title = title;
            this.keyManager = new KeyManager();
            this.mouseManager = new MouseManager();
        }

        private void Init()
        {
            this.display = new Display(this.title, this.width, this.height);
            this.display.GetFrame().AddKeyListener(this.keyManager);
            this.display.GetFrame().AddMouseListener(this.mouseManager);
            this.display.GetFrame().AddMouseMotionListener(this.mouseManager);
            this.display.GetCanvas().AddMouseListener(this.mouseManager);
            this.display.GetCanvas().AddMouseMotionListener(this.mouseManager);
            Assets.Init();
            this.handler = new Handler(this);
            this.menuState = new StateMenu(this.handler);
            this.gameState = new StateGame(this.handler);
            State.SetState(this.menuState);
        }

        public void Restart()
        {
            Assets.Init();
            this.gameState = new StateGame(this.handler);
        }

        private void Tick()
        {
            this.keyManager.Tick();
            if ((State.GetState() != null))
            {
                State.GetState().Tick();
            }

            if (CollectBall.gameOver)
            {
                if (KeyManager.restart)
                {
                    this.Restart();
                    CollectBall.gameOver = false;
                }

            }

            if (StateMenu.home)
            {
                this.Restart();
                StateMenu.home = false;
            }

        }

        private void Render()
        {
            this.bs = this.display.GetCanvas().GetBufferStrategy();
            if ((this.bs == null))
            {
                this.display.GetCanvas().CreateBufferStrategy(3);
                return;
            }

            this.g = this.bs.GetDrawGraphics();
            if ((State.GetState() != null))
            {
                State.GetState().Render(this.g);
            }

            this.bs.Show();
            this.g.Dispose();
        }

        public void Run()
        {
            this.Init();
            int fps = 60;
            double timePerTick = (1000000000 / fps);
            double delta = 0;
            long now;
            long lastTime = System.nanoTime();
            long timer = 0;
            int ticks = 0;
            while (this.running)
            {
                now = System.nanoTime();
                delta = (delta + ((now - lastTime) / timePerTick));
                timer = (timer + (now - lastTime));
                lastTime = now;
                if ((delta >= 1))
                {
                    this.Tick();
                    this.Render();
                    ticks++;
                    delta--;
                }

                if ((timer >= 1000000000))
                {
                    ticks = 0;
                    timer = 0;
                }

            }

            this.Stop();
        }

        public KeyManager GetKeyManager()
        {
            return this.keyManager;
        }

        public MouseManager GetMouseManager()
        {
            return this.mouseManager;
        }

        public int GetWidth()
        {
            return this.width;
        }

        public int GetHeight()
        {
            return this.height;
        }

        public void Start()
        {
            if (this.running)
            {
                return;
            }

            this.running = true;
            this.thread = new Thread(this.Start);
            this.thread.Start();
        }

        public void Stop()
        {
            if (!this.running)
            {
                return;
            }

            this.running = false;
            try
            {
                this.thread.Join();
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }
    }
}