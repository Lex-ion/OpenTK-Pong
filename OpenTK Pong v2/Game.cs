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
    internal class Game : GameWindow
    {

        static float fl = 0;

        

        static float test = 0;
        static float test2 = 0;


        private float[] vertices =
  {
      // positions        // colors
      0.5f, -0.5f, 0.0f,  1.0f, 1.0f, 1.0f,   // bottom right
     -0.5f, -0.5f, 0.0f,  1.0f, 1.0f, 1.0f,   // bottom left
      0.0f,  0.5f, 0.0f,  1.0f, 1.0f, 1.0f    // top 
    };



       
        Stopwatch _timer = new Stopwatch();

        int VertexBufferObject;
        int VertexArrayObject;
        int ElementBufferObject;

        Shader shader;

        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
        {  
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (input.IsKeyDown(Keys.Right))
            {
                test += 0.001f;
            }
            if (input.IsKeyDown(Keys.Left))
            {
                test -= 0.001f;
            }
            if (input.IsKeyDown(Keys.Up))
            {
                test2 += 0.001f;
            }
            if (input.IsKeyDown(Keys.Down))
            {
                test2 -= 0.001f;
            }



        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0, 0, 0, 1.0f);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);//vytvori buffer
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw); //kopiruje buffer do pameti na GPU; Dynamic -> caste zmeny (zajistuje "prioritu" v pameti) 

            shader = new Shader("shader.vert", "shader.frag");

            VertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(VertexArrayObject);
            // 2. copy our vertices array in a buffer for OpenGL to use
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);
            // 3. then set our vertex attributes pointers
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);


            _timer.Start();
        }

        

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            //Code goes here.
           


            shader.Use();

            
            Matrix4 translation = Matrix4.CreateTranslation(test,test2,0);
            Matrix4 rotation = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians( 50*(-test+test2)));
            Matrix4 scale = Matrix4.CreateScale(0.5f, 0.5f, 0.5f);
            Matrix4 trans = rotation*translation * scale;

            
            GL.UseProgram(1);

            int location = GL.GetUniformLocation(shader.Handle, "transform");

            GL.UniformMatrix4(location, true, ref trans);

            //  double timeValue = _timer.Elapsed.TotalSeconds;
            //  float greenValue = (float)Math.Abs(Math.Sin(timeValue) / 3.0f + 0.5f);
            //  float redValue = (float)Math.Abs(Math.Sin(timeValue+1) / 2.0f + 0.5f);
            //  int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");
            //  GL.Uniform4(vertexColorLocation,redValue , greenValue, (float)Math.Abs(Math.Sin(timeValue+2) / 1.0f + 0.5f), 1.0f);

            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);


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


    }
}
