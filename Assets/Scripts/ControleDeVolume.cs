using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDeVolume : MonoBehaviour
{
    public float volumeMaster;
    public float volumeFX;
    public float volumeMusica;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeMaster(float volume)
    {
        volumeMaster = volume;
        AudioListener.volume = volumeMaster;
    }
    public void VolumeFX(float volume)
    {
        volumeFX = volume;
        GameObject[] Fxs = GameObject.FindGameObjectsWithTag("FX");
        for (int i=0; i<Fxs.Length; i++)
        {
            Fxs[i].GetComponent<AudioSource>().volume = volumeFX;
        }
    }
        public void VolumeMusica(float volume)
    {
        volumeMusica = volume;
        GameObject[] Music = GameObject.FindGameObjectsWithTag("Music");
        for (int i=0; i<Music.Length; i++)
        {
            Music[i].GetComponent<AudioSource>().volume = volumeMusica;
        }
        
    }




}
