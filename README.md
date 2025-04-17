# Unit 2 - Objects!

Asteroids-like 2D retro-style game during CircuitStream Game Development Bootcamp using Unity 6

## Part 1 - Add Objects to Objects!

![screengrab of part 1 gameplay](./Docs/part1.gif)
Features:

- UI using Unity Events and C# Actions to update score, high schore, and player health
- WASD/Arrow movement for player, left mouse click to shoot bullet
- OOP Design for playable objects (Player and 3 types of enemies)
  - Melee Enemy: approaches the enemy and explodes, performing heavy damage to the player
  - Machine Gun Enemy: approaches the player and shoots bullets with high rate but low accuracy (e.g.: 5 bullets per second)
  - Shooter Enemy: stays away from the player and shoots bullets with high accuracy but low rate (e.g.: 1 shot per 3 seconds)
- Music and sound effects for shooting, damaging, reseting game

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
