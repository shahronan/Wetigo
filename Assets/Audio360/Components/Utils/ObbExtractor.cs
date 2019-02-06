/*
 *  Copyright 2013-Present Two Big Ears Limited
 *  All rights reserved.
 *  http://twobigears.com
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TBE
{
    public class ObbExtractor
    {
        /// <summary>
        /// Returns the path to streaming assets -- either persistent data path 
        /// if assets are extracted from a obb or the default streamingAssetsPath
        /// </summary>
        /// <returns>The path to the file</returns>
        public static string getStreamingAssetsPath()
        {
            return (streamingAssetsAreInObb()) ? Application.persistentDataPath : Application.streamingAssetsPath;
        }

        /// <summary>
        /// Returns true if the streaming assets are in an obb file
        /// </summary>
        /// <returns><c>true</c>, if the streaming assets are in an obb <c>false</c> otherwise.</returns>
        public static bool streamingAssetsAreInObb()
        {
            return Application.streamingAssetsPath.EndsWith("obb!/assets");
        }

        /// <summary>
        /// Extracts files from .obb to the persistent data path.
        /// </summary>
        /// <param name="assetNames">Asset names to extract.</param>
        /// <param name="forceReplace">If set to <c>true</c> the file will be extracted even if a similar file already exists in the persistent data path.</param>
        public static IEnumerator extractFromObb(string[] assetNames, bool forceReplace)
        {
            foreach (string name in assetNames)
            {
                string obbPath = Application.streamingAssetsPath + "/" + name;
                WWW www = new WWW(obbPath);
                yield return www;
                if (www == null || !www.isDone || !string.IsNullOrEmpty(www.error) || www.bytes == null)
                {
                    Utils.logError("Failed to open " + name + " from obb. " + www.error, null);
                    continue;
                }

                // Skip extraction if the file already exists in the persistent path
                // -- unless forceReplace is set to true
                long expectedSize = www.bytes.Length;
                string persistentFile = Application.persistentDataPath + "/" + name;
                if (File.Exists(persistentFile) && !forceReplace)
                {
                    long persistentSize = new FileInfo(persistentFile).Length;
                    if (persistentSize == expectedSize)
                    {
                        Utils.log(name + " already exists, skipping extraction", null);
                        continue;
                    }
                }

                File.WriteAllBytes(persistentFile, www.bytes);
                if (!File.Exists(persistentFile))
                {
                    Utils.logError("Failed to extract and write " + name + " from obb.", null);
                    continue;
                }

                long writtenSize = new FileInfo(persistentFile).Length;
                if (writtenSize != expectedSize)
                {
                    Utils.logError("Extracted file's (" + name + ") length "
                        + writtenSize + " does not match expected length " + expectedSize, null);
                    continue;
                }
            }
        }
    }
}
