## Enemy vision
Each x time interval, enemy following player will transition in DecisionState.  
On this state he will perform :

Determine :
- Facing angle : Vector in horizontal direction of target, it represent the walking direction of enemy
- Target angle : Vector in direction of target

For each vector :
- Determine a 30° angle centered on vector
- Raycast 6 rays in this angle from most horizontal to most vertical angle
- Check for Target, Wall, Pit

Take a decision according to these information