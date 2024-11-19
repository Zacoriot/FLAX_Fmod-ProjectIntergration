using FlaxEngine;
using System;

namespace Game;

/// <summary>
/// SoundSource Script.
/// </summary>
public class SoundSource : Script
{
    AudioManagerWrapper _AudioManager;
    IntPtr _EventInstance; //The audio event
    [ShowInEditor, Serialize] string _EventGUID;
    [ShowInEditor, Serialize] string _BankName;
    [ShowInEditor, Serialize] bool _PlayOnStart;


    public override void OnStart()
    {
        //Audio Manager referance
        _AudioManager = AudioManagerWrapper.Instance;

        string bankPath = BankPaths.Path + _BankName;

        if(!_AudioManager.IsBankLoaded(bankPath)) //Is bank loaded?
        {
            _AudioManager.LoadBank(bankPath); // Load said bank
            Debug.Log($"{_BankName} has been loaded!");
        }

        CreateEventInstance(); //Create event instance method run

        if(_PlayOnStart) // Should play on start?
        {
            Play();
        }
    }

    private void CreateEventInstance()
    {
        if (string.IsNullOrEmpty(_EventGUID)) //Has GUID been provided
        {
            Debug.LogWarning("Event path is not set");
            return;
        }
        _EventInstance = _AudioManager.CreateEventInstance(_EventGUID); //Create instance based on GUID
    }

    public void Play()
    {
        if (_EventInstance != IntPtr.Zero)
        {
            _AudioManager.PlayEvent(_EventInstance); //Play
        }
    }

    public bool IsPlaying()
    {
        if(_EventInstance != IntPtr.Zero)
        {
            return _AudioManager.IsEventPlaying(_EventInstance); //Is playing?
        }

        return false;
    }

    public void Stop(int stopMode = 0)
    {
        if (_EventInstance != IntPtr.Zero)
        {
            _AudioManager.StopEvent(_EventInstance, stopMode); //Stop
        }
    }

    public void SetParameter(string parameterName, float value)
    {
        if (_EventInstance != IntPtr.Zero)
        {
            _AudioManager.SetEventParameter(_EventInstance, parameterName, value); //Change perameter values
        }
    }

    public float GetMinDistance()
    {
        if (_EventInstance != IntPtr.Zero)
        {
            return _AudioManager.GetMinDistance(_EventInstance); //Get min distance
        }

        return 0;
    }

    public float GetMaxDistance()
    {
        if (_EventInstance != IntPtr.Zero)
        {
            return _AudioManager.GetMaxDistance(_EventInstance); //Get max distance
        }

        return 0;
    }

    public override void OnUpdate()
    {
        if (_EventInstance != IntPtr.Zero)
        {
            _AudioManager.Set3DEventAttributes(_EventInstance, Actor.Position, Vector3.Zero); // set 3D properties
        }
    }

    public override void OnDisable()
    {
        if (_EventInstance != IntPtr.Zero)
        {
            _AudioManager.ReleaseEventInstance(_EventInstance); //Unload event
            _EventInstance = IntPtr.Zero;
        }
    }
}