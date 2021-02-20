using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;

    public static ItemWorld SpawnItemWorld(Vector2 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.prefabItemWorld, position, Quaternion.identity);
    
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    
    }

    public static ItemWorld DropItem(Vector2 dropPosition, Item item)
    {
        ItemWorld itemWorld = SpawnItemWorld(dropPosition, item);
        Vector2 toss = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.forward.z, 3f);
        itemWorld.GetComponent<Rigidbody2D>().velocity = toss;
        return itemWorld;
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Item GetItem()
    {
        return item;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
