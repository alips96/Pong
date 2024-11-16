# Pong: Online Multiplayer Pong Game

This project is an **Online Multiplayer Pong Game** developed using **Unity**, **Microsoft Azure PlayFab**, **Photon Engine (Photon Cloud Services)**, and **Discord**. The project also leverages **Zenject** as a dependency injection framework.

---

## Key Features and Design Patterns

### Architecture
- The game follows the **MVC (Model-View-Controller)** architectural pattern:
  - **Model**: Handles data logic.
  - **View**: Handles user interface and rendering.
  - **Controller**: Orchestrates the interaction between the model and view.
- **Strategy Design Pattern**: Used for key features to allow future extensions of behaviors.
- **Observer Design Pattern**: Manages interaction between different modules.
- **Unit Tests**: Implemented for critical methods, primarily as edit-mode tests.

---

## Code Organization
The codebase is organized into five main folders:
1. **General**: Handles shared logic for ball and player movement, applicable to both single-player and multiplayer modes.
2. **Login Menu**: Manages the login process.
3. **Multiplayer**: Contains logic for Photon, PlayFab, and Discord integration.
4. **Single Player**: Handles single-player-specific logic.
5. **Installers**: Contains dependency injection containers using three MonoInstallers (one for each scene).

**Note**: No external packages are used apart from a button set.

---

## Game Flow

### Login Process
1. The game starts with a login prompt.
2. Authentication is handled via an API call to the **PlayFab server**.
3. After login, the game connects to the **Photon Master Server** using the player's PlayFab username, which acts as their Photon nickname.
4. The server connection is established based on settings configured on the Photon dashboard.

### Multiplayer Setup
1. **Join a Lobby**:
   - Players join a lobby to find friends.
   - Synchronization of PlayFab and Photon friends' lists occurs every 15 seconds via API calls.
   - New friends can be added by entering their PlayFab usernames (they must have a PlayFab account).
2. **Room Creation**:
   - A room is created with the host's username.
   - Invitations to friends are sent using **Photon Chat Services**.
   - Upon accepting an invitation, the invited player joins the room, which becomes closed and invisible.
3. **Gameplay**:
   - Players are directed to the multiplayer scene to play.
   - After a match concludes, stats are updated via PlayFab API calls.
   - Players can replay or return to the main menu. If one player exits, the other must return to the menu as well.
   - The room is destroyed when both players leave.

### Single-Player Mode
- Runs locally, independent of Photon services.
- High scores are stored in the **PlayFab database**, managed via the **PlayFab Leaderboard**.

### Notifications
- All notifications are handled via **Discord** using a webhook.
- Webhook URLs are used to make HTML calls with desired parameters.
- These scripts, written in **JavaScript**, execute on PlayFab cloud servers, with embedded functionalities for chat purposes.

---

## Performance
- The game achieves exceptional performance:
  - **1600 FPS** with 8 batches on Unity Editor.
  - **VSync** is set to "Every VBlank".
- The system design ensures most tasks run asynchronously, awaiting callbacks for efficiency.

---

This project showcases robust architecture and integration, creating a smooth multiplayer gaming experience. Let me know if you have feedback or suggestions!
