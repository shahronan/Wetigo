using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerController_old : MonoBehaviour {
    public GameObject Camera;
    public Text debugingText;

    public string[] paths = { "/mnt/sdcard/Wetigo/0.mp4",
                              "/mnt/sdcard/Wetigo/1.mp4",
                              "/mnt/sdcard/Wetigo/2.mp4",
                              "/mnt/sdcard/Wetigo/3.mp4",
                              "/mnt/sdcard/Wetigo/4.mp4",
                              "/mnt/sdcard/Wetigo/5.mp4",
                              "/mnt/sdcard/Wetigo/6.mp4",
                              "/mnt/sdcard/Wetigo/7.mp4",
                              "/mnt/sdcard/Wetigo/8.mp4",
                              "/mnt/sdcard/Wetigo/9.mp4",
                              "/mnt/sdcard/Wetigo/10.mp4"
                            };

    private VideoPlayer videoPlayer;
    private int count; // count is the video number
    private float lookAt;
    private bool haveAmulet;


    // Use this for initialization
    void Start () {
        count = 0;

        // count as 0, load the first video
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), paths[count]);

        videoPlayer.Play();

        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {   // First Video?
        videoPlayer.Pause();
        // decision one: get the amulet or not
        // This will determind the flow in the future
        if (count == 0)
        {


            if ( lookAt > 110 && lookAt < 330)
            {
                // look at the forest, do not get amulet
                haveAmulet = false;
            }
            else
            {
                // look at the person, get amulet
                haveAmulet = true;
            }

            // play ther next video
            count = 1;
            videoPlayer.url = System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), paths[count]);
            videoPlayer.Play();
            videoPlayer.loopPointReached += EndReached;
        }
        // Second Video?
        else if (count == 1)
        {


            if ( lookAt > 80 && lookAt < 300)
            {
                // follow Edward
                count = 6;
                videoPlayer.url = System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), paths[count]);
            }
            else
            {
                // follow Maria
                count = 3;
                videoPlayer.url = System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), paths[count]);
            }

            videoPlayer.Play();
            videoPlayer.loopPointReached += EndReached;

        }
        else if (count == 3)
        {


            if ( lookAt > 0 && lookAt < 180)
            {
                // Killed by Louis
                count = 5;
                videoPlayer.url = System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), paths[count]);
            }
            else
            {
                // The cave scene, eaten by Wetigo
                count = 4;
                videoPlayer.url = System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), paths[count]);
            }

            videoPlayer.Play();
            videoPlayer.loopPointReached += EndReached;
        }
        else if (count == 6)
        {


            if ( haveAmulet == true)
            {
                // Killed by Louis
                count = 8;
                videoPlayer.url = System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), paths[count]);
            }
            else
            {
                // The cave scene, eaten by Wetigo
                count = 7;
                videoPlayer.url = System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), paths[count]);
            }

            videoPlayer.Play();
            videoPlayer.loopPointReached += EndReached;
        }
        else if (count == 8)
        {

            count = 9;
            videoPlayer.url = System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), paths[count]);
            videoPlayer.Play();
            videoPlayer.loopPointReached += EndReached;
        }
        else if (count == 9)
        {


            if ( lookAt > 90 && lookAt < 270)
            {
                // Killed by Louis
                count = 10;
                videoPlayer.url = System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), paths[count]);
            }
            else
            {
                // Good End
                count = 11;
                videoPlayer.url = System.IO.Path.Combine(Application.persistentDataPath.Replace('\\', '/'), paths[count]);
            }

            videoPlayer.Play();
            videoPlayer.loopPointReached += EndReached;
        }
        // finished?
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("UIDemo");
        }
    }

    private void Update()
    {
        lookAt = Camera.transform.localRotation.eulerAngles.y;
        debugingText.text = count.ToString();
    }
}
