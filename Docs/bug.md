# Blackholio — Bug Log

Track all major bugs, networking issues, failed experiments, architecture mistakes, and fixes during development.

---

# 📊 Development Bug Timeline

```mermaid
timeline
    title Blackholio Development Timeline

    16 May 2026 : SpacetimeDB SDK Runtime Failure
    28 May 2026 : Input System Conflict
                  : Transport Build Mismatch
                  : Infinite Food Clone Explosion
    29 May 2026 : SyncVar Serialization Failure
                  : Death UI Networking Failure
                  : Gameplay Continued After Death
                  : Multiplayer Testing Workflow Problem
```

---

# 🧠 Architecture Evolution

```mermaid
flowchart LR
    A[SpacetimeDB Backend Attempt] --> B[Massive SDK Errors]
    B --> C[Abandoned SpacetimeDB]
    C --> D[Switched to Mirror Networking]
    D --> E[Local Gameplay Prototype]
    E --> F[Real Multiplayer Authority]
```

---

# 16 May 2026

# 🐛 Bug — SpacetimeDB SDK Runtime Failure 🔥😵

## Error

```txt
ITuple could not be found
System.Runtime.CompilerServices.ITuple
```

Also included:

```txt
CS0315
CS7069
Unable to resolve reference 'Microsoft.CodeAnalysis'
Unable to resolve reference 'System.Collections.Immutable'
```

---

## Failure Dependency Chain

```mermaid
flowchart TD
    A[Unity 6000.3.8f1] --> B[SpacetimeDB SDK]
    B --> C[Assembly Dependency Conflicts]
    C --> D[Missing ITuple Runtime]
    D --> E[Broken Generated SDK Files]
    E --> F[Unity Compile Failure]
```

---

## Cause

* SpacetimeDB SDK dependency conflict.
* Runtime conflicts with Unity 6.
* SDK expected different .NET/runtime environment.
* Generated SDK files failed type resolution.

---

## Why This Was Difficult 😵

```mermaid
mindmap
  root((SpacetimeDB Debugging))
    Huge Error Wall
    SDK Internal Errors
    Unity Package Issues
    Runtime Conflicts
    .NET Compatibility
    Outdated Tutorials
```

---

## Final Decision ✅

```mermaid
flowchart LR
    A[Keep Fighting SDK] --> B[Risk Project Delay]
    C[Switch Architecture] --> D[Mirror Networking]
    D --> E[Fast Gameplay Progress]
```

* Abandoned SpacetimeDB for MCA prototype.
* Switched to:

  * Mirror Networking
  * KcpTransport
  * Gameplay-first architecture

---

# 28 May 2026

# 🐛 Bug — Unity Input System Conflict ⚠️

## Error

```txt
InvalidOperationException:
You are trying to read Input using the UnityEngine.Input class,
but you have switched active Input handling to Input System package.
```

---

## Cause Diagram

```mermaid
flowchart TD
    A[Unity 6 Project] --> B[New Input System Enabled]
    C[Old Tutorial Code] --> D[Input.GetAxisRaw()]
    B --> E[Conflict]
    D --> E
```

---

## Fix ✅

```txt
Player Settings
→ Active Input Handling
→ Both
```

---

# 28 May 2026

# 🐛 Bug — Client Could Not Connect To Host 🌐

## Symptoms

* Host started correctly.
* Client clicked connect but nothing happened.

---

## Root Cause

```mermaid
flowchart LR
    A[Editor] --> B[KcpTransport]
    C[Old EXE Build] --> D[Edgegap Transport]
    B --> E[Transport Mismatch]
    D --> E
```

---

## Fix ✅

* Deleted old build folder.
* Rebuilt project completely.
* Reassigned:

```txt
NetworkManager → KcpTransport
```

---

# 28 May 2026

# 🐛 Bug — Infinite Food Clone Explosion 💥💀

## Symptoms

* Unity froze/crashed.
* Thousands of:

```txt
Food(Clone)
```

spawned instantly.

---

## Catastrophic Spawn Loop

```mermaid
flowchart TD
    A[FoodSpawner Attached To Food Prefab]
    A --> B[Food Spawned]
    B --> C[Each Food Has FoodSpawner]
    C --> D[Spawns More Food]
    D --> E[Exponential Explosion]
    E --> F[Unity Crash]
```

---

## Cause

🚨 `FoodSpawner.cs` accidentally attached to Food prefab.

---

## Fix ✅

```mermaid
flowchart LR
    A[Remove FoodSpawner From Prefab]
    --> B[Create Dedicated FoodSpawner Object]
    --> C[Assign Food Prefab Correctly]
```

---

# 29 May 2026

# 🐛 Bug — Mirror SyncVar Serialization Failure ⚡

## Error

```txt
Type '[Assembly-CSharp]PlayerMovement'
has an extra field 'size'
```

---

## Serialization Breakdown

```mermaid
flowchart TD
    A[Added SyncVar size]
    --> B[Old Cache Still Exists]
    --> C[Class Layout Mismatch]
    --> D[Build Failure]
```

---

## Fix ✅

Deleted:

```txt
Library/
Temp/
Obj/
```

Then:

* reopened Unity
* rebuilt project

---

# 29 May 2026

# 🐛 Bug — Death UI Only Worked On Host 🧠🔥

## Symptoms

* Host could eat client.
* Host grew correctly.
* Client never saw death screen.

---

## Multiplayer Authority Confusion

```mermaid
sequenceDiagram
    participant Host
    participant Server
    participant Client

    Host->>Server: Player Collision
    Server->>Client: Missing UI Trigger
    Client-->>Client: No Death Screen
```

---

## Realization 💡

```mermaid
flowchart LR
    A[Mirror Object Copies]
    --> B[Each Client Has Local Version]
    --> C[UI Cannot Be Triggered Directly]
    --> D[Need TargetRpc]
```

---

## Final Correct Networking Flow ✅

```mermaid
sequenceDiagram
    participant Client
    participant Server
    participant TargetClient

    Client->>Server: CmdPlayerEaten()
    Server->>Server: Validate Size
    Server->>TargetClient: TargetRpc Death Screen
    TargetClient->>TargetClient: Show Death UI
```

---

# 29 May 2026

# 🐛 Bug — Gameplay Continued After Death 👻

## Symptoms

* Death UI appeared.
* Dead player still:

  * moved
  * collided
  * existed in game

---

## Missing State Transition

```mermaid
stateDiagram-v2
    [*] --> Alive
    Alive --> Dead : Player Eaten
    Dead --> DeathUI
```

---

## Fix ✅

Added:

```csharp
[SyncVar]
bool isDead;
```

Then:

* disabled movement
* disabled collider
* hid sprite
* synchronized death state

---

# 29 May 2026

# 🐛 Bug — Multiplayer Testing Workflow Was Painful 😫

## Old Workflow

```mermaid
flowchart LR
    A[Code Change]
    --> B[Build EXE]
    --> C[Open EXE]
    --> D[Reconnect]
    --> E[Retest]
    --> F[Repeat Forever]
```

---

## Discovery 🚀

Unity 6 Multiplayer Play Mode:

```txt
Window
→ Play Mode
→ Scenarios
```

---

## New Workflow

```mermaid
flowchart LR
    A[Code Change]
    --> B[Press Play]
    --> C[Virtual Multiplayer Clients]
    --> D[Test Immediately]
```

---

## Result ✅

Now able to rapidly test:

* RPCs
* SyncVars
* Authority
* Collisions
* Multiplayer gameplay

WITHOUT rebuilding constantly.

---

# 🏁 Current Project State

```mermaid
journey
    title Blackholio Development Progress

    section Networking
      Multiplayer Spawn : 5: Done
      SyncVars : 4: Done
      RPC Flow : 4: Done

    section Gameplay
      Food System : 5: Done
      Growth System : 5: Done
      Player Eating : 5: Done
      Death State : 4: Done

    section Remaining
      Camera Polish : 2: In Progress
      Leaderboard : 1: Future
      Minimap : 1: Future
      Match Flow : 2: Future
```
