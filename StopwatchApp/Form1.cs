using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using System.Text.Json;

namespace StopwatchApp
{
    /// <summary>
    /// Main form — hosts the WebView2 control and bridges button clicks
    /// from the HTML frontend to the StopwatchEngine backend.
    /// </summary>
    public partial class Form1 : Form
    {
        private StopwatchEngine _engine;

        /// <summary>
        /// Sets up the form, creates the engine, and loads the web UI.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            _engine = new StopwatchEngine();
            InitializeWebView();
        }

        /// <summary>
        /// Loads the WebView2 runtime, maps the wwwroot folder, and navigates to our HTML page.
        /// </summary>
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

        /// <summary>
        /// Receives button click messages from the HTML page and forwards them to HandleAction.
        /// </summary>
        /// <param name="sender">The WebView2 control.</param>
        /// <param name="e">Contains the message string sent from JavaScript.</param>
        private void OnWebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string action = e.TryGetWebMessageAsString();
            
            if (!string.IsNullOrEmpty(action))
            {
                HandleAction(action);
            }
        }

        /// <summary>
        /// Routes the action string (start, pause, resume, reset, stop) to the right engine method
        /// and updates the frontend accordingly.
        /// </summary>
        /// <param name="action">The button action received from the web page.</param>
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

        /// <summary>
        /// Fires every second — ticks the engine and pushes the updated time to the frontend.
        /// </summary>
        private void TickTimer_Tick(object? sender, EventArgs e)
        {
            _engine.Tick();
            UpdateFrontendDisplay();
        }

        /// <summary>
        /// Sends the current formatted time to the HTML page.
        /// </summary>
        private void UpdateFrontendDisplay()
        {
            var data = new { type = "updateDisplay", time = _engine.GetFormattedTime() };
            SendMessageToFrontend(data);
        }

        /// <summary>
        /// Sends a status message (e.g. "Paused at 00:01:23") to the HTML page.
        /// </summary>
        /// <param name="status">The status text to display.</param>
        private void UpdateFrontendStatus(string status)
        {
            var data = new { type = "updateStatus", status = status };
            SendMessageToFrontend(data);
        }

        /// <summary>
        /// Tells the frontend which buttons should be enabled or disabled.
        /// </summary>
        /// <param name="running">Whether the stopwatch is currently running.</param>
        /// <param name="paused">Whether the stopwatch is paused.</param>
        private void UpdateFrontendButtons(bool running, bool paused)
        {
            var data = new { type = "updateButtons", running = running, paused = paused };
            SendMessageToFrontend(data);
        }

        /// <summary>
        /// Serializes and sends a JSON message to the WebView2 frontend.
        /// </summary>
        /// <param name="data">Object to serialize and send.</param>
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
