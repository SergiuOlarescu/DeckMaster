using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public List<AudioClip> playlist;
    public Dropdown musicDropdown;
    public Slider musicVolumeSlider;
    public AudioSource musicSource;

    private int currentTrackIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
    }

    private void Start()
    {
        PopulateDropdown();

     
        float savedVolume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        musicSource.volume = savedVolume;
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.AddListener(SetVolume);
            musicVolumeSlider.value = savedVolume;
        }

        
        currentTrackIndex = PlayerPrefs.GetInt("musicTrackIndex", 0);
        PlayTrack(currentTrackIndex);
        if (musicDropdown != null)
        {
            musicDropdown.onValueChanged.AddListener(OnDropdownChanged);
            musicDropdown.value = currentTrackIndex;
        }
    }


    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            NextTrack();
        }
    }

    public void PlayTrack(int index)
    {
        if (index < 0 || index >= playlist.Count) return;

        currentTrackIndex = index;
        musicSource.clip = playlist[index];
        musicSource.Play();
    }

    public void OnDropdownChanged(int index)
    {
        PlayTrack(index);
    }

    private void NextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % playlist.Count;
        PlayTrack(currentTrackIndex);
        musicDropdown.value = currentTrackIndex;
    }

    private void PopulateDropdown()
    {
        musicDropdown.ClearOptions();
        List<string> trackNames = new List<string>();

        foreach (var clip in playlist)
        {
            trackNames.Add(clip.name);
        }

        musicDropdown.AddOptions(trackNames);
        musicDropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    public void SetVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("musicVolume", value);
    }
}
