# Slot Machine Simulator

A console-based slot machine simulator built in C# to practice **Object-Oriented Programming (OOP)** principles and **Entity Framework Core** with a real SQL Server database.

## Overview

This project simulates a simple slot machine game with 4 reels and configurable payout rules. Players can create an account (or log into an existing one), place bets, spin the machine, and see their balance update in real time — with every player and bet persisted to a SQL Server database.

## Features

- **Multi-reel slot machine** (4 reels) with configurable symbols and payout rules
- **Player accounts** — create a new account or log into an existing one by name
- **Persistent balances** — a player's balance carries over between sessions
- **Bet history** — every wager is recorded in the database, linked to the player
- **Input validation** — handles invalid numbers and yes/no answers gracefully
- **Unique player names** — prevents duplicate accounts

## Tech Stack

- **C# / .NET 9**
- **Entity Framework Core 9** (Code-First, Migrations)
- **SQL Server** (running in Docker)

## Project Structure

| File | Responsibility |
|---|---|
| `Symbol.cs` | Represents a single reel symbol (e.g. Cherry, Bell, Seven, Wild) |
| `Reel.cs` | Holds a list of symbols and can `Spin()` to return a random one |
| `PayoutRule.cs` | Defines how many matching symbols are needed for a payout, and how much it pays |
| `SlotMachine.cs` | Coordinates the reels and payout rules; contains the core game logic (`SpinAll`, `CalculateWinnings`, `Play`) |
| `Player.cs` | Represents a player, their balance, and their bet history |
| `Bet.cs` | Represents a single wager placed by a player |
| `SlotMachineDbContext.cs` | Entity Framework `DbContext` connecting the models to SQL Server |
| `Program.cs` | Console application entry point — the interactive game loop |

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- A running SQL Server instance (this project was developed against SQL Server 2019 in Docker)
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

### Setup

1. Clone the repository:
   ```
   git clone <your-repo-url>
   cd SlotMachineSimulator
   ```

2. Restore dependencies:
   ```
   dotnet restore
   ```

3. Update the connection string in `SlotMachineDbContext.cs` with your own SQL Server credentials:
   ```csharp
   optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SlotMachineDb;User Id=sa;Password=YOUR_PASSWORD_HERE;TrustServerCertificate=True;");
   ```

4. Apply the migrations to create the database:
   ```
   dotnet ef database update
   ```

5. Run the game:
   ```
   dotnet run
   ```

## How to Play

1. Answer whether you already have an account.
2. Enter your name (new players get a starting balance of 100).
3. Enter a bet amount and spin!
4. Choose to play again or quit — your balance is saved after every spin.

## Possible Future Improvements

- Configurable number of reels and symbols via the database
- Admin view of top players / biggest wins
- Web or desktop UI instead of console
- Unit tests for the payout calculation logic