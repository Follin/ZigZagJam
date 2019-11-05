using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _target;

    Vector3 _offset;

    private void Awake()
    {
        _offset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        transform.position = _target.position + _offset;
    }
}
