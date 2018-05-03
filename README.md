Rube Goldberg Challenge:

 0. [x] 0. set up hand control scripts
 	- left hand activates arc on tp press that shows teleport point at the end
 	- right hand activates object menu for object placing
 	- added ControllerInput base class to reduce duplicate code across both hands
 1. [x] 1. add teleportation
 	- arc erupts from controller tip which connects to a target at the other end, denotes teleport target
 	- reworked arc renderer serveral times, now uses parabolic raycaster to determine time to target, then renders an arc.
 2. [x] 2. add object grabbing/throwing 
 	- can grab/throw ball.
 	- reworked grabbing/throwing using inheritance, various release actions defined
 3. [ ] 3. create rube goldberg objects
 	- metal plank - plank with side rails, can't drop ball -> created, works
 	- teleport target - place origin target, place destination target, moves ball from origin to destination
 	- trampoline - bounces ball -> created, grabable, needs to bounce things
 	- wood plank - plank without side rails for harder difficulty -> created, works
 4. [ ] 4. create object menu -> _**added, needs work**_
 	- attaches to right hand, appears on touchpad press
 	- shows object and the name/description of object near controller, can instantiate by placing or grabbing
 5. [x] 5. set special grab(/release) rules for rube goldberg objects
 	- can grab but cannot throw. on release, stays in place.
 	- put grab/release actions in the ControllerInput base class. 
 6. [ ] 6. gameplay!
 	- create collectible that ball must touch in order to win
 	- reenable collectible on ball touching floor
 	- create goal that loads next level on ball hitting it after having collected all collectibles
 	- create anti cheating mechanics: the ball must be thrown from initial platform area in order to not trigger this mechanic
 	- 4 different levels
 	- can limit the number of objects that can be placed in a level, can vary per level
 7. [ ] 7. final polish!
 	- set appropriate scale on objects
 	- rework the aimer object to show pointer above target position
 	- highlight objects when they're within grab range
 	- deleting objects
 	- create instruction UI
 	- make environment nice
 	- runs at 90fps