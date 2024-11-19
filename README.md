# FmodTest
**This implementation is not the best approach and is very hacky!** Please take this project with a grain of salt and it may have some unintended bugs here and there... I wrote it all in 3 days, whilst very confused and scared...***
OpenAL does still run!

Feel free to use this as a start point to a propper implementation into your project.

I will be attempting a better implementation in the future
 -----
 # Basic info
 The way this is build is the primarily be programming in C# and to use the `AudioManagerWrapper.cs` as if it is the main Audio Manager
 ## AudioEngine.h && AudioEngine.cpp
 This is what directly interacts with the FMOD Studio and Core API. Everything is bundles into nice methods that are exposed to the C# Scripting via `API_FUNCTION()`.
 
 **This should NOT be attached to any actor, dispite its ability to do so (REASON: I have found some strange initialisation issue of it running methods such as `onAwake() onEnable() onStart()` when the project loads up / scene is reloaded)**

 ### AudioManagerWrapper.cs
 This is the C# wrapper created to be able to call FMOD functions that are defined within `AudioEngne.h` && `AudioEngine.cpp` to allow the use of FMOD in C# projects

 ### AudioSource
 This is a script that allows you to play 2D and 3D sounds / event that are located within your FMOD `.bank` files.
 * **Event GUID:** The string GUID provided through FMOD studio for the event.
 * **Bank Name:** The name of the bank that the event belongs too (Will also allow for loading said bank if it hasnt been loaded already)
 * **Play On Start:** Should the audio play when `onStart` runs

### Audio Listener
Allows for the detection of sound, expecially in 3D space.

### Bank Paths
As of right now, `.bank` files are loaded via file URL. `BankPaths.cs` dynamically changes the search path for said `.bank` files depending on if its in engine or not.

**!!UPON BUILDING THE PROJECT YOU HAVE TO MANUALLY PLACE THE NEEDED `.bank` FILES INSIDE THE CORRECT OUTPUT FOLDER FOR BUILT PROJECTS --> FLAX DOES NOT SUPPORT COPYING ASSETS AS A POST BUILD COMMAND YET!!**

# Extra / Final
More detail on system can be found with in the code comments

**AS I MENTIONED, IT IS A VERY HACKY SYSTEM BUT IT WILL WORK FOR THE TIME BEING. FEEL FREE TO CONTRIBUTE TO THIS REPO AND BUILD THIS INTO A VERY NICE TEMPLATE PROJECT**
