using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    private readonly float defaultFontSize = 24;

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
        textMesh.fontSize = this.defaultFontSize;
        textMesh.color = Color.white;
        ObjectPooling.Instance.ReturnObject(gameObject);
    }

    /// <summary>
    /// value < 0 => take damage, value > 0 => healing HP
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isCrit"></param>
    /// <returns></returns>
    public FloatingText SetText(float value, bool isCrit = false)
    {
        if (value > 0)
        {
            SetColor(Color.green);
        }
        else if (isCrit)
        {
            SetColor(Color.red);
            SetFontSize(2);
        }

        textMesh.text = Mathf.Abs((int)value).ToString();

        return this;
    }

    public void SetColor(Color color) => textMesh.color = color;
    public void SetFontSize(float fontSize) => textMesh.fontSize *= fontSize;


    public static FloatingText Instantiate(GameObject floatingText, Vector3 position)
    {
        FloatingText t = ObjectPooling.Instance
            .GetObject(floatingText).GetComponent<FloatingText>();
        t.transform.position = position;
        t.OnInstantiate();
        return t;
    }
}
