// Make sure the platform is set to Android (to get mobile window)
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Speed")]
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _timerToIncreaseSpeed = 5;
    [SerializeField] private float _addedSpeed = 1;

    [Header("Other Settings")]
    //[SerializeField] GameObject _particleEffect;
    [SerializeField] private LayerMask _groundLayer;
    [Tooltip("Point the game restarts after player is falling")]
    [SerializeField] float _restartPoint = -2f;
    
    private GameManager _gameManager;
    private SoundManager _soundManager;
    private Rigidbody _rigidbody;
    private Animator _anim;
    private AudioSource _source;

    private bool _isWalkingright = true;
    private bool _isFalling = false;

    [HideInInspector] public bool IsDead = false;

    CapsuleCollider _collider;

    float _timer = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
        _source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _soundManager = FindObjectOfType<SoundManager>();
        IsDead = false;
    }

    private void FixedUpdate()
    {
        _anim.SetTrigger("startGame");
        _rigidbody.transform.position = transform.position + transform.forward * _speed * Time.deltaTime;
    }

    private void Update()
    {
        if (IsDead && _gameManager.StopUpdating) return;
        else if(transform.position.y < _restartPoint)        
            IsDead = true;

        IncreaseSpeed();                
        

        if (_isFalling) return;

        // For mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.tapCount > 0 && touch.phase == TouchPhase.Began)
                SwitchDirection();
        }

        // For PC
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            SwitchDirection();



        RaycastHit hit;
        if(!Physics.SphereCast(transform.position + Vector3.up, _collider.radius, Vector3.down, out hit, 1000, _groundLayer))    
        {
            _anim.SetTrigger("isFalling");
            _isFalling = true;
            _soundManager.IsFalling = true;
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
        if(!other.CompareTag("Collectible")) return;

        RoadPart roadPart = other.gameObject.GetComponentInParent<RoadPart>();

        if (roadPart.GetData.Score > 1)
            _source.pitch = Random.Range(1.5f, 2.5f);
        else
            _source.pitch = Random.Range(0.5f, 1f);

        _source.PlayOneShot(roadPart.GetAudio);
        _gameManager.IncreaseScore(roadPart.GetData.Score);

        GameObject particle = Instantiate(roadPart.GetData.Particles.gameObject, transform.position, Quaternion.identity);
        Destroy(particle, 2f);
        Destroy(other.gameObject);

    }

    void IncreaseSpeed()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timerToIncreaseSpeed)
        {
            _speed += (_addedSpeed * Time.deltaTime);
            _soundManager.AddPitch();
            _timer = 0;
        }
    }
}
