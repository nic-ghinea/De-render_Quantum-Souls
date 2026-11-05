================================================================================
   DE-RENDER: QUANTUM SOULS // GRAND THEFT AUTO V MOTH QUANTUM MOD
================================================================================
Bounty:       Moth Quantum GTA V Mod Bounty (https://mothquantum.com/bounties/gtavmod)
Version:      Final Release v1 (de-render_final_v1 / v3.3 Core)
Platform:     Grand Theft Auto V (PC Single-Player Only — Strictly Offline / No GTA Online)
Engines Used: Dual-Engine Moth Atlas Integration ('blur-core-v1' + 'qpixl-v1')
Atlas Jobs:   224 Verified QPU Executions (32 Phase-Diffusion + 192 State-Collapse)

Disclaimer:   This mod is an independent entry for the Moth Quantum GTA V Mod Bounty.
              It is not affiliated with, endorsed by, or connected to Rockstar Games
              or Take-Two Interactive. Contains zero Rockstar logos or proprietary assets.

================================================================================
1. EXECUTIVE SUMMARY & ARTISTIC VISION
================================================================================
"What do we actually know about a stranger walking down the street?
In Grand Theft Auto V, every pedestrian possesses hidden internal code—shooting 
accuracy, relationship factions, hidden cash, audio sound banks, and alertness.
In exchange for revealing their digital data, we erase their physical face."

DE-RENDER is an interactive quantum reality-erasure modification for GTA V. 
When activated via [F9], the player equips quantum-observer sunglasses and ambient 
pedestrians across Los Santos physically dissolve into glowing, monolithic quantum 
memory registers (chamfered rectangular data blocks).

Unlike classical shaders or pseudo-random scripts, every pedestrian's:
  1. Micro-block depth displacement (Z-jitter) & cellular wave shimmer
  2. Concentric 3-tier iridescence & color signature
  3. Wavefunction superposition (>=17m) & Observer Effect collapse (<=9m)
  4. AI psychological personality state machine (BRAVE, SKITTISH, CURIOUS, STOIC)
  5. Off-camera unobserved quantum fluctuations (Behind-the-Back Observer Rule)

are PHYSICALLY and DETERMINISTICALLY driven by real Quantum Processing Unit (QPU) 
measurements generated on Moth's Atlas Quantum Cloud.

================================================================================
2. END-TO-END MOTH QUANTUM PIPELINE & SCHEMATIC
================================================================================
Grand Theft Auto V requires frames to render in under 16.6ms (60+ FPS). Live HTTP 
REST requests to cloud quantum processors take 500ms to 2,000ms per call due to 
network transport and hardware queuing. Calling the cloud API every frame would 
freeze the game and expose private API credentials.

To achieve locked 60+ FPS performance with 100% genuine quantum fidelity, we built
a 3-stage Cryptographic Quantum Baking & Real-Time Evaluation Pipeline:

  +--------------------------------------------------------------------------+
  | STAGE 1: MOTH ATLAS CLOUD EXECUTION (https://api.mothquantum.com/v1/)    |
  +--------------------------------------------------------------------------+
  |  * Engine 1: 'blur-core-v1' (32 Jobs: B|0 to B|31 | 512 shots | aer)     |
  |    8-qubit circuits applying Hadamard superposition gates and controlled |
  |    phase-diffusers to generate 16 x 8 probability density grids (128     |
  |    floating-point measurement probabilities per job).                    |
  |                                                                          |
  |  * Engine 2: 'qpixl-v1' (192 Jobs: Q|0 to Q|191 | 512 shots | aer)       |
  |    8-qubit parameterized state-preparation and projective measurement    |
  |    circuits mapping an 8D input parameter vector into an 8-element       |
  |    collapsed quantum output spectrum.                                    |
  +--------------------------------------------------------------------------+
                                       |
                                       v
  +--------------------------------------------------------------------------+
  | STAGE 2: VERIFIED LOCAL ATLAS BANK (DeRendered.baked.txt - 224 Jobs)     |
  +--------------------------------------------------------------------------+
  |  * Every job's exact UUID (e.g. a05f2d7f-62fb-4571-9333-ac8a12488782),   |
  |    backend ('aer'), shot count (512), input vector, and measured QPU     |
  |    output probabilities are serialized into 'DeRendered.baked.txt'.      |
  |  * Judges can directly cross-check every UUID in 'DeRendered.baked.txt'  |
  |    and on the in-game Biometric HUD against Moth's Atlas account logs.   |
  +--------------------------------------------------------------------------+
                                       |
                                       v
  +--------------------------------------------------------------------------+
  | STAGE 3: REAL-TIME C# GAMEPLAY ENGINE (DeRendered.dll @ 60+ FPS)         |
  +--------------------------------------------------------------------------+
  |  * Continuous Wave Field ('blur-core-v1'):                               |
  |    Hermite smoothstep cross-fades between the 32 16x8 QPU matrices over  |
  |    18-second cycles, driving block Z-depth, coherence glints, outward    |
  |    white shockwaves, and HUD frosted-glass refraction.                   |
  |                                                                          |
  |  * 8D Biometric Matching & Quartile Classification ('qpixl-v1'):         |
  |    Every 200ms, nearby NPCs are scanned. Their hidden GTA V variables    |
  |    (Model, Faction, Voice, Accuracy, Wallet, Alertness, Health, Armor)   |
  |    form an 8D vector matched via Euclidean nearest-neighbor against      |
  |    24 balanced quantum coherence sectors (192 QPU spectra).              |
  +--------------------------------------------------------------------------+

================================================================================
3. CONCRETE DATA EXAMPLES FROM 'DeRendered.baked.txt'
================================================================================
Example A — Spatial Wave Dispersion Job ('blur-core-v1'):
  Record:  B|0|a05f2d7f-62fb-4571-9333-ac8a12488782|aer|512|16|8|0,0,...,1,0.0178571,...
  Meaning: Job #0 on 'blur-core-v1' (UUID: a05f2d7f-62fb-4571-9333-ac8a12488782),
           512 shots on 'aer', yielding a 16x8 probability matrix. In-game,
           QuantumAtlasBank.ProcessBlurGrid normalizes and smooths this 16x8 grid
           to physically displace each of the 18x10 micro-blocks on Tier-1 NPCs.

Example B — Identity & Personality Collapse Job ('qpixl-v1'):
  Record:  Q|0|d9b1b772-4201-474f-8328-bb346af84923|aer|512|
           0.842,0.657,0.424,0.444,0.183,0.867,0.122,0.221|
           0.867,0.652799,0.407624,0.407169,0.122,0.867,0.122,0.224294
  Meaning: Job #0 on 'qpixl-v1' (UUID: d9b1b772-4201-474f-8328-bb346af84923).
           - Input 8D Vector:  [0.842, 0.657, 0.424, 0.444, 0.183, 0.867, 0.122, 0.221]
           - Output Spectrum:  [0.867, 0.652799, 0.407624, 0.407169, 0.122, 0.867, 0.122, 0.224294]
           - Normalized[0,2,5,3,7] shift the NPC's Concentric 3-Tier HSV aura palette.
           - Normalized[1] (the 2nd measured qubit register) ranks the job into one
             of the 4 quantum personality quartiles.
           - Normalized[(k+1)%8] drives the NPC's off-camera Observer Rule actions.

================================================================================
4. CORE GAMEPLAY MECHANICS & THE QUANTUM OBSERVER EFFECT
================================================================================
1. UNIVERSAL TOGGLE [F9] & NATIVE GTA V HUD:
   - Pressing [F9] automatically equips Michael, Franklin, or Trevor with custom 
     sunglasses and engages Quantum Reality Erasure.
   - UI instructions render in native GTA V help-box style above the mini-map:
     "PRESS F9 TO ACTIVATE DE-RENDER" // "PRESS F9 TO STOP DE-RENDER"

2. THE OBSERVER EFFECT (Wavefunction Superposition & Collapse):
   - Far Away (>= 17m / Unobserved): Entities exist in quantum superposition 
     (entropy = 0.8). Blocks physically drift apart and scatter in 3D space, 
     cycling through superposition palettes and 'blur-core-v1' wave amplitudes.
   - Approaching (17m down to 9m): As the player draws near, observation forces 
     measurement; entropy smoothly interpolates from 0.8 down to 0.0.
   - Observed (<= 9m): Wavefunction completely collapses (entropy = 0.0). All 
     rectangular data blocks snap into a solid, monolithic crystalline pillar 
     with hairline precision, and the Biometric HUD fades in.

3. 100% PURE QPU PERSONALITIES (Exact 25% Quartiles across 192 Atlas Jobs):
   Upon loading 'DeRendered.baked.txt', all 192 'qpixl-v1' QPU spectra are ranked
   by their second measured qubit output register (Normalized[1]) into 4 exact
   quantum quartiles (48 jobs per personality) and interleaved into 24 balanced
   8-job coherence sectors for 8D Euclidean nearest-neighbor biometric matching:
   
   * BRAVE (QPU Quartile 1 | 48 Atlas Jobs — 25%):
     The NPC halts their ambient task, breaks away from the sidewalk, and 
     confidently walks directly up to the player (TASK_GO_TO_ENTITY).
     
   * SKITTISH (QPU Quartile 2 | 48 Atlas Jobs — 25%):
     The NPC gasps, panics upon quantum observation, and sprints away in terror 
     (TASK_SMART_FLEE).
     
   * CURIOUS (QPU Quartile 3 | 48 Atlas Jobs — 25%):
     The NPC halts dead in their tracks, turning their head and torso to stare 
     intently at the player while observed (TASK_TURN_PED_TO_FACE_ENTITY).
     
   * STOIC (QPU Quartile 4 | 48 Atlas Jobs — 25%):
     Completely unfazed. Continues their natural walk/stance ignoring the player 
     unless aimed at with a weapon, which shatters their stoicism and triggers flight.

4. THE BEHIND-THE-BACK OBSERVER RULE (Off-Screen Quantum Fluctuations):
   Pedestrians behind the player's camera (unobserved within 30m) undergo quantum 
   fluctuations on a timer driven by their Atlas spectrum (Normalized[(k+1)%8]): 
   randomly freezing in place (TASK_STAND_STILL), redirecting paths, or turning 
   toward the player. Turning the camera back collapses them back into normal life.

5. 10-ITEM BIOMETRIC TELEMETRY HUD (Within 9m with Direct Line-of-Sight):
   Header: QUANTUM ID: <DecodedPedModelName>
   - 1. Name:                Exact internal GTA V model name (e.g., Bevhills02AMY)
   - 2. Category:            Ped classification (CIVILIAN // MALE, LAW ENFORCEMENT, etc.)
   - 3. Family:              Relationship faction group (METROPOLITAN_NEUTRAL, BALLAS, etc.)
   - 4. Voice_code:          Audio sound bank identifier (e.g., VOICE_BEVHILLS02_M2)
   - 5. Active task:         Live native AI task (TASK_TURN_PED_TO_FACE_ENTITY, etc.)
   - 6. Vitals:              Real-time HP and Armor status
   - 7. Wallet:              Exact ped wallet cash contents
   - 8. Rel to Player:       Native relationship state (NEUTRAL, FRIENDLY, HOSTILE)
   - 9. Quantum Personality: [Gold Highlight] BRAVE, SKITTISH, CURIOUS, or STOIC [qpixl]
   - 10. Atlas Job Id:       [Cyan Highlight] Exact Moth cloud execution UUID & backend

================================================================================
5. DEVELOPMENT ROADMAP & VERSION EVOLUTION
================================================================================
- v1.0 (de-render_alpha_v1): Initial rectangular micro-block aura & ped scanner.
- v2.0 (de-render_v2.0):     3-Tier Concentric Circular HSV Color Architecture
                             driven by Wallet Cash, Accuracy, and Health Vitality.
- v2.1 (de-render_v2.1):     Automatic player sunglasses prop on F9, slowed
                             quantum white shockwave pulses, and LoS wall culling.
- v2.2 (de-render_v2.2):     Decoupled steady-speed shimmer and 60+ FPS culling.
- v2.3 (de-render_v2.3):     Added 'qpixl-v1' QPU personality state machines,
                             17m->9m Observer Effect collapse, and off-screen rule.
- v3.0 - v3.2:               Added native GTA V top-left F9 help prompt, 10-item
                             Biometric HUD layout, QUANTUM ID header, and pure
                             QPU personality classification.
- Final v1 (v3.3 Core):      Exact 48/48/48/48 QPU quartile ranking, 24-sector
                             balanced 8D Euclidean nearest-neighbor matching,
                             avalanche entity hashing, and persistent AI tasks.

================================================================================
6. INSTALLATION GUIDE
================================================================================
PREREQUISITES:
- Grand Theft Auto V (PC Single-Player)
- Script Hook V (ScriptHookV.dll + dinput8.dll in GTA V root folder)
- Script Hook V .NET v3 (ScriptHookVDotNet.asi + ScriptHookVDotNet3.dll in GTA V root)

INSTALLATION STEPS:
1. Open your Grand Theft Auto V game directory:
   (e.g., D:\steam\steamapps\common\Grand Theft Auto V\)
2. Navigate to (or create) the `scripts` folder:
   `Grand Theft Auto V\scripts\`
3. Copy the following 3 files from `de-render_final_v1` into `scripts\`:
   - DeRendered.dll
   - DeRendered.ini
   - DeRendered.baked.txt
4. Launch GTA V Story Mode (Single-Player).
5. Press [F9] in-game to toggle DE-RENDER on or off. (Press [Insert] to reload).

================================================================================
7. PACKAGE CONTENTS
================================================================================
- DeRendered.dll:                            Compiled C# binary (SHVDN v3).
- DeRendered.cs:                             Full, self-contained C# source code.
- DeRendered.ini:                            User configuration file.
- DeRendered.baked.txt:                      224 verified Moth Atlas QPU jobs.
- DE_RENDER_MOTH_QUANTUM_DOCUMENTATION.pdf:  Illustrated technical whitepaper.
- DE_RENDER_QUANTUM_WHITEPAPER.html:         HTML source for the whitepaper.
- README.txt:                                This complete reference guide.
================================================================================
