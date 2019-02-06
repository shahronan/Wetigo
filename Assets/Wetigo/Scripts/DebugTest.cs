using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TBE;

public class DebugTest : MonoBehaviour {

    public Text DebuggingText;

    VideoPlayer vp;
    SpatDecoderFile spat;

    bool isTimeGetted4FixedUpdate;
    double FixedUpdateVideoStartTime;
    long FixedUpdateVideoStartFrame;

    bool isTimeGetted4Update;
    double UpdateVideoStartTime;
    long UpdateVideoStartFrame;

	// Use this for initialization
	void Start () {
        vp = GetComponent<VideoPlayer>();
        spat = GetComponent<SpatDecoderFile>();

        isTimeGetted4FixedUpdate = false;
        isTimeGetted4Update = false;

        spat.play();
        vp.Play();
    }
	
	// Update is called once per frame
	void Update () {
        // tbe sync
        // Get the elapsed time/playback position from the video player/decoder. 
        // Make sure the value is in milliseconds! 
        double videoTime = vp.time*1000;
        // Pass the video time to SpatDecoderFile. This will automatically force it to synchronise 
        // with the video (since the syncMode in Awake() was changed to EXTERNAL) 
        spat.setExternalClockInMs(videoTime);


        // Unity Internal Clock
        var internalTime = Time.time;
        var internalSeconds = ((int)internalTime % 60).ToString("00");
        var internalMinutes = Mathf.Floor((int)internalTime / 60).ToString("00");
        var internalFrameCount = Time.frameCount;

        DebuggingText.text = @"Unity Internal Clock
---------------------------------
Internal Time: " + internalTime + @"
Internal Minutes : Seconds: " + internalMinutes + ":" + internalSeconds + @"
Internal Frame Count: " + internalFrameCount + @"
---------------------------------

";

        // Video Player Clock
        var source = vp.url;
        var frame = vp.frame;
        var frameCount = vp.frameCount;
        var frameRate = vp.frameRate;
        var isPlaying = vp.isPlaying;
        var isPrepared = vp.isPrepared;
        var time = vp.time;
        var minutes = Mathf.Floor((int)time / 60).ToString("00");
        var seconds = ((int)time % 60).ToString("00");
        // var timeReference = vp.timeReference;

        DebuggingText.text += @"Video Player Clock
---------------------------------
Path: " + source + @"
CurrentFrame: " + frame + @"
Frame Count: " + frameCount + @"
Frame Rate: " + frameRate + @"
Minutes : Seconds: " + minutes + ":" + seconds + @"
--------------------------------

";


        // TBE decoder Clock
        var file = spat.file;
        var playstate = spat.getPlayState();
        var elapsedTimeInMs = spat.getElapsedTimeInMs();
        var elapsedTimeInMinutes = Mathf.Floor((int)elapsedTimeInMs/1000 / 60).ToString("00");
        var elapsedTimeInSeconds = ((int)elapsedTimeInMs/1000 % 60).ToString("00");
        var elapsedTimeInSamples = spat.getElapsedTimeInSamples();
        var assetDurationInMs = spat.getAssetDurationInMs();
        var assetDurationInSamples = spat.getAssetDurationInSamples();
        var freewheelTimeInMs = spat.getFreewheelTimeInMs();
        var resyncThresholdMs = spat.getResyncThresholdMs();
        var syncMode = spat.syncMode;
        var isOpen = spat.isOpen();

        DebuggingText.text += @"TBE decoder Clock
---------------------------------
Path: " + file + @"
Play State: " + playstate + @"
Elapsed Time(Min:Sec): " + elapsedTimeInMinutes + ":" + elapsedTimeInSeconds + @"
Sync Mode: " + syncMode + @"
--------------------------------

";

        // Get Video Start Clock
        if (isTimeGetted4Update == false)
        {
            UpdateVideoStartTime = internalTime;
            UpdateVideoStartFrame = internalFrameCount;
            isTimeGetted4Update = true;
        }


//        DebuggingText.text += @"Video Start
//---------------------------------
//Video Start Time(Update): " + UpdateVideoStartTime + @"
//Video Start Frame(Update): " + UpdateVideoStartFrame + @"
//Video Start Time(FixedUpdate): " + FixedUpdateVideoStartTime + @"
//Video Start Frame(FixedUpdate): " + FixedUpdateVideoStartFrame + @"
//--------------------------------
//";
    }

    void FixedUpdate()
    {
        var internalTime = Time.time;
        var internalFrameCount = Time.frameCount;

        // Get Video Start Clock
        if (isTimeGetted4FixedUpdate == false)
        {
            FixedUpdateVideoStartTime = internalTime;
            FixedUpdateVideoStartFrame = internalFrameCount;
            isTimeGetted4FixedUpdate = true;
        }
    }
}
