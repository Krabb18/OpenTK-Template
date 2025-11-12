using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Drawing;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;

namespace SpaceEngine.Engine
{
    public class TestObject
    {
        float[] vertices =
        {
            //Position          Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };

        // 🔹 Indices für 2 Dreiecke
        uint[] indices = {
            0, 1, 2, // erstes Dreieck (unten rechts)
            2, 3, 0  // zweites Dreieck (oben links)
        };

        int VertexBufferObject;
        int EBO;

        int VertexArrayObject;

        public ShaderClass shader;
        public TextureClass texture;
        public Vector3 position = new Vector3();
        public TestObject() 
        {
            shader = new ShaderClass("ShaderCodes/shader.vert", "ShaderCodes/shader.frag");
            texture = new TextureClass("Texture/Ryuja.png");

            VertexBufferObject = GL.GenBuffer();
            EBO = GL.GenBuffer();
            VertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(VertexArrayObject);

            //VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            //EBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            // 3. then set our vertex attributes pointers
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            uint texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            
        }

        public void Render()
        {
            shader.Activate();

            //die glm scheiße
            Matrix4 rotation = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(0.0f));
            Matrix4 scale = Matrix4.CreateScale(1.0f, 1.0f, 1.0f);
            Matrix4 trans = Matrix4.CreateTranslation(position.X, position.Y, position.Z);
            Matrix4 matrix = scale * rotation * trans;

            int modelLoc = GL.GetUniformLocation(shader.ID, "model");
            GL.UniformMatrix4(modelLoc, false, ref matrix);

            GL.BindVertexArray(VertexArrayObject);
            texture.Bind();
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        public void Delete()
        {
            shader.Delete();
            texture.Delete();
            GL.DeleteBuffer(VertexArrayObject);
            GL.DeleteBuffer(VertexBufferObject);
        }
    }
}
