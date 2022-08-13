# MonkeTunes
A custom music loader for Gorilla Tag that uses unity web requests to grab music from a folder. </br>
(The mod does not come with any music and does not distribute any copyrighted material)

**REQUIRES TMP LOADER https://github.com/AHauntedArmy/TMPLoader**

<details>
<summary> <b> Use: </b> </summary>
To use this mod, add folders with the names of the playlists you want to make into the music folder - then place the songs you want into those playlists (must be .ogg files). Then go in game and use Computer Interface or the custom Monke Tunes Computer to control if it's playing.

I should add that pressing option 1 on the music list will toggle playing and the arrows on the music computer are to switch the play mode.
</details>

<details>
<summary> <b> Locations: </b> </summary> </br>

**Forest:** </br>
![Location_Forest](https://user-images.githubusercontent.com/40333513/175998997-31ed5de9-ff62-47ef-8079-fd9e1275020f.jpg)

**Cave:** </br>
![Location_Cave](https://user-images.githubusercontent.com/40333513/175999164-36d4ad9b-224e-4dcd-95ba-addc68f12a64.jpg)

**Canyon:** </br>
![Location_Canyon](https://user-images.githubusercontent.com/40333513/175999261-15bfc414-08c5-4f62-835c-1da2cb7412d0.jpg)

**City:** </br>
![Location_City](https://user-images.githubusercontent.com/40333513/175999269-1fc71442-2ed9-495f-b57b-e4501485bf9b.jpg)

**Mountain:** </br>
![Location_Mountain](https://user-images.githubusercontent.com/40333513/175999287-7ecdffcc-8e2b-4c5d-9eb2-71e96c4ef62c.jpg)

</details>

<details>
<summary> <b> Config: </b> </summary>
Settings for the mod can be found in BepInEx/config/MonkeTunes.cfg which will be generated the first time you play with the mod installed. At some point this may become a json file if I add certain settings as they may be easier to handle in json rather than in a BepInEx config. </br> </br>

**Music Settings** </br>
Initial Volume = What the volume of the music will start off as. </br>
Initial Mode = What mode the computer will start with. </br>
Play On Start = Wether or not the music starts playing when it is loaded or not. </br>

**Computer Settings** </br>
Computer Colour = Colour of the computer casing. </br>
Screen Colour = Colour of the screen on the computer. </br>
Button Colour = Colour of the buttons when not pressed. </br>
Pressed Colour = Colour of the buttons when pressed. </br>
Text Colour = Colour of the text on the screen.

![image](https://user-images.githubusercontent.com/40333513/177007876-3a543f8f-7cc9-4fc2-bb84-01f5210ec32e.png)

</details>

<details>
<summary> <b> Custom Maps: </b> </summary>
To make your map support the music computer - add a GameObject to the scene with the name "MT_CustomMapLocation" - for best results use the <a href="https://github.com/Frogrilla/MonkeTunes/blob/main/MT_Computer.obj">model provided</a> in this repo. You should also know that the computer is parented to the location in your map so if you want it to be animated go for it.

![MonkeTunes_CustomMaps](https://user-images.githubusercontent.com/40333513/176285676-3dbc63da-7142-47f4-b04a-9192b8fde36b.gif)

</details>
