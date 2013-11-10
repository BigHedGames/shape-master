===============================================================================
User Notes:

Nothing to see here at the moment...


===============================================================================
Developer Notes:

My thoughts (Dustin) - Need to at least cover the following:



1) Discuss the code repository plan (e.g. Git, SVN, Sharepoint)



2) Discuss a little bit about the design - my personal preference is to spend
as little time as possible on this first game, but in the future it should be
a big part of the process, so we definitely shouldn't skip it...

My preference is a Model-View-Controller approach:

Model Class: Holds access to all of our sprites, handles loading and unloading,
in Game 1 we would just instaniate this class in the constructor or LoadContent
method.

Controller Class: Handles all the game logic, updates the drawRectangles and
sourceRectangles of the sprites in the Model class (so the controller class
will need to know about the Model class e.g. as an argument).

View: Just draws the sprites... I'd make this into a separate class, but we can
just draw it in the Game1 class as well.

With this approach we would instantiate these classes in Game1, call methods
from these classes such as Controller.Update(Model) or View.Draw(Model).




3) Start the game! Here is a quick plan (don't need to do it this way):
	1. Load protagonist sprite and draw in the center of the screen.
	2. Give movement to the protagonist sprite, handling game screen edges.
	3. Give him the ability to shape shift.
	4. Add a randomly spawning baddie sprite (with a random shape) in a 
	   random place, making them move with constant velocity in a random
	   direction, (random time? or every N seconds?)
	5. Change their colors as the protagonist shifts.
	6. Add in a score counter and add in collision functionality.
	7. ???
	8. Profit!