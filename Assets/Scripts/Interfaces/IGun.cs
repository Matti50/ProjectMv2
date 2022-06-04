using UnityEngine;

public interface IGun: IPickeable
{
    public Gun GunData{ get; set; }
    public void Shoot();
    public int TotalBullets();
    public int CurrentBullets();
    public void SetPlayerPosition(Transform playerPosition);
    public void SetPlayerSpeed(float playerSpeed);
}
