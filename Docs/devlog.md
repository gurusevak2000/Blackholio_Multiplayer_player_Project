# Blackholio — Development Log

> Multiplayer agar.io-inspired game built in Unity using Mirror Networking.  
> This document tracks technical progress, fixes, experiments, and milestones during development.

---

# Week 1 — Networking Foundation

## 📅 28 May 2026

### 🎯 Goal
Establish a working multiplayer foundation before implementing gameplay systems.

---

## ✅ Project Initialization

### Completed
- Created fresh Unity 6000.3.8f1 project
- Selected Universal 2D template
- Planned architecture for multiplayer circle-based game
- Decided to move away from SpacetimeDB due to SDK/runtime instability during development

### Notes
Initial backend approach used SpacetimeDB, but repeated compatibility issues slowed iteration speed significantly. Switched to a more production-stable stack focused on rapid gameplay iteration.

---

# 🔄 Architecture Decision

## Previous Stack
- Unity
- SpacetimeDB
- Custom reducers/tables

## New Stack
- Unity 6
- Mirror Networking
- KCP Transport
- (Supabase planned later for leaderboard/auth)

### Why the Switch?
- Faster iteration
- Easier debugging
- Better Unity compatibility
- Larger tutorial ecosystem
- More suitable for MCA project deadline

---

# 🌐 Multiplayer Setup

## Mirror Networking Installed

### Completed
- Installed Mirror Networking via Git URL
- Added:
  - NetworkManager
  - KcpTransport
  - NetworkManagerHUD
- Created initial multiplayer architecture

### Learned
Mirror handles:
- player spawning
- synchronization
- host/client networking

Unlike SpacetimeDB, Mirror does NOT manage database tables or reducers.

---

# 👤 Player Prefab Setup

## Player Object Created

### Components Added
- SpriteRenderer
- Rigidbody2D
- CircleCollider2D
- NetworkIdentity
- NetworkTransformReliable

### Fixes
- Disabled gravity by setting:
  ```txt
  Gravity Scale = 0