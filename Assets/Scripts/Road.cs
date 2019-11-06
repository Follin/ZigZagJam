using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] GameObject _roadPrefab;
    [SerializeField] Vector3 _lastPosition;
    [SerializeField] float _offset = 0.71f;

    int _roadCount = 0;

    void CreateNewRoadPart()
    {
        Vector3 spawnPosition = Vector3.zero;
        float chance = Random.Range(0, 100);

        if (chance < 50)
            spawnPosition = new Vector3(_lastPosition.x + _offset, _lastPosition.y, _lastPosition.z + _offset);
        else
            spawnPosition = new Vector3(_lastPosition.x - _offset, _lastPosition.y, _lastPosition.z + _offset);

        GameObject newRoadPart = Instantiate(_roadPrefab, spawnPosition, Quaternion.Euler(0, 45f, 0));
        newRoadPart.transform.parent = gameObject.transform;
        _lastPosition = newRoadPart.transform.position;

        _roadCount++;

        if (_roadCount % 5 == 0)
            newRoadPart.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void StartBuilding()
    {
        InvokeRepeating("CreateNewRoadPart", 1f, 0.5f);
    }

}
