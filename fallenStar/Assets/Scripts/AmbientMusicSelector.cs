using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Audio;

public class AmbientMusicSelector : MonoBehaviour
{
    public AudioManager audio;
    public string song;
    // Start is called before the first frame update
    void Start(){
        audio.Play(song);
    }
    
}
