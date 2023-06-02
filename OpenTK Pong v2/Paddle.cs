using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace OpenTK_Pong_v2
{
    internal class Paddle
    {
        public float[] Vertices {get; set;}
        public uint[] Indices {get; set;}
        public RenderObject RenderObject;


        public float[] vertices;
        public float[] LeftVertrices = {
    -0.85f,  0.12f, 0.0f,   1.0f, 1.0f, 1.0f,  // top right
    -0.85f, -0.12f, 0.0f,   1.0f, 1.0f, 1.0f, // bottom right
    -0.90f, -0.12f, 0.0f,   1.0f, 1.0f, 1.0f, // bottom left
    -0.90f,  0.12f, 0.0f,   1.0f, 1.0f, 1.0f // top left
        };

        public float[] RightVertices = {
    0.85f,  0.12f, 0.0f,   1.0f, 1.0f, 1.0f,  // top right
    0.85f, -0.12f, 0.0f,   1.0f, 1.0f, 1.0f, // bottom right
    0.90f, -0.12f, 0.0f,   1.0f, 1.0f, 1.0f, // bottom left
    0.90f,  0.12f, 0.0f,   1.0f, 1.0f, 1.0f // top left
        };


        public uint[] indices = {  // note that we start from 0!
    0, 1, 3,   // first triangle
    1, 2, 3    // second triangle
        };

        public Paddle(bool isLeft)
        {
            if (isLeft)
            {
                vertices = LeftVertrices;
            }
            else
                vertices = RightVertices;

            RenderObject = new RenderObject(vertices, OpenTK.Graphics.OpenGL4.BufferUsageHint.DynamicDraw, indices);

        }


        


    }
}
