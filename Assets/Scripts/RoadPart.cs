using UnityEngine;

public class RoadPart : MonoBehaviour
{
    bool _hasPassed = false;
    CollectableData _data;
    AudioClip _clip;

    private void Update()
    {
        if(_hasPassed)
        {
            transform.position += Vector3.down * 3f * Time.deltaTime;
            Destroy(gameObject, 2f);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))        
            _hasPassed = true;            
    }

    public void SetData(CollectableData data) => _data = data;
    public CollectableData GetData => _data;

    public void SetAudio(AudioClip clip) => _clip = clip;
    public AudioClip GetAudio => _clip;



}

