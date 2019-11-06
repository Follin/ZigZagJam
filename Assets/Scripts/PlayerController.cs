using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _fallCheckStart;
    [Tooltip("Point the game restarts after player is falling")]
    [SerializeField] float _restartPoint = -2f;

    [SerializeField] GameObject _particleEffect;

    private Rigidbody _rigidbody;
    private Animator _anim;

    private bool _isWalkingright = true;

    private GameManager _gameManager;

    private void Awake()
    {
        if(!_particleEffect) Debug.LogError("No particle effect in " + this);        

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
            _anim.SetTrigger("isFalling");

        if (transform.position.y < _restartPoint)
            _gameManager.EndGame();
    }

    private void SwitchDirection()
    {
        if (!_gameManager.HasStarted) return;

        _isWalkingright = !_isWalkingright;

        if (_isWalkingright)
            transform.rotation = Quaternion.Euler(0, 45f, 0);
        else
            transform.rotation = Quaternion.Euler(0, -45f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Collectable")
        {
            _gameManager.IncreaseScore();

            GameObject particle = Instantiate(_particleEffect, _fallCheckStart.transform.position, Quaternion.identity);
            Destroy(particle, 2f);
            Destroy(other.gameObject);
        }
    }
}
