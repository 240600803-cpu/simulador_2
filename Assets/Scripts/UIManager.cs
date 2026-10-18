using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Image chargeBar;
    public Text chargeText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetCharge(float value, float min, float max)
    {
        if (chargeBar != null)
        {
            chargeBar.fillAmount = Mathf.InverseLerp(min, max, value);
        }

        if (chargeText != null)
        {
            chargeText.text = Mathf.RoundToInt(value).ToString();
        }
    }
}
