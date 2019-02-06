/*
 *  Copyright 2013-Present Two Big Ears Limited
 *  All rights reserved.
 *  http://twobigears.com
 */

using System.Collections;
using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace TBE
{
    /// <summary>
    /// Spatialise a sound in space
    /// </summary>
    public class AudioObject : MonoBehaviour
    {
        public string file = string.Empty;
        public bool playOnStart = false;
        public AudioObjectEventListener events = new AudioObjectEventListener();

        [SerializeField]
        float volume_ = 1.0f;
        [SerializeField]
        bool loop_ = false;
        [SerializeField]
        float pitch_ = 1.0f;
        [SerializeField]
        float minDistance_ = 1;
        [SerializeField]
        float maxDistance_ = 1000f;
        [SerializeField]
        float attenFactor_ = 1f;
        [SerializeField]
        AttenuationMode attenMode_;
        [SerializeField]
        bool directionality_ = false;
        [SerializeField]
        float directionalityLevel_ = 1.0f;
        [SerializeField]
        float directionalityConeArea_ = 150.0f;
        [SerializeField]
        bool spatialise_ = true;

        NativeAudioObject nativeObj_;
        AttenuationProps attenProps_ = new AttenuationProps();
        DirectionalProps directionalProps_ = new DirectionalProps();
        GCHandle thisHandle;

        [AOT.MonoPInvokeCallback(typeof(EventCallback))]
        static void eventCallback(Event e, global::System.IntPtr userData)
        {
            GCHandle gch = GCHandle.FromIntPtr(userData);
            AudioObject ob = (AudioObject)gch.Target;
            ob.events.onNewEvent(e, ob);
        }

        void Awake()
        {
            init();
            updateProps();
        }

        void Start()
        {
            if (nativeObj_ != null && file != null && file.Length > 0)
            {
                if (!open(file))
                {
                    Utils.logError("Failed to open " + file, this);
                    return;
                }

                if (playOnStart)
                {
                    nativeObj_.play();
                }
            }
        }

        private void init()
        {
            if (nativeObj_ != null)
            {
                return;
            }

            nativeObj_ = AudioEngineManager.Instance.nativeEngine.createAudioObject();
            if (nativeObj_ == null)
            {
                Utils.logError("Native audio object is invalid", this);
                return;
            }

            thisHandle = GCHandle.Alloc(this);
            nativeObj_.setEventCallback(eventCallback, GCHandle.ToIntPtr(thisHandle));
        }

        void Update()
        {
            if (nativeObj_ != null)
            {
                attenProps_.minimumDistance = minDistance;
                attenProps_.maximumDistance = maxDistance;
                attenProps_.factor = attenFactor;
                nativeObj_.setAttenuationProperties(attenProps_);
                directionalProps_.set(directionalityLevel_, directionalityConeArea_);
                nativeObj_.setDirectionalProperties(directionalProps_);
                nativeObj_.setRotation(Utils.toTBVector(transform.forward), Utils.toTBVector(transform.up));
                nativeObj_.setPosition(Utils.toTBVector(transform.position));
            }
        }

        public void updateProps()
        {
            if (nativeObj_ != null)
            {
                nativeObj_.setVolume(volume_, 0 /* default ramp */ );
                nativeObj_.enableLooping(loop_);
                nativeObj_.setPitch(pitch_);
                nativeObj_.setAttenuationMode(attenMode_);
                nativeObj_.setDirectionalityEnabled(directionality_);
                nativeObj_.shouldSpatialise(spatialise_);
            }
        }

        /// <summary>
        /// Opens an asset for playback. Currently .wav and .opus formats are supported. If no path is specified, 
        /// the asset will be loaded from Assets/StreamingAssets.
        /// While the asset is opened synchronously, it is loaded into the streaming buffer asynchronously. An
        /// event (Event.DECODER_INIT) will be dispatched to the event listener when the streaming buffer is ready for the
        /// asset to play.
        /// </summary>
        /// <param name="fileToplay">Name of the file in StreamAssets or the full path</param>
        /// <returns>true if the file was found and successfully opened</returns>
        public bool open(string fileToplay)
        {
            if (nativeObj_ != null)
            {
                if (nativeObj_.open(Utils.resolvePath(fileToplay, PathType.STREAMING_ASSETS)) == EngineError.OK)
                {
                    file = fileToplay;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Close an opened file.
        /// </summary>
        public void close()
        {
            if (nativeObj_ != null)
            {
                nativeObj_.close();
            }
        }

        /// <summary>
        /// Returns true if a file is open and ready.
        /// </summary>
        /// <returns>Returns true if a file is open and ready.</returns>
        public bool isOpen()
        {
            if (nativeObj_ != null)
            {
                return nativeObj_.isOpen();
            }
            return false;
        }

        /// <summary>
        /// Begin playback of an opened file.
        /// Any subsequent call to this function or any play function
        /// will disregard this event if it hasn't already been triggered.
        /// </summary>
        public void play()
        {
            if (nativeObj_ != null)
            {
                nativeObj_.play();
            }
        }

        /// <summary>
        /// Schedule playback x milliseconds from now.
        /// Any subsequent call to this function or any play function
        /// will disregard this event if it hasn't already been triggered.
        /// </summary>
        /// <param name="millisecondsFromNow">Time from now in milliseconds</param>
        public void playScheduled(float millisecondsFromNow)
        {
            if (nativeObj_ != null)
            {
                nativeObj_.playScheduled(millisecondsFromNow);
            }
        }

        /// <summary>
        /// Begin playback with a fade.
        /// Any subsequent call to this function or any play function
        /// will disregard this event if it hasn't already been triggered.
        /// </summary>
        /// <param name="fadeDurationInMs">Duration of the fade in milliseconds</param>
        public void playWithFade(float fadeDurationInMs)
        {
            if (nativeObj_ != null)
            {
                nativeObj_.playWithFade(fadeDurationInMs);
            }
        }

        /// <summary>
        /// Stop playback.
        /// Any subsequent call to this function or any stop function
        /// will disregard this event if it hasn't already been triggered.
        /// </summary>
        public void stop()
        {
            if (nativeObj_ != null)
            {
                nativeObj_.stop();
            }
        }

        /// <summary>
        /// Schedule to stop playback x milliseconds from now.
        /// Any subsequent call to this function or any stop function
        /// will disregard this event if it hasn't already been triggered.
        /// </summary>
        /// <param name="millisecondsFromNow">Time from now in milliseconds</param>
        public void stopScheduled(float millisecondsFromNow)
        {
            if (nativeObj_ != null)
            {
                nativeObj_.stopScheduled(millisecondsFromNow);
            }
        }

        /// <summary>
        /// Fadeout and stop playback.
        /// Any subsequent call to this function or any stop function
        /// will disregard this event if it hasn't already been triggered.
        /// </summary>
        /// <param name="fadeDurationInMs">Duration of the fade in milliseconds.</param>
        public void stopWithFade(float fadeDurationInMs)
        {
            if (nativeObj_ != null)
            {
                nativeObj_.stopWithFade(fadeDurationInMs);
            }
        }

        /// <summary>
        ///  Pause playback.
        /// Any subsequent call to this function or any pause function
        /// will disregard this event if it hasn't already been triggered.
        /// </summary>
        public void pause()
        {
            if (nativeObj_ != null)
            {
                nativeObj_.pause();
            }
        }

        /// <summary>
        /// Schedule playback to be paused x milliseconds from now.
        /// Any subsequent call to this function or any pause function
        /// will disregard this event if it hasn't already been triggered.
        /// </summary>
        /// <param name="millisecondsFromNow">Time from now in milliseconds</param>
        public void pauseScheduled(float millisecondsFromNow)
        {
            if (nativeObj_ != null)
            {
                nativeObj_.pauseScheduled(millisecondsFromNow);
            }
        }

        /// <summary>
        /// Fadeout and pause playback.
        /// Any subsequent call to this function or any pause function
        /// will disregard this event if it hasn't already been triggered.
        /// </summary>
        /// <param name="fadeDurationInMs">Duration of the fade in milliseconds.</param>
        public void pauseWithFade(float fadeDurationInMs)
        {
            if (nativeObj_ != null)
            {
                nativeObj_.pauseWithFade(fadeDurationInMs);
            }
        }

        /// <summary>
        /// Seek playback to an absolute point in milliseconds.
        /// </summary>
        /// <param name="ms">Time in milliseconds.</param>
        public void seekToMs(float ms)
        {
            if (nativeObj_ != null)
            {
                nativeObj_.seekToMs(ms);
            }
        }

        /// <summary>
        /// Returns elapsed playback time in milliseconds.
        /// </summary>
        /// <returns>Returns elapsed playback time in milliseconds.</returns>
        public double getElapsedTimeInMs()
        {
            if (nativeObj_ != null)
            {
                return nativeObj_.getElapsedTimeInMs();
            }
            return 0.0;
        }

        /// <summary>
        /// Returns the total duration of the asset in milliseconds.
        /// </summary>
        /// <returns>Returns the total duration of the asset in milliseconds.</returns>
        [Obsolete("getDurationInMs() is deprecated, please use getAssetDurationInMs().")]
        public double getDurationInMs()
        {
            if (nativeObj_ != null)
            {
                return nativeObj_.getAssetDurationInMs();
            }
            return 0.0;
        }

        /// <summary>
        /// Returns the total duration of the asset in milliseconds.
        /// </summary>
        /// <returns>Returns the total duration of the asset in milliseconds.</returns>
        public double getAssetDurationInMs()
        {
            if (nativeObj_ != null)
            {
                return nativeObj_.getAssetDurationInMs();
            }
            return 0.0;
        }

        /// <summary>
        /// Gets the play back state.
        /// </summary>
        /// <returns>The play back state.</returns>
        public PlayState getPlayState()
        {
            if (nativeObj_ != null)
            {
                return nativeObj_.getPlayState();
            }
            return PlayState.STOPPED;
        }

        void OnDestroy()
        {
            if (nativeObj_ != null && AudioEngineManager.Instance != null && AudioEngineManager.Instance.nativeEngine != null)
            {
                AudioEngineManager.Instance.nativeEngine.destroyAudioObject(nativeObj_);
            }
            thisHandle.Free();
        }

        /// <summary>
        /// Set the volume in linear gain
        /// </summary>
        public float volume
        {
            get
            {
                if (nativeObj_ != null)
                {
                    volume_ = nativeObj_.getVolume();
                }
                return volume_;
            }
            set
            {
                volume_ = value;
                if (nativeObj_ != null)
                {
                    nativeObj_.setVolume(volume_, 0 /* default ramp */ );
                }
            }
        }

        /// <summary>
        /// Set the volume in decibels
        /// </summary>
        public float volumeDecibels
        {
            get
            {
                return Utils.linearToDecibels(volume);
            }
            set
            {
                volume = Utils.decibelsToLinear(value);
            }
        }

        /// <summary>
        /// Set the pitch
        /// </summary>
        public float pitch
        {
            get
            {
                if (nativeObj_ != null)
                {
                    pitch_ = nativeObj_.getPitch();
                }
                return pitch_;
            }
            set
            {
                pitch_ = Mathf.Clamp(value, 0.001f, 4.0f);
                if (nativeObj_ != null)
                {
                    nativeObj_.setPitch(pitch_);
                }
            }
        }

        /// <summary>
        /// Toggle looping. Use this for sample accurate looping rather than manually 
        /// seeking the file to 0 when it finishes playing.
        /// </summary>
        public bool loop
        {
            get
            {
                return loop_;
            }
            set
            {
                loop_ = value;
                if (nativeObj_ != null)
                {
                    nativeObj_.enableLooping(loop_);
                }
            }
        }

        /// <summary>
        /// Minimum distance: the distance after which the attenuation effect kicks in
        /// </summary>
        public float minDistance
        {
            get
            {
                return minDistance_;
            }
            set
            {
                minDistance_ = Mathf.Max(0, value);
            }
        }

        /// <summary>
        /// Maximum distance: the distance after which the attenuation stops
        /// </summary>
        public float maxDistance
        {
            get
            {
                return maxDistance_;
            }
            set
            {
                maxDistance_ = Mathf.Max(0, value);
            }
        }

        /// <summary>
        /// The attenuation curve factor. 1 = 6dB drop with doubling of distance. > 1 steeper curve. 
        /// < 1 less steep curve
        /// </summary>
        public float attenFactor
        {
            get
            {
                return attenFactor_;
            }
            set
            {
                attenFactor_ = Mathf.Max(Mathf.Epsilon, value);
            }
        }

        /// <summary>
        /// The attenuation mode: LOGARITHMIC, LINEAR, DISABLE
        /// </summary>
        public AttenuationMode attenMode
        {
            get
            {
                if (nativeObj_ != null)
                {
                    attenMode_ = nativeObj_.getAttenuationMode();

                }
                return attenMode_;
            }
            set
            {
                attenMode_ = value;
                if (nativeObj_ != null)
                {
                    nativeObj_.setAttenuationMode(attenMode_);
                }
            }
        }

        /// <summary>
        /// Toggle directional filtering
        /// </summary>
        public bool directionality
        {
            get
            {
                return directionality_;
            }
            set
            {
                directionality_ = value;
                if (nativeObj_ != null)
                {
                    nativeObj_.setDirectionalityEnabled(directionality_);
                }
            }
        }

        /// <summary>
        /// A multiplier for the directionl filter, between 0 and 1, 
        /// which changes how subtle or exaggerated the effect is. 
        /// </summary>
        public float directionalityLevel
        {
            get
            {
                return directionalityLevel_;
            }
            set
            {
                directionalityLevel_ = Mathf.Clamp01(value);
            }
        }


        /// <summary>
        /// The directional cone area (in angles, between 0 and 359) where 
        /// the sound is not modified. The area outside this will be filtered.
        /// </summary>
        public float directionalityConeArea
        {
            get
            {
                return directionalityConeArea_;
            }
            set
            {
                directionalityConeArea_ = Mathf.Clamp(value, 0, 359);
            }
        }

        /// <summary>
        /// If the sound must be spatialised or not
        /// </summary>
        public bool spatialise
        {
            get
            {
                return spatialise_;
            }
            set
            {
                spatialise_ = value;
                if (nativeObj_ != null)
                {
                    nativeObj_.shouldSpatialise(spatialise_);
                }
            }
        }

        static public TBE.AudioObject createAndPlayOnObject(GameObject ga, string file)
        {
            var obj = ga.AddComponent<AudioObject>();
            if (!obj.open(file))
            {
                Utils.logError("Failed to open " + file, null);
                Destroy(obj);
                return null;
            }
            obj.play();
            return obj;
        }

        static public TBE.AudioObject createOnObject(GameObject ga, string file)
        {
            var obj = ga.AddComponent<AudioObject>();
            if (!obj.open(file))
            {
                Utils.logError("Failed to open " + file, null);
                Destroy(obj);
                return null;
            }
            return obj;
        }
    }
}