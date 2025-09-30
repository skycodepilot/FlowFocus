# FlowFocus

**FlowFocus** is a Blazor web application designed to help hybrid and fully-remote workers care for their well-being. This tech demo showcases modern .NET 8 Blazor features, built-in Bootstrap styling, and integrated account management using ASP.NET Core Identity.

## Features

- **Blazor Web UI**: Built with .NET 8 and C# 12, leveraging Blazor's component-based architecture.
- **Account Management**: Register, log in, and manage accounts using ASP.NET Core Identity.
- **External Authentication**: Support for external login providers (UI in place).
- **Bootstrap Styling**: Responsive and modern UI out-of-the-box.
- **Reminders**: Retrieve helpful wisdom via HTTP GET requests (see "Reminders" in navigation).
- **Test Coverage**: Includes a test project using xUnit, bUnit, and Moq.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or later

### Running the Application

1. **Clone the repository**:
    ```sh
    git clone <your-repo-url>
    cd FlowFocus
    ```

2. **Restore dependencies**:
    ```sh
    dotnet restore
    ```

3. **Apply database migrations** (if using local SQL Server):
    ```sh
    dotnet ef database update
    ```

4. **Run the application**:
    ```sh
    dotnet run --project FlowFocus
    ```

5. Open your browser and navigate to `https://localhost:5001` (or the URL shown in the console).
