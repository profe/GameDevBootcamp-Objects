# Unit 2 - Objects!

Asteroids-like 2D game during CircuitStream Game Development Bootcamp using Unity 6

![screengrab of part 3 gameplay](./Docs/part3_1.gif)

## Part 1 - Add Objects to Objects!

Features:

- UI using Unity Events and C# Actions to update score, high schore, and player health
- WASD/Arrow movement for player, left mouse click to shoot bullet
- OOP Design for playable objects (Player and 3 types of enemies)
  - Melee Enemy: approaches the enemy and explodes, performing heavy damage to the player
  - Machine Gun Enemy: approaches the player and shoots bullets with high rate but low accuracy (e.g.: 5 bullets per second)
  - Shooter Enemy: stays away from the player and shoots bullets with high accuracy but low rate (e.g.: 1 shot per 3 seconds)
- Music and sound effects for shooting, damaging, reseting game

## Part 2 - Add Power-Ups to Objects!

Features:

- UI now includes number of nukes available and start/end game screen
- Added powerups using OOP design (Pickup and 3 types of pickups):
  - Health: heals player with random number between min/max values
  - Nuke: destroys all enemies on screen (but not powerups), limited to 5 (based on size of array in Unity Inspector for UI Manager)
  - Gun Powerup: 10 second machine gun powerup
- Organized Scripting folder
- Added additional sound effects for powerups
- Used Player Prefs to save high score

## Part 3 - Enhance Objects!

Features:

- [Nery] Added levels to game to increase difficulty over time:
  - UI updated and store highest level in PlayerPrefs
  - Level dictates increasing enemy spawn rate using [logarithmic function](https://www.desmos.com/calculator/evhaxcv1jt)
  - Level dictates decreasing pickup spawn rate using [multiplative inverse function](https://www.desmos.com/calculator/djhkukw7ea)
  - Added health bar to boss enemy
  - Did WebGL build
- [Aurélie] UI redo and boss enemy:
  - Changed theme of UI to be animal enemies that fire desserts
  - Added boss enemy with shield powerup drop
  - Added shield (no bullet damage for few seconds) powerup
  - Added main menu with sound settings (control music and sound effects) and instructions

## Documentation

### Melee Enemy Activity Diagram

![activity diagram for melee enemy](./Docs/MeleeEnemy.png)

### Machine Gun Enemy Activity Diagram

![activity diagram for machine gun enemy](./Docs/MachineGunEnemy.png)

### Shooter Enemy Activity Diagram

![activity diagram for shooter enemy](./Docs/ShooterEnemy.png)

### Resources Used:

- ["Retro 8-bit RPG Music Pack" by May Genko](https://assetstore.unity.com/packages/audio/music/retro-8-bit-rpg-music-pack-by-may-genko-249721) for background music
- ["8-Bit Style Sound Effects"](https://assetstore.unity.com/packages/audio/sound-fx/8-bit-style-sound-effects-68228) for sound effects
