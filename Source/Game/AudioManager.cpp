#include "AudioManager.h"
#include "Engine/Debug/DebugLog.h"

AudioManager::AudioManager(const SpawnParams& params)
    : Script(params)
{
    // Enable ticking OnUpdate function
    _tickUpdate = true;
}

void AudioManager::Initialize()
{
    FMOD_RESULT result = FMOD::Studio::System::create(&_StudioSystem);

    if (result != FMOD_OK)
    {
        DebugLog::LogError(TEXT("Failed to create FMOD Studio system"));
        return;
    }

    DebugLog::Log(TEXT("Success to create FMOD Studio system"));

    result = _StudioSystem->getCoreSystem(&_CoreSystem);

    if (result != FMOD_OK)
    {
        DebugLog::LogError(TEXT("Failed to create FMOD Core system"));
        return;
    }

    DebugLog::Log(TEXT("Success to create FMOD Core system"));

    result = _StudioSystem->initialize(512, FMOD_STUDIO_INIT_NORMAL, FMOD_INIT_NORMAL, nullptr);

    if (result != FMOD_OK)
    {
        DebugLog::LogError(TEXT("Failed to initialise FMOD Core and Studio"));
        return;
    }

    DebugLog::Log(TEXT("FMOD Studio and core systems initialized successfully"));
}

void AudioManager::Update()
{
    if (!_StudioSystem)
    {
        return;
    }

    _StudioSystem->update();
}

void AudioManager::Shutdown()
{
    if (!_StudioSystem)
    {
        return;
    }

    FMOD_RESULT result = _StudioSystem->release();

    if (result != FMOD_OK)
    {
        DebugLog::LogError(String::Format(TEXT("Failed to release FMOD Studio system -- error number: {0}"), result));
    }

    DebugLog::Log(TEXT("FMOD studio system shut down"));
}

bool AudioManager::LoadBank(const StringAnsi& bankPath, int flags)
{
    FMOD::Studio::Bank* bank = nullptr;
    FMOD_RESULT result = _StudioSystem->loadBankFile(bankPath.GetText(), static_cast<FMOD_STUDIO_LOAD_BANK_FLAGS>(flags), &bank);

    if (result == FMOD_OK)
    {
        _LoadedBanks[bankPath.GetText()] = bank;
        return true;
    }

    FMOD_ErrorCheck(result);
    return false;
}

bool AudioManager::IsBankLoaded(const StringAnsi& bankPath)
{
    auto bank = _LoadedBanks.find(bankPath.GetText());

    if (bank != _LoadedBanks.end())
    {
        // Bank found in the map, now check if it's actually loaded
        FMOD::Studio::Bank* fmodBank = bank->second;
        FMOD_STUDIO_LOADING_STATE state;
        FMOD_RESULT result = fmodBank->getLoadingState(&state);

        if (result == FMOD_OK && state == FMOD_STUDIO_LOADING_STATE_LOADED)
        {
            return true;
        }
    }

    return false;
}

void AudioManager::UnloadBank(const StringAnsi& bankPath)
{
    auto bank = _LoadedBanks.find(bankPath.GetText());
    if (bank != _LoadedBanks.end())
    {
        bank->second->unload();
        _LoadedBanks.erase(bank);
    }
}

void* AudioManager::CreateEventInstance(const StringAnsi& eventPath)
{
    FMOD::Studio::EventDescription* eventDescription = nullptr;
    FMOD_RESULT result = _StudioSystem->getEvent(eventPath.GetText(), &eventDescription);

    if (result != FMOD_OK)
    {
        FMOD_ErrorCheck(result);
        return nullptr;
    }

    FMOD::Studio::EventInstance* eventInstance = nullptr;
    result = eventDescription->createInstance(&eventInstance);

    if (result != FMOD_OK)
    {
        FMOD_ErrorCheck(result);
        return nullptr;
    }

    return eventInstance;
}

void AudioManager::ReleaseEventInstance(void* eventInstance)
{
    if (eventInstance)
    {
        return;
    }

    FMOD::Studio::EventInstance* instance = static_cast<FMOD::Studio::EventInstance*>(eventInstance);

    // First, stop the event if it's still playing
    FMOD_RESULT result = instance->stop(FMOD_STUDIO_STOP_IMMEDIATE);
    FMOD_ErrorCheck(result);

    // Then release the instance
    result = instance->release();
    FMOD_ErrorCheck(result);
}

void AudioManager::PlayEvent(void* eventInstance)
{
    if (!eventInstance)
    {
        return;
    }

    static_cast<FMOD::Studio::EventInstance*>(eventInstance)->start();
}

bool AudioManager::IsEventPlaying(void* eventInstance)
{
    FMOD_STUDIO_PLAYBACK_STATE state;
    static_cast<FMOD::Studio::EventInstance*>(eventInstance)->getPlaybackState(&state);

    if (state == FMOD_STUDIO_PLAYBACK_PLAYING)
    {
        return true;
    }

    return false;
}

void AudioManager::StopEvent(void* eventInstance, int stopMode)
{
    if (!eventInstance)
    {
        return;
    }

    static_cast<FMOD::Studio::EventInstance*>(eventInstance)->stop(static_cast<FMOD_STUDIO_STOP_MODE>(stopMode));
}

float AudioManager::GetEventMinDistance(void* eventInstance)
{
    float minDistance;
    static_cast<FMOD::Studio::EventInstance*>(eventInstance)->getMinMaxDistance(&minDistance, nullptr);
    return minDistance;
}

float AudioManager::GetEventMaxDistance(void* eventInstance)
{
    float maxDistance;
    static_cast<FMOD::Studio::EventInstance*>(eventInstance)->getMinMaxDistance(nullptr, &maxDistance);
    return maxDistance;
}



void AudioManager::SetEventParameter(void* eventInstance, const StringAnsi& parameterName, float value)
{
    if (!eventInstance)
    {
        return;
    }

    static_cast<FMOD::Studio::EventInstance*>(eventInstance)->setParameterByName(parameterName.GetText(), value);
}

void AudioManager::Set3DListenerAttributes(const Vector3& position, const Vector3& velocity, const Vector3& forward, const Vector3& up)
{
    FMOD_3D_ATTRIBUTES attributes;
    attributes.position = { position.X, position.Y, position.Z };
    attributes.velocity = { velocity.X, velocity.Y, velocity.Z };
    attributes.forward = { forward.X, forward.Y, forward.Z };
    attributes.up = { up.X, up.Y, up.Z };

    _StudioSystem->setListenerAttributes(0, &attributes);
}

void AudioManager::Set3DEventAttributes(void* eventInstance, const Vector3& position, const Vector3& velocity)
{
    if (!eventInstance)
    {
        return;
    }

    FMOD_3D_ATTRIBUTES attributes;
    attributes.position = { position.X, position.Y, position.Z };
    attributes.velocity = { velocity.X, velocity.Y, velocity.Z };
    attributes.forward = { 0, 0, 1 };
    attributes.up = { 0, 1, 0 };

    static_cast<FMOD::Studio::EventInstance*>(eventInstance)->set3DAttributes(&attributes);
}

void AudioManager::FMOD_ErrorCheck(FMOD_RESULT result)
{
    if (result == FMOD_OK)
    {
        return;
    }

    DebugLog::LogError(String::Format(TEXT("FMOD Error: {0}"), (int)result));
}

