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
    internal class Game : GameWindow
    {

       


        private float[] vertices =
  {
      // positions        // colors
      0.5f, -0.5f, 0.0f,  1.0f, 1.0f, 1.0f,   // bottom right
     -0.5f, -0.5f, 0.0f,  1.0f, 1.0f, 1.0f,   // bottom left
      0.0f,  0.5f, 0.0f,  1.0f, 1.0f, 1.0f    // top 
    };

        public Vector2 Score = new Vector2();


        Stopwatch _timer = new Stopwatch();
        Stopwatch fpsStopWatch = new Stopwatch();



        Ball ball;

        Shader shader;


        Paddle RightPaddle;
        Paddle LeftPaddle;

        PauseObject PauseObject;

        bool Runing = true;


        public float LeftStep = 0;
        public float RightStep = 0;

        bool Debugging = false;
        bool AutoLeft = false;
        bool AutoRight = false;
        bool Lag = false;
        int LagValue=0;
        bool Step = false;
        int StepValue = 0;

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
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
                }else
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


            if (input.IsKeyDown(Keys.Up) &&RightStep <= 0.88)
            {
                RightStep += RightStepSize;
            }
            if (input.IsKeyDown(Keys.Down) &&RightStep >= -0.88)
            {
                RightStep -=RightStepSize;
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


            if (input.IsKeyDown(Keys.W)&&LeftStep<=0.88)
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

          //  VertexBufferObject = GL.GenBuffer();
          //  GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);//vytvori buffer
          //  GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw); //kopiruje buffer do pameti na GPU; Dynamic -> caste zmeny (zajistuje "prioritu" v pameti) 

            shader = new Shader("shader.vert", "shader.frag");

          // renderObject = new RenderObject(ball.vertices, BufferUsageHint.DynamicDraw, ball.indices);
          // renderObject2 = new RenderObject(ball.vertices, BufferUsageHint.DynamicDraw, ball.indices);

            RightPaddle = new Paddle(true);
            LeftPaddle = new Paddle(false);
            PauseObject = new PauseObject();

            ball = new Ball();

            //  VertexArrayObject = GL.GenVertexArray();
            //
            //  GL.BindVertexArray(VertexArrayObject);
            //  // 2. copy our vertices array in a buffer for OpenGL to use
            //  GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            //  GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);
            //  // 3. then set our vertex attributes pointers
            //  GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            //  GL.EnableVertexAttribArray(0);
            //
            //  GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            //  GL.EnableVertexAttribArray(1);
            //
            //  //ball
            //
            //  //VertexArrayObject = GL.GenVertexArray();
            //
            //  ElementBufferObject = GL.GenBuffer();
            //  GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            //  GL.BufferData(BufferTarget.ElementArrayBuffer, ball.indices.Length * sizeof(uint), ball.indices, BufferUsageHint.DynamicDraw);
            //
            //  GL.BindVertexArray(VertexArrayObject);
            //  // 2. copy our vertices array in a buffer for OpenGL to use
            //  GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            //  GL.BufferData(BufferTarget.ArrayBuffer, ball.vertices.Length * sizeof(float), ball.vertices, BufferUsageHint.DynamicDraw);
            //  // 3. then set our vertex attributes pointers
            //  GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            //  GL.EnableVertexAttribArray(2);
            //
            //  GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            //  GL.EnableVertexAttribArray(3);

            _timer.Start();
            fpsStopWatch.Start();

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
                string input= Console.ReadLine();

                switch (input.ToLower())
                {
                    case "newball":
                        ball.Start();
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
                        ball.Start();
                        ball.Score.X = 0;
                        ball.Score.Y = 0;
                        LeftStep = 0;
                        RightStep = 0;
                        Runing = true;
                        Debugging = false;
                        Lag = false;
                        LagValue = 0;
                        AutoLeft = false;
                        AutoRight = false;
                        Step = false;
                        StepValue=0;
                        Console.Clear();
                        break;
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
                        
                    }else
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

                if (input.Contains("setballpos"))
                {
                    input = input.Split("setballpos")[1].Trim();
                    if (input.Length > 0)
                    {
                        float.TryParse(input.Split(";")[0], out ball.myPos.X);
                        float.TryParse(input.Split(";")[1], out ball.myPos.Y);
                        ball.Render(shader);
                        Console.Clear();
                    }
                }
                if (input.Contains("setballspeed"))
                {
                    input = input.Split("setballspeed")[1].Trim();
                    if (input.Length > 0)
                    {
                        float.TryParse(input.Split(";")[0], out ball.Speed.X);
                        float.TryParse(input.Split(";")[1], out ball.Speed.Y);
                        
                        Console.Clear();
                    }
                }

            }

            if (fpsStopWatch.Elapsed.TotalMilliseconds >= 100)
            {
                fps *= 10;
                Title = "Skóre: " + ball.Score.X + "-" + ball.Score.Y + "   FPS: " +1/RenderTime;//fps.ToString();
                Settings.Score = ball.Score;
                fps = 0;
                fpsStopWatch.Restart();
                fpsStopWatch.Start();

                if (Debugging)
                {
                    Console.SetCursorPosition(0, 0);

                    Console.WriteLine("RenderLatency: "+ (decimal)RenderTime+"           ");
                    
                    Console.WriteLine();
                    Console.WriteLine("Ball pos: "+ball.myPos+"           ");
                    Console.WriteLine("Ball speed: " + ball.Speed+ "           "); 
                    Console.WriteLine();    
                    Console.WriteLine("LPaddle pos: " + new Vector3(0, RightStep, 0)+ "           ");
                    Console.WriteLine();
                    Console.WriteLine("RPaddle pos: " + new Vector3(0, LeftStep, 0)+ "           ");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("AutoLeft: " + AutoLeft+ "           ");
                    Console.WriteLine("AutoRight: " + AutoRight+ "           ");
                    Console.WriteLine();
                    Console.WriteLine("Lag: "+Lag+ "           ");
                    Console.WriteLine("LagValue: " + LagValue+ "           ");
                    Console.WriteLine("Step: "+Step+ "           ");
                    Console.WriteLine("StepValue: "+StepValue+ "           ");
                    Console.WriteLine();
                    Console.WriteLine("Paused: "+!Runing+ "           ");
                    
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

                
                PauseObject.Render(shader,_timer);
               
                SwapBuffers();
                return;
            }
            if(Lag)
            Thread.Sleep(LagValue);


            

            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            //Code goes here.
           


            shader.Use();

            if (!ball.Running)
            {
                ball.Start();
            }


            ball.GetPaddles(new Vector3(0,LeftStep,0),new Vector3(0,RightStep,0));
            ball.Render(shader,_timer.Elapsed.Ticks/25000);

            if(AutoRight)
            RightStep = ball.myPos.Y;

            if (AutoLeft)
                LeftStep = ball.myPos.Y;

            RightPaddle.RenderObject.Render(shader, new Vector3(0,LeftStep,0));
            LeftPaddle.RenderObject.Render(shader, new Vector3(0, RightStep, 0));
           

            GL.UseProgram(shader.Handle);





         //   int location = GL.GetUniformLocation(shader.Handle, "transform");
         //
         //   GL.UniformMatrix4(location, true, ref translation);
         //
         //   //  double timeValue = _timer.Elapsed.TotalSeconds;
         //   //  float greenValue = (float)Math.Abs(Math.Sin(timeValue) / 3.0f + 0.5f);
         //   //  float redValue = (float)Math.Abs(Math.Sin(timeValue+1) / 2.0f + 0.5f);
         //   //  int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");
         //   //  GL.Uniform4(vertexColorLocation,redValue , greenValue, (float)Math.Abs(Math.Sin(timeValue+2) / 1.0f + 0.5f), 1.0f);
         //
         //   GL.BindVertexArray(VertexArrayObject);
         //   GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
         //
         //   GL.DrawElements(PrimitiveType.Triangles, ball.indices.Length, DrawElementsType.UnsignedInt, 0);

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

            shader.Dispose();
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
