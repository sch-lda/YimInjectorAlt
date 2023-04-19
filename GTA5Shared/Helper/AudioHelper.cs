namespace GTA5Shared.Helper;

public static class AudioHelper
{


    /// <summary>
    /// 点击提示音索引
    /// </summary>
    public static Audio ClickAudio { get; set; } = Audio.None;

    /// <summary>
    /// 播放点击音效
    /// </summary>
    public static void PlayClickSound()
    {
        switch (ClickAudio)
        {
           
        }
    }

    public enum Audio
    {
        None,
 
    }
}
