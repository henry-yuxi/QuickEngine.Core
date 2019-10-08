public class AudioConstants
{
    public const int MaxAudioCount = 10;
}

public enum AudioType
{
    Music,
    Sound,
    Effect,
    UISound,
}

public enum AudioEffect
{
    None,//无效果
    FadeIn,//渐入
    FadeOut,//渐出
    Once,//播放一次删除 无淡入淡出
}