using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK_Pong_v2
{
    internal class Ball
    {

        public RenderObject RenderObject;

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

        public int CountOfBounces = 0;
        public float SpeedX = 0.0001f;
        public float SpeedY = 0.0001f;

        

        public Ball()
        {
            RenderObject = new RenderObject(vertices, OpenTK.Graphics.OpenGL4.BufferUsageHint.StreamDraw, indices);
        }

        public void CheckForBorder()
        {
          //  if()
        }
    }
}
