using UnityEngine;
using UnityEngine.SceneManagement;

public class ListenToShoot : MonoBehaviour
{
    private static int _shoot = Animator.StringToHash("pistolShot");
    private static int _died = Animator.StringToHash("isDead");

    private Animator _animator;

    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _damageClip;

    private AudioClip _defaultAudioClip;

    private LifeController _lifeController;

    [SerializeField]
    private UIEvent _changeUILifeEvent;

    [SerializeField]
    private GameEvent _playerDiedEvent;

    private bool _deathEventTriggered = false;

    private void Awake()
    {
        _lifeController = GetComponent<LifeController>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _defaultAudioClip = _audioSource.clip;
    }

    public void PlayShootAnimation()
    {
        _animator.SetTrigger(_shoot);
    }

    public void GetDamaged(GameEventParam param)
    {
        _lifeController.TakeDamage(param.Damage);
        _changeUILifeEvent.Raise(new UILifeChangedParam(-param.Damage));

        if (_lifeController.DidIDie())
        {
            _playerDiedEvent.Raise();
            _animator.SetBool(_died, true);
            gameObject.GetComponent<PlayerMovementController>().enabled = false;
            gameObject.GetComponent<PlayerRotationController>().enabled = false;
            gameObject.GetComponent<AimGun>().enabled = false;

            if(!_deathEventTriggered)
                _playerDiedEvent.Raise();


            if (Input.anyKey)
            {
                SceneManager.LoadScene("MainMenu");
            }

            return;
        }

        _audioSource.clip = _damageClip;
        _audioSource.Play();
    }

    private void OnCollisionExit(Collision collision)
    {
        //esto provoca el bug
        _audioSource.clip = _defaultAudioClip;
    }
}
