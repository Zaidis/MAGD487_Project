using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;

    public List<Item> level_one = new List<Item>();
    public List<Item> level_two = new List<Item>();
    public List<Item> level_three = new List<Item>();
    public List<Item> level_four = new List<Item>();
    public List<Item> level_five = new List<Item>();
    public List<Item> level_six = new List<Item>();
    public List<Item> level_seven = new List<Item>();
    public List<Item> level_eight = new List<Item>();
    public List<Item> level_nine = new List<Item>();
    public List<Item> level_ten = new List<Item>();
    public List<Item> level_eleven = new List<Item>();
    public List<Item> level_twelve = new List<Item>();
    public List<Item> level_thirteen = new List<Item>();
    public List<Item> level_fourteen = new List<Item>();
    public List<Item> level_fifteen = new List<Item>();


    //Holds all of these lists above
    private List<List<Item>> lists = new List<List<Item>>();

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
    private void Start() {
        InitializeLargeList();
    }

    private void InitializeLargeList() {
        lists.Add(level_one);
        lists.Add(level_two);
        lists.Add(level_three);
        lists.Add(level_four);
        lists.Add(level_five);
        lists.Add(level_six);
        lists.Add(level_seven);
        lists.Add(level_eight);
        lists.Add(level_nine);
        lists.Add(level_ten);
        lists.Add(level_eleven);
        lists.Add(level_twelve);
        lists.Add(level_thirteen);
        lists.Add(level_fourteen);
        lists.Add(level_fifteen);
    }
    /// <summary>
    /// Use when you want a random list to take an item from. 
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<Item> GetRandomList(int level) { //insert 16 to get any list
        int rand = Random.Range(0, level - 1);
        return lists[rand];
    }

    public List<Item> GetChestList(int min, int max) {
        int rand = Random.Range(min, max - 1);
        return lists[rand];
    }

    /// <summary>
    /// Use when you want a specific level of items
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public List<Item> GetList(int num) {
        return lists[num - 1];
    }
    /// <summary>
    /// Called when user wants a random item from a specific list. 
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public Item GetRandomItemFromList(List<Item> list) {
        int rand = Random.Range(0, list.Count);
        return list[rand];
    }

    public Item GetItem(int id) {
        for(int i = 0; i < lists.Count; i++) {
            for(int j = 0; j < lists[i].Count;j++) {
                Item item = lists[i][j];
                if(item.id == id) {
                    return item;
                } else {
                    continue;
                }
            }
        }
        //really should never reach this
        Debug.Log("Could not get an item!");
        return null;
    }

}
