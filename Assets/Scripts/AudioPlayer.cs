using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip _gameMusic;
    [SerializeField] AudioClip _winMusic;
    AudioSource _audioSource;
    Quiz _quiz;

    private void Awake()
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        _quiz = FindObjectOfType<Quiz>();
        EndScreen.onQuizCompleted += PlayWinMusic;
    }

    private void Start()
    {
        _audioSource.clip = _gameMusic;
        _audioSource.Play();
    }

    private void PlayWinMusic()
    {
        if (_quiz.isComplete)
        {
            if (_audioSource == null)
                return;

            _audioSource.clip = _winMusic;
            _audioSource.Play();
        }
    }
}
