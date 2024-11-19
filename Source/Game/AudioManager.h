#pragma once

#include "Engine/Scripting/Script.h"
#include "Engine/Core/Math/Vector3.h"

#include <fmod.hpp>
#include <Fmod/Studio/fmod_studio.hpp>

#include <unordered_map>
#include <string>

API_CLASS() class GAME_API AudioManager : public Script
{
API_AUTO_SERIALIZATION();
DECLARE_SCRIPTING_TYPE(AudioManager);

public:
    /* == LIFE TIME == */
    //Used to initialise FMOD Studio and Core
    API_FUNCTION() void Initialize();
    //Used to update Fmod::Studio::System
    API_FUNCTION() void Update();
    //Used to deallocate any loaded banks and systems
    API_FUNCTION() void Shutdown();

    /* == BANKS == */
    //Loads a bank via file URL with flags
    API_FUNCTION() bool LoadBank(const StringAnsi& bankPath, int flags = 0);
    //Check to see if a bank has already been loaded before
    API_FUNCTION() bool IsBankLoaded(const StringAnsi& bankPath);
    //Unloads a bank via file URL
    API_FUNCTION() void UnloadBank(const StringAnsi& bankPath);              

    /* == EVENTS == */
    //Create a new Fmod event provided by events GUID
    API_FUNCTION() void* CreateEventInstance(const StringAnsi& eventGUID); 
    //Releases an event via referance
    API_FUNCTION() void ReleaseEventInstance(void* eventInstance);
    //Plays an event via referance
    API_FUNCTION() void PlayEvent(void* eventInstance);
    //Returns true if the event is playing
    API_FUNCTION() bool IsEventPlaying(void* eventInstance);
    //Stop the event with flags
    API_FUNCTION() void StopEvent(void* eventInstance, int stopMode = 0);

    /* USEFUL? */
    //Gets the min attenuation distance
    API_FUNCTION() float GetEventMinDistance(void* eventInstance); 
    //Gets the max attenuation distance
    API_FUNCTION() float GetEventMaxDistance(void* eventInstance);

    /* == CONTROL == */
    //Edits a paremeter of an event
    API_FUNCTION() void SetEventParameter(void* eventInstance, const StringAnsi& parameterName, float value); 

    /* == 3D SPACE == */
    //Sets 3D listener properties
    API_FUNCTION() void Set3DListenerAttributes(const Vector3& position, const Vector3& velocity, const Vector3& forward, const Vector3& up);
    //Sets the properties for 3D events
    API_FUNCTION() void Set3DEventAttributes(void* eventInstance, const Vector3& position, const Vector3& velocity);

private:  
    //Returns an FMOD error
    void FMOD_ErrorCheck(FMOD_RESULT result); 

    //The studio system
    FMOD::Studio::System* _StudioSystem = nullptr; 
    //The core system
    FMOD::System* _CoreSystem = nullptr; 

    //All loaded banks
    std::unordered_map<const char*, FMOD::Studio::Bank*> _LoadedBanks;
};
