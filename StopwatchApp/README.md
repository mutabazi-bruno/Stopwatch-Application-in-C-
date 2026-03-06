# Stopwatch App

A stopwatch built with C# WinForms and an HTML/CSS/JS frontend rendered via WebView2.

## Features

- Start, Pause, Resume, Reset, Stop
- Time displayed in hh:mm:ss format
- Animated dark-themed UI with particle effects and glowing rings

## How to Run

1. Install the [.NET 7.0 SDK](https://dotnet.microsoft.com/download)
2. Make sure [WebView2 Runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/) is installed (comes with modern Windows)
3. Run:
   ```
   cd StopwatchApp
   dotnet run
   ```

## Structure

- `Form1.cs` — connects the C# logic to the HTML frontend
- `StopwatchEngine.cs` — core timing logic
- `wwwroot/` — HTML, CSS, and JS for the UI
- `StopwatchApp.csproj` — project config with WebView2 dependency
