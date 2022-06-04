using UnityEngine;

public class UiElementEquipedParam : UIEventParam
{
    private Sprite _itemSprite;
    private int? _currentBullets;
    private int? _totalBullets;

    public UiElementEquipedParam(Sprite sprite, int? currentBullets, int? totalBullets, string itemName)
    {
        _itemSprite = sprite;
        _currentBullets = currentBullets;
        _totalBullets = totalBullets;
        _name = itemName;
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

    public string ItemName()
    {
        return _name;
    }
}
