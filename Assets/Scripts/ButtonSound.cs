using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            GameObject sourceObj = new GameObject("UIClickAudio");
            sourceObj.transform.SetParent(Camera.main.transform);
            audioSource = sourceObj.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        GetComponent<Button>().onClick.AddListener(PlayClick);
    }

    private void PlayClick()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }
}
