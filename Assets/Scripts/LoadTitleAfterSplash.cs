using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadTitleAfterSplash : MonoBehaviour {

    VideoPlayer videoPlayer;

	// Use this for initialization
	void Start () {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update() {
        /*
         * According to docs, I should be able to use videoPlayer.isPlaying
         * but in practice it seemed to intially be true and then immediately switched to false,
         * so that the video never got to play.
         * 
         * This check compares the current frame to the total number of frames. 
         * When they're equal, you've hit the end of the video.
         * Have to cast the videoPlayer.frame to a ulong to get the comparison to work. I kinda hate this.
         * 
         * Also, I'll note that the video doesn't play in the editor view, but it does play in builds.
         * I've not been able to find out why this is the case so far.
         */
        if ((ulong)videoPlayer.frame == videoPlayer.frameCount) {
            SceneManager.LoadScene(1);
        }
    }
}
