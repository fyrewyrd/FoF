### Core Concept

Every unit exposes a fixed **5-slot dual-payload Sequence Grid**. Each slot contains both a **Tap** package and a **Hold** package (10 total attack definitions per unit, excluding bar skills). The player builds a sequence by choosing Tap or Hold for successive slots. Finishers are exact-match only and can trigger after any length from 1 to 5. The system is deliberately lean, predictable, and server-light.

### Sequence Rules

- Player may use 1–5 attacks and stop at any time for the desired effect.
- Sequence clears on:
    - Exact Finisher match, **or**
    - 3-second idle with no input.
- Player can freely abandon mid-sequence.
- Bar skills and equipment abilities **never** break the sequence.
- Only hits that deal **damage > 0** interrupt the sequence (100 % rate).
- Perfect blocks / parries / dodges (0 damage) do **not** interrupt and **do** reset the 3-second window.

### Defensive Layer

- Dodge / Block / Parry are available at any time.
- Multi-hit monster attacks require a fresh defensive input for **every** hit.
- Successful defense → resets the 3-second sequence window and preserves progress.
- After any successful defense, a **Tap** becomes a universal **Post-Defense Quick Attack** (common knowledge across all units).
    - Shield users: higher chance (or guaranteed) stagger via Shield Bash.
    - Dodge / Parry versions: same package, lower stagger chance.
- Successful stagger locks the monster for 1 second. The stagger runs **in parallel** with the player’s 3-second window (does not pause or extend it).
- Defensive actions cost stamina (self-limiting).

### Pets

- Pets never use the sequence system.
- They perform random assigned attacks, follow normal pet commands (follow / defend / stay / move-to), and scale damage from one of the owner’s stats.
- Pets are never interrupted by the sequence rules.

### Data Philosophy

- All combo definitions (Tap/Hold packages + Finisher tables) are **static**.
- Power comes only from character stats.
- No per-player combo variants or finisher leveling.
- Live server state per player is minimal: current unit, short ordered list of choices (max 5), 3-second timer, and a simple defense-window flag.

### Design Goals

- Predictable and inspectable (Unity sub-menu shows fixed sequences).
- Rewards knowledge of the unit without being rigid.
- Accessible to average reaction speeds (3-second window).
- Easy load/offload on unit swap.
- Server-light under the intended 4-player density.
- Deeper party synergies and true multi-stage skillchains (e.g. Ronin-style) are explicitly deferred.

### Combo Tap/Hold Setup

- There are two sets of combos: Tap and Hold. 
- Tap/Hold attacks are static animations/numbers per each step. 


---