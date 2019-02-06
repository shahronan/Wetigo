/*
 *  Copyright 2013-Present Two Big Ears Limited
 *  All rights reserved.
 *  http://twobigears.com
 */

using System.Collections;
using System.IO;
using UnityEngine;

namespace TBE
{

    public delegate void EventCallback(Event e, global::System.IntPtr u);

    public enum LoggerVerbosity
    {
        ALL,
        WARNINGS_AND_ERRORS,
        ERRORS
    }

    public enum SampleRate
    {
        SR_44100,
        SR_48000
    }

    public enum PathType
    {
        STREAMING_ASSETS,
        ABSOLUTE
    }

    public class Utils
    {
#if UNITY_IOS && !UNITY_EDITOR
        public const string DLL = "__Internal";
#else
        public const string DLL = "Audio360CSharp";
#endif

        // Do not change, unless you know what you are doing.
        public static float kFocusMin = -24.0f;
        public static float kFocusMax = 0.0f;
        public static float kFocusWidthMin = 40.0f;
        public static float kFocusWidthMax = 120.0f;

        static LoggerVerbosity logVerbosity = LoggerVerbosity.ALL;

        static public Vector3 toVector3(TBVector vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }

        static public TBVector toTBVector(Vector3 vector)
        {
            return new TBVector(vector.x, vector.y, vector.z);
        }

        static public Quaternion toQuaternion(TBQuat quat)
        {
            return new Quaternion(quat.x, quat.y, quat.z, quat.w);
        }

        static public TBQuat toTBQuat(Quaternion quat)
        {
            return new TBQuat(quat.x, quat.y, quat.z, quat.w);
        }

        static public void logError(string messg, Object context)
        {
                Debug.LogError("[FBAudio] " + messg, context);
            }

        static public void logWarning(string messg, Object context)
        {
            if (logVerbosity != LoggerVerbosity.ERRORS)
            {
                Debug.LogWarning("[FBAudio] " + messg, context);
            }
        }

        static public void log(string messg, Object context)
        {
            if (logVerbosity == LoggerVerbosity.ALL)
            {
                Debug.Log("[FBAudio] " + messg, context);
            }
        }

        static public float toSampleRateFloat(SampleRate sr)
        {
            switch (sr)
            {
                case SampleRate.SR_44100:
                    return 44100.0f;
                case SampleRate.SR_48000:
                    return 48000.0f;
                default:
                    return 48000.0f;
            }
        }

        static public SampleRate toSampleRateEnum(float sr)
        {
            if (sr == 44100.0f)
            {
                return SampleRate.SR_44100;
            }
            else if (sr == 48000.0f)
            {
                return SampleRate.SR_48000;
            }
            else
            {
                Utils.logError("Incompatible sample rate " + sr + ". Defaulting to 48000", null);
                return SampleRate.SR_48000;
            }
        }

        /// Resolve streaming assets path.
        static public string resolvePath(string path, PathType type)
        {
            if (type == PathType.STREAMING_ASSETS)
            {
                string streamingAssetsPath = Path.Combine(Application.streamingAssetsPath, path);
                if (streamingAssetsPath.Contains("apk!") && Application.platform == RuntimePlatform.Android)
                {
                    return "asset:///" + Path.GetFileName(streamingAssetsPath);
                }
                return streamingAssetsPath;
            }

            return path;
        }

        static public float decibelsToLinear(float db)
        {
            return Mathf.Pow(10.0f, (db * 0.05f));
        }

        static public float linearToDecibels(float linear)
        {
            return 20.0f * Mathf.Log10(linear);
        }
    }
}