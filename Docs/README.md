# Blackholio

Multiplayer agar.io-inspired game built using Unity 6 and Mirror Networking.

## 🚀 Overview

Blackholio is a real-time multiplayer 2D game where players control circles that grow by consuming smaller objects and eventually other players.

The project is being developed as an MCA final-year project, focusing on multiplayer networking architecture and real-time synchronization.

## 🎮 Current Features

* Multiplayer host/client system
* Real-time player synchronization
* Independent player movement
* Local multiplayer testing
* Mirror Networking integration


## 📋 Planned Features

* Food spawning system
* Player growth mechanic
* Eat smaller player mechanic
* Score system
* Leaderboard
* UI polish
* Online hosting support


## 🛠️ Tech Stack


| Technology | Purpose |
| :--- | :--- |
| **Unity 6000.3.8f1** | Game Engine |
| **Mirror Networking** | Multiplayer Networking |
| **KCP Transport** | Low-latency Transport |
| **C#** | Programming Language |

## 📺 Multiplayer Test

### Video Demo
* 🔗 [Watch Multiplayer Test](PASTE_VIDEO_LINK_HERE)

## 📸 Screenshots

*(Add screenshots here later)*

## 📂 Project Structure

```txt
Blackholio/
│
├── Assets/
├── Packages/
├── ProjectSettings/
│
├── Docs/
│   ├── README.md
│   ├── devlog.md
│   ├── bug.md
│   ├── features.md
│
├── Builds/
│
└── .gitignore
```

## 🌐 Networking Architecture

Mirror Networking handles:
* Player spawning
* Real-time synchronization
* Client/server communication

The project currently uses:
* **Host** = Server + Local Player
* **Client** = Remote Player

## 🎓 Learning Goals

This project focuses on:
* Multiplayer game architecture
* Real-time synchronization
* Client/server networking
* Unity multiplayer workflows
* Scalable project organization

## 📝 Development Notes

Initial development started with **SpacetimeDB** but later migrated to **Mirror Networking** due to SDK/runtime compatibility issues and development speed concerns.

## 👤 Author

* **MCA Project — 2026**
