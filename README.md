# Alien Showdown: Invasion of the Bleep-Blops

A 3D take on the classic *Space Invaders* formula, built in Unity. Rows of alien ships sweep side to side and creep toward your base while firing back at you. Shoot them down before they cross the end zone, and use the shields to soak up enemy fire.

---

## Download and play

### Requirements

- [Unity Hub](https://unity.com/download)
- Unity Editor **2022.3.4f1** (LTS)
- [Git](https://git-scm.com/) and [Git LFS](https://git-lfs.com/)

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

## Packages

- Universal Render Pipeline 14.0.8
- Cinemachine 2.9.7
- TextMesh Pro 3.0.6
- Timeline 1.7.4

## Credits

Sound effects and music in `Assets/Music/` come from [Pixabay](https://pixabay.com/) (royalty-free).
