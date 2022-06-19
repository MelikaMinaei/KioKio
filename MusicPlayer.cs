using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public Button musicButton;
    public Sprite offMusic;
    public Sprite onMusic;

    void Awake()
    {
        SetUpSing();
    }

    private void SetUpSing()
    {
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void TurnOffOnMusic(Image image)
    {
        bool music = FindObjectOfType<AudioSource>().mute;
        
        if (music == true)
        {
            image.sprite = offMusic;
        }
        else
        {
            image.sprite = onMusic;
        }
        FindObjectOfType<AudioSource>().mute = !music;
    }
}
