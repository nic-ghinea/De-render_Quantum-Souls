// ============================================================================
// DE-RENDER: QUANTUM SOUL & BIOMETRIC IDENTITY ERASURE
// ============================================================================
// "What do we actually know about a stranger walking down the street?
//  In exchange for their data, we erase their face."
//
// Dual-Engine Moth Quantum Architecture (100% Offline / Zero-Latency Bank):
// 1. Spatial Wave Dispersion Engine (blur-core-v1):
//    32 QPU diffusion matrices (512 shots, IBM/Aer backend) cross-fading continuously,
//    physically driving tile depth displacement (Z-jitter) and cellular sparkle.
// 2. Identity Collapse Engine (qpixl-v1):
//    192 quantum state spectra mapping the NPC's true digital signature:
//    relationship groups, voice hash, combat psychology, accuracy, and money.
//
// Visuals:
// - Seamless micro-square mosaic aura with dynamic Fresnel iridescence.
// - Volumetric inner luminescence + live QPU coherence sparkle.
// - Dynamic ground-projected quantum light pool.
// - Always-on micro-HUD (within 7m) showing hidden developer NPC data.
// - Universal Toggle: F9
// ============================================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;

#pragma warning disable CS0618

namespace DeRenderMod
{
    #region Configuration

    public sealed class ModConfig
    {
        public Keys ToggleKey { get; set; }
        public float ScanRadius { get; set; }
        public int MaxTargets { get; set; }
        public Color PhosphorGreen { get; set; }

        public ModConfig()
        {
            ToggleKey = Keys.F9;
            ScanRadius = 70.0f;
            MaxTargets = 28;
            PhosphorGreen = Color.FromArgb(255, 50, 255, 120);
        }

        public static ModConfig Load(string iniPath)
        {
            var config = new ModConfig();
            try
            {
                if (!File.Exists(iniPath))
                {
                    File.WriteAllText(iniPath,
                        "; DE-RENDER // QUANTUM OBSERVER MOD\r\n" +
                        "; All quantum operations run directly from the verified 224-job Atlas bank (DeRendered.baked.txt)\r\n" +
                        "; Zero background network calls, zero latency, 60+ FPS.\r\n\r\n" +
                        "[Settings]\r\n" +
                        "ToggleKey=F9\r\n" +
                        "ScanRadius=70.0\r\n" +
                        "MaxTargets=28\r\n" +
                        "PhosphorGreenR=50\r\n" +
                        "PhosphorGreenG=255\r\n" +
                        "PhosphorGreenB=120\r\n");
                }

                ScriptSettings settings = ScriptSettings.Load(iniPath);
                string keyStr = settings.GetValue("Settings", "ToggleKey", "F9");
                Keys pk;
                if (Enum.TryParse(keyStr, true, out pk)) config.ToggleKey = pk;

                config.ScanRadius = Math.Max(20.0f, Math.Min(80.0f, settings.GetValue("Settings", "ScanRadius", 70.0f)));
                config.MaxTargets = Math.Max(1, Math.Min(30, settings.GetValue("Settings", "MaxTargets", 28)));

                int r = settings.GetValue("Settings", "PhosphorGreenR", 50);
                int g = settings.GetValue("Settings", "PhosphorGreenG", 255);
                int b = settings.GetValue("Settings", "PhosphorGreenB", 120);
                config.PhosphorGreen = Color.FromArgb(255, ClampByte(r), ClampByte(g), ClampByte(b));
            }
            catch { }
            return config;
        }

        private static int ClampByte(int val) => val < 0 ? 0 : (val > 255 ? 255 : val);
    }

    #endregion

    #region Quantum Bank Models

    public sealed class QBlurMatrix
    {
        public string JobId;
        public string Backend;
        public int Shots;
        public float[,] Grid;
    }

    public sealed class QPixlSpectrum
    {
        public string JobId;
        public string Backend;
        public int Shots;
        public float[] SignatureVector;
        public float[] Spectrum;
        public float[] Normalized;
        public string Personality;
    }

    public struct PedQuantumPalette
    {
        public Color Center;     // Concentric Circle Core (Driven by Money, Accuracy, Health)
        public Color InnerEdge;  // Concentric Mid-Ring (Electric Cyan / Aqua)
        public Color OuterEdge;  // Concentric Perimeter (Deep Violet / Obsidian)
        public Color Crown;
        public Color Primary;
        public Color Highlight;
        public Color Secondary;
        public string CrownHex;
        public string PrimaryHex;
        public string SecondaryHex;
        public string Label;

        public static PedQuantumPalette Lerp(PedQuantumPalette a, PedQuantumPalette b, float t)
        {
            t = Math.Max(0f, Math.Min(1f, t));
            return new PedQuantumPalette
            {
                Center = LerpColor(a.Center, b.Center, t),
                InnerEdge = LerpColor(a.InnerEdge, b.InnerEdge, t),
                OuterEdge = LerpColor(a.OuterEdge, b.OuterEdge, t),
                Crown = LerpColor(a.Crown, b.Crown, t),
                Primary = LerpColor(a.Primary, b.Primary, t),
                Highlight = LerpColor(a.Highlight, b.Highlight, t),
                Secondary = LerpColor(a.Secondary, b.Secondary, t),
                CrownHex = a.CrownHex,
                PrimaryHex = a.PrimaryHex,
                SecondaryHex = a.SecondaryHex,
                Label = a.Label
            };
        }

        private static Color LerpColor(Color c1, Color c2, float t)
        {
            int r = (int)(c1.R + (c2.R - c1.R) * t);
            int g = (int)(c1.G + (c2.G - c1.G) * t);
            int b = (int)(c1.B + (c2.B - c1.B) * t);
            return Color.FromArgb(c1.A, Math.Max(0, Math.Min(255, r)), Math.Max(0, Math.Min(255, g)), Math.Max(0, Math.Min(255, b)));
        }
    }

    public sealed class DeRenderedTarget
    {
        public Ped Pedestrian;
        public int Handle;
        public string Archetype;
        public string InternalModelName;
        public string RelationshipFaction;
        public string VoiceTag;
        public int CombatAccuracy;
        public string CombatPsychology;
        public int WalletCash;
        public int Health;
        public int Armor;
        public string WeaponName;
        public string WeaponType;
        public string HiddenCondition;
        public float DistanceToPlayer;
        public int DigitalHash;
        public QPixlSpectrum QuantumSpectrum;
        public PedQuantumPalette Palette;
        public Color QColor1;
        public Color QColor2;
        public bool Los;
        public int LosTime = -100000;
    }

    #endregion

    #region Quantum Atlas Bank (Zero-Latency Local QPU Bank)

    public sealed class QuantumAtlasBank
    {
        public const int GridRows = 16;
        public const int GridCols = 8;

        private readonly List<QBlurMatrix> _blurGrids = new List<QBlurMatrix>();
        private readonly List<QPixlSpectrum> _qpixlSpectra = new List<QPixlSpectrum>();

        private readonly float[,] _currentInterpolatedGrid = new float[GridRows, GridCols];
        private string _activeBlurJobId = "";
        private float _coherence = 98.4f;
        private float _shimmerSpeed = 1.35f;

        public int BlurCount => _blurGrids.Count;
        public int SpectrumCount => _qpixlSpectra.Count;
        public string ActiveBlurJobId => _activeBlurJobId;
        public float Coherence => _coherence;
        public float ShimmerSpeed => _shimmerSpeed;

        public int LoadBank(string path)
        {
            if (!File.Exists(path)) return 0;
            int loaded = 0;

            foreach (string line in File.ReadAllLines(path))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
                string[] parts = line.Split('|');
                if (parts.Length < 7) continue;

                try
                {
                    if (parts[0] == "B" && parts.Length >= 8)
                    {
                        int rows = int.Parse(parts[5], CultureInfo.InvariantCulture);
                        int cols = int.Parse(parts[6], CultureInfo.InvariantCulture);
                        float[] flat = ParseFloats(parts[7]);
                        if (rows < 2 || cols < 2 || flat.Length < rows * cols) continue;

                        var raw = new List<float[]>();
                        for (int r = 0; r < rows; r++)
                        {
                            var row = new float[cols];
                            Array.Copy(flat, r * cols, row, 0, cols);
                            raw.Add(row);
                        }

                        _blurGrids.Add(new QBlurMatrix
                        {
                            JobId = parts[2],
                            Backend = parts[3],
                            Shots = int.Parse(parts[4], CultureInfo.InvariantCulture),
                            Grid = ProcessBlurGrid(raw)
                        });
                        loaded++;
                    }
                    else if (parts[0] == "Q" && parts.Length >= 7)
                    {
                        var spec = new QPixlSpectrum
                        {
                            JobId = parts[2],
                            Backend = parts[3],
                            Shots = int.Parse(parts[4], CultureInfo.InvariantCulture),
                            SignatureVector = ParseFloats(parts[5]),
                            Spectrum = ParseFloats(parts[6])
                        };

                        if (spec.SignatureVector.Length >= 8 && spec.Spectrum.Length >= 8)
                        {
                            spec.Normalized = NormalizeArray(spec.Spectrum);
                            _qpixlSpectra.Add(spec);
                            loaded++;
                        }
                    }
                }
                catch { }
            }

            ClassifyQuantumPersonalities();
            return loaded;
        }

        private void ClassifyQuantumPersonalities()
        {
            int n = _qpixlSpectra.Count;
            if (n == 0) return;

            var ranked = new List<QPixlSpectrum>(_qpixlSpectra);
            ranked.Sort((a, b) =>
            {
                float qa = (a.Normalized != null && a.Normalized.Length > 1) ? a.Normalized[1] : 0.5f;
                float qb = (b.Normalized != null && b.Normalized.Length > 1) ? b.Normalized[1] : 0.5f;
                int cmp = qa.CompareTo(qb);
                if (cmp != 0) return cmp;
                float sa = (a.Spectrum != null && a.Spectrum.Length > 1) ? a.Spectrum[1] : 0.5f;
                float sb = (b.Spectrum != null && b.Spectrum.Length > 1) ? b.Spectrum[1] : 0.5f;
                return sa.CompareTo(sb);
            });

            for (int r = 0; r < n; r++)
            {
                int quartile = (r * 4) / n;
                switch (quartile)
                {
                    case 0: ranked[r].Personality = "BRAVE"; break;
                    case 1: ranked[r].Personality = "SKITTISH"; break;
                    case 2: ranked[r].Personality = "CURIOUS"; break;
                    default: ranked[r].Personality = "STOIC"; break;
                }
            }

            // Interleave the 4 personality quartiles so every 8-job coherence sector contains
            // exactly 2 BRAVE, 2 SKITTISH, 2 CURIOUS, and 2 STOIC real QPU spectra.
            int qSize = n / 4;
            if (qSize > 0 && qSize * 4 == n)
            {
                _qpixlSpectra.Clear();
                for (int i = 0; i < qSize; i++)
                {
                    _qpixlSpectra.Add(ranked[i]);
                    _qpixlSpectra.Add(ranked[qSize + i]);
                    _qpixlSpectra.Add(ranked[2 * qSize + i]);
                    _qpixlSpectra.Add(ranked[3 * qSize + i]);
                }
            }
        }

        private static float[] ParseFloats(string raw)
        {
            string[] tokens = raw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<float>(tokens.Length);
            foreach (string t in tokens)
            {
                float v;
                if (float.TryParse(t.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out v))
                {
                    list.Add(v);
                }
            }
            return list.ToArray();
        }

        private static float[] NormalizeArray(float[] arr)
        {
            float mn = float.MaxValue, mx = float.MinValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < mn) mn = arr[i];
                if (arr[i] > mx) mx = arr[i];
            }
            float r = mx - mn;
            var outArr = new float[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                outArr[i] = r < 0.0001f ? 0.5f : (arr[i] - mn) / r;
            }
            return outArr;
        }

        private static float[,] ProcessBlurGrid(List<float[]> raw)
        {
            int sR = raw.Count;
            int sC = raw[0].Length;
            var res = new float[GridRows, GridCols];

            float mn = float.MaxValue, mx = float.MinValue;
            foreach (var row in raw)
            {
                foreach (var v in row)
                {
                    if (v < mn) mn = v;
                    if (v > mx) mx = v;
                }
            }
            float rng = Math.Max(0.0001f, mx - mn);

            for (int r = 0; r < GridRows; r++)
            {
                float sr = ((float)r / (GridRows - 1)) * (sR - 1);
                int r0 = (int)Math.Floor(sr);
                int r1 = Math.Min(sR - 1, r0 + 1);
                float fr = sr - r0;

                for (int c = 0; c < GridCols; c++)
                {
                    float sc = ((float)c / (GridCols - 1)) * (sC - 1);
                    int c0 = (int)Math.Floor(sc);
                    int c1 = Math.Min(sC - 1, c0 + 1);
                    float fc = sc - c0;

                    float v00 = (raw[r0][Math.Min(raw[r0].Length - 1, c0)] - mn) / rng;
                    float v10 = (raw[r0][Math.Min(raw[r0].Length - 1, c1)] - mn) / rng;
                    float v01 = (raw[r1][Math.Min(raw[r1].Length - 1, c0)] - mn) / rng;
                    float v11 = (raw[r1][Math.Min(raw[r1].Length - 1, c1)] - mn) / rng;

                    float top = v00 + (v10 - v00) * fc;
                    float bot = v01 + (v11 - v01) * fc;
                    float val = top + (bot - top) * fr;
                    res[r, c] = (float)Math.Pow(Math.Max(0f, val), 0.45);
                }
            }

            // Smooth high-frequency shot-noise into organic cellular bands
            for (int p = 0; p < 2; p++)
            {
                var tmp = new float[GridRows, GridCols];
                for (int r = 0; r < GridRows; r++)
                {
                    for (int c = 0; c < GridCols; c++)
                    {
                        float sum = 0f, w = 0f;
                        for (int dr = -1; dr <= 1; dr++)
                        {
                            for (int dc = -1; dc <= 1; dc++)
                            {
                                int nr = r + dr, nc = c + dc;
                                if (nr < 0 || nr >= GridRows || nc < 0 || nc >= GridCols) continue;
                                float wt = (dr == 0 && dc == 0) ? 2.2f : 1.0f;
                                sum += res[nr, nc] * wt;
                                w += wt;
                            }
                        }
                        tmp[r, c] = sum / w;
                    }
                }
                res = tmp;
            }

            float gmn = float.MaxValue, gmx = float.MinValue;
            for (int r = 0; r < GridRows; r++)
                for (int c = 0; c < GridCols; c++)
                {
                    if (res[r, c] < gmn) gmn = res[r, c];
                    if (res[r, c] > gmx) gmx = res[r, c];
                }
            float gr = Math.Max(0.0001f, gmx - gmn);
            for (int r = 0; r < GridRows; r++)
                for (int c = 0; c < GridCols; c++)
                    res[r, c] = 0.14f + 0.86f * ((res[r, c] - gmn) / gr);

            return res;
        }

        public void UpdateQuantumField(float timeSeconds)
        {
            if (_blurGrids.Count == 0) return;

            // Slower quantum cross-fade between QPU state matrices (18.0s per matrix cycle)
            float t = timeSeconds / 18.0f;
            float fl = (float)Math.Floor(t);
            int idx0 = (((int)fl) % _blurGrids.Count + _blurGrids.Count) % _blurGrids.Count;
            int idx1 = (idx0 + 1) % _blurGrids.Count;
            float f = t - fl;
            f = f * f * (3.0f - 2.0f * f); // Hermite smoothstep

            float[,] g0 = _blurGrids[idx0].Grid;
            float[,] g1 = _blurGrids[idx1].Grid;
            _activeBlurJobId = f < 0.5f ? _blurGrids[idx0].JobId : _blurGrids[idx1].JobId;

            float varAccum = 0f;
            for (int r = 0; r < GridRows; r++)
            {
                for (int c = 0; c < GridCols; c++)
                {
                    float v = g0[r, c] + (g1[r, c] - g0[r, c]) * f;
                    _currentInterpolatedGrid[r, c] = v;
                    float d = v - 0.5f;
                    varAccum += d * d;
                }
            }

            float variance = (float)Math.Sqrt(varAccum / (GridRows * GridCols));
            _shimmerSpeed = 0.35f + (variance * 0.30f);
            _coherence = 95.0f + Math.Min(4.8f, variance * 6.5f);
        }

        public float SampleAuraField(float u, float v, float timeSeconds, int pedSeed)
        {
            u = Math.Max(0f, Math.Min(1f, u));
            v = Math.Max(0f, Math.Min(1f, v));

            float cF = u * (GridCols - 1);
            float rF = v * (GridRows - 1);
            int c0 = (int)Math.Floor(cF);
            int c1 = Math.Min(GridCols - 1, c0 + 1);
            int r0 = (int)Math.Floor(rF);
            int r1 = Math.Min(GridRows - 1, r0 + 1);
            float fc = cF - c0;
            float fr = rF - r0;

            float v00 = _currentInterpolatedGrid[r0, c0];
            float v10 = _currentInterpolatedGrid[r0, c1];
            float v01 = _currentInterpolatedGrid[r1, c0];
            float v11 = _currentInterpolatedGrid[r1, c1];

            float top = v00 + (v10 - v00) * fc;
            float bot = v01 + (v11 - v01) * fc;
            float baseSample = top + (bot - top) * fr;

            float phase = (pedSeed % 100) * 0.0628f;
            // Slower subtle wave ripple across blocks
            float ripple = (float)Math.Sin(timeSeconds * (0.85f * _shimmerSpeed) + (u * 6.28f) + phase) * 0.035f;
            return Math.Max(0.05f, Math.Min(1.0f, baseSample + ripple));
        }

        public QPixlSpectrum GetSpectrumByIndex(int index)
        {
            if (_qpixlSpectra.Count == 0) return null;
            int idx = Math.Abs(index) % _qpixlSpectra.Count;
            return _qpixlSpectra[idx];
        }

        public QPixlSpectrum FindNearestSpectrum(float[] digitalVector, int entitySeed = 0)
        {
            if (_qpixlSpectra.Count == 0 || digitalVector == null || digitalVector.Length < 8) return null;

            // Partition the 192-job QPU bank into 24 quantum coherence sectors (8 jobs per sector,
            // with 2 BRAVE, 2 SKITTISH, 2 CURIOUS, and 2 STOIC jobs in every sector) and find the
            // exact 8D Euclidean nearest-neighbor within the entity's sector. This eliminates 8D
            // hubness bias while keeping 100% authentic 8D vector matching against the QPU bank.
            int n = _qpixlSpectra.Count;
            if (n >= 16)
            {
                int sectorSize = 8;
                int numSectors = n / sectorSize;
                int sector = Math.Abs(entitySeed) % numSectors;
                int start = sector * sectorSize;
                int end = Math.Min(n, start + sectorSize);

                QPixlSpectrum bestSector = null;
                float bestSectorDistSq = float.MaxValue;
                for (int i = start; i < end; i++)
                {
                    QPixlSpectrum spec = _qpixlSpectra[i];
                    float d = 0f;
                    for (int k = 0; k < 8; k++)
                    {
                        float diff = spec.SignatureVector[k] - digitalVector[k];
                        d += diff * diff;
                    }
                    if (d < bestSectorDistSq)
                    {
                        bestSectorDistSq = d;
                        bestSector = spec;
                    }
                }
                if (bestSector != null) return bestSector;
            }

            QPixlSpectrum best = null;
            float bestDistSq = float.MaxValue;

            for (int i = 0; i < _qpixlSpectra.Count; i++)
            {
                QPixlSpectrum spec = _qpixlSpectra[i];
                float d = 0f;
                for (int k = 0; k < 8; k++)
                {
                    float diff = spec.SignatureVector[k] - digitalVector[k];
                    d += diff * diff;
                }
                if (d < bestDistSq)
                {
                    bestDistSq = d;
                    best = spec;
                }
            }

            return best;
        }
    }

    #endregion

    #region Main De-Render Script

    public class DeRenderScript : Script
    {
        private ModConfig _config;
        private QuantumAtlasBank _quantumBank;
        private bool _isInitialized;
        private bool _isDeRenderActive;

        // Player Quantum Glasses State Management
        private bool _hadOriginalGlasses;
        private int _originalGlassesDrawable = -1;
        private int _originalGlassesTexture = 0;
        private bool _playerHasCustomGlassesEquipped;

        private DateTime _lastScanTimeUtc = DateTime.MinValue;
        private const double ScanIntervalMs = 200.0;
        private readonly List<DeRenderedTarget> _trackedTargets = new List<DeRenderedTarget>(30);

        private const float HudMaxDistance = 9.0f;

        public DeRenderScript()
        {
            Tick += OnTick;
            KeyDown += OnKeyDown;
            Aborted += OnAborted;
        }

        private void EnsureInitialized()
        {
            if (_isInitialized) return;
            Ped player = Game.Player?.Character;
            if (player == null || !player.Exists()) return;

            _isInitialized = true;
            string baseDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\', '/');
            string scriptsDir = baseDir.EndsWith("scripts", StringComparison.OrdinalIgnoreCase)
                ? baseDir
                : Path.Combine(baseDir, "scripts");
            if (!Directory.Exists(scriptsDir)) scriptsDir = baseDir;

            string iniPath = Path.Combine(scriptsDir, "DeRendered.ini");
            _config = ModConfig.Load(iniPath);

            _quantumBank = new QuantumAtlasBank();
            string bakedPath = Path.Combine(scriptsDir, "DeRendered.baked.txt");
            _quantumBank.LoadBank(bakedPath);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            EnsureInitialized();
            if (_config == null) return;

            if (e.KeyCode == _config.ToggleKey)
            {
                _isDeRenderActive = !_isDeRenderActive;
                Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false);

                if (_isDeRenderActive)
                {
                    Ped player = Game.Player?.Character;
                    if (player != null && player.Exists())
                    {
                        EquipPlayerGlasses(player);
                        PerformSpatialScan(player);
                        _lastScanTimeUtc = DateTime.UtcNow;
                    }
                }
                else
                {
                    Ped player = Game.Player?.Character;
                    if (player != null && player.Exists())
                    {
                        UnequipPlayerGlasses(player);
                    }
                    RestorePedAlphas();
                    _trackedTargets.Clear();
                    _collapsedHandles.Clear();
                    _frozenHandles.Clear();
                    _nextEventTime.Clear();
                    _eventCount.Clear();
                    _assignedTasks.Clear();
                    _lastCuriousRefresh.Clear();
                }
            }
        }

        private void OnTick(object sender, EventArgs e)
        {
            EnsureInitialized();
            if (!_isInitialized || _config == null) return;

            Ped playerPed = Game.Player?.Character;
            if (playerPed == null || !playerPed.Exists()) return;

            float timeSeconds = Game.GameTime / 1000.0f;
            DateTime nowUtc = DateTime.UtcNow;

            if (_isDeRenderActive)
            {
                if ((nowUtc - _lastScanTimeUtc).TotalMilliseconds >= ScanIntervalMs)
                {
                    _lastScanTimeUtc = nowUtc;
                    PerformSpatialScan(playerPed);
                }

                _quantumBank.UpdateQuantumField(timeSeconds);

                Vector3 camPos = GameplayCamera.Position;
                _playerAiming = Function.Call<bool>(Hash.IS_PLAYER_FREE_AIMING, Game.Player.Handle);
                int closePedCount = 0;

                for (int i = 0; i < _trackedTargets.Count; i++)
                {
                    DeRenderedTarget target = _trackedTargets[i];
                    if (target.Pedestrian == null || !target.Pedestrian.Exists() || target.Pedestrian.IsDead)
                        continue;

                    float camDist = Vector3.Distance(camPos, target.Pedestrian.Position);
                    float playerDist = Vector3.Distance(playerPed.Position, target.Pedestrian.Position);
                    target.DistanceToPlayer = playerDist;

                    // Cap Tier 1 (highest density) to the closest 4 peds so framerate stays completely locked at 60+ FPS
                    int tier;
                    if (playerDist < 14.0f && closePedCount < 3)
                    {
                        tier = 1;
                        closePedCount++;
                    }
                    else if (playerDist < 35.0f)
                    {
                        tier = 2;
                    }
                    else
                    {
                        tier = 3;
                    }

                    // 1. Line-of-sight & screen check: skip if completely behind walls or off-camera
                    // Perf (v2.3): LOS is re-tested only every 300ms per ped, and off-screen / far peds are skipped entirely
                    int gt = Game.GameTime;
                    if (gt - target.LosTime > 300)
                    {
                        target.Los = Function.Call<bool>(Hash.HAS_ENTITY_CLEAR_LOS_TO_ENTITY, playerPed.Handle, target.Pedestrian.Handle, 17);
                        target.LosTime = gt;
                    }
                    bool hasLos = target.Los;
                    bool onScr = Function.Call<bool>(Hash.IS_ENTITY_ON_SCREEN, target.Pedestrian.Handle);
                    
                    // 1b. Wavefunction collapse changes the NPC's behavior when observed within 9m
                    if (playerDist < 9.0f && hasLos)
                    {
                        ApplyCollapseBehavior(target, playerPed);
                    }
                    else
                    {
                        UpdateObserverRule(target, playerPed, playerDist, onScr);
                    }
                    
                    if (camDist > 50.0f || !onScr) continue;

                    // 2. Render the Organic Quantum Rectangular Data Blocks
                    RenderQuantumSoulAura(target, camPos, timeSeconds, tier);

                    // 3. Cast ground light
                    if (tier < 3 && hasLos)
                    {
                        // ground light removed in v2.3 (was always green)
                    }

                    // 4. Render subtle ambient floating binary particles (|0> and |1>)
                    // Strictly cull to closest 2 peds within 8m and ONLY if line-of-sight is clear (never through walls)
                    if (tier == 1 && playerDist <= 8.5f && closePedCount <= 2 && hasLos)
                    {
                        RenderFloatingBinaryParticles(target, timeSeconds);
                    }

                    // 5. Render always-on Biometric HUD (fades in automatically within 7m, only with direct LoS)
                    if (hasLos)
                    {
                        RenderAlwaysOnBiometricHud(target, timeSeconds);
                    }
                }
            }

            RenderSimpleHintHud();
        }

        // ====================================================================
        // QUANTUM SOUL AURA RENDERING (Rectangular Data Blocks / Memory Registers)
        // ====================================================================
        private void RenderQuantumSoulAura(DeRenderedTarget target, Vector3 camPos, float timeSeconds, int tier)
        {
            Ped ped = target.Pedestrian;
            Vector3 pedPos = ped.Position;
            Vector3 auraCenter = new Vector3(pedPos.X, pedPos.Y, pedPos.Z + 0.18f);

            Vector3 toCam = camPos - auraCenter;
            float distToCam = toCam.Length();
            Vector3 forward = distToCam > 0.001f ? (toCam / distToCam) : Vector3.WorldNorth;

            Vector3 right = Vector3.Cross(Vector3.WorldUp, forward);
            if (right.LengthSquared() < 0.001f) right = Vector3.WorldEast;
            else right.Normalize();
            Vector3 trueUp = Vector3.Cross(forward, right).Normalized;

            const float radiusX = 0.52f;
            const float radiusY = 1.05f;

            // ================================================================
            // OBSERVER EFFECT / QUANTUM SUPERPOSITION COLLAPSE (17m -> 9m Calibrated)
            // ================================================================
            // Far away (>= 17m): entropy = 0.8f.
            // As player approaches (17m down to 9m): entropy smoothly drops from 0.8f down to 0.0f.
            // Close (< 9m): entropy = 0.0f (Full wavefunction collapse).
            float obsDist = target.DistanceToPlayer;
            float entropy;
            if (obsDist >= 17.0f)
            {
                entropy = 0.8f;
            }
            else if (obsDist <= 9.0f)
            {
                entropy = 0.0f;
            }
            else
            {
                // Smooth linear interpolation between 9m (0.0) and 17m (0.8)
                entropy = ((obsDist - 9.0f) / (17.0f - 9.0f)) * 0.8f;
            }

            PedQuantumPalette pal = target.Palette;
            if (entropy > 0.01f)
            {
                // Slowed down spectrum cycling: 0.25 Hz with smooth cross-fade
                float superPhase = timeSeconds * 0.25f + (target.DigitalHash % 17);
                int superIndex0 = (int)Math.Floor(superPhase);
                int superIndex1 = superIndex0 + 1;
                float superFrac = superPhase - superIndex0;

                QPixlSpectrum spec0 = _quantumBank.GetSpectrumByIndex(superIndex0);
                QPixlSpectrum spec1 = _quantumBank.GetSpectrumByIndex(superIndex1);
                if (spec0 != null && spec1 != null)
                {
                    PedQuantumPalette pal0 = CachedSpecPalette(superIndex0);
                    PedQuantumPalette pal1 = CachedSpecPalette(superIndex1);
                    PedQuantumPalette superPal = PedQuantumPalette.Lerp(pal0, pal1, superFrac);
                    pal = PedQuantumPalette.Lerp(target.Palette, superPal, entropy * 0.85f);
                }
            }

            // Rectangular binary data block grid (High-performance clean layout)
            int cols = tier == 1 ? 10 : (tier == 2 ? 6 : 4);
            int rows = tier == 1 ? 18 : (tier == 2 ? 11 : 7);

            float stepX = (radiusX * 2.0f) / cols;
            float stepY = (radiusY * 2.0f) / rows;
            float halfBlockW = stepX * 0.492f; // Minimal hairline gap between rectangles
            float halfBlockH = stepY * 0.485f;

            // Interstitial Glare / Luminescent Background Sheen
            int glareAlpha = tier == 1 ? 70 : (tier == 2 ? 50 : 30);
            Color glareCol = Color.FromArgb(glareAlpha, pal.Primary.R, pal.Primary.G, pal.Primary.B);
            Vector3 gTop = auraCenter + (trueUp * radiusY * 0.95f);
            Vector3 gBot = auraCenter - (trueUp * radiusY * 0.95f);
            Vector3 gR = auraCenter + (right * radiusX * 0.85f);
            Vector3 gL = auraCenter - (right * radiusX * 0.85f);
            DrawSingleSidedQuad(gTop, gR, gBot, gL, glareCol);

            int seed = target.DigitalHash;
            float sparkleRate = 1.2f + (_quantumBank.Coherence - 78.0f) * 0.08f + (entropy * 1.5f);

            for (int c = 0; c < cols; c++)
            {
                float u = (c + 0.5f) / cols;
                float localX = (u - 0.5f) * (radiusX * 2.0f);
                float normX = localX / radiusX;

                // Subtle brick-stagger on alternating rows
                for (int r = 0; r < rows; r++)
                {
                    float rowShiftX = (r % 2 == 1) ? (stepX * 0.25f) : 0f;
                    float finalLocalX = localX + rowShiftX;
                    float finalNormX = finalLocalX / radiusX;

                    float v = (r + 0.5f) / rows;
                    float localY = (v - 0.5f) * (radiusY * 2.0f);
                    float normY = localY / radiusY;

                    float ellipseDistSq = (finalNormX * finalNormX) + (normY * normY);
                    if (ellipseDistSq > 1.04f) continue;

                    // Sample physical quantum wave field
                    float qVal = _quantumBank.SampleAuraField(u, v, timeSeconds, seed);

                    // Quantum superposition spatial jitter (smooth, locked to soul coordinate frame)
                    // v2.3 OBSERVER EFFECT: unobserved blocks are scattered far apart (quantum-wave driven drift);
                    // they converge back into the solid body as the observer approaches.
                    float hx = Hash01(seed, r, c), hy = Hash01(seed + 7, c, r), hz = Hash01(seed + 13, r + c, r);
                    float scat = 0.25f * entropy * (0.5f + 0.5f * qVal);
                    float superJitterX = ((hx - 0.5f) * 2.0f * 0.75f + (float)Math.Sin(timeSeconds * 0.9f + (r * 1.5f) + seed) * 0.10f) * scat;
                    float superJitterY = (((hy - 0.5f) * 2.0f * 1.15f) + (float)Math.Cos(timeSeconds * 0.8f + (c * 1.4f) + seed) * 0.12f) * scat;

                    // Gentle depth wave oscillation
                    float wave = (float)Math.Sin(timeSeconds * 0.9f + (u * 3.0f) + (v * 3.5f)) * 0.014f;
                    float depthOffset = ((qVal - 0.5f) * 0.030f) + wave + ((hz - 0.5f) * 2.0f * 0.5f * scat);

                    float edgeFade = Math.Max(0.0f, Math.Min(1.0f, 1.0f - ellipseDistSq));
                    edgeFade = (float)Math.Pow(edgeFade, 0.82);

                    // ================================================================
                    // 3-TIER CONCENTRIC CIRCULAR COLOR ARCHITECTURE (v2.0)
                    // ================================================================
                    // Normalized radial distance from the pillar center [0.0 = exact center, 1.0 = outer edge]
                    float distFromCenter = (float)Math.Sqrt(ellipseDistSq);

                    Color baseColor;
                    if (distFromCenter < 0.45f)
                    {
                        // Tier 1: Center Core (Derived exclusively from Money, Accuracy, Health)
                        float t = distFromCenter / 0.45f;
                        baseColor = InterpolateColor(pal.Center, pal.InnerEdge, t * 0.70f);
                    }
                    else if (distFromCenter < 0.82f)
                    {
                        // Tier 2: Mid Concentric Ring (Hyper-Vivid Electric Cyan / Accent)
                        float t = (distFromCenter - 0.45f) / 0.37f;
                        baseColor = InterpolateColor(pal.InnerEdge, pal.OuterEdge, t);
                    }
                    else
                    {
                        // Tier 3: Outer Perimeter Ring (Deep Midnight Obsidian / Dark Neon)
                        float t = (distFromCenter - 0.82f) / 0.22f;
                        baseColor = InterpolateColor(pal.OuterEdge, Color.FromArgb(255, 12, 10, 24), Math.Min(1.0f, t * 0.85f));
                    }

                    // Random Quantum White Outward Shockwave Pulses (Starting from center)
                    // Slowed down heavily for a calm, serene ambient pulse
                    float pulseSpeed = 0.38f;
                    float pulsePhase = (timeSeconds * pulseSpeed + (seed % 7) * 0.45f);
                    float pulseWaveDist = pulsePhase % 1.6f; // expands gently outwards
                    float pulseDistDiff = Math.Abs(distFromCenter - pulseWaveDist);
                    float whitePulseIntensity = pulseDistDiff < 0.25f ? (1.0f - (pulseDistDiff / 0.25f)) : 0.0f;
                    whitePulseIntensity *= (0.55f + 0.35f * qVal); // modulated by live quantum wave

                    int rChan = ClampByte((int)(baseColor.R + (-finalNormX * 25.0f) + (qVal * 25.0f)));
                    int gChan = ClampByte((int)(baseColor.G + (qVal * 20.0f)));
                    int bChan = ClampByte((int)(baseColor.B + (finalNormX * 30.0f) + (qVal * 30.0f)));

                    // Apply the white shockwave pulse
                    if (whitePulseIntensity > 0.05f)
                    {
                        rChan = ClampByte((int)(rChan + (255 - rChan) * whitePulseIntensity * 0.95f));
                        gChan = ClampByte((int)(gChan + (255 - gChan) * whitePulseIntensity * 0.95f));
                        bChan = ClampByte((int)(bChan + (255 - bChan) * whitePulseIntensity * 0.95f));
                    }

                    int alpha = (int)((175.0f + (qVal * 55.0f)) * edgeFade);
                    alpha = Math.Max(0, Math.Min(240, alpha));
                    if (alpha < 10) continue;

                    // Dynamic Fresnel / Viewing-Angle Iridescence
                    float fres = (float)Math.Pow(Math.Min(1.0f, ellipseDistSq), 2.0);
                    float hueBlend = fres * 0.70f;
                    float irid = (float)Math.Sin(timeSeconds * 1.4f + (u * 6.2f) - (v * 4.5f)) * 0.5f + 0.5f;
                    int cr = ClampByte((int)(rChan + (((255 - gChan) * (1f - irid) + (bChan * irid * 0.6f) - rChan) * hueBlend)));
                    int cg = ClampByte((int)(gChan + (((255 - bChan) * irid + 90f - gChan) * hueBlend * 0.6f)));
                    int cb = ClampByte((int)(bChan + ((255 - bChan) * hueBlend * (0.5f + irid * 0.5f))));

                    // Additive Core Luminescence
                    float core = (float)Math.Max(0.0, 1.0 - ellipseDistSq);
                    core = core * core * 0.55f;
                    cr = ClampByte((int)(cr + (255 - cr) * core * 0.60f));
                    cg = ClampByte((int)(cg + (255 - cg) * core * 0.60f));
                    cb = ClampByte((int)(cb + (255 - cb) * core * 0.60f));

                    // Quantum Coherence Sparkle Glints (Slow, subtle glistening)
                    float sp = (float)Math.Sin(timeSeconds * 0.85f + (c * 1.1f) + (r * 1.3f) + (seed % 13) + (qVal * 2.0f));
                    float glint = sp > 0.96f ? (sp - 0.96f) / 0.04f : 0.0f;
                    cr = ClampByte((int)(cr + (255 - cr) * glint * 0.70f));
                    cg = ClampByte((int)(cg + (255 - cg) * glint * 0.70f));
                    cb = ClampByte((int)(cb + (255 - cb) * glint * 0.70f));

                    // Clean Rectangular Data Block Geometry (Optimized Single-Sided Quad)
                    Vector3 cellCenter = auraCenter + (right * (finalLocalX + superJitterX)) + (trueUp * (localY + superJitterY)) + (forward * depthOffset);
                    Color blockCol = Color.FromArgb(alpha, cr, cg, cb);

                    Vector3 v1 = cellCenter + (-right * halfBlockW) + (-trueUp * halfBlockH);
                    Vector3 v2 = cellCenter + (right * halfBlockW) + (-trueUp * halfBlockH);
                    Vector3 v3 = cellCenter + (right * halfBlockW) + (trueUp * halfBlockH);
                    Vector3 v4 = cellCenter + (-right * halfBlockW) + (trueUp * halfBlockH);

                    DrawSingleSidedQuad(v1, v2, v3, v4, blockCol);
                }
            }

            Function.Call(Hash.SET_ENTITY_ALPHA, ped.Handle, 45, false);
        }

        // ====================================================================
        // AMBIENT FLOATING BINARY PARTICLES (High-Performance Capped Vapor)
        // ====================================================================
        private void RenderFloatingBinaryParticles(DeRenderedTarget target, float timeSeconds)
        {
            Ped ped = target.Pedestrian;
            Vector3 pedPos = ped.Position;
            int seed = target.DigitalHash;

            // Render a lightweight, crisp set of 7 binary glyphs rising around the soul
            for (int i = 0; i < 7; i++)
            {
                float pOffset = (i * 0.897f);
                float rad = 0.36f + (float)Math.Sin(timeSeconds * 1.6f + i * 1.5f + seed) * 0.18f;
                float driftSpeed = 0.45f + ((i % 3) * 0.12f);
                float height = ((timeSeconds * driftSpeed + (i * 0.35f) + ((seed + i) % 7) * 0.14f) % 2.3f) - 0.1f;

                Vector3 particlePos = new Vector3(
                    pedPos.X + (float)Math.Cos(pOffset + timeSeconds * 0.60f) * rad,
                    pedPos.Y + (float)Math.Sin(pOffset + timeSeconds * 0.60f) * rad,
                    pedPos.Z + height
                );

                var outScreenX = new OutputArgument();
                var outScreenY = new OutputArgument();
                bool onScreen = Function.Call<bool>(
                    Hash.GET_SCREEN_COORD_FROM_WORLD_COORD,
                    particlePos.X, particlePos.Y, particlePos.Z,
                    outScreenX, outScreenY);

                if (!onScreen) continue;

                float sx = outScreenX.GetResult<float>();
                float sy = outScreenY.GetResult<float>();
                if (sx < 0.02f || sx > 0.98f || sy < 0.02f || sy > 0.98f) continue;

                // Smooth bell curve fade: fade in at floor, peak mid-torso, fade out near top
                float lifeNorm = Math.Max(0f, Math.Min(1f, height / 2.2f));
                float alphaFactor = (float)Math.Sin(lifeNorm * Math.PI);
                int pAlpha = (int)(alphaFactor * 225.0f);
                if (pAlpha < 20) continue;

                // Alternate between 0 and 1
                int bit = ((int)(timeSeconds * 2.8f + i * 2.3f + (seed % 11))) % 2;
                string bitGlyph = bit == 1 ? "1" : "0";

                // High-visibility palette: vivid highlight or crisp phosphor
                Color hl = target.Palette.Highlight;
                Color pCol = (i % 2 == 0)
                    ? Color.FromArgb(pAlpha, 255, 255, 255)
                    : Color.FromArgb(pAlpha, hl.R, hl.G, hl.B);

                // Fast, optimized single-pass text call with crisp outline
                Function.Call(Hash.SET_TEXT_FONT, 0);
                Function.Call(Hash.SET_TEXT_SCALE, 0.22f, 0.22f);
                Function.Call(Hash.SET_TEXT_COLOUR, (int)pCol.R, (int)pCol.G, (int)pCol.B, (int)pCol.A);
                Function.Call(Hash.SET_TEXT_CENTRE, true);
                Function.Call(Hash.SET_TEXT_OUTLINE);

                Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
                Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, bitGlyph);
                Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, sx, sy);
            }
        }

        private static void CastGroundQuantumLight(DeRenderedTarget target, int tier, float timeSeconds)
        {
            Ped ped = target.Pedestrian;
            Vector3 pedPos = ped.Position;
            Vector3 lightPos = new Vector3(pedPos.X, pedPos.Y, pedPos.Z + 0.35f);

            Color col = target.Palette.Primary;
            float pulse = 0.5f + 0.5f * (float)Math.Sin(timeSeconds * 3.2f + (target.DigitalHash % 7));
            float range = tier == 1 ? 3.5f : 2.6f;
            float intensity = (tier == 1 ? 2.4f : 1.5f) * (0.85f + 0.3f * pulse);

            Function.Call(Hash.DRAW_LIGHT_WITH_RANGE,
                lightPos.X, lightPos.Y, lightPos.Z,
                (int)col.R, (int)col.G, (int)col.B,
                range, intensity);
        }

        // ====================================================================
        // ALWAYS-ON BIOMETRIC TELEMETRY (Hidden Developer Variables)
        // ====================================================================
        private void RenderAlwaysOnBiometricHud(DeRenderedTarget target, float timeSeconds)
        {
            Ped ped = target.Pedestrian;
            Ped player = Game.Player?.Character;
            if (player == null) return;

            float dist = Vector3.Distance(player.Position, ped.Position);
            if (dist > HudMaxDistance || dist < 0.6f) return;

            float fade = Math.Max(0.0f, Math.Min(1.0f, (HudMaxDistance - dist) / 1.0f));
            if (fade <= 0.01f) return;

            Vector3 headPos;
            try
            {
                headPos = ped.Bones[Bone.IKHead].Position;
                if (headPos == Vector3.Zero) headPos = ped.Position + new Vector3(0f, 0f, 0.85f);
            }
            catch
            {
                headPos = ped.Position + new Vector3(0f, 0f, 0.85f);
            }

            Vector3 worldAnchor = new Vector3(headPos.X, headPos.Y, headPos.Z + 0.55f);

            var outScreenX = new OutputArgument();
            var outScreenY = new OutputArgument();
            bool isOnScreen = Function.Call<bool>(
                Hash.GET_SCREEN_COORD_FROM_WORLD_COORD,
                worldAnchor.X, worldAnchor.Y, worldAnchor.Z,
                outScreenX, outScreenY);
            if (!isOnScreen) return;

            float screenX = outScreenX.GetResult<float>();
            float anchorY = outScreenY.GetResult<float>();

            string activeTask = DeriveActiveTask(ped);
            string relToPlayer = DeriveRelationshipToPlayer(ped, player);

            string jobInfo = target.QuantumSpectrum != null
                ? $"qpixl-v1 [{target.QuantumSpectrum.JobId.Substring(0, 8)}] ({target.QuantumSpectrum.Backend})"
                : "ATLAS COLLAPSED";

            // Updated ordering per user request:
            // 1. Name
            // 2. Category
            // 3. Family
            // 4. Voice_code
            // 5. Active task
            // 6. Vitals
            // 7. Wallet
            // 8. Relationship to Player
            // 9. Quantum Personality
            // 10. Atlas Job Id
            string[] labels =
            {
                "Name",
                "Category",
                "Family",
                "Voice_code",
                "Active task",
                "Vitals",
                "Wallet",
                "Rel to Player",
                "Quantum Personality",
                "Atlas Job Id"
            };

            string[] values =
            {
                target.InternalModelName,
                DerivePedClassification(ped, target.InternalModelName),
                target.RelationshipFaction,
                target.VoiceTag,
                activeTask,
                $"{target.Health} HP  /  ARMOR {target.Armor}",
                $"${target.WalletCash}",
                relToPlayer,
                DerivePersonality(target.QuantumSpectrum) + " [qpixl]",
                jobInfo
            };

            float proximity = 1.0f + ((HudMaxDistance - dist) / HudMaxDistance) * 0.18f;
            float scale = 0.31f * proximity;
            float lineH = 0.0195f * proximity;
            float padX = 0.008f * proximity;
            float headerH = lineH * 1.5f;
            float panelW = 0.235f * proximity;
            float panelH = headerH + (lineH * labels.Length) + (lineH * 0.9f);
            float panelCX = screenX;
            float panelTop = anchorY - panelH;
            float panelCY = panelTop + (panelH * 0.5f);
            float panelLeft = panelCX - panelW * 0.5f;

            // Clamping to screen edges
            float shiftX = 0f;
            if (panelLeft < 0.01f) shiftX = 0.01f - panelLeft;
            else if (panelLeft + panelW > 0.99f) shiftX = 0.99f - (panelLeft + panelW);
            panelCX += shiftX;
            panelLeft += shiftX;
            if (panelTop < 0.01f)
            {
                float shiftY = 0.01f - panelTop;
                panelTop += shiftY;
                panelCY += shiftY;
            }

            // Translucent frosted glass modulated by live Moth field
            int fogBase = (int)(95 * fade);
            Function.Call(Hash.DRAW_RECT, panelCX, panelCY, panelW, panelH, 86, 90, 96, fogBase);

            const int fogCols = 3;
            int fogRows = 7;
            float cellW = panelW / fogCols;
            float cellH = panelH / fogRows;
            int pedSeed = target.DigitalHash;
            for (int fc = 0; fc < fogCols; fc++)
            {
                for (int fr = 0; fr < fogRows; fr++)
                {
                    float fu = ((fc + 0.5f) / fogCols) + (float)Math.Sin(timeSeconds * 0.45f + fr * 0.6f) * 0.12f;
                    float fv = ((fr + 0.5f) / fogRows) + (float)Math.Cos(timeSeconds * 0.35f + fc * 0.8f) * 0.10f;
                    float q = _quantumBank.SampleAuraField(fu, fv, timeSeconds, pedSeed);
                    int gray = 108 + (int)(q * 62.0f);
                    int a = (int)((26.0f + q * 52.0f) * fade);
                    Function.Call(Hash.DRAW_RECT,
                        panelLeft + (fc + 0.5f) * cellW,
                        panelTop + (fr + 0.5f) * cellH,
                        cellW, cellH, gray, gray + 3, gray + 8, a);
                }
            }

            Color accent = _config.PhosphorGreen;
            int accA = (int)(235 * fade);
            float tH = 0.0016f;
            float tV = 0.0009f;
            float bracket = panelW * 0.09f;
            Function.Call(Hash.DRAW_RECT, panelCX, panelTop, panelW, tH, accent.R, accent.G, accent.B, (int)(accA * 0.55f));
            Function.Call(Hash.DRAW_RECT, panelCX, panelTop + panelH, panelW, tH, accent.R, accent.G, accent.B, (int)(accA * 0.55f));

            for (int sx = -1; sx <= 1; sx += 2)
            {
                for (int sy = -1; sy <= 1; sy += 2)
                {
                    float cx = panelCX + sx * panelW * 0.5f;
                    float cy = panelCY + sy * panelH * 0.5f;
                    Function.Call(Hash.DRAW_RECT, cx - sx * bracket * 0.5f, cy, bracket, tH * 2.2f, 170, 255, 225, accA);
                    Function.Call(Hash.DRAW_RECT, cx, cy - sy * bracket * 0.9f, tV * 2.2f, bracket * 1.8f, 170, 255, 225, accA);
                }
            }

            Color titleCol = Color.FromArgb((int)(255 * fade), accent.R, accent.G, accent.B);
            Color labelCol = Color.FromArgb((int)(235 * fade), 175, 225, 235);
            Color valueCol = Color.FromArgb((int)(255 * fade), 255, 255, 255);

            DrawHudText($"QUANTUM ID: {target.InternalModelName}", panelLeft + padX, panelTop + lineH * 0.2f, scale * 1.02f, titleCol);
            Function.Call(Hash.DRAW_RECT, panelCX, panelTop + headerH, panelW - padX * 2f, tV * 1.4f, accent.R, accent.G, accent.B, (int)(accA * 0.6f));

            float valueX = panelLeft + padX + panelW * 0.38f;
            for (int i = 0; i < labels.Length; i++)
            {
                float y = panelTop + headerH + (lineH * i) + lineH * 0.15f;
                DrawHudText(labels[i], panelLeft + padX, y, scale * 0.90f, labelCol);

                Color vc = valueCol;
                // Item 8: Quantum Personality (Vivid Amber / Gold)
                if (i == 8)
                {
                    vc = Color.FromArgb((int)(255 * fade), 255, 195, 45);
                }
                // Item 9: Atlas Job Id (Cyber Electric Cyan)
                else if (i == 9)
                {
                    vc = Color.FromArgb((int)(255 * fade), 60, 245, 255);
                }
                DrawHudText(values[i], valueX, y, scale * 0.90f, vc);
            }
        }

        private static void DrawHudText(string text, float screenX, float screenY, float scale, Color color)
        {
            Function.Call(Hash.SET_TEXT_FONT, 4);
            Function.Call(Hash.SET_TEXT_SCALE, scale, scale);
            Function.Call(Hash.SET_TEXT_COLOUR, (int)color.R, (int)color.G, (int)color.B, (int)color.A);
            Function.Call(Hash.SET_TEXT_DROPSHADOW, 1, 0, 0, 0, (int)(color.A * 0.8f));
            Function.Call(Hash.SET_TEXT_CENTRE, false);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, text);
            Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, screenX, screenY);
        }

        private static void ShowNativeHelpText(string text)
        {
            Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_HELP, "STRING");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, text);
            Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_HELP, 0, false, false, -1);
        }

        private void RenderSimpleHintHud()
        {
            string prompt = _isDeRenderActive
                ? "PRESS ~y~F9~s~ TO STOP DE-RENDER"
                : "PRESS ~y~F9~s~ TO ACTIVATE DE-RENDER";
            ShowNativeHelpText(prompt);
        }

        // ====================================================================
        // PED SCANNING & DEEP DIGITAL IDENTITY EXTRACTION
        // ====================================================================
        private void PerformSpatialScan(Ped playerPed)
        {
            RestorePedAlphas();
            _trackedTargets.Clear();

            Vector3 playerPos = playerPed.Position;
            Ped[] nearbyPeds = World.GetNearbyPeds(playerPos, _config.ScanRadius);
            if (nearbyPeds == null || nearbyPeds.Length == 0) return;

            Array.Sort(nearbyPeds, (a, b) =>
            {
                if (a == null || !a.Exists()) return 1;
                if (b == null || !b.Exists()) return -1;
                return Vector3.Distance(playerPos, a.Position).CompareTo(Vector3.Distance(playerPos, b.Position));
            });

            int maxAllowed = Math.Min(30, _config.MaxTargets);

            for (int i = 0; i < nearbyPeds.Length && _trackedTargets.Count < maxAllowed; i++)
            {
                Ped ped = nearbyPeds[i];
                if (ped == null || !ped.Exists() || ped.Handle == playerPed.Handle || ped.IsDead) continue;
                if (ped.IsInVehicle()) continue;

                float dist = Vector3.Distance(playerPos, ped.Position);
                if (dist > _config.ScanRadius) continue;

                int handle = ped.Handle;

                // 1. Extract Real GTA V Variables using Rock-Solid Tested Natives & SHVDN Properties
                int modelHash = ped.Model.Hash;
                string modelName = null;
                try
                {
                    uint uHash = unchecked((uint)modelHash);
                    modelName = Enum.GetName(typeof(PedHash), (PedHash)uHash);
                }
                catch { }

                if (string.IsNullOrWhiteSpace(modelName))
                {
                    try
                    {
                        modelName = Enum.GetName(typeof(PedHash), (PedHash)modelHash);
                    }
                    catch { }
                }

                if (string.IsNullOrWhiteSpace(modelName) || modelName.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    modelName = FormatFriendlyArchetype(ped);
                }

                int alertness = Function.Call<int>(Hash.GET_PED_ALERTNESS, handle);
                int nativeMoney = Function.Call<int>(Hash.GET_PED_MONEY, handle);
                int wallet = nativeMoney > 0 ? nativeMoney : DeriveWalletFallback(handle);

                // Exact GTA V Ped Model Name (e.g. A_F_Y_BEVHILLS_01, G_M_Y_BALLAS_01)
                string internalModel = modelName;
                string faction = DerivePedFaction(ped, modelName);
                string voiceTag = DeriveVoiceProfile(ped, modelName, handle);
                int accuracy = DerivePedAccuracy(ped, handle);
                string psychology = DeriveCombatPsychology(ped, alertness, accuracy);

                string condition = DeriveHiddenCondition(ped, alertness);
                Tuple<string, string> weapon = ExtractEquippedWeapon(ped);

                // 2. Build 8-Dimensional Digital Signature Vector
                // Maps the pedestrian's physical entity signature into full 8D quantum input space
                // [Model, Faction, Voice, Accuracy, Cash, Alertness, Vitality, Entity Seed]
                int pedSeedVal = ComputeStableEntitySeed(handle, modelHash);
                float[] digitalVector = new float[8];
                digitalVector[0] = Hash01(modelHash, pedSeedVal, 11);
                digitalVector[1] = Hash01(faction.GetHashCode(), pedSeedVal, 23);
                digitalVector[2] = Hash01(voiceTag.GetHashCode(), pedSeedVal, 37);
                digitalVector[3] = ((accuracy * 17 + (pedSeedVal % 83)) % 100) / 100.0f;
                digitalVector[4] = ((wallet * 37 + (pedSeedVal % 97)) % 800) / 800.0f;
                digitalVector[5] = Hash01(alertness * 131 + pedSeedVal, handle, 53);
                digitalVector[6] = Math.Max(0.10f, Math.Min(1.0f, ((ped.Health % 100) * 0.4f + (pedSeedVal % 100) * 0.6f) / 100.0f));
                digitalVector[7] = Hash01(ped.Armor * 73 + pedSeedVal, modelHash, 79);

                // 3. Match against Quantum Atlas Bank (Balanced 8D Nearest-Neighbor)
                QPixlSpectrum qSpec = _quantumBank.FindNearestSpectrum(digitalVector, pedSeedVal);
                Color qCol1, qCol2;
                PedQuantumPalette pal = BuildQuantumPaletteFromDigitalSignature(qSpec, digitalVector, out qCol1, out qCol2);

                _trackedTargets.Add(new DeRenderedTarget
                {
                    Pedestrian = ped,
                    Handle = handle,
                    Archetype = modelName,
                    InternalModelName = internalModel,
                    RelationshipFaction = faction,
                    VoiceTag = voiceTag,
                    CombatAccuracy = accuracy,
                    CombatPsychology = psychology,
                    WalletCash = wallet,
                    Health = Math.Max(0, ped.Health),
                    Armor = Math.Max(0, ped.Armor),
                    WeaponName = weapon.Item1,
                    WeaponType = weapon.Item2,
                    HiddenCondition = condition,
                    DistanceToPlayer = dist,
                    DigitalHash = pedSeedVal,
                    QuantumSpectrum = qSpec,
                    Palette = pal,
                    QColor1 = qCol1,
                    QColor2 = qCol2
                });
            }
        }

        private static string FormatFriendlyArchetype(Ped ped)
        {
            int num = Math.Abs(ped.Model.Hash) % 99;
            bool isMale = Function.Call<bool>(Hash.IS_PED_MALE, ped.Handle);
            string genderTag = isMale ? "M" : "F";
            return $"CIV_METROPOLIS_{genderTag}{num:D2}";
        }

        private static string DerivePedClassification(Ped ped, string modelName)
        {
            string upper = modelName.ToUpperInvariant();
            if (upper.Contains("COP") || upper.Contains("POLICE") || upper.Contains("SHERIFF")) return "LAW ENFORCEMENT";
            if (upper.Contains("SWAT") || upper.Contains("ARMY") || upper.Contains("MARINE")) return "TACTICAL // MILITARY";
            if (upper.Contains("MEDIC") || upper.Contains("PARAMEDIC")) return "PARAMEDIC // EMS";
            if (upper.Contains("FIRE")) return "FIRE SERVICES";
            if (upper.Contains("BALLAS") || upper.Contains("FAMILIES") || upper.Contains("VAGOS") || upper.Contains("GANG")) return "GANG AFFILIATE";
            if (upper.Contains("SECURITY")) return "PRIVATE SECURITY";
            if (upper.Contains("BUSINESS") || upper.Contains("EXECUTIVE")) return "CIVILIAN // CORPORATE";
            return upper.Contains("_F_") || upper.Contains("FEMALE") ? "CIVILIAN // FEMALE" : "CIVILIAN // MALE";
        }

        private static string DerivePedFaction(Ped ped, string modelName)
        {
            string upper = modelName.ToUpperInvariant();
            if (upper.Contains("COP") || upper.Contains("POLICE")) return "LSPD_DISPATCH";
            if (upper.Contains("SHERIFF")) return "BCSO_DISPATCH";
            if (upper.Contains("BALLAS")) return "AMBIENT_GANG_BALLAS";
            if (upper.Contains("FAMILIES")) return "AMBIENT_GANG_FAMILY";
            if (upper.Contains("VAGOS")) return "AMBIENT_GANG_MEXICAN";
            if (upper.Contains("SECURITY")) return "SEC_GARD";
            if (upper.Contains("FIRE")) return "FD_ENGINE";
            if (upper.Contains("MEDIC")) return "EMS_DISPATCH";
            return "METROPOLITAN_NEUTRAL";
        }

        private string DeriveActiveTask(Ped ped)
        {
            if (_assignedTasks.ContainsKey(ped.Handle)) return _assignedTasks[ped.Handle];
            if (ped.IsInCombat || ped.IsShooting) return "TASK_COMBAT";
            if (ped.IsFleeing) return "TASK_SMART_FLEE";
            if (ped.IsInVehicle()) return "TASK_IN_VEHICLE";
            if (ped.IsProne) return "TASK_WRITHE";
            if (ped.IsSprinting) return "TASK_SPRINT";
            if (ped.IsRunning) return "TASK_RUN";
            if (ped.IsWalking) return "TASK_WANDER_STANDARD";
            return "TASK_STAND_STILL";
        }

        private static string DeriveRelationshipToPlayer(Ped ped, Ped player)
        {
            try
            {
                if (player != null && player.Exists())
                {
                    Relationship rel = ped.GetRelationshipWithPed(player);
                    switch (rel)
                    {
                        case Relationship.Hate: return "HOSTILE // HATE";
                        case Relationship.Dislike: return "UNFRIENDLY";
                        case Relationship.Like: return "FRIENDLY";
                        case Relationship.Respect: return "ALLY // RESPECT";
                        case Relationship.Companion: return "COMPANION";
                        default: return "NEUTRAL";
                    }
                }
            }
            catch { }
            return "NEUTRAL";
        }

        private static string DeriveVoiceProfile(Ped ped, string modelName, int handle)
        {
            int seed = Math.Abs(handle % 8);
            string upper = modelName.ToUpperInvariant();
            string gender = (upper.Contains("_F_") || upper.Contains("FEMALE")) ? "F" : "M";
            return $"VOICE_{modelName.Substring(0, Math.Min(10, modelName.Length)).ToUpperInvariant()}_{gender}{seed}";
        }

        private static int DerivePedAccuracy(Ped ped, int handle)
        {
            if (ped.Weapons.Current != null && ped.Weapons.Current.Hash != WeaponHash.Unarmed)
            {
                return 45 + Math.Abs((handle * 17) % 45); // 45% - 90%
            }
            return 10 + Math.Abs((handle * 11) % 25);     // 10% - 35%
        }

        private static string DeriveCombatPsychology(Ped ped, int alertness, int accuracy)
        {
            if (ped.IsInCombat || ped.IsShooting) return "COMBATANT // ADRENALINE";
            if (ped.IsFleeing) return "PANIC // HIGH CORTISOL";
            if (accuracy >= 65) return "TACTICAL // ADVANCED";
            if (alertness >= 2) return "HYPER-VIGILANT // NERVOUS";
            return "STANDOFFISH // EQUILIBRIUM";
        }

        private static string DeriveHiddenCondition(Ped ped, int alertness)
        {
            if (ped.IsProne) return "GROUNDED / PRONE";
            if (ped.IsFleeing) return "ELEVATED CORTISOL // FLEEING";
            if (ped.IsInCombat) return "COMBAT READINESS";
            if (alertness >= 2) return "HYPER-VIGILANT";
            return "EQUILIBRIUM // STABLE";
        }

        private static int DeriveWalletFallback(int handle)
        {
            return 15 + Math.Abs((handle * 83) % 430);
        }

        private static Tuple<string, string> ExtractEquippedWeapon(Ped ped)
        {
            try
            {
                var cur = ped.Weapons.Current;
                if (cur != null && cur.Hash != WeaponHash.Unarmed)
                {
                    return Tuple.Create(cur.Hash.ToString().Replace("Weapon", ""), "Armed");
                }
            }
            catch { }
            return Tuple.Create("NONE", "Clear");
        }

        private static PedQuantumPalette BuildQuantumPaletteFromDigitalSignature(
            QPixlSpectrum spec,
            float[] digitalVector,
            out Color qCol1,
            out Color qCol2)
        {
            // 3 Exclusive Biometric Factors for Center Color:
            // 1. Accuracy: digitalVector[3] (0.0 - 1.0)
            // 2. Wallet Cash: digitalVector[4] (0.0 - 1.0)
            // 3. Health Vitality: digitalVector[6] (0.0 - 1.0)
            float accuracyNorm = digitalVector != null && digitalVector.Length > 3 ? digitalVector[3] : 0.5f;
            float walletNorm = digitalVector != null && digitalVector.Length > 4 ? digitalVector[4] : 0.2f;
            float healthNorm = digitalVector != null && digitalVector.Length > 6 ? digitalVector[6] : 1.0f;

            // v2.3: the measured qpixl-v1 OUTPUT spectrum (real Atlas result) now shifts the palette.
            float qShift = 0f, qSat = 0f, qBri = 0f;
            if (spec != null && spec.Normalized != null && spec.Normalized.Length >= 8)
            {
                qShift = (spec.Normalized[0] + spec.Normalized[2] + spec.Normalized[5] - 1.5f) * 0.16f;
                qSat = (spec.Normalized[3] - 0.5f) * 0.14f;
                qBri = (spec.Normalized[7] - 0.5f) * 0.10f;
            }

            // Compute center core hue from the 3 pure factors + quantum spectrum shift
            float coreHue = (walletNorm * 0.38f + accuracyNorm * 0.42f + healthNorm * 0.20f + qShift + 1.0f) % 1.0f;
            Color centerColor = HsvToColor(coreHue, Math.Max(0.6f, Math.Min(1f, 0.90f + qSat)), Math.Max(0.8f, Math.Min(1f, 1.00f + qBri)));

            // Ring 2 (Inner Edge): High-contrast complementary electric cyan/aqua or hyper-violet
            float innerHue = (coreHue + 0.38f) % 1.0f;
            Color innerEdgeColor = HsvToColor(innerHue, 0.85f, 0.98f);

            // Ring 3 (Outer Perimeter Edge): Deep neon midnight obsidian / electric cobalt
            float outerHue = (coreHue + 0.68f) % 1.0f;
            Color outerEdgeColor = HsvToColor(outerHue, 0.95f, 0.60f);

            qCol1 = centerColor;
            qCol2 = innerEdgeColor;

            string hexCenter = $"#{centerColor.R:X2}{centerColor.G:X2}{centerColor.B:X2}";
            string hexInner = $"#{innerEdgeColor.R:X2}{innerEdgeColor.G:X2}{innerEdgeColor.B:X2}";
            string hexOuter = $"#{outerEdgeColor.R:X2}{outerEdgeColor.G:X2}{outerEdgeColor.B:X2}";

            return new PedQuantumPalette
            {
                Center = centerColor,
                InnerEdge = innerEdgeColor,
                OuterEdge = outerEdgeColor,
                Crown = innerEdgeColor,
                Primary = centerColor,
                Highlight = innerEdgeColor,
                Secondary = outerEdgeColor,
                CrownHex = hexInner,
                PrimaryHex = hexCenter,
                SecondaryHex = hexOuter,
                Label = "CONCENTRIC 3-TIER"
            };
        }

        private static Color HsvToColor(float h, float s, float v)
        {
            h = h - (float)Math.Floor(h);
            float hh = h * 6.0f;
            int i = (int)Math.Floor(hh);
            float f = hh - i;
            float p = v * (1f - s), q = v * (1f - s * f), t = v * (1f - s * (1f - f));
            float r, g, b;
            switch (i % 6)
            {
                case 0: r = v; g = t; b = p; break;
                case 1: r = q; g = v; b = p; break;
                case 2: r = p; g = v; b = t; break;
                case 3: r = p; g = q; b = v; break;
                case 4: r = t; g = p; b = v; break;
                default: r = v; g = p; b = q; break;
            }
            return Color.FromArgb(255, ClampByte((int)(r * 255f)), ClampByte((int)(g * 255f)), ClampByte((int)(b * 255f)));
        }

        private static void DrawHexagonCell(Vector3 center, Vector3 right, Vector3 trueUp, float radius, Color col)
        {
            // Highly optimized single-sided hexagonal geometry (4 triangles instead of 12 native calls)
            const float hx = 0.866025f; // sqrt(3)/2

            Vector3 p0 = center + (right * radius);
            Vector3 p1 = center + (right * (radius * 0.5f)) + (trueUp * (radius * hx));
            Vector3 p2 = center + (right * (-radius * 0.5f)) + (trueUp * (radius * hx));
            Vector3 p3 = center + (right * -radius);
            Vector3 p4 = center + (right * (-radius * 0.5f)) + (trueUp * (-radius * hx));
            Vector3 p5 = center + (right * (radius * 0.5f)) + (trueUp * (-radius * hx));

            // 4 Triangles seamlessly fill the hexagon without extra unmanaged calls
            Function.Call(Hash.DRAW_POLY, p0.X, p0.Y, p0.Z, p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z, (int)col.R, (int)col.G, (int)col.B, (int)col.A);
            Function.Call(Hash.DRAW_POLY, p0.X, p0.Y, p0.Z, p2.X, p2.Y, p2.Z, p3.X, p3.Y, p3.Z, (int)col.R, (int)col.G, (int)col.B, (int)col.A);
            Function.Call(Hash.DRAW_POLY, p0.X, p0.Y, p0.Z, p3.X, p3.Y, p3.Z, p4.X, p4.Y, p4.Z, (int)col.R, (int)col.G, (int)col.B, (int)col.A);
            Function.Call(Hash.DRAW_POLY, p0.X, p0.Y, p0.Z, p4.X, p4.Y, p4.Z, p5.X, p5.Y, p5.Z, (int)col.R, (int)col.G, (int)col.B, (int)col.A);
        }

        private static void DrawSingleSidedQuad(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Color col)
        {
            Function.Call(Hash.DRAW_POLY, a.X, a.Y, a.Z, b.X, b.Y, b.Z, c.X, c.Y, c.Z, (int)col.R, (int)col.G, (int)col.B, (int)col.A);
            Function.Call(Hash.DRAW_POLY, a.X, a.Y, a.Z, c.X, c.Y, c.Z, d.X, d.Y, d.Z, (int)col.R, (int)col.G, (int)col.B, (int)col.A);
        }

        private static void DrawDoubleSidedQuad(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Color col)
        {
            Function.Call(Hash.DRAW_POLY, a.X, a.Y, a.Z, b.X, b.Y, b.Z, c.X, c.Y, c.Z, (int)col.R, (int)col.G, (int)col.B, (int)col.A);
            Function.Call(Hash.DRAW_POLY, c.X, c.Y, c.Z, b.X, b.Y, b.Z, a.X, a.Y, a.Z, (int)col.R, (int)col.G, (int)col.B, (int)col.A);
            Function.Call(Hash.DRAW_POLY, a.X, a.Y, a.Z, c.X, c.Y, c.Z, d.X, d.Y, d.Z, (int)col.R, (int)col.G, (int)col.B, (int)col.A);
            Function.Call(Hash.DRAW_POLY, d.X, d.Y, d.Z, c.X, c.Y, c.Z, a.X, a.Y, a.Z, (int)col.R, (int)col.G, (int)col.B, (int)col.A);
        }

        // ====================================================================
        // v2.3: PERSISTENT QUANTUM PERSONALITY + OBSERVER RULE
        // ====================================================================
        // Each ped's personality comes from its measured qpixl-v1 output spectrum (real Atlas result).
        // Observer rule: peds you are watching stay stable; peds you are NOT watching behave unpredictably
        // (freeze / wander off / turn toward you) on a timer driven by that same spectrum.
        private readonly HashSet<int> _collapsedHandles = new HashSet<int>();
        private readonly HashSet<int> _frozenHandles = new HashSet<int>();
        private readonly Dictionary<int, int> _nextEventTime = new Dictionary<int, int>();
        private readonly Dictionary<int, int> _eventCount = new Dictionary<int, int>();
        private bool _playerAiming;

        private static string DerivePersonality(QPixlSpectrum s)
        {
            if (s == null) return "CURIOUS";
            if (!string.IsNullOrEmpty(s.Personality)) return s.Personality;
            if (s.Normalized == null || s.Normalized.Length < 8) return "CURIOUS";
            float q = s.Normalized[1];
            if (q < 0.25f) return "BRAVE";
            if (q < 0.50f) return "SKITTISH";
            if (q < 0.75f) return "CURIOUS";
            return "STOIC";
        }

        private static bool IsProtectedPed(DeRenderedTarget t)
        {
            string u = (t.InternalModelName ?? "").ToUpperInvariant();
            return u.Contains("COP") || u.Contains("POLICE") || u.Contains("SHERIFF") || u.Contains("SWAT")
                || u.Contains("ARMY") || u.Contains("MARINE") || u.Contains("FIRE") || u.Contains("MEDIC");
        }

        private readonly Dictionary<int, string> _assignedTasks = new Dictionary<int, string>();
        private readonly Dictionary<int, int> _lastCuriousRefresh = new Dictionary<int, int>();

        private void ApplyCollapseBehavior(DeRenderedTarget target, Ped player)
        {
            Ped ped = target.Pedestrian;
            if (ped == null || !ped.Exists() || ped.IsDead || ped.IsInCombat || ped.IsFleeing || IsProtectedPed(target)) return;

            string personality = DerivePersonality(target.QuantumSpectrum);

            // If already collapsed within 9m, keep CURIOUS peds attentively facing/looking at the observer
            if (_collapsedHandles.Contains(target.Handle))
            {
                if (personality == "CURIOUS")
                {
                    int now = Game.GameTime;
                    int lastRef;
                    if (!_lastCuriousRefresh.TryGetValue(target.Handle, out lastRef) || now - lastRef > 2200)
                    {
                        _lastCuriousRefresh[target.Handle] = now;
                        _assignedTasks[target.Handle] = "TASK_TURN_PED_TO_FACE_ENTITY";
                        Function.Call(Hash.TASK_TURN_PED_TO_FACE_ENTITY, ped.Handle, player.Handle, 2500);
                        Function.Call(Hash.TASK_LOOK_AT_ENTITY, ped.Handle, player.Handle, 3000, 2048, 3);
                    }
                }
                return;
            }

            _collapsedHandles.Add(target.Handle);
            try
            {
                switch (personality)
                {
                    case "BRAVE":    // walks directly up to the observer
                        ped.Task.ClearAll();
                        _assignedTasks[target.Handle] = "TASK_GO_TO_ENTITY";
                        Function.Call(Hash.TASK_GO_TO_ENTITY, ped.Handle, player.Handle, -1, 1.8f, 1.5f, 0, 0);
                        Function.Call(Hash.TASK_LOOK_AT_ENTITY, ped.Handle, player.Handle, -1, 2048, 3);
                        break;
                    case "SKITTISH": // startled by quantum observation, flees from player
                        ped.Task.ClearAll();
                        _assignedTasks[target.Handle] = "TASK_SMART_FLEE";
                        Function.Call(Hash.TASK_SMART_FLEE_PED, ped.Handle, player.Handle, 65.0f, -1, false, false);
                        break;
                    case "CURIOUS":  // stops and turns to inspect the observer
                        ped.Task.ClearAll();
                        _lastCuriousRefresh[target.Handle] = Game.GameTime;
                        _assignedTasks[target.Handle] = "TASK_TURN_PED_TO_FACE_ENTITY";
                        Function.Call(Hash.TASK_TURN_PED_TO_FACE_ENTITY, ped.Handle, player.Handle, 4000);
                        Function.Call(Hash.TASK_LOOK_AT_ENTITY, ped.Handle, player.Handle, 5000, 2048, 3);
                        break;
                    default:         // STOIC: unbothered, continues natural life unless aimed at
                        if (_playerAiming && Function.Call<bool>(Hash.IS_PLAYER_FREE_AIMING_AT_ENTITY, Game.Player.Handle, ped.Handle))
                        {
                            _assignedTasks[target.Handle] = "TASK_SMART_FLEE";
                            ped.Task.ReactAndFlee(player);
                        }
                        break;
                }
            }
            catch { }
        }

        private void UpdateObserverRule(DeRenderedTarget target, Ped player, float camDist, bool onScreen)
        {
            Ped ped = target.Pedestrian;
            if (ped == null || !ped.Exists() || ped.IsDead || ped.IsInCombat || ped.IsFleeing || IsProtectedPed(target)) return;
            int h = target.Handle;
            int now = Game.GameTime;
            try
            {
                // STOIC peds only react when you aim at them
                if (_playerAiming && camDist < 25.0f && DerivePersonality(target.QuantumSpectrum) == "STOIC"
                    && Function.Call<bool>(Hash.IS_PLAYER_FREE_AIMING_AT_ENTITY, Game.Player.Handle, h))
                {
                    _assignedTasks[h] = "TASK_SMART_FLEE";
                    ped.Task.ReactAndFlee(player);
                    return;
                }

                if (onScreen)
                {
                    // Observed: wavefunction stays stable. Anything frozen while unobserved resumes normal life.
                    if (_frozenHandles.Remove(h)) Function.Call(Hash.TASK_WANDER_STANDARD, h, 10.0f, 10);
                    return;
                }

                // Unobserved within 30m: unpredictable quantum events
                if (camDist > 30.0f || camDist < 3.0f) return;
                int due;
                if (!_nextEventTime.TryGetValue(h, out due)) { _nextEventTime[h] = now + 2000 + (int)(Hash01(h, 3, 5) * 4000f); return; }
                if (now < due) return;

                int k; _eventCount.TryGetValue(h, out k); _eventCount[h] = k + 1;
                float u = (target.QuantumSpectrum != null && target.QuantumSpectrum.Normalized != null)
                    ? target.QuantumSpectrum.Normalized[(k + 1) % 8]
                    : Hash01(h, k, 9);
                _nextEventTime[h] = now + 5000 + (int)(u * 4000f);

                if (u < 0.34f)
                {
                    Function.Call(Hash.TASK_STAND_STILL, h, 3000);      // freezes in place
                    _frozenHandles.Add(h);
                }
                else if (u < 0.67f)
                {
                    Function.Call(Hash.TASK_WANDER_STANDARD, h, 10.0f, 10); // new random direction
                }
                else
                {
                    Function.Call(Hash.TASK_TURN_PED_TO_FACE_ENTITY, h, player.Handle, 2000); // turns to where you are
                }
            }
            catch { }
        }
        private static int ComputeStableEntitySeed(int handle, int modelHash)
        {
            unchecked
            {
                uint h = (uint)handle * 2654435761u;
                uint m = (uint)modelHash * 2246822519u;
                uint mix = (h ^ (m >> 13) ^ (h >> 7)) * 3266489917u;
                return (int)((mix ^ (mix >> 16)) & 0x7FFFFFFF);
            }
        }

        private static float Hash01(int a, int b, int c)
        {
            double s = Math.Sin(a * 12.9898 + b * 78.233 + c * 37.719) * 43758.5453;
            return (float)(s - Math.Floor(s));
        }

        private readonly Dictionary<int, PedQuantumPalette> _palCache = new Dictionary<int, PedQuantumPalette>();
        private PedQuantumPalette CachedSpecPalette(int index)
        {
            int n = _quantumBank.SpectrumCount;
            int k = n == 0 ? 0 : Math.Abs(index) % n;
            PedQuantumPalette p;
            if (_palCache.TryGetValue(k, out p)) return p;
            QPixlSpectrum spec = _quantumBank.GetSpectrumByIndex(k);
            Color a, b;
            p = BuildQuantumPaletteFromDigitalSignature(spec, spec.Normalized, out a, out b);
            _palCache[k] = p;
            return p;
        }

        private void RestorePedAlphas()
        {
            for (int i = 0; i < _trackedTargets.Count; i++)
            {
                Ped ped = _trackedTargets[i].Pedestrian;
                if (ped != null && ped.Exists())
                {
                    Function.Call(Hash.RESET_ENTITY_ALPHA, ped.Handle);
                }
            }
        }

        private static int ClampByte(int val) => val < 0 ? 0 : (val > 255 ? 255 : val);

        private static Color InterpolateColor(Color a, Color b, float t)
        {
            t = Math.Max(0.0f, Math.Min(1.0f, t));
            int r = (int)(a.R + (b.R - a.R) * t);
            int g = (int)(a.G + (b.G - a.G) * t);
            int bl = (int)(a.B + (b.B - a.B) * t);
            return Color.FromArgb(255, ClampByte(r), ClampByte(g), ClampByte(bl));
        }

        private void OnAborted(object sender, EventArgs e)
        {
            Ped player = Game.Player?.Character;
            if (player != null && player.Exists())
            {
                UnequipPlayerGlasses(player);
            }
            RestorePedAlphas();
            _trackedTargets.Clear();
            _collapsedHandles.Clear();
            _frozenHandles.Clear();
            _nextEventTime.Clear();
            _eventCount.Clear();
            _assignedTasks.Clear();
            _lastCuriousRefresh.Clear();
        }

        private void EquipPlayerGlasses(Ped player)
        {
            try
            {
                // Anchor point 1 = Eyes (Glasses prop)
                int currentProp = Function.Call<int>(Hash.GET_PED_PROP_INDEX, player.Handle, 1);
                int currentTex = Function.Call<int>(Hash.GET_PED_PROP_TEXTURE_INDEX, player.Handle, 1);

                if (currentProp >= 0)
                {
                    _hadOriginalGlasses = true;
                    _originalGlassesDrawable = currentProp;
                    _originalGlassesTexture = currentTex;
                }
                else
                {
                    _hadOriginalGlasses = false;
                    _originalGlassesDrawable = -1;
                    _originalGlassesTexture = 0;
                }

                // Check character model: Franklin, Michael, Trevor or MP/ambient
                int hash = player.Model.Hash;
                int glassesDrawable = 0;
                int glassesTexture = 0;

                // Franklin: hash == (int)PedHash.Franklin
                if (hash == unchecked((int)PedHash.Franklin))
                {
                    glassesDrawable = 0; // Iconic shades
                    glassesTexture = 0;
                }
                else if (hash == unchecked((int)PedHash.Michael))
                {
                    glassesDrawable = 0; // Michael's aviators
                    glassesTexture = 0;
                }
                else if (hash == unchecked((int)PedHash.Trevor))
                {
                    glassesDrawable = 1; // Trevor's aviator/sunglasses
                    glassesTexture = 0;
                }
                else
                {
                    // Generic MP or Ambient character
                    int variations = Function.Call<int>(Hash.GET_NUMBER_OF_PED_PROP_DRAWABLE_VARIATIONS, player.Handle, 1);
                    glassesDrawable = variations > 0 ? 0 : -1;
                }

                if (glassesDrawable >= 0)
                {
                    // Set glasses: anchor 1 (Eyes), drawable, texture, attach = true
                    Function.Call(Hash.SET_PED_PROP_INDEX, player.Handle, 1, glassesDrawable, glassesTexture, true);
                    _playerHasCustomGlassesEquipped = true;
                }
            }
            catch { }
        }

        private void UnequipPlayerGlasses(Ped player)
        {
            try
            {
                if (!_playerHasCustomGlassesEquipped) return;

                if (_hadOriginalGlasses && _originalGlassesDrawable >= 0)
                {
                    Function.Call(Hash.SET_PED_PROP_INDEX, player.Handle, 1, _originalGlassesDrawable, _originalGlassesTexture, true);
                }
                else
                {
                    // Anchor point 1 = Eyes
                    Function.Call(Hash.CLEAR_PED_PROP, player.Handle, 1);
                }

                _playerHasCustomGlassesEquipped = false;
            }
            catch { }
        }
    }

    #endregion
}
