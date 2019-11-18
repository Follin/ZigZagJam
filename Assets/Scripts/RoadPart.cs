using UnityEngine;

public class RoadPart : MonoBehaviour
{
    bool _hasPassed = false;
    public CollectableData Data;

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

    public void SetData(CollectableData data) => Data = data;
    public CollectableData GetData => Data;
    
}

