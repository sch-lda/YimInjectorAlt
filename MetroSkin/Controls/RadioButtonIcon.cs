﻿namespace MetroSkin.Controls;

public class RadioButtonIcon : RadioButton
{
    /// <summary>
    /// Icon图标
    /// </summary>
    public string Icon
    {
        get { return (string)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(string), typeof(RadioButtonIcon), new PropertyMetadata("\xe610"));
}
