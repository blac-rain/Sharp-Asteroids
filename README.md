# Sharp Asteroids
**a small asteroids inspired game**
*using raylib in csharp*

# Goal
- Every few seconds enemies come from one of the four sides
- You must shoot them to get points
- Don't get hit by them
- When you have **10 points you win**
- After **10 hits** from enemies **you lose**

# Controls
- Rotate with <- and -> or with A and D
- Move forward with &#8593; or W
- Move backwards with &#8595; or S
- Shoot with Space

---

# Future Updates and Known Bugs
## Next Update
- change or improve the representation of player (perhaps a new form?)
- change font
- BUG: enemies not always spawning

## Planned
- Resizable window
- window and other information as variables not literals
- Start Button (new overlay or menu class for such functions?)
- Pause function (needs `elapsedTime` rework)
- different shape for enemy, more like asteroids (use image?)
- Display of points and death counters and game time not only as `DrawText`, e.g. as a separate bar at the bottom or top
- add game sounds
	- fire bullet
	- player movement
	- enemy spawn