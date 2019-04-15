﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profession : MonoBehaviour
{
    [SerializeField]
    private Text title;

    [SerializeField]
    private Text description;

    [SerializeField]
    private GameObject materialPrefab;

    [SerializeField]
    private Transform parent;

    private List<GameObject> materials = new List<GameObject>();

    [SerializeField]
    private Recipe selectedRecipe;

    [SerializeField]
    private Text countTxt;

    [SerializeField]
    private ItemInfo craftItemInfo;

    private int maxAmount;

    private int amount;

    private void Start()
    {
        InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateMaterialCount);
        ShowDescription(selectedRecipe);

    }

    public void ShowDescription(Recipe recipe)
    {
        if (selectedRecipe != null)
        {
            selectedRecipe.Deselect();
        }

        this.selectedRecipe = recipe;

        this.selectedRecipe.Select();

        foreach (GameObject gameObject in materials)
        {
            Destroy(gameObject);
        }

        materials.Clear();

        title.text = recipe.Output.MyTitle;

        description.text = recipe.MyDescription + " " + recipe.Output.MyTitle.ToLower();

        craftItemInfo.Initialize(recipe.Output, 1);

        foreach (CraftingMaterial material in recipe.Materials)
        {
            GameObject tmp = Instantiate(materialPrefab, parent);

            tmp.GetComponent<ItemInfo>().Initialize(material.MyItem, material.MyCount);

            materials.Add(tmp);

        }

        UpdateMaterialCount(null);
    }

    private void UpdateMaterialCount(Item item)
    {
        foreach (GameObject material in materials)
        {
            ItemInfo tmp = material.GetComponent<ItemInfo>();
            tmp.UpdateStackCount();
        }
    }

    public void Craft()
    {
        StartCoroutine(CraftRoutine(0));
    }

    private IEnumerator CraftRoutine(int count)
    {
        yield return Player.MyInstance.MyInitRoutine = StartCoroutine(Player.MyInstance.CraftRoutine(selectedRecipe));
    }

    public void AdddItemsToInventory()
    {
        InventoryScript.MyInstance.AddItem(craftItemInfo.MyItem);
    }
}
