# Rogue-lite-project

Important things : this repo has 2 scenes, one that does the procedural generation and the other one is the player

**Procedural Generation :**
- for the procedural generation go to ProceduralMap and lunch the scene, you will see the generation, then go on the room manager and click on the Destroy button, then just Init.
if you want to see the generation piece by piece stop the scene click Init 1 time (otherwise it will not destroy everything) then click generate and here you go. Of course once finished just Destoy and do it again if you desire.

**PlayerTest :**
- for the player scene go to PlayTestPlayer where you can manipulate this small player without sprite (it's still in testing mode) in a small room, move with WASD/ and left mouse click to shoot. The gun is automatically looking at the mouse indicator but bullets are bugged so if you want to see them just divide your screen in half one for the Game and the other one for the Scene, you'll see small black points moving.

**My opinion on the generation :**
Conserning the genration i thing this is what i want from the start, i just need to find a way to put the player in there and to make things look better (like adding sprites and stuff). I also am able to know wich room is the first one because it is generated differantly than the rest (wich is why you need a init button) and i can change my script to be able to know wich one is the last to place the camera in the right room and give the info about the last room (would be a boss) or just to know if the player has done all rooms. All rooms have something that is called an "index" in my script so i can pretty much identify all rooms for the camera, this still needs to be tested but it is looking really good!
