using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    static SoundManager _instance;

    [SerializeField] float _pitchToAdd = 0.01f;
    [SerializeField] AudioMixer _mixer;
    [SerializeField] float _orginalPitch = 0.9f;

    [HideInInspector] public bool IsFalling = false;

    float _currentPitch;
    private void Awake()
    {
        if (!_instance)
            _instance = this;

        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _currentPitch = _orginalPitch;

        _mixer.SetFloat("MyPitch", _currentPitch);
    }

    private void Update()
    {
        if (IsFalling)
        {
            _currentPitch -= (_pitchToAdd * Time.deltaTime);
            _mixer.SetFloat("MyPitch", _currentPitch);
        }
        else
            _currentPitch = _orginalPitch;
    }

    public void AddPitch()
    {
        _currentPitch += (_pitchToAdd * Time.deltaTime);
        _mixer.SetFloat("MyPitch", _currentPitch);

    }

    public void ResetPitch()
    {
        _currentPitch = _orginalPitch;
        _mixer.SetFloat("MyPitch", _currentPitch);
    }   

}
