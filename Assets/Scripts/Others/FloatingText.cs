using TMPro;
using UnityEngine;

public enum DamageType
{
    Nomal,
    Crit,
    Healing,
    Poisoned,
    Blaze,
    Freeze,
    Bolt
}

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    private readonly float defaultFontSize = 24;
    private static readonly Color poisonedColor = new(1, 55 / 255f, 1, 200 / 255f);
    private static readonly Color blazeColor = new(1, 155 / 255f, 55 / 255f, 200 / 255f);
    private static readonly Color FreezeColor = new(85 / 255f, 195 / 255f, 1, 200 / 255f);

    public void OnInstantiate()
    {
        float randomX = Random.Range(-0.33f, 0.33f);
        float randomY = Random.Range(0.45f, 0.79f);
        transform.position =
            new Vector2(transform.position.x + randomX, transform.position.y + randomY);

        Invoke(nameof(Die), 0.5f);
    }

    public void Die()
    {
        ObjectPooling.Instance.ReturnObject(gameObject);
    }

    /// <summary>
    /// value < 0 => take damage, value > 0 => healing HP
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isCrit"></param>
    /// <returns></returns>
    public FloatingText SetText(string value, DamageType type = DamageType.Nomal)
    {
        textMesh.text = value;
        switch (type)
        {
            case DamageType.Nomal:
                SetColor(Color.white);
                SetFontSize(1);
                break;
            case DamageType.Crit:
                SetColor(Color.red);
                SetFontSize(2);
                break;

            case DamageType.Healing:
                SetColor(Color.green);
                break;

            case DamageType.Poisoned:
                SetColor(poisonedColor);
                SetFontSize(0.8f);
                break;

            case DamageType.Blaze:
                SetColor(blazeColor);
                SetFontSize(0.8f);
                break;

            case DamageType.Freeze:
                SetColor(FreezeColor);
                SetFontSize(0.8f);
                break;
            case DamageType.Bolt:
                SetColor(Color.yellow);
                SetFontSize(0.8f);
                break;

            default:
                break;
        }

        return this;
    }

    public void SetColor(Color color) => textMesh.color = color;
    public void SetFontSize(float fontSize) => textMesh.fontSize = defaultFontSize * fontSize;


    public static FloatingText Instantiate(Vector3 position)
    {
        FloatingText t = ObjectPooling.Instance
            .GetObject(ObjectPoolingType.FloatingText).GetComponent<FloatingText>();
        t.transform.position = position;
        t.OnInstantiate();
        return t;
    }
}
