using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SpaceEngine.Engine
{
    public class Camera
    {
        Matrix4 viewMatrix = Matrix4.Identity;
        Matrix4 projectionMatrix = Matrix4.Identity;
        Matrix4 camMatrix = Matrix4.Identity;

        Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 target = new Vector3(0.0f, 0.0f, -1.0f);
        float zoom = 1.0f;
        Vector3 front = Vector3.Zero;
        public float width = 0.0f;
        public float height = 0.0f;

        float speed = 0.5f;

        public Camera()
        {

        }

        public void CaculateMatrix()
        {
            //for testing
            target = position + new Vector3(0.0f, 0.0f, -1.0f);
            front = Vector3.Normalize(target - position);

            viewMatrix = Matrix4.LookAt(position, target, new Vector3(0.0f, 1.0f, 0.0f));
            projectionMatrix =  Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)width / (float)height, 0.1f, 100.0f);
            camMatrix =  viewMatrix * projectionMatrix;
        }

        public void Activate(ShaderClass shaderClass)
        {
            shaderClass.Activate();
            int modelLoc = GL.GetUniformLocation(shaderClass.ID, "camMatrix");
            GL.UniformMatrix4(modelLoc, false, ref camMatrix);
        }

        public void Movement(KeyboardState input, float dt)
        {
            if (input.IsKeyDown(Keys.W))
            {
                position += front * speed * dt;
            }

            if (input.IsKeyDown(Keys.S))
            {
                position -= front * speed * dt;
            }

            if (input.IsKeyDown(Keys.A))
            {
                position -= Vector3.Normalize(Vector3.Cross(front, new Vector3(0.0f, 1.0f, 0.0f))) * speed * dt;
            }

            if (input.IsKeyDown(Keys.D))
            {
                position += Vector3.Normalize(Vector3.Cross(front, new Vector3(0.0f, 1.0f, 0.0f))) * speed * dt;
            }
        }
    }
}
