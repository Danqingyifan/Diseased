using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public AudioClip[] sounds; // ��Ų�ͬ��Ч������
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

    // ����ָ����������Ч
    public void PlaySound(int index)
    {
        if (index < sounds.Length)
        {
            audioSource.clip = sounds[index]; // ���õ�ǰ��Ч
            audioSource.Play(); // ������Ч
        }
        else
        {
            Debug.LogError("Sound index out of range");
        }
    }
}
