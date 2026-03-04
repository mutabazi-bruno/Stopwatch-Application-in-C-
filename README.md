# Stopwatch Application

A simple stopwatch built with **C# Windows Forms** as a group project. It supports starting, pausing, resuming, resetting, and stopping a timer that displays elapsed time in **hh:mm:ss** format.

## How It Works

The app is split into two parts:

- **StopwatchEngine** — a plain C# class that handles all the timing logic (counting seconds, formatting the display, managing state). This is completely independent of the UI, which makes it easy to test.
- **Form1 (UI)** — the WinForms window with five buttons and a timer label. A `System.Windows.Forms.Timer` fires every second and calls `Tick()` on the engine, then updates the label.

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
- .NET 10 SDK (or later) — [download here](https://dotnet.microsoft.com/download)
- Windows (WinForms requires it)

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
├── StopwatchApp/                  # Main WinForms project
│   ├── Form1.cs                   # Button handlers and UI logic
│   ├── Form1.Designer.cs          # UI layout (controls, colors, positions)
│   ├── StopwatchEngine.cs         # Core stopwatch logic (testable)
│   └── Program.cs                 # Entry point
├── StopwatchApp.Tests/            # NUnit test project
│   └── StopwatchEngineTests.cs    # Unit tests for the engine
├── StopwatchApp.sln               # Solution file
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
