using UnityEngine;

public class ListenToShoot : MonoBehaviour
{
    private static int _shoot = Animator.StringToHash("pistolShot");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayShootAnimation()
    {
        _animator.SetTrigger(_shoot);
    }
}
