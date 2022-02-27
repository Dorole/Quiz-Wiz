using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteAudio : MonoBehaviour
{
    private Image _image;
    [SerializeField] private Button _muteButton;
    [SerializeField] private Sprite _soundOnIcon;
    [SerializeField] private Sprite _soundOffIcon;
    private bool _muted = false;

    private void Awake()
    {
        _image = _muteButton.GetComponent<Image>();
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
            Load();

        AudioListener.pause = _muted;
        UpdateButtonIcon();
    }

    public void OnButtonPress()
    {
        _muted = !_muted;
        AudioListener.pause = _muted;

        Save();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (!_muted)
            _image.sprite = _soundOnIcon;
        else
            _image.sprite = _soundOffIcon;
    }

    private void Load()
    {
        _muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", _muted ? 1 : 0); 
    }
}
