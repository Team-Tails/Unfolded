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

    public void PlaySound(string soundtype){
        var audioType = (
            from audioFile in audioFiles
            where audioFile.name == soundtype
            select audioFile
        ).First();

        audioSource.PlayOneShot(audioType.sound);
    }
}

[Serializable]
public struct AudioFile
{
    [SerializeField] public string name;
    [SerializeField] public AudioClip sound;
}