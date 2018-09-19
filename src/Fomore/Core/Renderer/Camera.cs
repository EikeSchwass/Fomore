using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Core.Renderer
{
    public class Camera
    {
        public OpenTK.Vector2 TopLeftViewCorner { get; }
        public OpenTK.Vector2 BottomRightViewCorner { get; }
        public OpenTK.Vector2 Position { get; set; }
        public OpenTK.Vector2 Scale { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; } = 6.81f;
        
        public SimulationRenderer SimulationWindow { get; }

        public Camera(SimulationRenderer simulationWindow, float viewWidth = 64)
        {
            SimulationWindow = simulationWindow;
            float widthHeightRatio = 1.0f * simulationWindow.Width / simulationWindow.Height;
            float viewHeight = viewWidth / widthHeightRatio;
            TopLeftViewCorner = new OpenTK.Vector2(-viewWidth / 2, -viewHeight / 2);
            BottomRightViewCorner = new OpenTK.Vector2(viewWidth / 2, viewHeight / 2);

            Scale = new OpenTK.Vector2(50, 50);
        }

        public void Update(double deltaTime)
        {
            Scale = OpenTK.Vector2.One * Zoom;
        }

        public void UpdateView()
        {
            GL.LoadIdentity();
            GL.Ortho(TopLeftViewCorner.X, BottomRightViewCorner.X, BottomRightViewCorner.Y, TopLeftViewCorner.Y, 0, 1);
            GL.Scale(Scale.X, Scale.Y, 1);
            GL.Rotate((float)(Rotation / Math.PI * 180), Vector3.UnitZ);
            GL.Translate(new Vector3(-Position));
        }
    }
}