using UnityEngine;

public class DoorLeverController : MonoBehaviour
{
    [SerializeField]
    private GameEvent _leverTriggered;

    private Animator _animator;

    private int _triggerLeverId = Animator.StringToHash("LeverActivate");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            if (Input.GetKey(KeyCode.E))
            {
                _animator.SetBool(_triggerLeverId, true);
                _leverTriggered.Raise();
                this.enabled = false;
            }
        }
    }
}
