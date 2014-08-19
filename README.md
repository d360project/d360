d360
====

Xbox 360 Controller application for Diablo 3 (Windows)


Prerequisites:

Microsoft .NET Framework 4.5
http://www.microsoft.com/en-au/download/details.aspx?id=30653

Microsoft XNA Framework (At least 4.0)
http://www.microsoft.com/en-au/download/details.aspx?id=30653

===============================

D360 is an attempt at a useable method of controlling Diablo 3 via an Xbox 360 controller on Windows. While I don't think it'll replace keyboard/mouse for everyday play, I hope it'll be useful enough to allow users to drop a Torment level or two and run some bounties or similar from their couch/stationary bike/bed. This is a very early beta - things work on my development machine, but are not necessarily going to work everywhere. Issues/suggestions/crash reports are welcome at d360project@gmail.com. I expect to wake up to a multitude of people on the internet angry that this (very, VERY early) software doesn't work perfectly. :D

The software will only work with Diablo 3 running in Windowed (Fullscreen) mode, on the left monitor. I'll add in the ability to modify that at a later date.

Quit the software by ALT+Tab'ing to D360, then hitting ALT+F4.

This version is without any configuration UI/Logic - the layout and bindings that worked during development are what you get, for now. I figured giving people something to play with sooner was more valuable.

My (relevant) Diablo 3 bindings are as follows:

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


D360 will (currently) only work with these as your bindings, as it emulates the keyboard/mouse. There are two modes of input, Move and Pointer (displayed in the bottom-right corner of the screen in ugly green text).

CONTROLS
---------
Left Stick: 				Move character (Move Mode), Move mouse cursor (Pointer Mode)
Left Stick Click: 			Toggle between Move and Pointer mode
Right Stick: 				Move 'Target' cursor
Right Stick Click:			Current does nothing, considering options. :)
Left Bumper/Shoulder : 		Primary Skill (Cast at location of normal cursor, or 'Target' cursor if active)
Right Bumper/Shoulder : 	Secondary Skill (Cast at location of normal cursor, or 'Target' cursor if active)
Left Trigger : 				Left Click (Used to interact, but will cast primary skill if over an enemy/breakable)
Right Trigger : 			Currently does nothing, considering options. :)
X face button : 			Action Bar Skill 1 (Cast at location of normal cursor, or 'Target' cursor if active)
A face button : 			Action Bar Skill 2 (Cast at location of normal cursor, or 'Target' cursor if active)
Y face button : 			Action Bar Skill 3 (Cast at location of normal cursor, or 'Target' cursor if active)
B face button : 			Action Bar Skill 4 (Cast at location of normal cursor, or 'Target' cursor if active)
DPad Up : 					Potion
DPad Down : 				Inventory (Also toggles to Pointer mode)
DPad Left : 				Map (Also toggles to Pointer mode)
DPad Right : 				Town Portal
Back : 						Map
Start : 					Game Menu (simulates ESCAPE, so can also close on-screen dialogs, skip cutscenes etc)

A few ideas of things I'd like to improve:

* Looting from the ground is probably the most annoying part of current use - depending on the ToS, it may be permissable to bind an unused trigger/button to perform a number of clicks in the character's immediate vicinity, picking up everything close-by. I'll think about it some more, but suggestions are always welcome. :)

* Also annoying is inventory management. Swapping out for Nemesis Bracers is a pain - I think it should be possible to add a new control mode specifically for inventory/stash interaction. In this mode, the d-pad would snap the mouse position to different inventory/stash grid positions, allowing a three-or-four click method for swapping gear quickly.
