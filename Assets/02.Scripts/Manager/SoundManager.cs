using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] Sound[] bgmSounds = null;
    [SerializeField] Sound[] sfxSounds = null;

    [Header("브금 플레이어")]
    public AudioSource bgmPlayer = null;

    [Header("효과음 플레이어")]
    public AudioSource[] sfxPlayer = null;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    void Start()
    {
        PlayRandomBGM();
    }

    //private void Update()
    //{
    //    if (!bgmPlayer.isPlaying && !GameManager.instance.isStop)
    //    {
    //        int random = Random.Range(0, bgmSounds.Length - 1);

    //        bgmPlayer.clip = bgmSounds[random].clip;
    //        bgmPlayer.Play();
    //    }
    //}

    public void PlayRandomBGM()
    {
        int random = Random.Range(0, bgmSounds.Length - 1); // 정수타입은 MAX값 미포함 실수 타입은 MAX값 포함

        bgmPlayer.clip = bgmSounds[random].clip;
        bgmPlayer.Play();
    }

    public void PlaySE(string _soundName)
    {

        // 효과음 플레이어에 여러개의 오디오소를 넣어주면 연속적인 재생이 가능해진다. (개수가 적을 수록 소리 딜레이가 크다)
        for (int i = 0; i < sfxSounds.Length; i++)
        {
            if (_soundName == sfxSounds[i].soundName)
            {
                for (int x = 0; x < sfxPlayer.Length; x++)
                {
                    if (!sfxPlayer[x].isPlaying)
                    {
                        sfxPlayer[x].clip = sfxSounds[i].clip;
                        sfxPlayer[x].Play();
                        //Debug.Log("플레이함" + _soundName);
                        return;
                    }
                }
                Debug.Log("모든 효과음 플레이어가 사용 중입니다!");
                return;
            }
        }
        Debug.Log("등록된 효과음이 없습니다.");
    }

    public void PauseBGM()
    {
        //Debug.Log("비지엠  스탑");
        bgmPlayer.Pause();
    }
    
    public void RestartBGM()
    {
        //ebug.Log("비지엠  다시 재생");
        if(!bgmPlayer.isPlaying)
        {
            bgmPlayer.Play();
        }
    }
}
