using UnityEngine;
using UnityEngine.UI;
using InvsoftEngine.UI;
using UnityEditor;

namespace InvsoftEditor.ExtensionEditor
{
    public class ProgressBarExtensionEditor
    {
        [MenuItem("GameObject/UI/InvsoftEngine/Progress Bar")]
        private static void InstantiateObject()
        {
            Transform canvas = GameObject.FindObjectOfType<Canvas>().transform;
            GameObject progressBar  = CreateObject(canvas, "Progress Bar");
            RectTransform rectTransformPB = progressBar.GetComponent<RectTransform>();

            rectTransformPB.anchoredPosition = Vector2.zero;
            rectTransformPB.localScale = Vector3.one;
            rectTransformPB.sizeDelta = new Vector2(100, 30);
        }

        private static void SetAnchor(RectTransform obj, Vector2 min, Vector2 max)
        {
            obj.anchorMax = max;
            obj.anchorMin = min;
        }

        private static GameObject CreateObject(Transform parent, string name)
        {
            GameObject pbObject = new GameObject(name, typeof(RectTransform), typeof(ProgressBar));
            ProgressBar progressBar = pbObject.GetComponent<ProgressBar>();
            Transform transformPB = pbObject.transform;

            GameObject fillObject = new GameObject("Fill", typeof(RectTransform), typeof(Image));
            Image fill = fillObject.GetComponent<Image>();
            fill.sprite = Resources.Load<Sprite>("Sprites/UI/Background");
            fill.type = Image.Type.Filled;
            fill.color = Color.green;
            fill.fillAmount = 0;
            SetAnchor(fill.rectTransform, new Vector2(0, 0), new Vector2(1, 1));

            GameObject textObject = new GameObject("Text", typeof(RectTransform), typeof(Text));
            Text text = textObject.GetComponent<Text>();
            text.alignment = TextAnchor.MiddleCenter;
            SetAnchor(text.rectTransform, new Vector2(0, 0), new Vector2(1, 1));

            progressBar.imageFill = fill;
            progressBar.text = text;

            fill.transform.SetParent(transformPB);
            text.transform.SetParent(transformPB);
            transformPB.SetParent(parent);

            return pbObject;
        }
    }
}
