
public class UILifeChangedParam : UIEventParam
{
    private float _ammountOfLifeChanged;

    public UILifeChangedParam(float ammount)
    {
        _ammountOfLifeChanged = ammount;
    }

    public float AmmountOfLifeChanged()
    {
        return _ammountOfLifeChanged;
    }
}
