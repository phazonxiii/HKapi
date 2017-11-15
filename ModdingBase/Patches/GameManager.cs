﻿using MonoMod;

namespace Modding.Patches
{

    [MonoModPatch("global::GameManager")]
    public class GameManager : global::GameManager
    {

        /*TODO: SaveGame(int)
            Add this right after SaveGameData saveGameData = new SaveGameData(this.playerData, this.sceneData);
            	Modding.ModHooks.Instance.OnBeforeSaveGameSave(obj);
            Add this after string text4 = JsonUtility.ToJson(saveGameData, !this.gameConfig.useSaveEncryption);
				Modding.ModHooks.Logger.LogFine("[API] - About to Serialize Save Data\n" + text4);
            
            Add this right before the return after the try:
      			Modding.ModHooks.Instance.OnSavegameSave(saveSlot);
        */

        /*TODO: LoadGame(int)
           Add this right after SceneData instance2 = saveGameData.sceneData;
                Modding.ModHooks.Instance.OnAfterSaveGameLoad(saveGameData);
           Add this right after this.inputHandler.RefreshPlayerData();
				Modding.ModHooks.Instance.OnSavegameLoad(saveSlot);
        */
        public void orig_OnApplicationQuit() { }

        public void OnApplicationQuit()
        {
            orig_OnApplicationQuit();
            ModHooks.Instance.OnApplicationQuit();
        }

        public void orig_LoadScene(string destScene) { }

        public void LoadScene(string destScene)
        {
            destScene = ModHooks.Instance.BeforeSceneLoad(destScene);
            orig_LoadScene(destScene);
            ModHooks.Instance.OnSceneChanged(destScene);
        }

        public string orig_GetSaveFilename(int saveSlot) { return string.Empty; }

        public string GetSaveFilename(int saveSlot)
        {
            string saveFileName = ModHooks.Instance.GetSaveFileName(saveSlot);
            if (!string.IsNullOrEmpty(saveFileName))
            {
                return saveFileName;
            }
            return orig_GetSaveFilename(saveSlot);
        }

        public void orig_ClearSaveFile(int saveSlot) { }

        public void ClearSaveFile(int saveSlot)
        {
    		ModHooks.Instance.OnSavegameClear(saveSlot);
            orig_ClearSaveFile(saveSlot);
            ModHooks.Instance.OnAfterSaveGameClear(saveSlot);
        }


    }
}
