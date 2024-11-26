CS4455 Final Game - Scratch Studio: Cat Burglar

i. Starting scene file: StartMenu.scene


ii. How to play and what parts of the level to observe technology requirements:

Press play on the start menu, then read the instructions on the following page.
When ready, press the play button again. Give it a few seconds to load.
You will spawn into the world facing the Buzz statue and a scooter to the left.
Walk up to the statue and press T when you are near it. 
The statue will show the following instructions:
======
Some basic interaction controls:
-Press Ctrl to align yourself onto the scooter
-Press F to mount/dismount the scooter
-Press Q and E to change the camera angle
-Press T to interact with objects (buzz statue + food bus)
-Press Z to attack students

You can pick up power ups around the map or from food trucks to help you with heists!
- Green: Charge Scooter Battery
- Red: Speed Boost
- Blue: Add More Time

Make sure you don't bump into the students! If you do, they will kidnap you if 
you aren't on a scooter. If you run into one while riding a scooter, you will also 
lose 20% of your battery and fall off your scooter.

Good Luck!
======
Press T again to close the instructions.
Go near the scooter to get on it.
Ctrl aligns the cat onto the scooter, but doesn't actually attach it yet.
You need to then press F to mount the scooter.
You don't need to press ctrl, it's just to make it easier to fulfill the conditions to mount.
Once you are on, you can ride around and explore the map.
You have a total of 4 minutes to begin with in order to collect all the T's.
As noted earlier, you can use powerups to add more time to your remaining time.
If you run out of time, you will lose.

TECHNOLOGY REQUIREMENTS

Clearly defined, achievable, objective/goal? (E.g. player can complete, or alternatively fail at, a level. NOT a sandbox)
- Goal is to collect all the T’s within time limit
Communication of success or failure to player!
- You win screen, game over screen, sfx when you collect T’s
Start menu to support Starting Action?
- Start menu -> instructions -> gameplay
Able to reset and replay on success or failure (e.g. In Minecraft when “You died”, there is a “respawn” button)
- Restart & Menu buttons

Character control is a predominant part of gameplay and not simply a way to traverse between non-“Game Feel” interactions. (e.g. walking between different point-and-click puzzles.)
- You need to control a character (the cat) to get to the T’s to win the game.
Utilize a character/vehicle controlled by the player with engaging animations that react to the player’s inputs.
- Cat is able to ride a scooter and you can clearly see it mount the scooter.
Auditory feedback on character state and actions (e.g. footsteps, landing, collisions, tire squeal, engine revs, etc.)
- Cat footsteps when you walk on the ground
- Collisions with various objects like chairs and trucks
Both graphically and auditorily represented physical interactions
- Collection of T triggers sound and confetti
- Auditory and visual feedback when colliding with chairs, collecting powerups, crashing into food trucks, attacking NPCs
A variety of environmental physical interactions possibly including:
Interactive scripted objects (buttons that open doors, pressure plates, jump pads, computer terminals, etc.)
- food trucks
Simulated Newtonian physics rigid body objects (crates, boulders, detritus, etc.)
- chairs collision
Animated objects using Mecanim (can be used for non-humanoid animations too!), programmatic pose changes, kinematic rigid body settings, etc. (moving platforms, machinery gears, gun turrets, etc.)
- eagles flying around in the sky using mechanim
State changing or destroyable objects (glass pane that shatters, boulder that breaks into bits, bomb that blows up, etc.)
- shatter glass pane (upon collision change texture of the glass window on the trucks)







iii. Known problem areas

iv. Manifest of which files authored by each teammate:
1. Detail who on the team did what
2. For each team member, list each asset implemented.
3. Make sure to list C# script files individually so we can confirm
each team member contributed to code writing