using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public AudioClip[] sounds; // 存放不同音效的数组
    private AudioSource audioSource;

    public static SoundFXManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // 播放指定索引的音效
    public void PlaySound(int index)
    {
        if (index < sounds.Length)
        {
            audioSource.clip = sounds[index]; // 设置当前音效
            audioSource.Play(); // 播放音效
        }
        else
        {
            Debug.LogError("Sound index out of range");
        }
    }
}
