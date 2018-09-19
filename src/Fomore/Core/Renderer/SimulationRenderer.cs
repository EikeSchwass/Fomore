using System;
using System.Drawing;
using System.Linq;
using Core.Physics;
using FarseerPhysics.Collision.Shapes;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Core.Renderer
{
    public class SimulationRenderer : GameWindow
    {
        public Simulation Simulation { get; }
        public Camera Camera { get; }

        public SimulationRenderer(Simulation simulation, int width, int height) : base(width,
                                                                                       height,
                                                                                       GraphicsMode.Default,
                                                                                       "Simulation",
                                                                                       GameWindowFlags.Default,
                                                                                       DisplayDevice.Default)
        {
            Simulation = simulation;
            Camera = new Camera(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Black);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            Camera.Zoom += e.DeltaPrecise / 20.0f * Camera.Zoom;
        }

        /// <inheritdoc />
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            Simulation.Tick(0.25f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            Simulation.Tick((float)e.Time);
            Camera.Position = Simulation.SimulationEntities.First().Bones.Select(b => b.Position).Average().ToOpenTK();
            Camera.Update(e.Time);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.LoadIdentity();
            Camera.UpdateView();

            foreach (var entity in Simulation.SimulationEntities) RenderEntity(entity, Color.Blue);
            RenderTerrain(Color.SandyBrown);

            SwapBuffers();
        }

        public void RenderTerrain(Color terrainColor)
        {
            GL.Color3(terrainColor);

            var vertices = Simulation.Terrain.Vertices.ToList();
            GL.Begin(PrimitiveType.Lines);
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                GL.Vertex2(vertices[i].X, vertices[i].Y);
                GL.Vertex2(vertices[i + 1].X, vertices[i + 1].Y);
            }
            GL.End();
        }

        public void RenderEntity(SimulationEntity entity, Color entityColor)
        {
            GL.Color3(entityColor);

            foreach (var body in entity.Bones)
            {
                var shape = (PolygonShape)body.FixtureList[0].Shape;
                GL.PushMatrix();
                GL.Translate(body.Position.X, body.Position.Y, 0);
                GL.Rotate((float)(body.Rotation / (Math.PI * 2) * 360), Vector3.UnitZ);
                GL.Translate(-body.Position.X, -body.Position.Y, 0);
                GL.Begin(PrimitiveType.Polygon);
                {
                    for (int i = 0; i < shape.Vertices.Count; i++)
                    {
                        double x1 = body.Position.X;
                        double y1 = body.Position.Y;

                        x1 += shape.Vertices[i].X;
                        y1 += shape.Vertices[i].Y;

                        GL.Vertex2((float)x1, (float)y1);
                    }
                }
                GL.End();
                GL.PopMatrix();
            }

            GL.Color3(Color.Red);

            foreach (var joint in entity.Joints)
            {
                var body = joint;
                var shape = (PolygonShape)body.FixtureList[0].Shape;
                GL.PushMatrix();
                GL.Translate(body.Position.X, body.Position.Y, 0);
                GL.Rotate((float)(body.Rotation / (Math.PI * 2) * 360), Vector3.UnitZ);
                GL.Translate(-body.Position.X, -body.Position.Y, 0);
                GL.Begin(PrimitiveType.Polygon);
                {
                    for (int i = 0; i < shape.Vertices.Count; i++)
                    {
                        double x1 = body.Position.X;
                        double y1 = body.Position.Y;

                        x1 += shape.Vertices[i].X;
                        y1 += shape.Vertices[i].Y;

                        GL.Vertex2((float)x1, (float)y1);
                    }
                }
                GL.End();
                GL.PopMatrix();
            }
        }
    }
}