# Stopwatch Application

A simple stopwatch built with **C# Windows Forms** as a group project. It supports starting, pausing, resuming, resetting, and stopping a timer that displays elapsed time in **hh:mm:ss** format.

## How It Works

The app is split into two parts:

- **StopwatchEngine** — a plain C# class that handles all the timing logic (counting seconds, formatting the display, managing state). This is completely independent of the UI, which makes it easy to test.
- **Form1 (UI)** — the WinForms window that hosts a WebView2 control. The HTML/CSS/JS frontend in `wwwroot/` handles the display, and communicates with the C# backend via WebView2 messaging.

### Button Behavior

| Button   | What it does |
|----------|-------------|
| **Start**  | Begins counting from 00:00:00 |
| **Pause**  | Freezes the timer; displays current time |
| **Resume** | Continues from where it was paused |
| **Reset**  | Sends the timer back to 00:00:00 (keeps running) |
| **Stop**   | Stops completely and shows the last recorded time |

## How to Run

### Prerequisites
- .NET 7.0 SDK — [download here](https://dotnet.microsoft.com/download)
- Windows (WinForms requires it)
- WebView2 Runtime (included in modern Windows)

### Steps

1. Clone the repository:
   ```
   git clone <your-repo-url>
   cd "Stopwatch Application"
   ```

2. Build and run:
   ```
   dotnet run --project StopwatchApp
   ```

3. Run the tests:
   ```
   dotnet test
   ```

## Project Structure

```
Stopwatch Application/
├── StopwatchApp/
│   ├── Form1.cs                   # WebView2 bridge and event handling
│   ├── Form1.Designer.cs          # UI layout
│   ├── StopwatchEngine.cs         # Core stopwatch logic
│   ├── Program.cs                 # Entry point
│   └── wwwroot/                   # HTML/CSS/JS frontend
│       ├── index.html
│       └── style.css
├── StopwatchApp.Tests/
│   └── StopwatchEngineTests.cs
├── StopwatchApp.sln
└── README.md
```

## Testing

We used a **Test-Driven Development (TDD)** approach. The `StopwatchEngineTests` class covers:

- Initial state checks
- Start/Stop behavior
- Pause and Resume transitions
- Reset functionality
- Time formatting (seconds → minutes → hours)
- Edge cases (pausing when not running, stopping when already stopped)

Run all tests with:
```
dotnet test
```

## Team Members

- Ishimwe Bruno
-Thierry Gusenga
-Ntare Gama
