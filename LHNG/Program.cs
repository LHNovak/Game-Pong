using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace LHNG
{
    class Program : GameWindow
    {
        int XB = 0;
        int YB = 0;
        int TB = 20;
        int VBX = 3;
        int VBY = 3;

        int YJ1 = 0;
        int YJ2 = 0;

        int XJ1()
        {
            return -ClientSize.Width / 2 + LarguraJ() / 2;
        }

        int XJ2()
        {
            return ClientSize.Width / 2 - LarguraJ() / 2;
        }

        int LarguraJ()
        {
            return TB;
        }

        int AlturaJ()
        {
            return 3 * TB;
        }



        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            XB = XB + VBX;
            YB = YB + VBY;

            if (XB + TB / 2 > XJ2() - LarguraJ() / 2
                && YB - TB / 2 < YJ2 + AlturaJ() / 2
                && YB + TB / 2 > YJ2 - AlturaJ() / 2)
          
            {
                VBX = -VBX;
            }
            
            if (XB - TB / 2 < XJ1() + LarguraJ() / 2
                && YB - TB / 2 < YJ1 + AlturaJ() / 2
                && YB + TB / 2 > YJ1 - AlturaJ() / 2)
            {
                VBX = -VBX;
            }

            if (YB + TB / 2 > ClientSize.Height / 2)
            {
                VBY = -VBY;
            }

            if (YB - TB / 2 < -ClientSize.Height / 2)
            {
                VBY = -VBY;
            }

            if (XB < -ClientSize.Width / 2 || XB > ClientSize.Width / 2)
            {
                XB = 0;
                YB = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Key.W))
            {
                YJ1 = YJ1 + 5;
            }

            if (Keyboard.GetState().IsKeyDown(Key.S))
            {
                YJ1 = YJ1 - 5;
            }

            if (Keyboard.GetState().IsKeyDown(Key.Up))
            {
                YJ2 = YJ2 + 5;
            }

            if (Keyboard.GetState().IsKeyDown(Key.Down))
            {
                YJ2 = YJ2 - 5;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

            Matrix4 projection = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            DR(XB, YB, TB, TB, 1.0f, 1.0f, 0.0f);
            DR(XJ1(), YJ1, LarguraJ(), AlturaJ(), 1.0f, 0.0f, 0.0f);
            DR(XJ2(), YJ2, LarguraJ(), AlturaJ(), 0.0f, 0.0f, 1.0f);

            SwapBuffers();
        }

        void DR(int x, int y, int largura, int altura, float r, float g, float b)
        {
            GL.Color3(r, g, b);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, 0.5f * altura + y);
            GL.Vertex2(-0.5f * largura + x, 0.5f * altura + y);
            GL.End();
        }
        static void Main()
        {
            new Program().Run();
        }
    }
}
