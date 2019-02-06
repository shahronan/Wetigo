/*
 *  Copyright 2013-Present Two Big Ears Limited
 *  All rights reserved.
 *  http://twobigears.com
 */

using UnityEngine;
using System.Collections;

namespace TBE
{
    public class AudioEngineSettings : Singleton<AudioEngineSettings>, IObject
    {
        public SampleRate sampleRate = SampleRate.SR_48000;
        public int objectPoolSize = 32;
        public AudioDeviceType audioDevice = AudioDeviceType.DEFAULT;
        public string customAudioDevice = string.Empty;

        public void onInit() {}
        public void onTerminate() {}

        public bool mustNotDestroyOnLoad()
        {
            return true;
        }
    }
}
