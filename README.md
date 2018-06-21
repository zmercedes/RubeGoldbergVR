# Rube Goldberg Challenge! 
Zoilo Mercedes

 0. [x] 0. set up hand control scripts
 	- left hand activates arc on tp press that shows teleport point at the end
 	- right hand activates object menu for object placing
 	- added ControllerInput base class to reduce duplicate code across both hands
 1. [x] 1. add teleportation
 	- arc spawns from  left controller tip on touchpad press which connects to a target at the other end, denotes teleport target
 	- on removing finger from touchpad, play area will move to target position
 	- reworked arc renderer serveral times, now uses parabolic raycaster to determine time to target, then renders an arc.
 2. [x] 2. add object grabbing/throwing 
 	- can grab/throw ball on grip press/release.
 	- reworked grabbing/throwing using inheritance, various release actions defined
 3. [x] 3. create rube goldberg objects
 	- [x] - metal plank - plank with side rails, can't drop ball
 	- [x] - teleport target - place origin target, place destination target, moves ball from origin to destination
 	- [x] - trampoline - bounces ball -> created, works as intended
 	- [x] - wood plank - plank without side rails for harder difficulty
 4. [x] 4. create object menu
 	- [x] - attaches to right hand, appears/closes on touchpad press
 	- [x] - shows object and the name/description of object near controller
 	- [x] - scroll through objects by pressing left or right on trackpad
 	- [x] - can instantiate object by pressing trigger
 5. [x] 5. set special grab(/release) rules for rube goldberg objects
 	- grab on grip press. on grip release, stays in place
 	- grab/release actions are now in the ControllerInput base class. 
 6. [x] 6. gameplay!
 	- [x] - create collectible that ball must touch in order to win
 	- [x] - reenable collectible on ball touching floor
 	- [x] - create goal that loads next level on ball hitting it after having collected all collectibles
 	- [x] - create anti cheating mechanics: the ball must be thrown from initial platform area in order to not trigger this mechanic
 	- [x] - can limit the number of objects that can be placed in a level, can vary per level
 7. [x] 7. final polish!
 	- [x] - set appropriate scale and physics on objects
 	- [x] - create UI
 	- [x] - raycast selector for UI
 	- [x] - no teleport zones added
 	- [x] - level progression
 	- [x] - add sound 	
 	- [x] - rework the target object to show pointer above target position
 	- [x] - 4 different levels
 	- [x] - make environment nice
 	- [x] - runs at 90fps
 	- [ ] - deleting objects __* -> Scrapped *__
 	- [ ] - highlight objects when they're within grab range __* -> Scrapped *__


# About this Project
This project took about 3-4 weeks worth of work to complete. I really enjoyed learning SteamVR and hooking up various functions to the controllers. I found a couple of things very challenging while creating this project; among these were figuring out how to best organize the code I was writing, and implementing the specific teleportation aim mechanic I desired. I developed and tested this environment using the HTC Vive on Unity version 2017.1.3, SteamVR version 1.2.3.