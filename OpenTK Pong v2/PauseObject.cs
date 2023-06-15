
using OpenTK.Mathematics;
using System.Diagnostics;


namespace OpenTK_Pong_v2
{
    internal class PauseObject
    {
        RenderObject RenderObject;
        RenderObject RenderObject2;
        RenderObject RenderObject3;
        RenderObject RenderObject4;

        public float[] vertices = {
     0.025f,  0.025f, 0.0f,   1.0f, 0, 0000,  // top right
     0.025f, -0.025f, 0.0f,   0000, 1.0f, 0, // bottom right
    -0.025f, -0.025f, 0.0f,   0000, 0000, 1.0f, // bottom left
    -0.025f,  0.025f, 0.0f,   1.0f, 0, 0000 // top left
        };
        public uint[] indices = {  // note that we start from 0!
    0, 1, 3,   // first triangle
    1, 2, 3    // second triangle
        };

        public PauseObject()
        {
            RenderObject = new RenderObject(vertices, OpenTK.Graphics.OpenGL4.BufferUsageHint.StreamDraw, indices);
            RenderObject2 = new RenderObject(vertices, OpenTK.Graphics.OpenGL4.BufferUsageHint.StreamDraw, indices);
            RenderObject3 = new RenderObject(vertices, OpenTK.Graphics.OpenGL4.BufferUsageHint.DynamicDraw, indices);
            RenderObject4 = new RenderObject(vertices, OpenTK.Graphics.OpenGL4.BufferUsageHint.DynamicDraw, indices);
        }

        public void Render(Shader shader, Stopwatch timer)
        {



            RenderObject.Render2(shader, new Vector3(0.5f * (float)Math.Sin(0.125f * MathHelper.DegreesToRadians(timer.ElapsedMilliseconds)), 0, 0), MathHelper.DegreesToRadians(timer.ElapsedMilliseconds));
            RenderObject2.Render2(shader, new Vector3(-0.5f * (float)Math.Sin(0.125f * MathHelper.DegreesToRadians(timer.ElapsedMilliseconds)), 0, 0), MathHelper.DegreesToRadians(timer.ElapsedMilliseconds));
            RenderObject3.Render2(shader, new Vector3(0, -0.55f, 0), MathHelper.DegreesToRadians(-timer.ElapsedMilliseconds));
            RenderObject4.Render2(shader, new Vector3(0, 0.55f, 0), MathHelper.DegreesToRadians(-timer.ElapsedMilliseconds));
        }

    }
}
