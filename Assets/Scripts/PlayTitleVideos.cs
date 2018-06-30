using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayTitleVideos : MonoBehaviour {

    VideoPlayer[] videoPlayers;    

    // Use this for initialization
    void Start() {
        videoPlayers = GetComponents<VideoPlayer>();
    }

    // Update is called once per frame
    void Update() {
       
        if ((ulong)videoPlayers[0].frame == videoPlayers[0].frameCount) {
            videoPlayers[1].Play();
        }
    }
}
