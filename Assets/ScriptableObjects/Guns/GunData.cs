using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Guns/GunData", fileName = "NewGunData")]
public class GunData : ScriptableObject
{
    public string ItemName;
    public int ShootDamage;
    public int ShotDistance;
    public int Recoil;
    public int BulletsInMagazine;
    public GameObject GunPrefab;
}
