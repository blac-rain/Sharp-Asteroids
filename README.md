# Sharp Asteroids
a small asteroids like game

# Todo List
## Coming
- change or improve the representation of player (perhaps a new form?)
- change font
- why enemies spawn slow or not always at defined tick?

## QoL
- different shape for enemy, more like asteroids (use image?)
- Display of points and death counters and game time not only as `DrawText`, e.g. as a separate bar at the bottom or top
- add game sounds
	- fire bullet
	- player movement
	- enemy spawn

## Maybe
- Resizable window
- window and other information as variables not literals
- Start Button (new overlay or menu class for such functions?)
- Pause function (needs `elapsedTime` rework)
- ~~win or lose stops player movement~~ (now a feature not a bug that movement after game over is possible)

# Changes
changes before pushed to github

*24/01/11*
- added todo list

*24/01/10*
- added collision check for player, enemies and bullets
- added counter for death
- added points
- added text to show deaths and points

*24/01/09*
- added correct enemies
- corrected enemy spawning