using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using System.Text.Json;

namespace StopwatchApp
{
    public partial class Form1 : Form
    {
        private StopwatchEngine _engine;

        public Form1()
        {
            InitializeComponent();
            _engine = new StopwatchEngine();
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            try
            {
                await webView.EnsureCoreWebView2Async(null);
                webView.CoreWebView2.Settings.IsWebMessageEnabled = true;

                string contentDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");
                webView.CoreWebView2.SetVirtualHostNameToFolderMapping(
                    "app.stopwatch", contentDirectory, CoreWebView2HostResourceAccessKind.Allow);

                webView.CoreWebView2.Navigate("https://app.stopwatch/index.html");
                webView.CoreWebView2.WebMessageReceived += OnWebMessageReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize WebView2: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnWebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string action = e.TryGetWebMessageAsString();
            
            if (!string.IsNullOrEmpty(action))
            {
                HandleAction(action);
            }
        }

        private void HandleAction(string action)
        {
            switch (action.ToLower())
            {
                case "start":
                    _engine.Start();
                    tickTimer.Start();
                    UpdateFrontendDisplay();
                    UpdateFrontendStatus("Running...");
                    UpdateFrontendButtons(true, false);
                    break;

                case "pause":
                    _engine.Pause();
                    tickTimer.Stop();
                    UpdateFrontendStatus($"Paused at {_engine.GetFormattedTime()}");
                    UpdateFrontendButtons(true, true);
                    break;

                case "resume":
                    _engine.Resume();
                    tickTimer.Start();
                    UpdateFrontendStatus("Running...");
                    UpdateFrontendButtons(true, false);
                    break;

                case "reset":
                    _engine.Reset();
                    UpdateFrontendDisplay();
                    UpdateFrontendStatus("Reset — timer restarted from 00:00:00");
                    break;

                case "stop":
                    _engine.Stop();
                    tickTimer.Stop();
                    UpdateFrontendDisplay();
                    UpdateFrontendStatus($"Stopped at {_engine.LastRecordedTime}");
                    UpdateFrontendButtons(false, false);
                    break;
            }
        }

        private void TickTimer_Tick(object? sender, EventArgs e)
        {
            _engine.Tick();
            UpdateFrontendDisplay();
        }

        private void UpdateFrontendDisplay()
        {
            var data = new { type = "updateDisplay", time = _engine.GetFormattedTime() };
            SendMessageToFrontend(data);
        }

        private void UpdateFrontendStatus(string status)
        {
            var data = new { type = "updateStatus", status = status };
            SendMessageToFrontend(data);
        }

        private void UpdateFrontendButtons(bool running, bool paused)
        {
            var data = new { type = "updateButtons", running = running, paused = paused };
            SendMessageToFrontend(data);
        }

        private void SendMessageToFrontend(object data)
        {
            if (webView?.CoreWebView2 != null)
            {
                string json = JsonSerializer.Serialize(data);
                webView.CoreWebView2.PostWebMessageAsJson(json);
            }
        }
    }
}
