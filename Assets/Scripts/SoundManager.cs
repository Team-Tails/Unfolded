using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    [SerializeField] private List<AudioFile> audioFiles;

    private System.Random rand = new System.Random();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance != null)
        {   
            Destroy(gameObject);
        }
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string soundtype, float volume = 1){
        var audioType = (
            from audioFile in audioFiles
            where audioFile.name == soundtype
            select audioFile
        ).First();

        int soundSelect = rand.Next(0,audioType.sounds.Length);

        audioSource.PlayOneShot(audioType.sounds[soundSelect], volume);
    }
}

[Serializable]
public struct AudioFile
{
    [SerializeField] public string name;
    [SerializeField] public AudioClip[] sounds;
}