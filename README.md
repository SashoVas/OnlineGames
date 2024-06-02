# Tic-Tac-Toe and Connect4 Online Platform
## Overview
This project is an online gaming platform built with ASP.NET Core as the backend and Angular as the frontend. It offers two classic games: Tic-Tac-Toe and Connect4, both of which can be played in two different modes: multiplayer and single-player against the computer. The platform also includes features such as friend management, chatting, notifications, and user profiles.

## Features
* Multiplayer Mode: Play against other players around the world using WebSocket communication.
  * Room-based gameplay where a random GUID is generated for room creation.
  * Players can join a game by entering the room GUID.
* Single-Player Mode: Play against the computer with an AI opponent.
  * Tic-Tac-Toe AI: Implements a simple Minimax algorithm.
  * Connect4 AI: Uses an optimized Minimax algorithm with enhancements like alpha-beta pruning, memoization, and bitmasking to store the game board as an integer.
* Friend Management: Add and manage friends on the platform.
* Chat: Communicate with friends through a built-in chat system.
* Notifications: Receive notifications for game invites, friend requests, and other activities.
* User Profiles: Create and manage user profiles.

## Menu Preview

https://github.com/SashoVas/OnlineGames/assets/98760930/369bcc8b-8e81-4c5d-a654-bf739e2c2320

## Game Algorithms
### Tic-Tac-Toe Minimax
The Minimax algorithm for Tic-Tac-Toe is a recursive method that evaluates all possible moves and chooses the best one by maximizing the minimum gain. It is straightforward due to the small size of the game board (3x3).

### Connect4 Minimax with Optimizations
The Connect4 AI is more complex due to the larger board (7x6). It uses the following optimizations to improve performance:

* Alpha-Beta Pruning: Reduces the number of nodes evaluated in the Minimax algorithm.
  * Purpose: To reduce the number of nodes evaluated by the Minimax algorithm.
  * Method: Introduces two additional parameters, alpha and beta, which represent the minimum score that the maximizing player is assured of and the maximum score that the minimizing player is assured of, respectively.
  * Pruning: When it is determined that a move cannot possibly improve the result, further exploration of that move is abandoned, thus saving computation time.
* Memoization: Stores already evaluated board states to avoid redundant calculations.
  * Purpose: To avoid redundant calculations by storing and reusing previously computed results.
  * Method: A cache is maintained to store the results of evaluated game states. When the same game state is encountered again, the stored result is used instead of recalculating.
* Bitmasking: Represents the game board as an integer for efficient storage and manipulation.
  * Purpose: To represent the game board efficiently for quick access and manipulation.
  * Method: The game board is encoded as a series of bits in an integer. Each position on the board is represented by a bit, and the entire board state can be stored in a small number of integers. This representation allows for fast bitwise operations to check for wins, possible moves, and other game state evaluations.
#### Position Scoring Algorithm
In both games, an additional algorithm is used to evaluate and score each possible move. This scoring algorithm is crucial for the AI to determine the most strategic moves by assigning a score to every position on the board. The scoring takes into account various factors such as potential wins, blocks, and strategic advantages.

## Game Preview

https://github.com/SashoVas/OnlineGames/assets/98760930/bb0d3adb-295f-4cc1-8a22-4a58e4e81f98

## Technologies Used
* Backend: ASP.NET Core
  * Handles game logic, user management, WebSocket communication, notifications, and chat functionality.
* Frontend: Angular
  * Provides a responsive and interactive user interface for the games and other platform features.
* WebSocket: Used for real-time multiplayer communication.
* Database: To store user data, game records, and chat history.

## Getting Started
### Prerequisites
* .NET Core SDK
* Node.js and npm
* Angular CLI
### Installation
1. Clone the repository:

```bash
git clone https://github.com/your-username/tictactoe-connect4-platform.git
cd OnlineGames
```
2. Backend Setup:

Navigate to the backend project directory and restore the dependencies:

```bash
cd OnlineGames.Server
dotnet restore
```
3. Run the backend server:

```bash
dotnet run
```
4. Frontend Setup:

Navigate to the frontend project directory, install the dependencies, and start the Angular development server:

```bash
cd OnlineGames.Client
npm install
ng serve
```
5. Access the Application:

Open your web browser and navigate to http://localhost:4200 to start playing.

## Contributing
Contributions are welcome! Please open an issue or submit a pull request with your changes.


## Contact
For questions or feedback, please contact your-email@example.com.

Enjoy playing Tic-Tac-Toe and Connect4!
