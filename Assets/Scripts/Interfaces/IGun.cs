
public interface IGun: IPickeable
{
    public Gun GunData{ get; set; }
    public void Shoot();
    public int TotalBullets();
    public int CurrentBullets();
}
