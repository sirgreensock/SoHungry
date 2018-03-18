using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioSetup
{
    public string clipName;
    public AudioClip soundClip;

}

public class AudioController : MonoBehaviour {

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    List<AudioSetup> audioClipList;

    private AudioClip foundClip;

    public void PlayAudioClip(string requestedClip)
    {
        AudioClip clip = FindAudioClip(requestedClip);
        audioSource.PlayOneShot(clip);
    }

    private AudioClip FindAudioClip(string requestedClip)
    {

        for (int i = 0; i < audioClipList.Count; i ++)
        {
            if (audioClipList[i].clipName == requestedClip)
            {
                foundClip = audioClipList[i].soundClip;
            } else
            {
                continue;
            }
        }
        
        return foundClip;
    }
    
}
