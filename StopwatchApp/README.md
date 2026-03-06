# Premium Stopwatch Application ⏱️

A modern, high-performance Stopwatch application built using **C# (WinForms)** for the core logic and **HTML/CSS/JS** for a premium, hardware-accelerated frontend via **Microsoft WebView2**.

## ✨ Key Features

- **Modern Aesthetics**: Built with glassmorphism, dark mode, and dynamic glow animations.
- **Precision Logic**: Core time tracking handled in C# using the system timer for reliability.
- **Full Control**: 
  - **Start**: Initializes the timer from 00:00:00.
  - **Pause**: Halts the timer while preserving current progress.
  - **Resume**: Continues exactly from where you left off.
  - **Reset**: Restarts the timer to zero without stopping.
  - **Stop**: Ends the session and displays the final recorded time.
- **Progressive Display**: Automatically increments seconds to minutes and hours (hh:mm:ss format).

## 🛠️ Technical Stack

- **Backend**: .NET 7.0 + C#
- **Frontend**: HTML5, Vanilla CSS3 (Custom Design), JavaScript
- **Bridge**: WebView2 for bidirectional communication between UI and Logic.
- **Documentation**: Fully documented using XML Documentation comments (///).

## 🚀 How to Run

1. **Prerequisites**:
   - [.NET 7.0 SDK](https://dotnet.microsoft.com/download)
   - [WebView2 Runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/) (Included in most modern Windows installations).

2. **Clone & Open**:
   ```bash
   # Navigate to the project directory
   cd StopwatchApp
   ```

3. **Build & Execute**:
   ```bash
   dotnet run
   ```

## 📂 Project Structure

- `Form1.cs`: The bridge between C# and the Web frontend.
- `StopwatchEngine.cs`: Pure C# logic for time calculations and state management.
- `wwwroot/`: Contains the premium HTML/CSS frontend assets.
- `StopwatchApp.csproj`: Project configuration including WebView2 dependency.

## 📝 XML Documentation

Every method in this project includes thorough XML documentation to explain its purpose, parameters, and return values, adhering to professional C# standards.
