using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class LifeUpdater : MonoBehaviour
{
    [SerializeField] private GameObject h1, h2, h3, h4, h5, h6, h7;
    [SerializeField] private Player player;
    [SerializeField] private Sprite fire, darkFire;
    private List<GameObject> images;
    [SerializeField] private PlayerData data;

    // Start is called before the first frame update
    void Start()
    {
        images = new List<GameObject>();
        images.Add(h1);
        images.Add(h2);
        images.Add(h3);
        images.Add(h4);
        images.Add(h5);
        images.Add(h6);
        images.Add(h7);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateLife();
    }

    void UpdateLife(){
        for (int i = 0; i <= 6; i++)
        {
            if (i<player.life)
            {
                images[i].GetComponent<Image>().sprite = fire;
                images[i].GetComponent<Image>().color =new Color32(255,255,255,255);
            }else if (player.life <= i && i < data.maxLife)
            {
                images[i].GetComponent<Image>().sprite = darkFire;
                images[i].GetComponent<Image>().color =new Color32(255,255,255,255);
            }else if(i >= data.maxLife)
            {
                images[i].GetComponent<Image>().color =new Color32(255,255,255,0);
            }
        }
    }
}
