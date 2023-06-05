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
        public Vector3 Speed;


        int rotator = 1;

       public bool Running = false;

       public Vector2 Score = new Vector2(0, 0);

        public Ball()
        {
            RenderObject = new RenderObject(vertices, OpenTK.Graphics.OpenGL4.BufferUsageHint.StreamDraw, indices);
            timer.Start();
            timer2.Start();
            Start();
        }

    /// <summary>
    /// Check if ball is inside of the game field and renders it
    /// </summary>
    /// <param name="shader">Shader for rendering</param>
    /// <param name="deg">Rotation in radians</param>
    /// <param name="sca">Scale factor</param>
        public void Render(Shader shader, float deg = 0, float sca=1)
        {
            myPos += Speed;
            
            CheckForBorder(myPos);

            



            RenderObject.Render(shader, myPos, deg*rotator, sca);
        }


        /// <summary>
        /// Checks if ball is inside of the game field
        /// </summary>
        /// <param name="pos">Positon of ball</param>
        public async void CheckForBorder(Vector3 pos)
        {
             Random random = new Random();


            if (timer.ElapsedMilliseconds>300&&myPos.Y>=0.975||myPos.Y<=-0.975f&& timer.ElapsedMilliseconds > 300) // Y border check
            {


                Speed.Y *= -1;
                
                timer.Reset();
                timer.Start();
         
                
                
                new Thread(() => Settings.Beep(5000, 25)).Start();
         
            }

            if(timer2.ElapsedMilliseconds > 20 && pos.X >= 0.975 || pos.X <= -0.975f && timer2.ElapsedMilliseconds > 20) // X border check
            {


               
                timer2.Reset();
                timer2.Start();
                new Thread(() => Settings.Beep(500, 500)).Start();

                if (pos.X > 0)
                {
                    Score.X++;
                }
                else
                    Score.Y++;

                Start();

            }

            

           if (pos.Y<LeftPaddle.Y+0.12&&pos.Y>LeftPaddle.Y-0.12f&&pos.X<-0.85f&&pos.X>-0.9&&timer2.ElapsedMilliseconds>300) //leftpaddle check
           {
                countOfBounces++;
                Speed *= 1.05f;
                Speed.X *= -1;

                float speedDif = (float)random.Next(20) / 1000000;
                if (random.NextDouble() > .5)
                {
                    Speed.X += speedDif;
                    Speed.Y -= speedDif;
                }else
                {
                    Speed.X -= speedDif;
                    Speed.Y += speedDif;
                }
                


                timer2.Reset();
                timer2.Start();
                rotator *= -1;

                

                new Thread(() => Settings.Beep(3000, 25)).Start();
            }

            if (pos.Y < RightPaddle.Y + 0.12 && pos.Y > RightPaddle.Y - 0.12f && pos.X > 0.85f &&pos.X<0.9&& timer2.ElapsedMilliseconds > 300) //rightpaddle check
            {
                
                countOfBounces++;
                Speed *= 1.05f;
                Speed.X *= -1;

                float speedDif = (float)random.Next(20) / 1000000;
                if (random.NextDouble() > .5)
                {
                    Speed.X += speedDif;
                    Speed.Y -= speedDif;
                }
                else
                {
                    Speed.X -= speedDif;
                    Speed.Y += speedDif;
                }



                timer2.Reset();
                timer2.Start();
                rotator *= -1;


                

                 new Thread(() => Settings.Beep(3000, 25)).Start();
            }
        }

        /// <summary>
        /// Sets position vector and speed vector
        /// </summary>
        public void Start()
        {
            Running = true;

            Random rnd = new Random();

            int randomAngle = rnd.Next(-70, 70);

            while (randomAngle == 0)
            {
                randomAngle = rnd.Next(-70, 70);
            }

            if (rnd.NextDouble() > 0.5)
            {
                randomAngle += -180;
            }

            float angleInRad = MathHelper.DegreesToRadians(randomAngle);
            
            


            Speed = new Vector3((float)Math.Cos(angleInRad)* 0.0001f,(float)Math.Sin(angleInRad) *  0.0001f, 0);
            myPos = new Vector3(0, 0, 0);

            Settings.Beep(1000, 50);
        }

        /// <summary>
        /// Gets paddles vectors
        /// </summary>
        /// <param name="leftPaddle">Left paddle</param>
        /// <param name="rightPaddle">Right paddle</param>
        public void GetPaddles(Vector3 leftPaddle, Vector3 rightPaddle)
        {
            LeftPaddle = leftPaddle;
            RightPaddle = rightPaddle;
        }
        
    }
}
