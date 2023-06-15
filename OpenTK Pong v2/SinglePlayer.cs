
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.ComponentModel;
using System.Diagnostics;

namespace OpenTK_Pong_v2
{
    internal class SinglePlayer : GameWindow
    {
        public SinglePlayer(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
        {
        }

        Shader Shader;

        Ball Ball;
        Paddle LeftPaddle;
        Paddle RightPaddle;

        float LeftStep = 0;
        float RightStep = 0;

        PauseObject PauseObject;
        bool Running;

        Stopwatch Timer;
        Stopwatch FPSTimer;



        bool Debugging = false;
        bool AutoLeft = false;
        bool AutoRight = false;
        bool Lag = false;
        int LagValue = 0;
        bool Step = false;
        int StepValue = 0;




        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0, 0, 0, 1.0f);


            Shader = new Shader("shader.vert", "shader.frag");

            LeftPaddle = new Paddle(true);
            RightPaddle = new Paddle(false);
            PauseObject = new PauseObject();


            Ball = new Ball();

            Timer = new Stopwatch();
            Timer.Start();
            FPSTimer = new Stopwatch();
            FPSTimer.Start();

            Running = true;

            Settings.Score = new Vector2(0, 0);

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
                    case "newball":
                        Ball.Start();
                        Console.Clear();
                        break;

                    case "debug":
                        Debugging = !Debugging;
                        Console.Clear();
                        break;

                    case "autoleft":
                        AutoLeft = !AutoLeft;
                        Console.Clear(); break;
                    case "autoright":
                        AutoRight = !AutoRight;
                        Console.Clear(); break;

                    case "stop":
                        Close();
                        break;

                    case "reset":
                        Ball.Start();
                        Ball.Score.X = 0;
                        Ball.Score.Y = 0;
                        LeftStep = 0;
                        RightStep = 0;
                        Running = true;
                        Debugging = false;
                        Lag = false;
                        LagValue = 0;
                        AutoLeft = false;
                        AutoRight = false;
                        Step = false;
                        StepValue = 0;
                        Console.Clear();
                        break;
                    case "pause":
                        Running = !Running;
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
                    Running = true;
                }

                if (input.Contains("setballpos"))
                {
                    input = input.Split("setballpos")[1].Trim();
                    if (input.Length > 0)
                    {
                        float.TryParse(input.Split(";")[0], out Ball.myPos.X);
                        float.TryParse(input.Split(";")[1], out Ball.myPos.Y);
                        Ball.Render(Shader);
                        Console.Clear();
                    }
                }
                if (input.Contains("setballspeed"))
                {
                    input = input.Split("setballspeed")[1].Trim();
                    if (input.Length > 0)
                    {
                        float.TryParse(input.Split(";")[0], out Ball.Speed.X);
                        float.TryParse(input.Split(";")[1], out Ball.Speed.Y);

                        Console.Clear();
                    }
                }

            }

            if (FPSTimer.Elapsed.TotalMilliseconds >= 100)
            {
                fps *= 10;
                Title = "Skóre: " + Ball.Score.X + "-" + Ball.Score.Y + "   FPS: " + 1 / RenderTime;//fps.ToString();
                Settings.Score = Ball.Score;
                fps = 0;
                FPSTimer.Restart();
                FPSTimer.Start();

                if (Debugging)
                {
                    Console.SetCursorPosition(0, 0);

                    Console.WriteLine("RenderLatency: " + (decimal)RenderTime + "           ");

                    Console.WriteLine();
                    Console.WriteLine("Ball pos: " + Ball.myPos + "           ");
                    Console.WriteLine("Ball speed: " + Ball.Speed + "           ");
                    Console.WriteLine("Count of bounces: " + Ball.countOfBounces + "          ");
                    Console.WriteLine();
                    Console.WriteLine("LPaddle pos: " + new Vector3(0, RightStep, 0) + "           ");
                    Console.WriteLine();
                    Console.WriteLine("RPaddle pos: " + new Vector3(0, LeftStep, 0) + "           ");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("AutoLeft: " + AutoLeft + "           ");
                    Console.WriteLine("AutoRight: " + AutoRight + "           ");
                    Console.WriteLine();
                    Console.WriteLine("Lag: " + Lag + "           ");
                    Console.WriteLine("LagValue: " + LagValue + "           ");
                    Console.WriteLine("Step: " + Step + "           ");
                    Console.WriteLine("StepValue: " + StepValue + "           ");
                    Console.WriteLine();
                    Console.WriteLine("Paused: " + !Running + "           ");

                }
            }


            //Console.WriteLine(RenderTime);

            if (Step && StepValue <= 0)
            {
                Running = false;
                Step = false;
                StepValue = 0;
            }
            else if (Step)
            {
                StepValue--;
            }

            if (!Running)
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

            if (AutoLeft)
            {
                RightStep = Ball.myPos.Y;
            }


            Ball.GetPaddles(new Vector3(0, RightStep, 0), new Vector3(0, LeftStep, 0));
            Ball.Render(Shader, Timer.Elapsed.Ticks / 25000);

            LeftPaddle.RenderObject.Render(Shader, new Vector3(0, RightStep, 0));
            RightPaddle.RenderObject.Render(Shader, new Vector3(0, LeftStep, 0));

            if (Ball.TouchedPaddle)
            {


                Ball.TouchedPaddle = false;


                new Thread(() => LeftStep = AIForPaddle.PredictPos(Ball).Y).Start();


            }

            GL.UseProgram(Shader.Handle);

            SwapBuffers();
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
                Running = !Running;
                if (!Running)
                {
                    Console.WriteLine("Hra pozastavena!");
                }
                else
                    Console.WriteLine("Hra spuštěna!");
            }


            if (!Running)
            {
                return;
            }


            float RightStepSize = 0.0004f;
            if (input.IsKeyDown(Keys.Right) || input.IsKeyDown(Keys.D))
            {
                RightStepSize = 0.00009f;
            }
            else if (input.IsKeyDown(Keys.Left) || input.IsKeyDown(Keys.A))
            {
                RightStepSize = 0.001f;
            }


            if ((input.IsKeyDown(Keys.Up) || input.IsKeyDown(Keys.W)) && RightStep <= 0.88)
            {
                RightStep += RightStepSize;
            }
            if ((input.IsKeyDown(Keys.Down) || input.IsKeyDown(Keys.S)) && RightStep >= -0.88)
            {
                RightStep -= RightStepSize;
            }


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
