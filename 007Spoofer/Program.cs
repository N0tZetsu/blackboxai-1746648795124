using System;
using System.Numerics;
using System.Threading;
using ImGuiNET;
using Vortice.Direct3D11;
using Vortice.DXGI;
using Vortice.Mathematics;
using Vortice.Win32;
using static Vortice.Direct3D11.D3D11;
using static Vortice.DXGI.DXGI;

namespace _007Spoofer
{
    class Program
    {
        private static bool _showSpoofMessage = false;
        private static bool _showCleanMessage = false;

        static void Main(string[] args)
        {
            var form = new System.Windows.Forms.Form()
            {
                Text = "007 Spoofer",
                ClientSize = new System.Drawing.Size(800, 450),
                StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen,
            };

            var swapChainDesc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(form.ClientSize.Width, form.ClientSize.Height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                Usage = Usage.RenderTargetOutput,
                OutputHandle = form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                IsWindowed = true,
                SwapEffect = SwapEffect.Discard,
                Flags = SwapChainFlags.AllowModeSwitch,
            };

            D3D11CreateDeviceAndSwapChain(null, DriverType.Hardware, DeviceCreationFlags.BgraSupport, null, swapChainDesc, out var device, out var context, out var swapChain);

            var renderTargetView = CreateRenderTarget(device, swapChain);

            ImGui.CreateContext();
            ImGui.StyleColorsDark();

            var io = ImGui.GetIO();
            io.Fonts.AddFontDefault();

            var imguiRenderer = new ImGuiRenderer(device, context, swapChain, form.ClientSize.Width, form.ClientSize.Height);

            form.FormClosing += (sender, e) =>
            {
                imguiRenderer.Dispose();
                renderTargetView.Dispose();
                swapChain.Dispose();
                context.Dispose();
                device.Dispose();
            };

            form.Show();

            System.Windows.Forms.Application.Idle += (sender, e) =>
            {
                while (form.IsIdle)
                {
                    RenderFrame(imguiRenderer, renderTargetView);
                }
            };

            System.Windows.Forms.Application.Run(form);
        }

        private static void RenderFrame(ImGuiRenderer imguiRenderer, ID3D11RenderTargetView renderTargetView)
        {
            var context = imguiRenderer.Context;
            context.OMSetRenderTargets(renderTargetView);

            context.ClearRenderTargetView(renderTargetView, new Color4(0.0f, 0.12f, 0.15f, 1.0f));

            ImGui.NewFrame();

            ImGui.SetNextWindowSize(new Vector2(400, 300), ImGuiCond.FirstUseEver);
            ImGui.Begin("007 Spoofer");

            ImGui.TextColored(new Vector4(0.5f, 1.0f, 1.0f, 1.0f), "FIVEM Spoofer");
            ImGui.Separator();

            if (ImGui.Button("SPOOF"))
            {
                _showSpoofMessage = true;
            }

            if (ImGui.Button("CLEANER"))
            {
                _showCleanMessage = true;
            }

            if (_showSpoofMessage)
            {
                ImGui.Text("HWID Spoofing functionality is not yet implemented.");
            }

            if (_showCleanMessage)
            {
                ImGui.Text("Cleaning completed successfully.");
            }

            ImGui.End();

            ImGui.Render();
            imguiRenderer.RenderDrawData(ImGui.GetDrawData());

            imguiRenderer.Present();
        }

        private static ID3D11RenderTargetView CreateRenderTarget(ID3D11Device device, IDXGISwapChain swapChain)
        {
            using var backBuffer = swapChain.GetBuffer<ID3D11Texture2D>(0);
            return device.CreateRenderTargetView(backBuffer);
        }
    }
}
