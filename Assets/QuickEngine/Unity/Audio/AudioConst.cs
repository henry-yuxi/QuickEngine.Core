using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioConst
{

}

public enum AudioType
{
    Music,
    Sound,
    Effect,
}

public enum AudioEffet
{
    None,//无效果
    FadeIn,//渐入
    FadeOut,//渐出
    Once,//播放一次删除 无淡入淡出
}
