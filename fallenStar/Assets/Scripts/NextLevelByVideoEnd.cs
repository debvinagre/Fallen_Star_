using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class NextLevelByVideoEnd : MonoBehaviour
{

    [SerializeField] private string sceneToLoad;
    private VideoPlayer video;

    void Start(){
        video = GetComponent<VideoPlayer>();
    }

    void Update(){
        if(video.frame > 0 && video.isPlaying == false)
        {
            SceneManager.LoadScene(sceneToLoad);     
        }
    }
}
