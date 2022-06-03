using UnityEngine;

public class UiElementEquipedParam : UIEventParam
{
    private Sprite _itemSprite;
    private int? _currentBullets;
    private int? _totalBullets;

    public UiElementEquipedParam(Sprite sprite, int? currentBullets, int? totalBullets)
    {
        _itemSprite = sprite;
        _currentBullets = currentBullets;
        _totalBullets = totalBullets;
    }

    public Sprite ItemSpray()
    {
        return _itemSprite;
    }

    public int? CurrentBullets()
    {
        return _currentBullets;
    }

    public int? TotalBullets()
    {
        return _totalBullets;
    }
}
