using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Synchronisation : MonoBehaviour {

    TBE.SpatDecoderFile spat;

    VideoPlayer videoPlayer;

	// Use this for initialization
	void Awake () {
        spat = GetComponent<TBE.SpatDecoderFile>();

        videoPlayer = GetComponent<VideoPlayer>();

        spat.syncMode = TBE.SyncMode.EXTERNAL;
	}
	
	// Update is called once per frame
	void Update () {
        double videoTime = videoPlayer.time;

        spat.setExternalClockInMs(videoTime);

        somePlayEvent();
    }

    void somePlayEvent()
    {
        // spat.open("\\mnt\\sdcard\\Wetigo\\FinalSound\\0\\TBE\\WETIGOSCENE0.tbe");
        spat.play();
        videoPlayer.Play();
    }
}
