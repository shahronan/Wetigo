using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TBE;

public class VideoPlayerController : MonoBehaviour {
    public GameObject Camera;
    public Text DebuggingText;
    public Text PlayingText;
    public Text LeftText;
    public Text RightText;

    public OVRScreenFade screenFade;
    public float fadeTime;

    private readonly string[] paths = { "/mnt/sdcard/Wetigo/Videos/0.mp4", // 0
                                        "/mnt/sdcard/Wetigo/Videos/0-1.mp4", // 1
                                        "/mnt/sdcard/Wetigo/Videos/1.mp4", // 2
                                        "/mnt/sdcard/Wetigo/Videos/2a.mp4", // 3
                                        "/mnt/sdcard/Wetigo/Videos/2b.mp4", // 4
                                        "/mnt/sdcard/Wetigo/Videos/3.mp4", // 5
                                        "/mnt/sdcard/Wetigo/Videos/4.mp4", // 6
                                        "/mnt/sdcard/Wetigo/Videos/5.mp4", // 7
                                        "/mnt/sdcard/Wetigo/Videos/6a.mp4", // 8
                                        "/mnt/sdcard/Wetigo/Videos/6b.mp4", // 9
                                        "/mnt/sdcard/Wetigo/Videos/7.mp4", // 10
                                        "/mnt/sdcard/Wetigo/Videos/8.1.mp4", // 11
                                        "/mnt/sdcard/Wetigo/Videos/8.2.mp4", // 12
                                        "/mnt/sdcard/Wetigo/Videos/9.mp4", // 13
                                        "/mnt/sdcard/Wetigo/Videos/10.mp4", // 14
                                    };

    private readonly string[] spatPaths = { "/mnt/sdcard/Wetigo/Sounds/0.tbe", // 0
                                            "/mnt/sdcard/Wetigo/Sounds/0-1.tbe", // 1
                                            "/mnt/sdcard/Wetigo/Sounds/1.tbe", // 2
                                            "/mnt/sdcard/Wetigo/Sounds/2a.tbe", // 3
                                            "/mnt/sdcard/Wetigo/Sounds/2b.tbe", // 4
                                            "/mnt/sdcard/Wetigo/Sounds/3.tbe", // 5
                                            "/mnt/sdcard/Wetigo/Sounds/4.tbe", // 6
                                            "/mnt/sdcard/Wetigo/Sounds/5.tbe", // 7
                                            "/mnt/sdcard/Wetigo/Sounds/6a.tbe", // 8
                                            "/mnt/sdcard/Wetigo/Sounds/6b.tbe", // 9
                                            "/mnt/sdcard/Wetigo/Sounds/7.tbe", // 10
                                            "/mnt/sdcard/Wetigo/Sounds/8.1.tbe", // 11
                                            "/mnt/sdcard/Wetigo/Sounds/8.2.tbe", // 12
                                            "/mnt/sdcard/Wetigo/Sounds/9.tbe", // 13
                                            "/mnt/sdcard/Wetigo/Sounds/10.tbe" // 14
                                        };

    public GameObject VideoSphere;
    private VideoPlayer videoPlayer;
    private SpatDecoderFile spat;
    private int currentlyPlaying;
    private int leftPlaying;
    private int rightPlaying;
    private float lookAt;
    private bool haveAmulet;

    public AudioSource gunShotSound;

    private bool FadeOutLock;

    private double videoTotalTime;
        //= videoPlayer.frameCount / videoPlayer.frameRate;
    //var totalMinutes = Mathf.Floor((int)videoTotalTime / 60).ToString("00");
    //var totalSeconds = ((int)videoTotalTime % 60).ToString("00");
    //PlayingText.text += "\nTotal Video Time: " + totalMinutes + ":" + totalSeconds;


    public GameObject Sc2EdwardChoice;
    public GameObject Sc2MarieAndPierreLouisChoice;
    public GameObject Sc3MarieChoice;
    public GameObject Sc3PierreLouisChoice;
    public GameObject Sc8CanoeChoice;
    public GameObject Sc8EdwardChoice;

    public double SelectionTimeInSec = 5;
    public bool isDebug;
    public bool isLookAt;


    // Use this for initialization
    void Start () {
        currentlyPlaying = 0;
        leftPlaying = 1;
        rightPlaying = 1;

        videoPlayer = GetComponent<VideoPlayer>();
        spat = GetComponent<SpatDecoderFile>();
        spat.syncMode = TBE.SyncMode.EXTERNAL;

        haveAmulet = false;

        // // parse all the path to absolute path
        // for (int i = 0; i < paths.Length; i++)
        // {
        //   paths[i] = System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), paths[i]);
        // }

        ChoicesCanvasInit();

        videoTotalTime = 0;

        FadeOutLock = false;

        NextVideo();

        // debugging propose
        //isDebug = true;
        //isLookAt = true;
    }

    void ChoicesCanvasInit()
    {
        Sc2EdwardChoice.SetActive(false);
        Sc2MarieAndPierreLouisChoice.SetActive(false);
        Sc3MarieChoice.SetActive(false);
        Sc3PierreLouisChoice.SetActive(false);
        Sc8CanoeChoice.SetActive(false);
        Sc8EdwardChoice.SetActive(false);
    }

    public void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        // make sure everything is stopped first
        videoPlayer.loopPointReached -= EndReached;
        videoPlayer.Stop();

        spat.stop();
        spat.close();

        ChoicesCanvasInit();

        // Use the LookAt or the button to switch scene
        if (isLookAt)
            SelectNextVideoByLookAt();
        else
            SelectNextVideoByButton();

    }

    void SelectNextVideoByButton()
    {

    }

    void SelectNextVideoByLookAt()
    {
        switch (currentlyPlaying)
        {
            case 0:

                //// Get Amulet or not
                //if (isDebug)
                //{
                //    if (lookAt > 180 && lookAt < 360)
                //    {
                //        haveAmulet = true;
                //    }
                //}
                //else if (lookAt > 137 && lookAt < 317)
                //{
                //    haveAmulet = true;
                //}


                haveAmulet = true;


                ChooseNextVideo(1, 2, 2);
                break;
            case 1:
                ChooseNextVideo(2, 3, 4);
                break;
            case 2: // 1 -> 2a or 2b
                if (isDebug)
                {
                    if (lookAt > 180 && lookAt < 360)
                    {
                        ChooseNextVideo(3, 5, 8);
                    }
                    else
                    {
                        ChooseNextVideo(4, 5, 9);
                    }
                }
                else if (haveAmulet == true)
                {
                    ChooseNextVideo(3, 5, 8);
                }
                else
                {
                    ChooseNextVideo(4, 5, 9);
                }
                break;
            case 3: // 2a -> 3 or 6a
                if (isDebug)
                {
                    if (lookAt > 180 && lookAt < 360)
                    {
                        ChooseNextVideo(5, 6, 7);
                    }
                    else
                    {
                        ChooseNextVideo(8, 11, 11);
                    }
                }
                else if (lookAt > 152 && lookAt < 332)
                {
                    ChooseNextVideo(5, 6, 7);
                }
                else
                {
                    ChooseNextVideo(8, 11, 11);
                }
                break;
            case 4:  // 2b -> 3 or 6b
                if (isDebug)
                {
                    if (lookAt > 152 && lookAt < 332)
                    {
                        ChooseNextVideo(5, 6, 7);
                    }
                    else
                    {
                        ChooseNextVideo(9, 10, 10);
                    }
                }
                else if (lookAt > 80 && lookAt < 260)
                {
                    ChooseNextVideo(5, 6, 7);
                }
                else
                {
                    ChooseNextVideo(9, 10, 10);
                }
                break;
            case 5: // 3 -> 4 or 5
                if (isDebug)
                {
                    if (lookAt > 180 && lookAt < 360)
                    {
                        ChooseNextVideo(6, 15, 15);
                    }
                    else
                    {
                        ChooseNextVideo(7, 15, 15);
                    }
                }
                else if (lookAt > 169 && lookAt < 349)
                {
                    ChooseNextVideo(6, 15, 15);
                }
                else
                {
                    ChooseNextVideo(7, 15, 15);
                }
                break;
            case 8:
                ChooseNextVideo(11, 12, 12);
                break;
            case 9:
                ChooseNextVideo(10, 15, 15);
                break;
            case 11:
                ChooseNextVideo(12, 13, 14);
                break;
            case 12:    // 8.2 -> 9 or 10
                if (isDebug)
                {
                    if (lookAt > 180 && lookAt < 360)
                    {
                        ChooseNextVideo(13, 15, 15);
                    }
                    else
                    {
                        ChooseNextVideo(14, 15, 15);
                    }
                }
                else if (lookAt > 60 && lookAt < 240)
                {
                    ChooseNextVideo(13, 15, 15);
                }
                else
                {
                    ChooseNextVideo(14, 15, 15);
                }
                break;
            default:
                UnityEngine.SceneManagement.SceneManager.LoadScene("UIDemo");
                break;
        }
        NextVideo();
    }

    void ChooseNextVideo(int play, int leftnext, int rightnext)
    {
        currentlyPlaying = play;
        leftPlaying = leftnext;
        rightPlaying = rightnext;
    }

    void NextVideo()
    {
        // init the Fade
        FadeOutLock = false;
        screenFade.SetFadeLevel(0);


        // convert the url into persistent data path
        videoPlayer.url = System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), paths[currentlyPlaying]);
        spat.open(System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), spatPaths[currentlyPlaying]));
    
        
        // Play the video
        videoPlayer.Play();

        // now everything are slaved to the video
        // videoPlayer.prepareCompleted
        videoPlayer.prepareCompleted += PrepareVideoAssets;
        videoPlayer.loopPointReached += EndReached;


        // FadeIn effect at the beginning of every video
        // screenFade.fadeTime = fadeTime;
        // screenFade.FadeIn();
    }

    void ScreenFade()
    {
        switch(currentlyPlaying)
        {
            case 2:
            case 3:
            case 4:
                screenFade.FadeIn();
                break;
            default:
                break;
        }
    }

    void PrepareVideoAssets(UnityEngine.Video.VideoPlayer vp)
    {
        spat.play();
    }

    void Update()
    {
        // slave the Audio Time to Video Time
        double videoTime = videoPlayer.time * 1000;
        spat.setExternalClockInMs(videoTime);

        videoTotalTime = videoPlayer.frameCount / videoPlayer.frameRate;

        switch (currentlyPlaying)
        {
            case 3:
            case 4:
                if (videoPlayer.isPlaying)
                {
                    if ((videoTotalTime - videoPlayer.time) < SelectionTimeInSec)
                    {
                        Sc2EdwardChoice.SetActive(true);
                        Sc2MarieAndPierreLouisChoice.SetActive(true);
                    }
                    else
                    {
                        ChoicesCanvasInit();
                    }
                }
                break;
            case 5:
                if (videoPlayer.isPlaying)
                {
                    if ((videoTotalTime - videoPlayer.time) < SelectionTimeInSec)
                    {
                        Sc3MarieChoice.SetActive(true);
                        Sc3PierreLouisChoice.SetActive(true);
                    }
                    else
                    {
                        ChoicesCanvasInit();
                    }
                }
                break;
            case 12:
                if (videoPlayer.isPlaying)
                {
                    if ((videoTotalTime - videoPlayer.time) < SelectionTimeInSec)
                    {
                        Sc8CanoeChoice.SetActive(true);
                        Sc8EdwardChoice.SetActive(true);
                    }
                    else
                    {
                        ChoicesCanvasInit();
                    }
                }
                break;
            default:
                ChoicesCanvasInit();
                break;
        }


        // Fade out effect for every end of the video
        //if (videoPlayer.isPlaying && !FadeOutLock && (videoTotalTime - videoPlayer.time) < fadeTime + 1.5f)
        //{
        //    FadeOutLock = true;
        //    screenFade.fadeTime = fadeTime;
        //    screenFade.FadeOut();
        //}

        UpdateUI();
    }

    void UpdateUI()
    {
        // Get the looking Position
        lookAt = Camera.transform.localRotation.eulerAngles.y;
        DebuggingText.text = lookAt.ToString();

        string temp;
        if (currentlyPlaying == 15)
        {
            PlayingText.text = "End";
        }
        else
        {
            temp = paths[currentlyPlaying];
            PlayingText.text = currentlyPlaying.ToString() + " + ";
            PlayingText.text += temp.Substring(26, temp.IndexOf(".mp4") - 26);
            PlayingText.text += "\n " + spatPaths[currentlyPlaying];

            var internalTime = Time.time;
            var internalSeconds = ((int)internalTime % 60).ToString("00");
            var internalMinutes = Mathf.Floor((int)internalTime / 60).ToString("00");
            PlayingText.text += "\nInternal Minutes: Seconds: " + internalMinutes + ":" + internalSeconds;



            
            var totalMinutes = Mathf.Floor((int)videoTotalTime / 60).ToString("00");
            var totalSeconds = ((int)videoTotalTime % 60).ToString("00");
            PlayingText.text += "\n" + totalMinutes + ":" + totalSeconds;


            var minutes = Mathf.Floor((int)videoPlayer.time / 60).ToString("00");
            var seconds = ((int)videoPlayer.time % 60).ToString("00");
            PlayingText.text += "\nVideo Time: " + minutes + ":" + seconds;

            if (currentlyPlaying == 13 && (int)videoPlayer.time == 74)
            {
                gunShotSound.Play();
            }

            var elapsedTimeInMs = spat.getElapsedTimeInMs();
            var elapsedTimeInMinutes = Mathf.Floor((int)elapsedTimeInMs / 1000 / 60).ToString("00");
            var elapsedTimeInSeconds = ((int)elapsedTimeInMs / 1000 % 60).ToString("00");
            PlayingText.text += "\nAudio Time: " + elapsedTimeInMinutes + ":" + elapsedTimeInSeconds;
        }

        if (leftPlaying == 15)
        {
            LeftText.text = "End";
        }
        else
        {
            temp = paths[leftPlaying];
            LeftText.text = temp.Substring(26, temp.IndexOf(".mp4") - 26);
        }


        if (rightPlaying == 15)
        {
            RightText.text = "End";
        }
        else
        {
            temp = paths[rightPlaying];
            RightText.text = temp.Substring(26, temp.IndexOf(".mp4") - 26);
        }
    }
}
