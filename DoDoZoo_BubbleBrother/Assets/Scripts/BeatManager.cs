using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 拍子管理器：生成拍子
/// </summary>
public class BeatManager : MonoBehaviour
{
    [Header("音樂資料")]
    public MusicData data;
    [Header("角色：要控制動畫的角色")]
    public Animator aniCharacter;
    [Header("拍子")]
    public GameObject goBeat;
    [Header("生成拍子位置")]
    public Transform traSpwanBeatPoint;
    [Header("檢查點擊位置與半徑")]
    public Transform traClickPosition;
    public float radiusClick = 3;

    private AudioSource aud;
    /// <summary>
    /// 生成的拍子位置
    /// </summary>
    private Transform traSpawnBead;

    private Button btnPushHand;
    private Button btnKickLeg;

    private void Start()
    {
        Initialize();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(traClickPosition.position, radiusClick);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Initialize()
    {
        aud = GetComponent<AudioSource>();
        Invoke("DelayStartMusic", data.delayStartMusic);

        StartCoroutine(SpawnBeat());

        btnPushHand = GameObject.Find("舉手").GetComponent<Button>();
        btnKickLeg = GameObject.Find("踢腿").GetComponent<Button>();

        btnPushHand.onClick.AddListener(() => { ClickCheck(TypeBeat.pushHand); });
        btnKickLeg.onClick.AddListener(() => { ClickCheck(TypeBeat.kickLeg); });
    }

    /// <summary>
    /// 生成拍子
    /// </summary>
    private IEnumerator SpawnBeat()
    {
        for (int i = 0; i < data.beats.Length; i++)
        {
            switch (data.beats[i])
            {
                case TypeBeat.none:
                    break;
                case TypeBeat.pushHand:
                    SettingBeat(TypeBeat.pushHand);
                    break;
                case TypeBeat.kickLeg:
                    SettingBeat(TypeBeat.kickLeg);
                    break;
            }

            yield return new WaitForSeconds(data.interval);
        }
    }

    /// <summary>
    /// 延遲開始音樂
    /// </summary>
    private void DelayStartMusic()
    {
        aud.volume = data.volume;
        aud.pitch = data.pitch;
        aud.clip = data.music;
        aud.Play();
    }

    /// <summary>
    /// 設定拍子
    /// </summary>
    /// <param name="type">拍子類型</param>
    private void SettingBeat(TypeBeat type)
    {
        Beat beat = Instantiate(goBeat, traSpwanBeatPoint.position, Quaternion.identity).GetComponent<Beat>();
        beat.typeBeat = type;
        beat.speed = data.speedBeat;
    }

    /// <summary>
    /// 點擊判定
    /// </summary>
    private void ClickCheck(TypeBeat type)
    {
        Collider2D hit = Physics2D.OverlapCircle(traClickPosition.position, radiusClick);

        if (hit)
        {
            float dis = Vector3.Distance(traClickPosition.position, hit.transform.position);
            TypeBeat typeBeat = hit.GetComponent<Beat>().typeBeat;

            if (dis < radiusClick)
            {
                if (typeBeat == type)
                {
                    switch (type)
                    {
                        case TypeBeat.pushHand:
                            aniCharacter.SetTrigger("舉手");
                            break;
                        case TypeBeat.kickLeg:
                            aniCharacter.SetTrigger("踢腿");
                            break;
                    }
                }
                else aniCharacter.SetTrigger("失敗");

                Destroy(hit.gameObject);
            }
        }
        else aniCharacter.SetTrigger("失敗");
    }
}
