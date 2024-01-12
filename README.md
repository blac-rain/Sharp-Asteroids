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

# Ideas for Future Updates
- Resizable window
- window and other information as variables not literals
- Start Button (new overlay or menu class for such functions?)
- Pause function (needs `elapsedTime` rework)
- different shape for enemy, more like asteroids (use image?)
- Reset player position when hit
- Display of points and death counters and game time not only as `DrawText`, e.g. as a separate bar at the bottom or top
- add more game sounds
	- player movement
	- enemy spawn
	- at win and lose

--- 

assets and sounds by [kenney](https://www.kenney.nl) under CC0 license

font [Fira Code](https://github.com/tonsky/FiraCode) under OFL-1.1 license