using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    private void Awake()
    {
        if (!_instance)
            _instance = this;

        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
