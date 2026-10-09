# Alien Showdown: Invasion of the Bleep-Blops

A 3D take on the classic *Space Invaders* formula, built in Unity. Rows of alien ships sweep side to side and creep toward your base while firing back at you. Shoot them down before they cross the end zone, and use the shields to soak up enemy fire.

---

## Download and play

### Requirements

| Tool | Version |
| --- | --- |
| [Unity Hub](https://unity.com/download) | Latest |
| Unity Editor | **2022.3.4f1** (LTS), with the Universal Render Pipeline |
| [Git](https://git-scm.com/) + [Git LFS](https://git-lfs.com/) | Any recent version |

The project uses Unity 2022.3.4f1. A newer 2022.3.x LTS release should also open it, but Unity may upgrade some project files when you do.

### 1. Clone the repository

```bash
git lfs install
git clone https://github.com/Jay-A-Kad/alien-invasion-IOBB.git
cd alien-invasion-IOBB
```

### 2. Open the project in Unity

1. Open **Unity Hub**, go to **Projects**, and click **Add → Add project from disk**.
2. Select the cloned `alien-invasion-IOBB` folder.
3. If Hub reports a missing editor version, install **2022.3.4f1** from the **Installs** tab, or pick another 2022.3 LTS release.
4. Open the project. The first import takes a few minutes because Unity rebuilds the `Library/` folder, which is not stored in the repo.

### 3. Play in the editor

1. In the **Project** window, open `Assets/Scenes/MenuManager.unity`.
2. Press **Play** (▶) at the top of the editor.
3. Click **Play** on the main menu to load the game scene.

### 4. (Optional) Build a standalone game

1. Go to **File → Build Settings**.
2. Make sure both scenes are listed in this order:
   - `Scenes/MenuManager` (index 0)
   - `Scenes/AlienShowdown` (index 1)
3. Choose your platform (macOS, Windows or Linux) and click **Build And Run**.

---

## How to play

### Controls

| Action | Input |
| --- | --- |
| Move left / right | `A` / `D` or `←` / `→` |
| Shoot | Hold **left mouse button** |
| Switch camera (third-person ↔ first-person) | `C` |

### Rules

- **Aliens** move sideways together. Each time they hit a side wall, they turn around and step one row closer to you.
- Aliens fire back at random intervals, every 0.5 to 2 seconds.
- **One hit** destroys an alien.
- **Your ship** survives **5 hits**. On the 5th hit it is destroyed.
- **Shields** absorb enemy shots and break after **10 hits**. Your own shots pass through them harmlessly.
- **Game over:** if any alien reaches the **end zone** near your side of the field, the *Game Over* screen appears.

---

## Code structure

```
Assets/
├── Scenes/
│   ├── MenuManager.unity       # Main menu (build index 0)
│   └── AlienShowdown.unity     # Gameplay scene (build index 1)
├── Scripts/                    # All gameplay C# code (see below)
├── GameAssets/
│   ├── bullet.prefab           # Player projectile
│   └── enemyBullet.prefab      # Alien projectile
├── SpaceShips/                 # Player and alien ship models, textures, prefabs
├── Materials/                  # Colours for bullets, floor, shields, end zone
├── Music/                      # Background soundtrack, laser SFX, Timeline asset
├── Fonts/                      # UI fonts
├── Physics/                    # Physic materials
├── Settings/                   # URP render pipeline assets
└── TextMesh Pro/               # TMP package resources
Packages/                       # Unity package manifest (URP, Cinemachine, TMP, Timeline…)
ProjectSettings/                # Input, tags, physics, build scene list, etc.
```

### Scenes

**MenuManager** contains the start screen. Its UI buttons call `menu.PlayGame()` and `menu.ExitGame()`.

**AlienShowdown** is the gameplay scene. It contains:

| Object | Purpose | Scripts |
| --- | --- | --- |
| `player` | The player's ship | `playerMovement`, `playerBulletShooting`, `PlayerHealth` |
| `enemies` (9 ships) | The alien formation | `enemyMovement`, `enemyBulletShooting`, `EnemyHealth` |
| `shields` (`shield_1`–`shield_3`) | Destructible cover | `BlockHealth` |
| `walls` (`Lwall`, `Rwall`) | Side boundaries, tagged `Wall`, that turn the aliens around | none |
| `gameOverZone` / `GameOverLine` | Trigger volume at the player's end of the field | `gameOverZone` |
| `gameOverCanvas` | "Game Over" UI, hidden until triggered | none |
| `ThrirdPerson` / `FirstPerson` | The two gameplay cameras | toggled by `cameraSwitch` |
| `CameraSwitchManager` | Holds the camera toggle logic | `cameraSwitch` |

### Scripts (`Assets/Scripts/`)

#### Player

| Script | Responsibility |
| --- | --- |
| `playerMovement.cs` | Reads the `Horizontal` input axis and sets the Rigidbody's X velocity (`playerSpeed`, default 10). |
| `playerBulletShooting.cs` | While the left mouse button is held, spawns `bulletPrefab` at `firePoint` every `fireRate` seconds (0.2s) and plays the laser sound. Each bullet gets a `PlayerBulletBehavior` component at runtime. |
| `PlayerBulletBehavior` *(in `playerBulletShooting.cs`)* | On collision: ignores objects tagged `Shield`, and calls `EnemyHealth.TakeDamage()` on objects tagged `Enemy`, then destroys itself. |
| `PlayerHealth.cs` | Counts hits. At 5 hits it destroys the player and, in the editor, stops Play mode. |

#### Enemies

| Script | Responsibility |
| --- | --- |
| `enemyMovement.cs` | Moves the alien along X at `speed`. On hitting a `Wall`, it reverses direction and steps forward by `stepForward` toward the player. |
| `enemyBulletShooting.cs` | Fires `bulletPrefab` from `firePoint` after a random delay between `minShootInterval` and `maxShootInterval`, then schedules the next shot. Ignores collisions with its own ship. |
| `BulletBehavior` *(in `enemyBulletShooting.cs`)* | On collision: damages a `Shield` (`BlockHealth`) or the `Player` (`PlayerHealth`), then destroys itself. |
| `EnemyHealth.cs` | Destroys the alien after 1 hit. |

#### Environment, UI and camera

| Script | Responsibility |
| --- | --- |
| `BlockHealth.cs` | Shield health. Destroys the shield after 10 hits. |
| `gameOverZone.cs` | Trigger volume. Shows `uiCanvas` (the Game Over screen) when an `Enemy` enters and hides it again when the enemy leaves. |
| `cameraSwitch.cs` | Starts in the third-person camera (`TPV`). Pressing `C` swaps to the first-person camera (`FPV`) and back. |
| `menu.cs` | `PlayGame()` loads the next scene in build order. `ExitGame()` quits the application, or stops Play mode in the editor. |

### How the pieces interact

```
 Player input ──► playerMovement ──► Rigidbody (X axis)
      │
      └─(LMB)──► playerBulletShooting ──► bullet + PlayerBulletBehavior
                                               │ hits "Enemy"
                                               ▼
                                          EnemyHealth ──► Destroy alien

 enemyMovement ──hits "Wall"──► reverse + step forward ──► enters gameOverZone ──► Game Over UI

 enemyBulletShooting ──► enemyBullet + BulletBehavior
                               ├─ hits "Shield" ──► BlockHealth  (10 hits)
                               └─ hits "Player" ──► PlayerHealth (5 hits)
```

### Tags used

The project's tag list defines `Wall`, `Enemy` and `Shield`. The scripts also rely on Unity's built-in `Player` tag. Collision logic depends on these tags, so keep them assigned when you add new objects.

---

## Packages

- Universal Render Pipeline 14.0.8
- Cinemachine 2.9.7
- TextMesh Pro 3.0.6
- Timeline 1.7.4

## Credits

Sound effects and music in `Assets/Music/` come from [Pixabay](https://pixabay.com/) (royalty-free).
