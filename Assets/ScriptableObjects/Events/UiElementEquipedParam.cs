using UnityEngine;

public class UiElementEquipedParam : UIEventParam
{
    private Sprite _itemSprite;

    public UiElementEquipedParam(Sprite sprite)
    {
        _itemSprite = sprite;
    }

    public Sprite ItemSpray()
    {
        return _itemSprite;
    }
}
