using UnityEngine;

public class ListenToShoot : MonoBehaviour
{
    private static int _shoot = Animator.StringToHash("pistolShot");

    private Animator _animator;

    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _damageClip;

    private AudioClip _defaultAudioClip;

    private LifeController _lifeController;

    [SerializeField]
    private UIEvent _changeUILifeEvent;

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
        _audioSource.clip = _damageClip;
        _audioSource.Play();
    }

    private void OnCollisionExit(Collision collision)
    {
        //esto provoca el bug
        _audioSource.clip = _defaultAudioClip;
    }
}
