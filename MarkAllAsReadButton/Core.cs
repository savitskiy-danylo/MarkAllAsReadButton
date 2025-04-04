using Il2CppScheduleOne.PlayerScripts;
using MelonLoader;
using UnityEngine.SceneManagement;

[assembly: MelonInfo(typeof(MarkAllAsReadButton.Core), "MarkAllAsReadButton", "1.0.0", "Squonk", null)]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace MarkAllAsReadButton
{
    public class Core : MelonMod
    {
        private bool isMainSceneInited, isButtonAdded;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName.Contains("Main"))
            {
                isMainSceneInited = true;
            }
        }

        public override void OnUpdate()
        {
            if (isMainSceneInited && !isButtonAdded)
            {
                var player = Player.Local;
                if (player != null)
                {
                    var currentScene = SceneManager.GetActiveScene();
                    var isPlayerSpawned = currentScene.GetRootGameObjects().Any(x => x.name.Contains(player.name));
                    if (isPlayerSpawned)
                    {
                        MarkAllAsReadButton.Add(currentScene);
                        isButtonAdded = true;
                    }
                }
            }
        }
    }
}