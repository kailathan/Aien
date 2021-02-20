using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public enum World
    {
        Mora, 
        Dor, 
        Aeon,
        Ykjimr
    }
    public int worldIndex;
    public void LoadWorld() {
        PlayerData save = SaveSystem.LoadPlayer();
        worldIndex = save.currentWorld;

    }
    public World getWorld()
    {
        switch (worldIndex)
        {
            default:
            case 1: return World.Mora;
            case 2: return World.Dor;
            case 3: return World.Aeon;
            case 4: return World.Ykjimr;
        }
    }
}
