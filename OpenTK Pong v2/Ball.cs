using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK_Pong_v2
{
    internal class Ball
    {

       public float[] vertices = {
     0.05f,  0.05f, 0.0f,   1.0f, 1.0f, 1.0f,  // top right
     0.05f, -0.05f, 0.0f,   1.0f, 1.0f, 1.0f, // bottom right
    -0.05f, -0.05f, 0.0f,   1.0f, 1.0f, 1.0f, // bottom left
    -0.05f,  0.05f, 0.0f,   1.0f, 1.0f, 1.0f // top left
        };

       public uint[] indices = {  // note that we start from 0!
    0, 1, 3,   // first triangle
    1, 2, 3    // second triangle
        };


    }
}
