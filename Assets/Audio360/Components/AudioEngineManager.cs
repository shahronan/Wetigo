/*
 *  Copyright 2013-Present Two Big Ears Limited
 *  All rights reserved.
 *  http://twobigears.com
 */

using System.Collections;
using UnityEngine;

namespace TBE
{
    /// <summary>
    /// A singleton that is automtically constructed when the audio engine is in use.
    /// It manages the audio engine and all its child objects.
    /// Its lifecycle is tied to the Unity application.
    /// </summary>
    public class AudioEngineManager : Singleton<AudioEngineManager>, IObject
    {
        public AudioEngine nativeEngine { get; private set; }
        private AudioListener listener_ = null;

        void Awake()
        {
            onInit();
        }

        void Update()
        {
            if (listener_ == null)
            {
                findAudioListener();
            }

            if (nativeEngine != null && listener_ != null)
            {
                Transform t = listener_.transform;
                nativeEngine.setListenerPosition(Utils.toTBVector(t.position));
                nativeEngine.setListenerRotation(Utils.toTBVector(t.forward), Utils.toTBVector(t.up));
            }
        }

        // Called by Singleton<>
        public void onInit()
        {
            if (nativeEngine != null || AudioEngineSettings.Instance == null)
            {
                return;
            }

            findAudioListener();

            EngineInitSettings settings = new EngineInitSettings();
            settings.memorySettings.spatQueueSizePerChannel = 4096 * 4;
            settings.audioSettings.sampleRate = Utils.toSampleRateFloat(AudioEngineSettings.Instance.sampleRate);

            // Use the audio output from OVRManager. 
            // This requires the Oculus Utilities package to be imported in your project.
#if AUDIO360_USE_OVR_AUDIO_OUT
            string ovrAudioGuid = OVRManager.audioOutId;
            if (!string.IsNullOrEmpty(ovrAudioGuid))
            {
                string deviceName = AudioEngine.getAudioDeviceNameFromId(ovrAudioGuid);
                if (!string.IsNullOrEmpty(deviceName))
                {
                    AudioEngineSettings.Instance.audioDevice = AudioDeviceType.CUSTOM;
                    AudioEngineSettings.Instance.customAudioDevice = deviceName;
                    Utils.log("Using audio device from OVRManager: " + deviceName, this);
                }
            }
#endif
            settings.audioSettings.deviceType = AudioEngineSettings.Instance.audioDevice;
            if (settings.audioSettings.deviceType == AudioDeviceType.CUSTOM)
            {
                settings.audioSettings.customAudioDeviceName = AudioEngineSettings.Instance.customAudioDevice;
            }
            settings.memorySettings.audioObjectPoolSize = AudioEngineSettings.Instance.objectPoolSize;
            settings.memorySettings.spatDecoderFilePoolSize = AudioEngineSettings.Instance.objectPoolSize;
            settings.memorySettings.spatDecoderQueuePoolSize = 0;

            nativeEngine = null;
            nativeEngine = AudioEngine.create(settings);
            if (nativeEngine != null)
            {
                Utils.log("Initialised Engine. " + nativeEngine.getSampleRate() + " " + nativeEngine.getBufferSize() +
                    ". v" + nativeEngine.getVersionMajor() + "." + nativeEngine.getVersionMinor() + "." + nativeEngine.getVersionPatch(), this);
                nativeEngine.start();
            }
            else
            {
                Utils.logError("Failed to initialise engine", this);
            }
            settings = null;
        }

        public bool mustNotDestroyOnLoad()
        {
            return true;
        }

        public void onTerminate()
        {
            if (nativeEngine != null)
            {
                nativeEngine.Dispose();
                nativeEngine = null;
            }
        }

        void OnApplicationPause(bool pauseStatus)
        {
            if (nativeEngine != null)
            {
                if (pauseStatus)
                {
                    nativeEngine.suspend();
                }
                else
                {
                    nativeEngine.start();
                }
            }
        }

        private void findAudioListener()
        {
            var listeners = FindObjectsOfType<AudioListener>();
            if (listeners == null || listeners.Length < 1)
            {
                Utils.logError("Failed to find Unity's AudioListener. Audio will not be spatialised.", this);
                return;
            }
            listener_ = listeners[0];
        }
    }
}