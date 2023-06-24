using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{

    //variavel que verifica se a musica esta tocando.
    private bool _isPlayingMusic = false;

    //lista de efeito sonoro e musica
    [Header("Lista de esffeito sonoro")]
    [SerializeField] private List<AudioClip> _listAudioFx;
    

    [Header("Lista de musicas")]
    [SerializeField] private List<AudioClip> _listAudioMusic;

    //COMPONETE audio source
    [Header("Audio Source")]
    [SerializeField] private AudioSource _audioSourceMusic;


    //metodo para tocar effeitos sonoros
    public void PlaySoundEffect(AudioSource audioSource, int indexFx)
    {

        
        audioSource.clip = _listAudioFx[indexFx];
        audioSource.Play();
    }

     //metodo para tocar musica de fundo
    public void PlayBackgroundMusic(int musicIndex)
    {
        StopBackgroundMusic();
        _audioSourceMusic.clip = _listAudioMusic[musicIndex];
        _audioSourceMusic.loop = true;

        _audioSourceMusic.Play();
        _isPlayingMusic = true;
    }


    //metodo para parar a musica
    public void StopBackgroundMusic()
    {
        if(_isPlayingMusic)
        {
            _audioSourceMusic.Stop();
            _isPlayingMusic = false;
        }
    }


    //metodo para pause e/ou retonar a musica

    private void PauseAndUnpauseBackgroundMusic()
    {
        if(_isPlayingMusic == true)
        {
            _audioSourceMusic.Pause();
            _isPlayingMusic = false;
        }
        else
        {
            _audioSourceMusic.UnPause();
            _isPlayingMusic = true;
        }
        
    }

    private void MuteAudio()
    {
        //_audioSourceMusic.Mute(true);
    }

    public void UpdateMusicVolume(float newvolume)
    {
        _audioSourceMusic.volume = newvolume;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}