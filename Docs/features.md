# Blackholio — Feature Roadmap

> Multiplayer agar.io-inspired game built in Unity using Mirror Networking.  
> This document tracks implemented systems, planned gameplay mechanics, networking architecture, UI systems, and future improvements.

---

# 🎮 Current Game Overview

```mermaid
mindmap
  root((Blackholio))
    Multiplayer
      Mirror Networking
      KCP Transport
      SyncVars
      RPCs
    Gameplay
      Food System
      Growth System
      Player Eating
      Death State
    UI
      Death Screen
      Multiplayer HUD
    Future
      Leaderboard
      Minimap
      Main Menu
      Match System
```

---

# 📊 Overall Feature Progress

```mermaid
journey
    title Blackholio Feature Progress
    section Core Networking
      Multiplayer Spawn : 5: Done
      Player Sync : 5: Done
      RPC System : 4: Done
    section Gameplay
      Food System : 5: Done
      Growth : 5: Done
      Player Eating : 5: Done
      Death State : 4: Done
    section Polish
      Camera Follow : 4: Done
      UI Systems : 2: In Progress
      Audio : 1: Planned
    section Future
      Leaderboard : 1: Planned
      Minimap : 1: Planned
      Matchmaking : 1: Planned
```

---

# ✅ COMPLETED FEATURES

## 🌐 Multiplayer Foundation
* **Status:** Implemented

### Features
* Host/client multiplayer architecture
* Player spawning handling
* Local player authority isolation
* Synced transform movement
* Optimized multiplayer testing workflow
* KCP transport protocol layer setup

### Networking Architecture
```mermaid
flowchart LR
    A[Client Input] --> B[Mirror Commands]
    B --> C[Server Authority]
    C --> D[SyncVars]
    C --> E[RPCs]
    D --> F[All Clients Synced]
```

---

## 👤 Player System
* **Status:** Implemented

### Features
* Circle player movement handling
* Rigidbody2D physics configuration
* Local player isolated control loops
* Multiplayer network transform synchronization
* Independent camera rigs per client

### Player Lifecycle
```mermaid
stateDiagram-v2
    [*] --> Spawned
    Spawned --> Alive
    Alive --> Growing
    Growing --> Giant
    Giant --> Dead : Eaten
    Dead --> DeathScreen
```

---

## 🍎 Food System
* **Status:** Implemented

### Features
* Instantiated food prefabs
* Random coordinate world spawning
* 2D trigger collision detection
* Scale growth calculations on consumption

### Food Gameplay Loop
```mermaid
flowchart TD
    A[Spawn Food] --> B[Player Collision]
    B --> C[Destroy Food]
    C --> D[Increase Player Size]
```

---

## 📏 Growth System
* **Status:** Implemented

### Features
* `[SyncVar]` engine size synchronization
* Real-time local mesh scale updates
* Multiplayer-visible scaling loops
* Globally shared growth state tracking

### Growth Sync Flow
```mermaid
sequenceDiagram
    participant Client
    participant Server
    participant OtherClients

    Client->>Server: Eat Food
    Server->>Server: Validate
    Server->>OtherClients: SyncVar Update
```

---

## ⚔️ Player Eating System
* **Status:** Implemented

### Features
* Scale checks allowing bigger players to consume smaller ones
* Strict server-side collision validation
* Multiplayer-safe network authority flows
* Smooth scale transfer processing on successful consumption

### Eat Mechanic
```mermaid
flowchart TD
    A[Player Collision] --> B{Is Bigger?}
    B -->|Yes| C[Command To Server]
    C --> D[Server Validation]
    D --> E[Kill Smaller Player]
    E --> F[Increase Winner Size]
    B -->|No| G[Nothing Happens]
```

---

## ☠️ Death System
* **Status:** Implemented

### Features
* Fully synchronized death logic states
* Dedicated TargetRpc Death Screen UI panels
* Automatic movement script disabling post-death
* Invisible visual state toggles for dead players
* Dynamic physics collider state disabling

### Death Flow
```mermaid
sequenceDiagram
    participant BiggerPlayer
    participant Server
    participant Victim

    BiggerPlayer->>Server: CmdPlayerEaten()
    Server->>Server: Validate Sizes
    Server->>Victim: TargetRpc Death UI
    Server->>AllClients: Disable Dead Player
```

---

## 🎥 Camera Follow System
* **Status:** Implemented

### Features
* Isolated local player camera tracking loops
* Multiplayer-safe network camera targeting passes
* Completely independent viewport rendering per client

### Camera Logic
```mermaid
flowchart LR
    A[Find Local Player] --> B[Assign Camera Target]
    B --> C[Follow Player]
```

---

## 🧪 Multiplayer Testing Workflow
* **Status:** Implemented

### Features
* Unity 6 native Multiplayer Play Mode configurations
* Concurrent virtual background client runtimes
* Fast intra-editor design iteration workflows
* Complete removal of slow executable project rebuilding phases

### Testing Workflow Evolution
```mermaid
flowchart TD
    A[Old Workflow] --> B[Build EXE]
    B --> C[Reconnect]
    C --> D[Slow Testing]

    E[New Workflow] --> F[Multiplayer Play Mode]
    F --> G[Instant Testing]
```

---

# 🚧 CURRENTLY IN PROGRESS

## 🗺️ Map Boundary System
* **Status:** In Progress

### Planned Features
* Prevent infinite runtime world movement drift
* Systematically force active cluster player interaction
* Locked containment arena bounds walls

### Planned Logic
```mermaid
flowchart LR
    A[Player Position] --> B[Mathf.Clamp]
    B --> C[Stay Inside Arena]
```

---

## ⚖️ Size-Based Speed System
* **Status:** In Progress

### Goal
Balance core pacing dynamically by mapping scale thresholds:
* Smaller players retain high speed profiles
* Massive players suffer scalar speed reductions

### Planned Formula
```mermaid
flowchart LR
    A[Player Size] --> B[Speed Calculation]
    B --> C[Bigger = Slower]
```

---

# 📋 PLANNED FEATURES

## 🏆 Leaderboard System
* **Status:** Planned

### Planned Features
* Active top player stack ui rendering
* High-frequency real-time rank sorting
* Multi-client network score synchronization passes

### Planned Stack
```mermaid
flowchart LR
    A[Player Size] --> B[Score Calculation]
    B --> C[Leaderboard UI]
```

---

## 🧭 Minimap System
* **Status:** Planned
* **Planned Features:** World map scale indicators, player positioning dots, perimeter boundary alerts.

## 🍎 Food Respawn System
* **Status:** Planned
* **Goal:** Dynamic respawn processing loops maintaining optimized grid food density levels.
* **Logic flow:**
```mermaid
flowchart TD
    A[Food Eaten] --> B[Respawn Timer]
    B --> C[Spawn New Food]
```

## 🎵 Audio System
* **Status:** Planned
* **Planned Features:** Consumption audio cues, clean death impacts, ui click events, atmospheric backing music tracking.

## 🧾 Score UI
* **Status:** Planned
* **Planned Features:** Dedicated scale tracking modules, current mass counters, optimized update tracking loops.

## 🏠 Main Menu System
* **Status:** Planned
* **Planned Features:** Dedicated starter scenes, network matchmaking profiles, engine application exit routing.

## 🔐 Authentication + Backend
* **Status:** Planned
* **Planned Stack:** Supabase integration, profile database tables, global cloud database records, post-match statistical arrays.

### Planned Architecture
```mermaid
flowchart LR
    A[Unity Client] --> B[Mirror Gameplay Server]
    B --> C[Supabase Backend]
    C --> D[Leaderboard]
    C --> E[Authentication]
    C --> F[Cloud Save]
```

---

# 🧠 Technical Lessons Learned

```mermaid
mindmap
  root((Lessons Learned))
    Multiplayer Needs Authority
    Gameplay First Matters
    Overengineering Slows Projects
    Mirror Easier For Rapid Iteration
    SyncVars Affect Serialization
    RPCs Required For UI Events
```

---

# 🏁 CURRENT PLAYABLE LOOP

```mermaid
flowchart TD
    A[Spawn] --> B[Move]
    B --> C[Eat Food]
    C --> D[Grow]
    D --> E[Eat Smaller Players]
    E --> F[Become Larger]
    F --> G[Die]
    G --> H[Death Screen]
```

---

# 📌 Current State Summary

### Core Systems Working
* ✅ Multiplayer network player spawning  
* ✅ Synced transform network movement  
* ✅ Dynamic network food spawning  
* ✅ Active food consumption tracking  
* ✅ Accurate growth scale synchronization  
* ✅ Dynamic relative player eating rules  
* ✅ Isolated client death sequence cycles  
* ✅ Local TargetRpc Death UI panels  
* ✅ Unity 6 Multi-process Editor workflows  
* ✅ Isolated local player camera tracking  

---

# 🚀 Long-Term Vision

```mermaid
mindmap
  root((Future Vision))
    Competitive Multiplayer
    Ranked Matches
    Online Leaderboards
    Persistent Accounts
    Better UI Polish
    Matchmaking
    Mobile Support
```
