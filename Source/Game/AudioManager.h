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
    API_FUNCTION() void Initialize();
    API_FUNCTION() void Update();
    API_FUNCTION() void Shutdown();

    API_FUNCTION() bool LoadBank(const StringAnsi& bankPath, int flags = 0);
    API_FUNCTION() bool IsBankLoaded(const StringAnsi& bankPath);
    API_FUNCTION() void UnloadBank(const StringAnsi& bankPath);

    API_FUNCTION() void* CreateEventInstance(const StringAnsi& eventPath);
    API_FUNCTION() void ReleaseEventInstance(void* eventInstance);
    API_FUNCTION() void PlayEvent(void* eventInstance);
    API_FUNCTION() bool IsEventPlaying(void* eventInstance);
    API_FUNCTION() void StopEvent(void* eventInstance, int stopMode = 0);

    API_FUNCTION() float GetEventMinDistance(void* eventInstance);
    API_FUNCTION() float GetEventMaxDistance(void* eventInstance);


    API_FUNCTION() void SetEventParameter(void* eventInstance, const StringAnsi& parameterName, float value);

    API_FUNCTION() void Set3DListenerAttributes(const Vector3& position, const Vector3& velocity, const Vector3& forward, const Vector3& up);
    API_FUNCTION() void Set3DEventAttributes(void* eventInstance, const Vector3& position, const Vector3& velocity);

private:  
    void FMOD_ErrorCheck(FMOD_RESULT result);

    FMOD::Studio::System* _StudioSystem = nullptr;
    FMOD::System* _CoreSystem = nullptr;

    std::unordered_map<const char*, FMOD::Studio::Bank*> _LoadedBanks;
};
