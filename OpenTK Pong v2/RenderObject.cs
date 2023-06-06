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
using System.Xml.Linq;

namespace OpenTK_Pong_v2
{
    internal class RenderObject
    {

        float[] Vertices { get; set; }
        uint[] Indices { get; set; }
        BufferUsageHint BufferUsageHint { get; set; }

        bool UniformUsed = false;
        string LocationName;
        Vector4 UniformData = new Vector4();
        Shader UniformShader;

        int VBOHandle;
        int VAOHandle;
        int EBOHandle;

        
        public RenderObject(float[] vertices, BufferUsageHint bufferUsageHint, uint[] indices=null, bool colors = true)
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

            if (colors)
            {
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);

            
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
                GL.EnableVertexAttribArray(1);
            }else
            {
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);
            }

           
        }

        public void SetUniformValueVector4(Shader shader, string name,float XValue,float YValue,float ZValue, float WValue)
        {
            UniformData = new Vector4(XValue, YValue, ZValue, WValue);
            LocationName = name;
            UniformShader = shader;
            UniformUsed = true;
        }

        public void Render(Shader shader, Vector3 pos, float deg = 0, float sca=1)
        {
            Matrix4 model = Matrix4.Identity;
            model *= Matrix4.CreateScale(sca);
            
            model*=Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(deg));
            model *= Matrix4.CreateTranslation(pos);


            int location = GL.GetUniformLocation(shader.Handle, "transform");
            GL.UniformMatrix4(location, true, ref model);


            if (UniformUsed)
            {
                location = GL.GetUniformLocation(UniformShader.Handle, LocationName);
                GL.Uniform4(location,ref UniformData);
                UniformUsed = false;
            }

            GL.BindVertexArray(VAOHandle);
            if (Indices != null)
            {
                GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
            }else
            {
                GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            }
            
        }

        public void Render2(Shader shader, Vector3 pos, float deg = 0, float sca = 1)
        {
            Matrix4 model = Matrix4.Identity;
            model *= Matrix4.CreateScale(sca);
            model *= Matrix4.CreateTranslation(pos);
            model *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(deg));
            


            int location = GL.GetUniformLocation(shader.Handle, "transform");
            GL.UniformMatrix4(location, true, ref model);

            GL.BindVertexArray(VAOHandle);
            if (Indices != null)
            {
                GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
            }
            else
            {
                GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            }
            UniformUsed = false;
        }
    }
}
