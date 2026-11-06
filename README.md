# DE-RENDER: Quantum Souls

**Moth Quantum Atlas Bounty Submission · Single-Player GTA V PC Mod**  
**Author:** Nic Ghinea  
**Platform:** PC (Single-Player GTA V · ScriptHookV + ScriptHookVDotNet v3)  
**Atlas Engines Used:** Quantum Blur (`blur-core-v1`) & Tessa / QPIXL (`qpixl-v1`)  
**Atlas Dataset:** 224 Verified Quantum Jobs (`aer` backend · 512 shots per circuit)

---

## Overview

Pressing **`F10`** equips the player with quantum observer glasses that give the ability to see NPCs in their underlying quantum state. The mod de-renders the physical NPC facade—reducing their character mesh to a faint ghost silhouette (`alpha = 45`)—and transforms them into vertical 3D cellular **quantum soul pillars** made of rectangular data blocks that pulse and shimmer in real time.

Walking close to a soul (within **9m** with clear line-of-sight) opens a live **Biometric & Atlas HUD** above their head that exposes their hidden developer metadata—including wallet cash, vitals, internal model hash, skeletal height, outfit bitmask, quantum personality, and their verified **Atlas QPU Job ID**.

---

## Quantum Mechanics & The Observer Effect

The quantum data from Moth's Atlas platform directly governs how NPC souls look and behave through the **Quantum Observer Effect**:

### 1. Distance-Based Superposition & Wavefunction Collapse (`17m` $\rightarrow$ `9m`)
* **Superposition (`17m+`):** When NPCs are far away (`17m` or more), they exist in high-entropy superposition (`entropy = 0.8`). Their rectangular data blocks are scattered apart in 3D space and their colours continuously cycle and cross-fade across different quantum spectra at `0.25 Hz`.
* **Wavefunction Collapse (`17m` down to `9m`):** As you walk closer to observe them, their wavefunction linearly collapses (`entropy` drops from `0.8` down to `0.0`)—the scattered blocks physically pull together into a solid, coherent pillar and lock into their true collapsed 3-tier concentric colour palette.
* **Full Collapse (`< 9m`):** Once you get within `9m` with direct line-of-sight, full wavefunction collapse spawns floating `|0>` and `|1>` binary particles (within `8.5m`), reveals the live Biometric HUD, and triggers the NPC's **Quantum Personality**.

```csharp
// Entropy Calculation & Superposition Spectrum Cycling (DeRendered.cs #L786-820)
float obsDist = target.DistanceToPlayer;
float entropy = (obsDist >= 17.0f) ? 0.8f : (obsDist <= 9.0f ? 0.0f : ((obsDist - 9.0f) / (17.0f - 9.0f)) * 0.8f);
```

### 2. Four Quantum Personalities (`< 9m` Collapse)
On initialization, the 192 `qpixl-v1` spectra are ranked by their measured QPU channel `q1` amplitude into four balanced quartiles and interleaved across 24 quantum coherence sectors. Collapsing an NPC's wavefunction within `9m` triggers their assigned personality:
* **BRAVE:** Walks directly up to the player and stares at them (`TASK_GO_TO_ENTITY` + `TASK_LOOK_AT_ENTITY`).
* **SKITTISH:** Startled by the quantum observation and immediately flees from the player (`TASK_SMART_FLEE_PED`).
* **CURIOUS:** Stops walking and turns around to face and watch the player (`TASK_TURN_PED_TO_FACE_ENTITY`).
* **STOIC:** Unbothered by observation and continues normal life, unless the player aims a weapon directly at them, which triggers an instant flee reaction.

*(Note: Law enforcement, military, fire, and paramedic NPCs are protected from task overrides so normal police/emergency gameplay remains intact).*

### 3. Unobserved Off-Camera Superposition (`9m` to `30m`)
When NPCs outside `9m` (up to `30m`) are off-camera, they enter unobserved states where their measured `qpixl-v1` spectrum amplitudes (`Normalized[(k + 1) % 8]`) trigger unpredictable quantum events on a timer:
* **`u < 0.34`:** Freezes mid-step (`TASK_STAND_STILL`).
* **`0.34 <= u < 0.67`:** Wanders onto a new path (`TASK_WANDER_STANDARD`).
* **`u >= 0.67`:** Turns towards the player (`TASK_TURN_PED_TO_FACE_ENTITY`).

As soon as you turn the camera back to observe them (`onScreen == true`), any frozen state immediately releases back into normal walking.

---

## Zero-Latency Dual-Engine Atlas Architecture

Direct HTTP calls to cloud quantum endpoints add `100ms–500ms` of latency, causing frame drops and network stalls in GTA V. To keep the game running at a locked **60+ FPS** without dropped frames or network lag, **DE-RENDER** makes zero HTTP requests during gameplay. Instead, it reads directly from a pre-baked local bank of **224 verified quantum jobs** ([`DeRendered.baked.txt`](de_render_mod/DeRendered.baked.txt)) executed on Moth's Atlas platform via the Qiskit `aer` backend (`512` shots per circuit):

### 1. Spatial Wave Dispersion Engine — `blur-core-v1` (32 Jobs)
* **Format in `DeRendered.baked.txt`:** `B|idx|job_uuid|aer|512|16|8|flat_128_floats`
* **Role in Gameplay:** 32 diffusion matrices (`16x8` grids) smoothly cross-fade over `18.0s` Hermite-interpolated cycles, computing live QPU coherence (`95.0% – 99.8%`) and shimmer speed.
* **Visual Modulation:** Physically drives 3D tile depth displacement (`depthOffset`), superposition block scatter (`scat`), outward-expanding white shockwave pulses (`whitePulseIntensity`), and the animated frosted-glass background cells of the Biometric HUD.

### 2. Identity Collapse & Colour Synthesis Engine — `qpixl-v1` (192 Jobs)
* **Format in `DeRendered.baked.txt`:** `Q|idx|job_uuid|aer|512|input8|output8`
* **8D Hidden Engine Signature Extraction:** When an NPC is scanned, the mod extracts 8 hidden GTA V engine variables:
  1. `v0`: Model Hash (`ped.Model.Hash`)
  2. `v1`: 12-Slot Component, Drawable, Texture & Palette FNV-1a Bitmask (`GET_PED_DRAWABLE_VARIATION`)
  3. `v2`: Skeletal `IKHead`-to-Root Bone Micro-Offset (`ExtractSkeletonMicroOffset`)
  4. `v3`: Derived Combat Accuracy (`10% – 90%`)
  5. `v4`: Wallet Cash (`GET_PED_MONEY` / deterministic fallback)
  6. `v5`: Raw 32-Bit Relationship Group Hash & Ped Type (`GET_PED_RELATIONSHIP_GROUP_HASH`)
  7. `v6`: Normalized Health (`ped.Health`)
  8. `v7`: Spawn Orientation Quaternion Entropy (`ped.Quaternion`) & Armour
* **3-Tier Concentric Colour Synthesis:** Using sector-balanced 8D Euclidean nearest-neighbour matching (`FindNearestSpectrum`), each NPC is permanently bound to a `qpixl-v1` spectrum. All 8 measured QPU output channels (`q0..q7`) combine with their hidden biometrics to synthesise three concentric colour rings across the elliptical pillar:
  * **Tier 1 — Center Core (`distFromCenter < 0.45`):** Driven by `q0, q1, q2, q7` + wallet cash, combat accuracy, component bitmask, and bone entropy.
  * **Tier 2 — Mid Concentric Ring (`0.45 <= distFromCenter < 0.82`):** Driven by `q3, q4, q5` + relationship group hash.
  * **Tier 3 — Outer Perimeter Ring (`distFromCenter >= 0.82`):** Driven by `q6, q7, q0` + quaternion orientation entropy, fading into deep obsidian neon with dynamic Fresnel viewing-angle iridescence.

---

## Always-On Biometric & Atlas HUD (`< 9m`)

Approaching within `9m` of an NPC with clear line-of-sight (`HAS_ENTITY_CLEAR_LOS_TO_ENTITY`) fades in a frosted-glass telemetry panel anchored above their head displaying 10 live attributes:

| Row | Label | Source / Description |
| :--- | :--- | :--- |
| **1** | `Name` | Internal GTA V ped model name (e.g. `A_F_Y_BEVHILLS_01`, `G_M_Y_BALLAS_01`) |
| **2** | `Model` | 32-bit hex model hash + entity handle (`0xXXXXXXXX // ID #handle`) |
| **3** | `Outfit Code` | Drawable/Texture matrix + 16-bit FNV hash (`D:XX.XX.XX.XX T:XX.XX [XXXX]`) |
| **4** | `Voice_code` | Derived internal voice profile tag |
| **5** | `Active task` | Live AI task state (`TASK_WANDER_STANDARD`, `TASK_GO_TO_ENTITY`, `TASK_SMART_FLEE`, etc.) |
| **6** | `Vitals` | Live Health (`HP`), Armour (`ARM`), velocity/stress-driven Heart Rate (`BPM`), and Sweat (`SWT %`) |
| **7** | `Wallet` | NPC cash on hand (`$XX`) |
| **8** | `Height` | Skeletal `IKHead` bone-delta height in metres (`X.XX m (Δ+0.XXX)`) |
| **9** | `Quantum Personality` | Collapsed QPU quartile behaviour (`BRAVE`, `SKITTISH`, `CURIOUS`, or `STOIC [qpixl]`) |
| **10** | `Atlas Job Id` | Verified Atlas job identifier and backend (`qpixl-v1 [XXXXXXXX] (aer)`) |

---

## Requirements & Installation

### Requirements
1. **Grand Theft Auto V** (PC Single-Player only)
2. **[ScriptHookV](http://www.dev-c.com/gtav/scripthookv/)**
3. **[ScriptHookVDotNet v3](https://github.com/scripthookvdotnet/scripthookvdotnet/releases)** (v3.6+ / Nightly)

### Installation
1. Ensure you have a `scripts` folder inside your main **Grand Theft Auto V** directory (create `Grand Theft Auto V/scripts/` if it does not exist).
2. Copy the following files from [`de_render_mod/`](de_render_mod/) into your `Grand Theft Auto V/scripts/` folder:
   * `DeRendered.dll`
   * `DeRendered.baked.txt`
   * `DeRendered.ini`

### Controls & Configuration (`DeRendered.ini`)
* Press **`F10`** in-game to equip/unequip the Quantum Observer Glasses and toggle **DE-RENDER** ON or OFF.
* Edit `DeRendered.ini` inside `scripts/` to customize settings:
  ```ini
  [Settings]
  ToggleKey=F10
  ScanRadius=70.0
  MaxTargets=28
  PhosphorGreenR=50
  PhosphorGreenG=255
  PhosphorGreenB=120
  ```

---

## Repository Structure

* [`DeRendered.cs`](DeRendered.cs) — Complete C# source code for the mod (single-file architecture, zero external dependencies beyond ScriptHookVDotNet v3).
* [`de_render_mod/`](de_render_mod/) — Ready-to-install release package containing `DeRendered.dll`, `DeRendered.baked.txt` (224 verified Atlas jobs), `DeRendered.ini`, and `README.txt`.
* [`DeRender_Quantum_Architecture_Report.pdf`](DeRender_Quantum_Architecture_Report.pdf) — Technical architecture & Moth Atlas quantum engine integration specification.
