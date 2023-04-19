﻿using GTA5Shared.Helper;

namespace YimInjectorAlt.Views.ReadMe;

/// <summary>
/// LSCHax.xaml 的交互逻辑
/// </summary>
public partial class LSCHax : UserControl
{
    public LSCHax()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 超链接请求导航事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessHelper.OpenLink(e.Uri.OriginalString);
        e.Handled = true;
    }
}
