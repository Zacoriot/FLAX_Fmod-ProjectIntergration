using FlaxEngine;
using System;

namespace Game;

/// <summary>
/// AudioManagerWrapper Script.
/// </summary>
public class AudioManagerWrapper : Script
{
    public static AudioManagerWrapper Instance;
    AudioManager _AudioManager;

    public override void OnAwake()
    {
        if(Instance != null)
        {
            Destroy(Actor);
            return;
        }
        Instance = this;


        _AudioManager = new AudioManager();
        _AudioManager.Initialize();
    }

    public override void OnUpdate()
    {
        _AudioManager.Update();
    }

    public override void OnDisable()
    {
        _AudioManager.Shutdown();
    }

    public bool LoadBank(string bankPath, int flags = 0)
    {
        return _AudioManager.LoadBank(bankPath, flags);
    }

    public bool IsBankLoaded(string bankPath)
    {
        return _AudioManager.IsBankLoaded(bankPath);
    }

    public void UnloadBank(string bankPath)
    {
        _AudioManager.UnloadBank(bankPath);
    }

    public IntPtr CreateEventInstance(string eventGUID)
    {
        return _AudioManager.CreateEventInstance(eventGUID);
    }

    public void ReleaseEventInstance(IntPtr eventInstance)
    {
        _AudioManager.ReleaseEventInstance(eventInstance);
    }

    public void PlayEvent(IntPtr eventInstance)
    {
        _AudioManager.PlayEvent(eventInstance);
    }

    public bool IsEventPlaying(IntPtr eventInstance)
    {
        return _AudioManager.IsEventPlaying(eventInstance);
    }

    public void StopEvent(IntPtr eventInstance, int stopMode = 0)
    {
        _AudioManager.StopEvent(eventInstance, stopMode);
    }

    public float GetMinDistance(IntPtr eventInstance)
    {
        return _AudioManager.GetEventMinDistance(eventInstance);
    }

    public float GetMaxDistance(IntPtr eventInstance)
    {
        return _AudioManager.GetEventMaxDistance(eventInstance);
    }

    public void SetEventParameter(IntPtr eventInstance, string parameterName, float value)
    {
        _AudioManager.SetEventParameter(eventInstance, parameterName, value);
    }

    public void Set3DListenerAttributes(Vector3 position, Vector3 velocity, Vector3 forward, Vector3 up)
    {
        _AudioManager.Set3DListenerAttributes(position, velocity, forward, up);
    }

    public void Set3DEventAttributes(IntPtr eventInstance, Vector3 position, Vector3 velocity)
    {
        _AudioManager.Set3DEventAttributes(eventInstance, position, velocity);
    }
}
