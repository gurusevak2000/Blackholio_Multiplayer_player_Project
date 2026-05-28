# Blackholio — Bug Log

Track all major bugs, errors, failed experiments, and fixes during development.

---

# 28 May 2026

## 🐛 Bug — SpacetimeDB SDK Runtime Failure

### Error
```txt
ITuple could not be found
System.Runtime.CompilerServices.ITuple
```

### Cause
* SpacetimeDB SDK dependency conflict.
* Runtime conflicts with Unity 6000.3.8f1.
* Incompatibility with .NET setup.

### Attempted Fixes
* Changed API Compatibility Level.
* Deleted `Library/` and `Temp/` folders.
* Reimported all packages.
* Checked SDK version compatibility.

### Final Decision
* Migrated networking architecture to Mirror Networking.

### Status
* ✅ **Closed**

---

## 🐛 Bug — Player Falling Down Continuously

### Cause
* `Rigidbody2D` gravity enabled by default.

### Fix
* Set `Gravity Scale = 0`.

### Status
* ✅ **Fixed**

---

## 🐛 Bug — Input System Exception

### Error
```txt
InvalidOperationException:
You are trying to read Input using the UnityEngine.Input class...
```

### Cause
* Project used the **New Input System**.
* Scripts still used legacy `Input.GetAxisRaw()`.

### Fix
* Opened **Player Settings**.
* Navigated to **Active Input Handling**.
* Changed setting to **Both**.

### Status
* ✅ **Fixed**

---

## 🐛 Bug — Client Failed To Connect

### Cause
* Mirror automatically used `EdgegapKcpTransport`.
* Transport interfered with `localhost` testing.

### Fix
* Removed `EdgegapKcpTransport`.
* Added standard `KcpTransport`.
* Rebuilt the executable.

### Status
* ✅ **Fixed**

---

## 🐛 Bug — Second Player Not Spawning

### Cause
* Testing attempted inside the same game instance.
* Did not use separate host/client environments.

### Fix
* Configured Unity Editor as **Host**.
* Built standalone EXE as **Client**.
* Connected through `localhost`.

### Status
* ✅ **Fixed**

---

## 🐛 Bug — Old Build Still Using Previous Transport

### Cause
* Standalone EXE build contained outdated transport data.

### Fix
* Deleted the old `Build/` folder.
* Rebuilt the project completely.

### Status
* ✅ **Fixed**
