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

namespace OpenTK_Pong_v2
{
    internal class Ball
    {

        public RenderObject RenderObject;

        Stopwatch timer = new Stopwatch(); 
        Stopwatch timer2 = new Stopwatch();

        int countOfBounces = 0;

        public float[] vertices = {
     0.025f,  0.025f, 0.0f,   1.0f, 1.0f, 0000,  // top right
     0.025f, -0.025f, 0.0f,   0000, 1.0f, 1.0f, // bottom right
    -0.025f, -0.025f, 0.0f,   1.0f, 0000, 1.0f, // bottom left
    -0.025f,  0.025f, 0.0f,   1.0f, 1.0f, 0000 // top left
        };

       public uint[] indices = {  // note that we start from 0!
    0, 1, 3,   // first triangle
    1, 2, 3    // second triangle
        };

        
        public float SpeedX = -0.00015f;
        public float SpeedY = 0.0001f;

        public float PosX = 0;
        public float PosY = 0;

        Vector3 LeftPaddle;
        Vector3 RightPaddle;
        public Vector3 myPos;
        int rotator = 1;

       public bool Running = false;

       public Vector2 Score = new Vector2(0, 0);

        public Ball()
        {
            RenderObject = new RenderObject(vertices, OpenTK.Graphics.OpenGL4.BufferUsageHint.StreamDraw, indices);
            timer.Start();
            timer2.Start();
        }

    
        public void Render(Shader shader, float deg = 0, float sca=1)
        {
            myPos = new Vector3(PosX, PosY,0);
            PosX += SpeedX;
            PosY += SpeedY;
            CheckForBorder(myPos);

            



            RenderObject.Render(shader, myPos, deg*rotator, sca);
        }

        public async void CheckForBorder(Vector3 pos)
        {
             Random random = new Random();


            if (timer.ElapsedMilliseconds>300&&pos.Y>=0.975||pos.Y<=-0.975f&& timer.ElapsedMilliseconds > 300)
            {

               

                SpeedY *= -1;
                timer.Reset();
                timer.Start();

                
                
                new Thread(() => Console.Beep(5000, 25)).Start();

            }

            if(timer2.ElapsedMilliseconds > 20 && pos.X >= 0.975 || pos.X <= -0.975f && timer2.ElapsedMilliseconds > 20)
            {


                SpeedX *= -1;
                timer2.Reset();
                timer2.Start();
                new Thread(() => Console.Beep(500, 500)).Start();

                if (pos.X > 0)
                {
                    Score.X++;
                }
                else
                    Score.Y++;

                Start();

            }

            

           if (pos.Y<LeftPaddle.Y+0.12&&pos.Y>LeftPaddle.Y-0.12f&&pos.X<-0.85f&&pos.X>-0.9&&timer2.ElapsedMilliseconds>300)
           {
                countOfBounces++;
              
                float tmp = random.Next(10);
              
              //  SpeedY += 0.00001f * random.Next(10)*countOfBounces;
              //  SpeedX += 0.00001f * random.Next(10)*countOfBounces;
              //
                SpeedX *= -1;
                timer2.Reset();
                timer2.Start();
                rotator *= -1;

                SpeedX *= 1.05f;
                SpeedY *= 1.05f;

                
                

               
                new Thread(() => Console.Beep(3000, 25)).Start();
            }

            if (pos.Y < RightPaddle.Y + 0.12 && pos.Y > RightPaddle.Y - 0.12f && pos.X > 0.85f &&pos.X<0.9&& timer2.ElapsedMilliseconds > 300)
            {
                countOfBounces++;


              // SpeedY += 0.00001f * random.Next(5) * countOfBounces;
              // SpeedX += 0.00001f * random.Next(5) * countOfBounces;

                SpeedX *= -1;
                timer2.Reset();
                timer2.Start();
                rotator *= -1;

                SpeedX *= 1.05f;
                SpeedY *= 1.05f;

                 new Thread(() => Console.Beep(3000, 25)).Start();
            }
        }

        public void Start()
        {
            Running = true;

            Random rnd = new Random();

            SpeedX = (float)rnd.Next(20) / 100000; 
            SpeedY = (float)rnd.Next(20) / 100000;

            PosX = 0;
            PosY = 0;

            if (rnd.NextDouble() > 0.5)
            {
                SpeedX *= -1;
            }
            if (rnd.NextDouble() > 0.5)
            {
                SpeedY *= -1;
            }
            Console.Beep(1000, 50);
        }

        public void GetPaddles(Vector3 leftPaddle, Vector3 rightPaddle)
        {
            LeftPaddle = leftPaddle;
            RightPaddle = rightPaddle;
        }
    }
}
