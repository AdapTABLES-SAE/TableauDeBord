using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

public enum Item_ID
{
    ARMOR = 0,
    HELMET = 1,
    PANTS = 2,
    SHIELD = 3,
    SHOULDERS = 4,
    BAG = 5,
    LANTERN = 6,
    COMPAS = 7,
    BOOTS = 8,
    HP = 9,
    BRACELET = 10,
    BELT = 11,
    GLOVE = 12,
    KEYS = 13,
    COAT = 14,
    CARROT = 15
}

public class Item
{
    public Item_ID id;
    //public string name;
    public bool isBought;
    public bool isActivated;

    public Item(ItemJSON itemJSON)
    {
        Enum.TryParse<Item_ID>(itemJSON.id, out id);
        isActivated = itemJSON.isActivated;
        isBought = itemJSON.isBought;
    }

}

public class ItemJSON
{
    public string id;
    public bool isBought;
    public bool isActivated;

    public ItemJSON()
    {
        //this.id = "";
        //isBought = false;
        //isActivated = false;
    }

    public ItemJSON(Item_ID id)
    {
        this.id = id.ToString();
        //name = id.ToString();
        isBought = false;
        isActivated = false;
    }

    public ItemJSON(Item item)
    {
        this.isBought = item.isBought;
        this.isActivated = item.isActivated;
        id = item.id.ToString();
    }
}

public class EquipmentsJSON
{
    public string learnerID;
    public int usedCoins;
    public ItemJSON[] items;

    public EquipmentsJSON()
    {
        learnerID = EleveClass.studentChosen.idStudent;
        //classroomID = StudentLogged.instance.classroomId;
        items = new ItemJSON[Enum.GetNames(typeof(Item_ID)).Length];
        int index = 0;
        foreach (Item_ID id in Enum.GetValues(typeof(Item_ID)))
        {
            items[index] = new ItemJSON(id);
            index++;
        }


    }

    public EquipmentsJSON(Equipments items)
    {
        this.learnerID = EleveClass.studentChosen.idStudent;
        //this.classroomID = StudentLogged.instance.classroomId;

        this.items = new ItemJSON[items.items.Length];
        int index = 0;
        foreach (Item item in items.items)
        {
            this.items[index++] = new ItemJSON(item);
        }

    }
}

[Serializable]
public class BuyableItem
{
    public Item item;
    public Item_ID type;
    public string name;
    public string description;
    public int cost;
    public Sprite image;
}

public class Equipments
{
    public Item[] items;
    [NonSerialized]
    public bool internetError;

    public Equipments(EquipmentsJSON equipsJSON, BuyableItem[] buyableItems)
    {
        items = new Item[buyableItems.Length];
        int index = 0;
        foreach (BuyableItem toBuyItem in buyableItems)
        {
            ItemJSON mappedItemJSON = mappingItemJson(equipsJSON, toBuyItem.type);
            if (mappedItemJSON == null)
                mappedItemJSON = new ItemJSON(toBuyItem.type);
            items[index++] = new Item(mappedItemJSON);
        }

    }

    private ItemJSON mappingItemJson(EquipmentsJSON equipsJSON, Item_ID type)
    {
        foreach (ItemJSON jSON in equipsJSON.items)
        {
            if (jSON.id.Equals(type.ToString()))
                return jSON;
        }
        return null;
    }

    internal bool Bought(Item_ID id)
    {
        foreach (Item item in items)
        {
            if (item.id == id)
                return item.isBought && item.isActivated;
        }
        return false;
    }
}


