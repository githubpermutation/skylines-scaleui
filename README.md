# skylines-scaleui
ScaleUI mod for Cities: Skylines

This mod adds buttons to scale / resize the complete user interface in Cities: Skylines.

## Usage
Activate the mod, load or start a new game (editors not yet supported/tested), then click the new buttons in the top right corner to change the scale.

To reset the scale to default values, hit Ctrl+0.

## Known issues
This method only works to a certain scale, after which elements will overlap or be out of screen. In that case, either reset the scale or hit the (-) button to decrease it a step. If a reset doesn't work, quicksave your game with F1 and exit with Alt+F4.

The scaling value is not saved permanently, only when you load a savegame while in a city.

If you use other mods which add UI elements, this mod will probably reposition them off-screen.

Please report any issues you find.

## Notice on building the project
I set up MonoDevelop to automatically delete and copy the resulting .dll using Pre-/After-Build commands. It uses deldll.cmd to achieve, which will **delete** a file, so be careful. Additionally, the project references the assemblies on my local hard drive.

## Attributions 

Inspired by TextScaleMod (http://steamcommunity.com/sharedfiles/filedetails/?id=407225523) 
Thanks to nlight for help with Reflection.
