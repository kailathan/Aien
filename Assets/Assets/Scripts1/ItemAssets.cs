using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{

    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform prefabItemWorld;

    public Sprite swordSprite;
    public Sprite bowSprite;
    public Sprite potSprite;
    public Sprite gunSprite;
    public Sprite armorSprite;
}
