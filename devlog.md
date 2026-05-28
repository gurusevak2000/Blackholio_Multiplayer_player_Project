# Devlog

## Week 1

### Day 1
- Setup Unity project
- Installed Mirror Networking library
- Created NetworkManager with components:
  - NetworkManager
  - KcpTransport
  - NetworkManagerHUD
- Created Player prefab with components:
  - Rigidbody2D (Gravity Scale = 0)
  - CircleCollider2D
  - NetworkIdentity
  - NetworkTransformReliable
- Verified player spawning works correctly
- Created PlayerMovement.cs script with:
  - WASD/Arrow key input handling
  - isLocalPlayer check to prevent all players moving together
  - Multiplayer-aware movement synchronization
- Tested Host mode - player movement working correctly
- Ready for multiplayer client/server testing

### Day 2
- [ ] Build the game executable
- [ ] Test Host/Client multiplayer synchronization
- [ ] Verify two clients can connect and control their own players independently
- [ ] Test movement sync across network
