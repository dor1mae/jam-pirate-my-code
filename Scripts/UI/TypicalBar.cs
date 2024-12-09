public class TypicalBar : AbstractBar
{
    public override void ChangeValue(float value)
    {
        if(!(_imageFilled.fillAmount + value / 100 > 1 && _imageFilled.fillAmount + value / 100 < 0))
        {
            _imageFilled.fillAmount += value / 100;
        }
    }

    public override float GetValue()
    {
        return _imageFilled.fillAmount;
    }

    public override void SetValue(float value)
    {
        _imageFilled.fillAmount = value / 100;
    }
}
