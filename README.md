# turtle_challenge
My solution for the turtle challenge task:

https://s3-eu-west-1.amazonaws.com/lgc-public/TurtleChallenge.pdf

The application can work as a console stand alone app where the turtle can move and chose any orientatation in the grid, apps will ask first the number of columns and rows we intend to use to build the grid and the number of mines, it will generate the grid, place randomnly all the mines and exit and also place the Turtle in a random initial (free) cell with a random orientation.

The application also work with json files (passed as arguments, first one is the grid information file and the second one is the sequence of moves file). The sequence of moves consist of list of rotation command (90 degree right) and move forward only command (as per challenge requirements).

The example files and structure can be found in the "files" project folder:
- GameGrid.json (game grid information, columns and rows definition, mine positions initial turtle position and orientation, exit position)
- TurtleSequence.json --> test list of moves the turtle does not find the exit but it doesn't hit the mines either
- TurtleSequenceLose.json --> the turtle hit a mine after a sequence of forward moves and 90 rotations
- TurtleSequenceWin.json --> the turtle hit the exit after a sequence of forward moves and 90 rotations


