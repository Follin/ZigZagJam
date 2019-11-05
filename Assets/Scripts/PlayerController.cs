using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _fallCheckStart;

    private Rigidbody _rigidbody;
    private Animator _anim;

    private bool _isWalkingright = true;

    private GameManager _gameManager;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        if (!_gameManager.HasStarted) return;

       
        _anim.SetTrigger("startGame");
        _rigidbody.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SwitchDirection();

        RaycastHit hit;
        if (!Physics.Raycast(_fallCheckStart.position, -transform.up, out hit, Mathf.Infinity))
        {
            _anim.SetTrigger("isFalling");
        }


    }

    private void SwitchDirection()
    {
        _isWalkingright = !_isWalkingright;

        if (_isWalkingright)
            transform.rotation = Quaternion.Euler(0, 45f, 0);
        else
            transform.rotation = Quaternion.Euler(0, -45f, 0);
    }
}
