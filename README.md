d360
====

Xbox 360 Controller application for Diablo 3 (Windows)

Prerequisites:

Microsoft .NET Framework 4.5
http://www.microsoft.com/en-au/download/details.aspx?id=30653

Microsoft XNA Framework (At least 4.0)
http://www.microsoft.com/en-au/download/details.aspx?id=20914

===============================

D360 is an attempt at a useable method of controlling Diablo 3 via an Xbox 360 controller on Windows. While I don't think it'll replace keyboard/mouse for everyday play, I hope it'll be useful enough to allow users to drop a Torment level or two and run some bounties or similar from their couch/stationary bike/bed. This is a very early beta - things work on my development machine, but are not necessarily going to work everywhere. Issues/suggestions/crash reports are welcome at d360project@gmail.com. I expect to wake up to a multitude of people on the internet angry that this (very, VERY early) software doesn't work perfectly. :D

The software will only work with Diablo 3 running in Windowed (Fullscreen) mode, on the left monitor. I'll add in the ability to modify that at a later date.

Quit the software by ALT+Tab'ing to D360, then hitting ALT+F4.

***NEW IN VERSION 0.2***
This version contains a small amount of configuration / binding UI. the defaults are as stated below, but upon first startup, you will be presented with the configuration dialogs. The 'Bindings' UI allows the user to specify which keys are bound to what functions within Diablo 3. The 'Configuration' UI is a start at allowing for custom behaviour - in this case, it allows the user to specify which game functions should be assigned to the left and right triggers on the controller. This should assist in the case that the user has positional/targeted abilities on the 1-4 keys, thus making it difficult to use the right stick for targeting while using the face buttons for abilities. 

***NEW IN VERSION 0.2.1***
Works without desktop composition in Windows 7. No HUD in that mode, though.

***NEW IN VERSION 0.2.2***
Updated D3 key bindings and trigger bindings are actually applied when you hit 'Save and Close'. My bad.

The default expected Diablo 3 bindings are as follows:

Inventory :					I
Map : 						Tab
Force Stand Still : 		SHIFT
Primary Skill : 			Left Mouse Button
Secondary Skill :			Right Mouse Button
Action Bar Skill 1 : 		1
Action Bar Skill 2 : 		2
Action Bar Skill 3 : 		3
Action Bar Skill 4 : 		4
Potion : 					Q
Town Portal : 				T
Force Move : 				SPACE
Game Menu : 				ESCAPE

When the Bindings UI is up, double-click on the binding to edit it, then press the keyboard key associated with the action in Diablo 3. If your Action Bar Skill 1 is bound to Q in Diablo 3, double-click the 'D1' entry next to 'Action Bar Skill 1', then press 'Q'. Make sure to save.

When the Configuration UI is up, you can simply select from the dropdown what you want the trigger to do.


 There are two modes of input, Move and Pointer (displayed in the bottom-right corner of the screen in ugly green text).

CONTROLS
---------
Left Stick: 				Move character (Move Mode), Move mouse cursor (Pointer Mode)
Left Stick Click: 			Toggle between Move and Pointer mode
Right Stick: 				Move 'Target' cursor
Right Stick Click:			Loot nearby items (experimental - hold down to loot as you walk over items with left stick)
Left Bumper/Shoulder : 		Primary Skill (Cast at location of normal cursor, or 'Target' cursor if active)
Right Bumper/Shoulder : 	Secondary Skill (Cast at location of normal cursor, or 'Target' cursor if active)
Left Trigger : 				Whatever is bound, defaults to Action Bar Skill 1
Right Trigger : 			Whatever is bound, defaults to Action Bar Skill 2
X face button : 			Action Bar Skill 1 (Cast at location of normal cursor, or 'Target' cursor if active)
A face button : 			Action Bar Skill 2 (Cast at location of normal cursor, or 'Target' cursor if active)
Y face button : 			Action Bar Skill 3 (Cast at location of normal cursor, or 'Target' cursor if active)
B face button : 			Action Bar Skill 4 (Cast at location of normal cursor, or 'Target' cursor if active)
DPad Up : 					Potion
DPad Down : 				Inventory (Also toggles to Pointer mode)
DPad Left : 				Map 
DPad Right : 				Town Portal
Back : 						World Map (Also toggles to Pointer mode)
Start : 					Game Menu (simulates ESCAPE, so can also close on-screen dialogs, skip cutscenes etc)

Show/Hide configuration UI : Ctrl + F10
Show/Hide Diablo 3 Bindings UI: CTRL + F11

Terminate D360 : CTRL+F12


A few ideas of things I'd like to improve:

* Inventory management. Swapping out for Nemesis Bracers is a pain - I think it should be possible to add a new control mode specifically for inventory/stash interaction. In this mode, the d-pad would snap the mouse position to different inventory/stash grid positions, allowing a three-or-four click method for swapping gear quickly.

* Full configuration of bindings. Kinda involved and difficult to get right. Looking for feedback on this.


