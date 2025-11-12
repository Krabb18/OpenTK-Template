using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using SpaceEngine.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceEngine
{
    public class Game : GameWindow
    {
        TestObject testObject;
        Camera cam;

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            cam = new Camera();

            cam.width = 1280.0f;
            cam.height = 720.0f;

            Console.WriteLine(cam.width);
            Console.WriteLine(cam.height);

            testObject = new TestObject();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            
            cam.Movement(KeyboardState, (float)e.Time);
            cam.CaculateMatrix();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Enable(EnableCap.DepthTest);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            cam.Activate(testObject.shader);
            testObject.Render();

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
            cam.width = e.Width;
            cam.height = e.Height;
        }

    }
}
