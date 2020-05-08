using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using InvsoftEngine.UI;

namespace InvsoftEditor.ExtensionEditor
{
    public class SwichExtensionEditor
    {
        [MenuItem("GameObject/UI/InvsoftEngine/Swich")]
        private static void InstantiateObject()
        {
            Transform canvas = GameObject.FindObjectOfType<Canvas>().transform;
            GameObject swichObj = CreateObject(canvas, "Swich");
            RectTransform swichRect = swichObj.GetComponent<RectTransform>();

            swichRect.anchoredPosition = Vector2.zero;
            swichRect.localScale = Vector2.one;
            swichRect.sizeDelta = new Vector2(50, 30);
        }

        private static GameObject CreateObject(Transform parent, string name)
        {
            GameObject swichObj = new GameObject(name, typeof(RectTransform), typeof(Swich));
            Swich swich = swichObj.GetComponent<Swich>();
            GameObject backgroundObj = new GameObject("Background", typeof(RectTransform), typeof(Image));
            RectTransform backgroundRect = backgroundObj.GetComponent<RectTransform>();
            Image backgroundImage = backgroundObj.GetComponent<Image>();
            GameObject fillObj = new GameObject("Fill", typeof(RectTransform), typeof(Image));
            RectTransform fillRect = fillObj.GetComponent<RectTransform>();
            Image fillImage = fillObj.GetComponent<Image>();
            GameObject handleAreaObj = new GameObject("Handle Area", typeof(RectTransform));
            RectTransform handleAreaRect = handleAreaObj.GetComponent<RectTransform>();
            GameObject handleObj = new GameObject("Handle", typeof(RectTransform), typeof(Image));
            RectTransform handleRect = handleObj.GetComponent<RectTransform>();

            swichObj.transform.SetParent(parent);
            fillObj.transform.SetParent(backgroundObj.transform);
            handleObj.transform.SetParent(handleAreaObj.transform);
            backgroundObj.transform.SetParent(swichObj.transform);
            handleAreaObj.transform.SetParent(swichObj.transform);

            backgroundImage.color = Color.gray;
            fillImage.color = Color.cyan;

            handleAreaRect.anchorMin = Vector2.zero;
            handleAreaRect.anchorMax = Vector2.one;
            handleAreaRect.offsetMin = new Vector2(10, 2.5f);
            handleAreaRect.offsetMax = new Vector2(-10, -2.5f);
            backgroundRect.anchorMin = Vector2.zero;
            backgroundRect.anchorMax = Vector2.one;
            backgroundRect.offsetMin = new Vector2(0, 5);
            backgroundRect.offsetMax = new Vector2(0, -5);
            fillRect.anchoredPosition = Vector2.zero;
            fillRect.sizeDelta = Vector2.zero;
            handleRect.anchoredPosition = Vector2.zero;
            handleRect.sizeDelta = new Vector2(20, 0);

            ColorBlock colorBlock = ColorBlock.defaultColorBlock;
            colorBlock.normalColor = new Color(1, 1, 1, 0);
            swich.colors = colorBlock;
            swich.fillRect = fillRect;
            swich.handleRect = handleRect;
            swich.value = false;

            return swichObj;
        }
    }
}
