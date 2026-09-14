-Swashbuckler sub activities

### How it maps to systems

| State | Player control | NavMesh / movement | Crew |
|-------|----------------|--------------------|------|
| **Docked** | Free walk on deck + dock | Deck can use a normal (static) NavMesh; world NavMesh for the pier | Idle / stand on deck points |
| **Underway** | Locked to helm, steer like a vehicle | Player agent disabled or set to “vehicle mode”; boat is moved by server | NPCs follow simple deck paths or stay at assigned stations |
| **Boarding / disembark** | Trigger + interact | Teleport / parent to helm seat or to a dock spawn point | Same |

### Why this works well

- **Underway**: no need for a moving NavMesh. The boat is a vehicle; the player sends steer input, the server moves the boat.
- **Docked**: the boat is treated as a static platform, so a normal baked NavMesh on the deck (or even just the world NavMesh if the deck is simple) is enough for walking.
- **Crew**: path only on the deck when docked; when underway they can be kinematic / animated at stations or follow short local paths relative to the boat.

### Server authority sketch

1. Player interacts with gangplank / “Board” → server validates, parents (or teleports) them to the helm seat, sets state `OnShip_Helming`.
2. While helming, client sends only steering input; server integrates boat position/rotation (simple physics or direct transform).
3. “Dock” / arrive at pier → server snaps boat to dock, switches state to `OnShip_Docked`, enables free movement on deck.
4. “Disembark” → server places player on the pier NavMesh and clears ship state.

### Crest / RAM

Still client-only for the water surface. Server can use a flat water plane or a simple height check for “is the boat in water” and dock zones.

This approach avoids almost all of the moving-NavMesh pain while still giving you steering, crew presence, and walkable decks when it matters (at dock). When you want to flesh out the board/helm/dock state machine and the network messages, we can do that next.

-aura users
top down aura tossing slasher/spinner/aoe'r. the player can finally let loose

-supply forts 