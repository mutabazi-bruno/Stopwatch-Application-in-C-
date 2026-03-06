using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using System.Text.Json;

namespace StopwatchApp
{
    /// <summary>
    /// Main form for the Stopwatch application using a WebView2 HTML/CSS frontend.
    /// This class acts as the bridge between the high-performance C# logic and the modern HTML UI.
    /// </summary>
    public partial class Form1 : Form
    {
        private StopwatchEngine _engine;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// Sets up the core logic engine and starts the WebView2 initialization process.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            _engine = new StopwatchEngine();
            InitializeWebView();
        }

        /// <summary>
        /// Asynchronously initializes the WebView2 control.
        /// Sets up environment, loads the HTML frontend, and hooks up message event handlers.
        /// </summary>
        private async void InitializeWebView()
        {
            try
            {
                // Wait for the CoreWebView2 environment to be ready
                await webView.EnsureCoreWebView2Async(null);
                
                // Allow JavaScript to C# communication
                webView.CoreWebView2.Settings.IsWebMessageEnabled = true;

                // Map the local wwwroot folder to a virtual host name for better security and reliability
                string contentDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");
                webView.CoreWebView2.SetVirtualHostNameToFolderMapping(
                    "app.stopwatch", contentDirectory, CoreWebView2HostResourceAccessKind.Allow);

                // Load the application frontend
                webView.CoreWebView2.Navigate("https://app.stopwatch/index.html");

                // Hook into the message event to receive actions from the UI
                webView.CoreWebView2.WebMessageReceived += OnWebMessageReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Critical: Failed to initialize WebView2 frontend. Error: {ex.Message}\n\nMake sure the WebView2 Runtime is installed on your system.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles messages received from the HTML/JavaScript frontend.
        /// This is the entry point for all UI-driven actions.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data containing the message (usually an action string).</param>
        private void OnWebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            // We expect the frontend to send simple action strings like "start", "pause", etc.
            string action = e.TryGetWebMessageAsString();
            
            if (!string.IsNullOrEmpty(action))
            {
                HandleAction(action);
            }
        }

        /// <summary>
        /// Executes the appropriate engine logic based on the action string received from the UI.
        /// This manages the state transitions of the stopwatch.
        /// </summary>
        /// <param name="action">The command to execute (start, pause, resume, reset, stop).</param>
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
        /// Event handler for the system timer tick (1-second interval).
        /// Increments the engine's counter and pushes the new time to the UI.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void TickTimer_Tick(object? sender, EventArgs e)
        {
            _engine.Tick();
            UpdateFrontendDisplay();
        }

        /// <summary>
        /// Sends a command to the JavaScript frontend to update the main time display.
        /// </summary>
        private void UpdateFrontendDisplay()
        {
            var data = new { type = "updateDisplay", time = _engine.GetFormattedTime() };
            SendMessageToFrontend(data);
        }

        /// <summary>
        /// Sends a message to the JavaScript frontend to update the status text.
        /// </summary>
        /// <param name="status">The text to display in the status area.</param>
        private void UpdateFrontendStatus(string status)
        {
            var data = new { type = "updateStatus", status = status };
            SendMessageToFrontend(data);
        }

        /// <summary>
        /// Synchronizes the UI button states (Enabled/Disabled) with the C# engine's state.
        /// </summary>
        /// <param name="running">Whether the stopwatch is currently active.</param>
        /// <param name="paused">Whether the stopwatch is currently paused.</param>
        private void UpdateFrontendButtons(bool running, bool paused)
        {
            var data = new { type = "updateButtons", running = running, paused = paused };
            SendMessageToFrontend(data);
        }

        /// <summary>
        /// Helper method to serialize an object to JSON and post it as a message to the WebView2.
        /// </summary>
        /// <param name="data">The object to send to the frontend.</param>
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
