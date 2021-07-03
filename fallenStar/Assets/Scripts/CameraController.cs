using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] CinemachineVirtualCamera cm;
    public Transform back1, back2, neve;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(player.firstCamC){
            back1.parent = null;
            neve.parent = null;
        }
        if (player.secondCamC)
        {
            back2.parent = null;
            cm.Follow = null;
        }      
    }
}
