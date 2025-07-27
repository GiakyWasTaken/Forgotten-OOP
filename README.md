# Forgotten OOP

Forgotten OOP is a text-based adventure game built using object-oriented programming principles. The game challenges players to explore a mysterious map, collect items, and interact with various entities while solving puzzles and overcoming obstacles.

## About the Project

This project was developed as part of an Object-Oriented Programming (OOP) exam. It was created in just two weeks by a team of five university students, consisting of two storytellers and three developers. The collaboration combined creative storytelling with solid programming practices to deliver an engaging and dynamic gameplay experience.

## Features

- **Exploration**: Navigate through a procedurally generated map with interconnected rooms.
- **Inventory Management**: Collect, use, and manage items like keys, torches, and repellents.
- **Commands**: Execute various commands such as moving, inspecting items, and interacting with the environment.
- **Save and Load**: Save your progress and resume the game later.
- **Dynamic Gameplay**: Encounter enemies, grab items, and adapt to changing scenarios.
 
## Grid Structure

The game environment is structured as a **7x7 grid**, where rooms are distributed across the space.

- **Special Rooms**: The outermost ring of the grid is reserved for special rooms, specifically:
  - **The Key Room**: Contains the key required for progression.
  - **The NPC Room**: Hosts the NPC essential to the storyline.

These rooms are critical for advancing through the game and solving its challenges.

## Safe Zones (Pink Rooms)

The game introduces **pink rooms**, which serve as safe zones where the monster cannot enter. These rooms are strategically placed and correspond to key points in the game's storyline. They contain essential elements such as:

- **The Torch**: Allows exploration of darker rooms.
- **The Key**: Unlocks closed doors.
- **The NPC**: Provides guidance or assistance.

This design balances the difficulty by offering players strategic safe zones, encouraging meaningful exploration while maintaining a sense of challenge.

## Requirements

- **.NET 8.0**: The game is built using .NET 8.0 and requires it to run.
- **C# 12.0**: The project uses modern C# features.

## Installation

1. Clone the repository: `git clone https://github.com/GiakyWasTaken/Forgotten-OOP.git`  
2. Navigate to the project directory: `cd Forgotten-OOP`  
3. Build the project using the .NET CLI: `dotnet build`

## Usage

1. Run the game: `dotnet run`
2. Follow the on-screen instructions to navigate the game menu.
3. Use commands like `Move`, `Inspect`, `Grab`, and `Use` to interact with the game world.

## Commands

- **Move**: Navigate to adjacent rooms using cardinal directions (North, South, East, West).
- **Inspect**: Examine items or the environment.
- **Grab**: Pick up items from the ground.
- **Use**: Use items like keys or torches to progress.
- **Map**: View the current map layout.
- **Pause**: Pause the game and return to the main menu.
- **Help**: Display a list of available commands.

## Save and Load

- Save your progress at any time using the `Save` command.
- Load a previously saved game from the main menu.

## Acknowledgments

- This project was made possible through the combined efforts of a dedicated team of university students in two busy weeks.
- See contributors in the GitHub repository for more details on the members of this team.
