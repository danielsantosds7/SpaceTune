using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    int musicaAtual = 0;
    AudioSource audioSource;
    public AudioClip[] clipNames;
    public Text musicName;
    public Slider musicLength;
    private bool stop = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartAudio();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            musicLength.value += Time.deltaTime;
            if (musicLength.value >= audioSource.clip.length)
            {
                musicaAtual++;
                if (musicaAtual >= clipNames.Length)
                {
                    musicaAtual = 0;
                    StartAudio();
                }
            }
        }
    }
    public void StartAudio (int changeMusic = 0)
    {
        musicaAtual += changeMusic;
        if (musicaAtual >= clipNames.Length)
        {
            musicaAtual = 0;
        }
        else if (musicaAtual <= 0)
        {
            musicaAtual = clipNames.Length - 1;
        }
        if (audioSource.isPlaying && changeMusic == 0)
        {
            return;
        }
        if (stop)
        {
            stop = false;
        }
        audioSource.clip = clipNames [musicaAtual];
        musicName.text = audioSource.clip.name;
        musicLength.maxValue = audioSource.clip.length;
        musicLength.value = 0;
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
        stop = true;
    }
}
