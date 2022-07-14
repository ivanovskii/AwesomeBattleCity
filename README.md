# AwesomeBattleCity

<img width="400" src="src/game.gif">

**AwesomeBattleCity** is an attempt to clone a [Battle City](https://ru.wikipedia.org/wiki/Battle_City) game using Unity.

## How it run

* [Install Unity](https://unity.com/download) [Editor Version 2021.3.6f1]
* [Configure Visual Studio](https://docs.microsoft.com/en-us/visualstudio/gamedev/unity/get-started/getting-started-with-visual-studio-tools-for-unity?pivots=windows)

## TODO

- [x] Generating a 10x10 game field
- [x] The field must have indestructible walls, destructible walls, the player's tank
- [x] The player can move his tank left and right up and down with the arrow keys and shoot by pressing the space button
- [x] If a shell hits an enemy tank, the enemy tank is destroyed
- [x] If a shell hits a destructable wall, the wall is destroyed
- [x] All tank movements must be animated
- [x] The destruction of tanks and walls should be animated
- [ ] There must be a flag (base) on the field
- [ ] Every 30 seconds on the field in a specially designated place should enemy tank should spawn. A total of 5 enemy tanks must spawn per battle.
- [ ] The flag (base) must be surrounded by destructible walls
- [ ] Enemy tanks can fire, but no more than once every 15 seconds
- [ ] If a shell hits a flag (base), the flag is destroyed
- [ ] The player wins if he destroyed all five enemy tanks
- [ ] The player loses if the flag is destroyed
- [ ] If the player's tank is hit by a shell, the player's tank is respawned at the starting point, and the game continues
- [ ] When you win or lose, the inscription should be displayed and a "Play Again" button, which, when clicked, starts the game again
- [ ] The algorithm of enemy tanks should be written so that in case of inactivity player's inaction, the enemy tanks win within 2-3 minutes, and in the case of active player's actions, the player should have the opportunity to win.
