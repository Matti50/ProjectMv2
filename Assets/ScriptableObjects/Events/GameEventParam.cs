using UnityEngine;

public class GameEventParam 
{
    public float Damage { get; set; }
    public Transform? PlayerPosition { get; set; }
    public float? PlayerSpeed { get; set; }

    public int? ZombieId;
}
