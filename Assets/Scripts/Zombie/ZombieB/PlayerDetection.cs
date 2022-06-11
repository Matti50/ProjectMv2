using System;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField]
    private Transform _rayOriginTransform;

    public event Action<Transform, float> OnPlayerDetected;

    private bool _playerDetected;

    private void Start()
    {
        _playerDetected = false;
    }

    private void FixedUpdate()
    {
        if (_playerDetected) return;

        Ray ray = new Ray(_rayOriginTransform.position, _rayOriginTransform.forward);

        if (Physics.Raycast(ray,out RaycastHit hitInfo,100f,LayerMask.GetMask("Player")))
        {
            _playerDetected = true;
            OnPlayerDetected.Invoke(hitInfo.transform, hitInfo.collider.gameObject.GetComponent<PlayerMovementController>().GetSpeed());
        }
    }

    public void Enable()
    {
        this.enabled = true;
    }
}
