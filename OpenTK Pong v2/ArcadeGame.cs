using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.ComponentModel;

namespace OpenTK_Pong_v2
{
    internal class ArcadeGame : GameWindow
    {
        List<Ball> Balls = new List<Ball>();

        Shader Shader;

        Stopwatch Timer;
        Stopwatch FPSTimer;

        Paddle LeftPaddle;
        public float LeftStep = 0;

        PauseObject PauseObject;

        bool Runing = true;


        public float RightStep = 0;

        bool Debugging = false;
        bool AutoLeft = false;
        bool AutoRight = false;
        bool Lag = false;
        int LagValue = 0;
        bool Step = false;
        int StepValue = 0;

        

        public ArcadeGame(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
        {
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            KeyboardState input = KeyboardState;

            if (input.IsKeyPressed(Keys.End))
            {
                Close();
            }

            if (input.IsKeyPressed(Keys.Escape))
            {
                Runing = !Runing;
                if (!Runing)
                {
                    Console.WriteLine("Hra pozastavena!");
                }
                else
                    Console.WriteLine("Hra spuštěna!");
            }


            if (!Runing)
            {
                return;
            }


            float RightStepSize = 0.0004f;
            if (input.IsKeyDown(Keys.Right))
            {
                RightStepSize = 0.00009f;
            }
            else if (input.IsKeyDown(Keys.Left))
            {
                RightStepSize = 0.001f;
            }


            if (input.IsKeyDown(Keys.Up) && RightStep <= 0.88)
            {
                RightStep += RightStepSize;
            }
            if (input.IsKeyDown(Keys.Down) && RightStep >= -0.88)
            {
                RightStep -= RightStepSize;
            }


            float LeftStepSize = 0.0004f;
            if (input.IsKeyDown(Keys.D))
            {
                LeftStepSize = 0.00009f;
            }
            else if (input.IsKeyDown(Keys.A))
            {
                LeftStepSize = 0.001f;
            }


            if (input.IsKeyDown(Keys.W) && LeftStep <= 0.88)
            {
                LeftStep += LeftStepSize;
            }
            if (input.IsKeyDown(Keys.S) && LeftStep >= -0.88)
            {
                LeftStep -= LeftStepSize;
            }


        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0, 0, 0, 1.0f);

            
            Shader = new Shader("shader.vert", "shader.frag");

            LeftPaddle = new Paddle(true);
            PauseObject = new PauseObject();


            Balls.Add(new Ball(true));

            Timer = new Stopwatch();
            Timer.Start();
            FPSTimer = new Stopwatch();
            FPSTimer.Start();

            

            new Thread(() => Settings.Beep(1000, 25)).Start();
            new Thread(() => Settings.Beep(2000, 25)).Start();
            new Thread(() => Settings.Beep(3000, 25)).Start();

            Console.WriteLine("Inicializováno!");
        }

        

        int fps = 0;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            fps += 1;



            if (Console.KeyAvailable)
            {
                string input = Console.ReadLine();

                switch (input.ToLower())
                {
                   

                    
                    case "pause":
                        Runing = !Runing;
                        Console.Clear();
                        break;

                }
                if (input.Contains("lag"))
                {
                    input = input.Split("lag")[1].Trim();
                    if (input.Length <= 0)
                    {
                        Lag = false;

                    }
                    else
                    {
                        int.TryParse(input, out LagValue);
                        Lag = true;
                    }
                    Console.Clear();
                }

                if (input.Contains("step"))
                {
                    input = input.Split("step")[1].Trim();
                    if (input.Length <= 0)
                    {
                        Step = false;

                    }
                    else
                    {
                        int.TryParse(input, out StepValue);
                        Step = true;
                    }
                    Console.Clear();
                    Runing = true;
                }

               

            }

            if (FPSTimer.Elapsed.TotalMilliseconds >= 100)
            {
                fps *= 10;
                Title = "FPS: " + 1 / RenderTime;//fps.ToString();

                fps = 0;
                FPSTimer.Restart();
                FPSTimer.Start();

                if (Debugging)
                {
                    Console.SetCursorPosition(0, 0);

                    Console.WriteLine("RenderLatency: " + (decimal)RenderTime + "           ");

                    
                    Console.WriteLine("AutoLeft: " + AutoLeft + "           ");
                    Console.WriteLine("AutoRight: " + AutoRight + "           ");
                    Console.WriteLine();
                    Console.WriteLine("Lag: " + Lag + "           ");
                    Console.WriteLine("LagValue: " + LagValue + "           ");
                    Console.WriteLine("Step: " + Step + "           ");
                    Console.WriteLine("StepValue: " + StepValue + "           ");
                    Console.WriteLine();
                    Console.WriteLine("Paused: " + !Runing + "           ");

                }
            }


            //Console.WriteLine(RenderTime);

            if (Step && StepValue <= 0)
            {
                Runing = false;
                Step = false;
                StepValue = 0;
            }
            else if (Step)
            {
                StepValue--;
            }

            if (!Runing)
            {

                PauseObject.Render(Shader, Timer);
                SwapBuffers();
                return;
            }
            if (Lag)
                Thread.Sleep(LagValue);




            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            //Code goes here.



            Shader.Use();

            int ballsToAdd = 0;
            foreach (Ball ball in Balls)
            {
                ball.GetPaddles(new Vector3(0,RightStep,0), ball.myPos);
                ball.Render(Shader, Timer.Elapsed.Ticks / 25000);
                if (ball.TouchedPaddle)
                {
                    ball.TouchedPaddle = false;
                    Random rnd = new Random();
                    if (rnd.NextDouble() > 0.55)
                    {
                        ballsToAdd++;
                        new Thread(() => Settings.Beep(2750, 15)).Start();
                        new Thread(() => Settings.Beep(2750, 15)).Start();
                    }
                }

                if (!ball.Alive)
                {
                    Close();
                }
            }
            for (int i = 0; i < ballsToAdd; i++)
            {
                Random rnd = new Random();
                Balls.Add(new Ball(true));
                Balls[Balls.Count - 1].Start((float)rnd.Next(-30, 80) / 100, (float)rnd.Next(-90, 90) / 100);
            }


            LeftPaddle.RenderObject.Render(Shader, new Vector3(0, RightStep, 0));


            GL.UseProgram(Shader.Handle);

            SwapBuffers();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            Shader.Dispose();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            new Thread(() => Settings.Beep(3000, 25)).Start();
            new Thread(() => Settings.Beep(2000, 25)).Start();
            new Thread(() => Settings.Beep(1000, 25)).Start();
        }


    }
}
