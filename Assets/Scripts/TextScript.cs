using UnityEngine;
using TMPro;

public class TextScript : MonoBehaviour
{
    public Color outlineColor = Color.black;
    public float outlineWidth = 0.1f;

    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Adiciona o componente de contorno ao Text Mesh Pro
        textMeshPro.outlineColor = outlineColor;
        textMeshPro.outlineWidth = outlineWidth;
    }
}
