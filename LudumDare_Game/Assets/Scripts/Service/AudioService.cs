/****************************************************
    文件：AudioService.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/19 13:56:24
    功能：Nothing
*****************************************************/

using UnityEngine;

public class AudioService : MonoBehaviour 
{
    public static AudioService Instance = null;

    public AudioSource bgAudioSource;
    public AudioSource uiAudioSource;

    public void InitSvc()
    {
        Instance = this;
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    public void PlayBGMusic(string audioName, bool isLoop = true)
    {
        AudioClip audio = ResService.Instance.LoadAudio("ResAudio/" + audioName, true);
        if (bgAudioSource.clip == null || bgAudioSource.clip.name != audio.name)
        {
            bgAudioSource.clip = audio;
            bgAudioSource.loop = isLoop;
            bgAudioSource.Play();
        }
    }

    /// <summary>
    /// 播放UI操作音效
    /// </summary>
    public void PlayUIAudio(string audioName)
    {
        AudioClip audio = ResService.Instance.LoadAudio("ResAudio/" + audioName, true);
        //uiAudioSource.clip = audio;
        //uiAudioSource.Play();

        uiAudioSource.PlayOneShot(audio);
    }
}