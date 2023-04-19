﻿namespace MetroSkin.Controls;

public class LabelIcon : Label
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
        DependencyProperty.Register("Icon", typeof(string), typeof(LabelIcon), new PropertyMetadata("\xe610"));
}
