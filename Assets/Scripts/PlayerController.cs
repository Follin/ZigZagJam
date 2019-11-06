using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _fallCheckStartLeft;
    [SerializeField] Transform _fallCheckStartRight;

    [Tooltip("Point the game restarts after player is falling")]
    [SerializeField] float _restartPoint = -2f;
    [SerializeField] private float _speed = 1;


    [SerializeField] GameObject _particleEffect;

    private GameManager _gameManager;
    private Rigidbody _rigidbody;
    private Animator _anim;

    private bool _isWalkingright = true;
    private bool _isFalling = false;

    CapsuleCollider _collider;

    [SerializeField] private LayerMask _groundLayer;


    private void Awake()
    {
        if(!_particleEffect) Debug.LogError("No particle effect in " + this);        

        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {       
        _anim.SetTrigger("startGame");
        _rigidbody.transform.position = transform.position + transform.forward * _speed * Time.deltaTime;
    }

    private void Update()
    {
        if (transform.position.y < _restartPoint)
            _gameManager.RestartGame();

        if (_isFalling) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            SwitchDirection();

        RaycastHit hit;
        if(!Physics.SphereCast(transform.position + Vector3.up, _collider.radius, Vector3.down, out hit, 1000, _groundLayer))    
        {
            _anim.SetTrigger("isFalling");
            _isFalling = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Collectable"))
        {
            _gameManager.IncreaseScore();

            GameObject particle = Instantiate(_particleEffect, transform.position, Quaternion.identity);
            Destroy(particle, 2f);
            Destroy(other.gameObject);
        }
    }
}
