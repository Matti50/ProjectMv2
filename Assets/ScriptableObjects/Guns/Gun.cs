using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Guns/Gun", fileName = "New Gun")]
public class Gun : Pickeable
{
    public int ShootDamage;
    public int ShotDistance;
    public float Recoil;
    public int BulletsInMagazine;
    public GameObject GunPrefab;
}
