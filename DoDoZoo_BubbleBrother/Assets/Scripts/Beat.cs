using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 拍子
/// </summary>
public class Beat : MonoBehaviour
{
    /// <summary>
    /// 拍子類型
    /// </summary>
    public TypeBeat typeBeat;
    /// <summary>
    /// 移動速度
    /// </summary>
    public float speed = 1;
    
    /// <summary>
    /// 拍子文字：子物件 拍子畫布 的 子物件
    /// </summary>
    private Text textBeat;

    private void Start()
    {
        UpdateText();
    }

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// 更新拍子文字
    /// </summary>
    private void UpdateText()
    {
        textBeat = transform.GetChild(0).GetChild(0).GetComponent<Text>();

        switch (typeBeat)
        {
            case TypeBeat.none:
                break;
            case TypeBeat.pushHand:
                textBeat.text = "舉手";
                break;
            case TypeBeat.kickLeg:
                textBeat.text = "踢腿";
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }
}

/// <summary>
/// 拍子類型：無、舉手、踢腿
/// </summary>
public enum TypeBeat
{
    none, pushHand, kickLeg
}