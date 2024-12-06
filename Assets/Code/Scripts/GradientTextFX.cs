using System.Collections;
using TMPro;
using UnityEngine;

public class RainbowText_V1 : MonoBehaviour
{
    public static RainbowText_V1 main;

    [SerializeField] private Gradient textGradient;
    [SerializeField] private float gradientSpeed = .1f;

    private TMP_Text m_TextComponent;
    private float _totalTime;

    void Awake()
    {
        main = this;
        m_TextComponent = GetComponent<TMP_Text>();
    }

    void Start()
    {
        StartCoroutine(AnimateVertexColors());
    }

    IEnumerator AnimateVertexColors()
    {
        m_TextComponent.ForceMeshUpdate();

        TMP_TextInfo textInfo = m_TextComponent.textInfo;
        int currentCharacter = 0;

        Color32[] newVertexColors;
        Color32 color0 = m_TextComponent.color;

        while (true)
        {
            int characterCount = textInfo.characterCount;

            if (characterCount == 0)
            {
                yield return new WaitForSeconds(0.25f);
                continue;
            }

            int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;

            newVertexColors = textInfo.meshInfo[materialIndex].colors32;

            int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;

            if (textInfo.characterInfo[currentCharacter].isVisible)
            {
                var offset = currentCharacter / characterCount;
                color0 = textGradient.Evaluate((_totalTime + offset) % 1f);
                _totalTime += Time.deltaTime;

                newVertexColors[vertexIndex + 0] = color0;
                newVertexColors[vertexIndex + 1] = color0;
                newVertexColors[vertexIndex + 2] = color0;
                newVertexColors[vertexIndex + 3] = color0;



                m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }

            currentCharacter = (currentCharacter + 1) % characterCount;

            yield return new WaitForSeconds(gradientSpeed);
        }
    }
}