using UnityEngine;

public interface IPickeable
{
    public void GetPickedUp();
    public GameObject GetItem { get; }
    public (Vector3, Vector3) GetOkRotationAndPosition();

    public Sprite GetImage();

    public string GetName();
}
