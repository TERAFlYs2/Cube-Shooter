# Multiplayer shooter with good graphics on Unity and network framework Photon Pun 

## About the project
This is a session-based shooter developed in Unity using the Photon Pun network framework. Players can create their own session or join an existing one, navigate a small map with a custom physics controller, use power-ups, and fire a rocket launcher at other players.

## What has been implemented
- Weapon and shooting system with network hit synchronization;
- Character health, damage, and respawn system;
- Synchronization of player positions, animations, and rotations;
- HUD interface with health and stamina;
- Room join menu and matchmaking via Photon;
- Optimized state transfer for minimal network latency

## How to play

### Objective
Destroy other players

### Controls
- **WASD** — move
- **Space** — jump
- **Shift** — run
- **RMB** — shoot
- **R** — reload

### Gameplay
1. Create a room or join an existing one.
2. After the map loads, a character will appear.
3. Use the flare gun to eliminate opponents.
4. The player with the most kills wins.

## Technologies
- Unity 6
- C#
- Photon Pun 

## Architecture
- MVC
- Event-driven
- ScriptableObjects
- Facade
- OOP
- EntryPoint

## Screenshots

<img width="1280" height="720" alt="photo_2025-07-03_12-29-49" src="https://github.com/user-attachments/assets/8465fef4-1899-40cf-89f7-1e7047ca3578" />

Main menu


<img width="1280" height="720" alt="photo_2025-07-03_12-29-53" src="https://github.com/user-attachments/assets/aeba228c-86a5-44bb-bfd6-15485e35be9a" />

Session search menu


<img width="1280" height="512" alt="photo_2025-
07-03_12-29-56" src="https://github.com/user-attachments/assets/d3ad48f0-adbf-4d66-ab65-602344562718" />

Demonstration of the scene and interface of the health and stamina hood


<img width="1280" height="524" alt="photo_2025-07-03_12-30-04" src="https://github.com/user-attachments/assets/5d8140e8-e844-410e-adf5-261a485b85d2" />

Demonstration of a shot and explosion from a bazooka, and damage to the player


<img width="1280" height="526" alt="photo_2025-07-03_12-30-10" src="https://github.com/user-attachments/assets/7865ed2d-bc39-4bd8-8591-c0761edc7544" />

A frame of a flying projectile and correct processing of the physics of fast projectiles and synchronization


## Video demonstration

https://drive.google.com/drive/folders/1wO48FAtIYbDhtdgvaGRRWf-7AJDCAuCf?usp=sharing



