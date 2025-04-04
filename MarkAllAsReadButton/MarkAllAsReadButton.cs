using Il2CppScheduleOne.PlayerScripts;
using Il2CppScheduleOne.UI.Phone.Messages;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MarkAllAsReadButton
{
    internal static class MarkAllAsReadButton
    {
        private const string MessagesHeaderPath = "Container/Home/Header";
        private const string MessagesPath = "/Player_Local/CameraContainer/Camera/OverlayCamera/GameplayMenu/Phone/phone/AppsCanvas/Messages";
        private static MessagesApp messagesApp;
        public static void Add(Scene mainScene)
        {
            var player = Player.Local;

            Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<GameObject> rootObjs = mainScene.GetRootGameObjects();
            GameObject playerGO = rootObjs.FirstOrDefault(x => x.gameObject.name.Contains(player.name));

            var messages = playerGO?.transform.FindByPath(MessagesPath);
            var header = messages.FindByPath(MessagesHeaderPath);
            messagesApp = messages.GetComponent<MessagesApp>();

            if (header != null)
            {
                CreateButton(header);
            }
        }

        private static void CreateButton(Transform header)
        {
            GameObject buttonGO = new GameObject("MarkAllAsReadButton");
            buttonGO.transform.SetParent(header, false);

            RectTransform rect = buttonGO.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(250, 50);
            rect.anchoredPosition = new Vector2(150, -5);

            buttonGO.AddComponent<CanvasRenderer>();

            var image = buttonGO.AddComponent<Image>();
            image.color = new Color(1f, 1f, 1f, 0.9f);
            image.sprite = Resources.FindObjectsOfTypeAll<Sprite>().FirstOrDefault(x => x.name == "Rectangle_RoundedEdges");
            image.type = Image.Type.Sliced;
            image.pixelsPerUnitMultiplier = 25;
            image.m_ShouldRecalculateStencil = false;
            image.color = new Color(0.389f, 0.8019f, 0.3911f);

            var button = buttonGO.AddComponent<Button>();

            button.onClick.AddListener((UnityEngine.Events.UnityAction)OnMarkAllAsReadClicked);

            GameObject textGO = new GameObject("Text");
            textGO.transform.SetParent(buttonGO.transform, false);

            RectTransform textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0, 0);
            textRect.anchorMax = new Vector2(1, 1);
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            textGO.AddComponent<CanvasRenderer>();

            Text text = textGO.AddComponent<Text>();
            text.text = "Mark All As Read";
            text.alignment = TextAnchor.MiddleCenter;
            text.fontSize = 20;
            text.color = Color.black;

            var existingText = header.GetComponent<Text>();
            if (existingText != null)
            {
                text.font = existingText.font;
            }
            else
            {
                text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            }
        }

        private static void OnMarkAllAsReadClicked()
        {
            if (messagesApp != null)
            {
                var unreadConvs = messagesApp.unreadConversations.ToArray();
                foreach (var conv in unreadConvs)
                {
                    conv.SetRead(true);
                }
            }
        }
    }
}
