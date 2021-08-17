using UnityEngine;

[CreateAssetMenu(menuName = "KID/音樂資料")]
/// <summary>
/// 音樂資料：決定音樂與音樂對應的拍子
/// </summary>
public class MusicData : ScriptableObject
{
    [Header("音樂")]
    /// <summary>
    /// 音樂
    /// </summary>
    public AudioClip music;
    [Header("音量")]
    /// <summary>
    /// 音量
    /// </summary>
    public float volume = 1;
    [Header("音調")]
    /// <summary>
    /// 音調
    /// </summary>
    public float pitch = 1;
    [Header("音樂延遲開始時間")]
    /// <summary>
    /// 音樂延遲開始時間
    /// </summary>
    public float delayStartMusic = 0;
    [Header("拍子間隔")]
    /// <summary>
    /// 拍子間隔
    /// </summary>
    public float interval = 0.5f;
    [Header("拍子速度")]
    /// <summary>
    /// 拍子速度
    /// </summary>
    public float speedBeat = 10;
    [Header("拍子")]
    /// <summary>
    /// 拍子
    /// </summary>
    public TypeBeat[] beats;
}
