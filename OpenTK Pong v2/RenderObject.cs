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
    internal class RenderObject
    {

        float[] Vertices { get; set; }
        uint[] Indices { get; set; }
        BufferUsageHint BufferUsageHint { get; set; }

        int VBOHandle;
        int VAOHandle;
        int EBOHandle;

        
        public RenderObject(float[] vertices, BufferUsageHint bufferUsageHint, uint[] indices=null)
        {
            Vertices = vertices;
            Indices = indices;


            BufferUsageHint = bufferUsageHint;

            VAOHandle = GL.GenVertexArray();
            GL.BindVertexArray(VAOHandle);

            VBOHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint);

            if (indices != null)
            {
                EBOHandle = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBOHandle);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint);
            }
            
            GL.VertexAttribPointer(0,3,VertexAttribPointerType.Float,false,6*sizeof(float),0);
            GL.EnableVertexAttribArray(0);


            GL.VertexAttribPointer(1,3,VertexAttribPointerType.Float,false,6*sizeof(float),3*sizeof(float));
            GL.EnableVertexAttribArray(1);
        }
        
        public void Render(Shader shader, Vector3 pos)
        {
            Matrix4 model = Matrix4.Identity;
            model*=Matrix4.CreateTranslation(pos);

            int location = GL.GetUniformLocation(shader.Handle, "transform");
            GL.UniformMatrix4(location, true, ref model);

            GL.BindVertexArray(VAOHandle);
            if (Indices != null)
            {
                GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
            }else
            {
                GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            }



        }
    }
}
