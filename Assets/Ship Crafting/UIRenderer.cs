using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UIRenderer : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset itemCard;
    [SerializeField] private UIDocument uiDocument;

    private CraftingManager craftingManager;
    private InventoryManager inventoryManager;
    private VisualElement craftedBG;
    private Label craftedText;
    private VisualElement craftedImage;

    void Start()
    {
        craftingManager = CraftingManager.Instance;
        inventoryManager = InventoryManager.Instance;
        SetupUI();

        List<ResourceQuantity> displayList = inventoryManager.Inventory.Values.ToList().Where(r => r.Quantity > 0).OrderByDescending(r => r.Ressorce.Rarity.SortOrder).ToList();
        foreach (ResourceQuantity item in displayList)
        {
            VisualElement itemCard = CreateItemCard(item);
            uiDocument.rootVisualElement.Q<VisualElement>("items").Add(itemCard);

        }
    }

    public void SetupUI()
    {
        VisualElement root = uiDocument.rootVisualElement;
        Button craftButton = root.Q<Button>("craft");
        craftButton.RegisterCallback<ClickEvent>(OnCraftClik);

        VisualElement craftedDisplay = root.Q<VisualElement>("product-holder");
        craftedBG = craftedDisplay.Q<Button>();
        craftedImage = craftedDisplay.Q<VisualElement>("image");
        craftedText = craftedDisplay.Q<Label>("label");
        craftedDisplay.Q<Label>("quantity").text = "0";

    }

    public void OnCraftClik(ClickEvent e)
    {
        CraftedData value = craftingManager.Craft();
        Debug.Log($"crafted: {value.Name}");

        craftedBG.style.backgroundImage = new StyleBackground(value.Rarity.Background);
        craftedImage.style.backgroundImage = new StyleBackground(value.Sprite);
        craftedText.text = value.Name;
    }

    public VisualElement CreateItemCard(ResourceQuantity rQ)
    {
        Item item = rQ.Ressorce;

        VisualElement card = new();
        itemCard.CloneTree(card);

        Label label = card.Q<Label>("label");
        Label quantity = card.Q<Label>("quantity");
        Button background = card.Q<Button>();
        VisualElement image = card.Q<VisualElement>("image");

        label.text = item.Name;
        quantity.text = rQ.Quantity.ToString();
        background.style.backgroundImage = new StyleBackground(item.Rarity.Background);
        image.style.backgroundImage = new StyleBackground(item.Sprite);

        background.RegisterCallback<ClickEvent, Item>(InventoryClick, item);

        return card;
    }

    public void InventoryClick(ClickEvent evt, Item resource)
    {
        Debug.Log($"item {resource.name} clicked");
        bool canBeAdded = craftingManager.AddResource(resource);
        if (!canBeAdded) return;

        inventoryManager.ReduceQuantity(resource, 1);
        VisualElement itemCard = CreateSelectedItemCard(new ResourceQuantity { Ressorce = resource, Quantity = 1 });

        VisualElement root = uiDocument.rootVisualElement;
        root.Q<VisualElement>("container").Add(itemCard);
    }

    public VisualElement CreateSelectedItemCard(ResourceQuantity rQ)
    {
        Item item = rQ.Ressorce;

        VisualElement card = new();
        itemCard.CloneTree(card);

        Label label = card.Q<Label>("label");
        Label quantity = card.Q<Label>("quantity");
        Button background = card.Q<Button>();
        VisualElement image = card.Q<VisualElement>("image");

        label.text = item.Name;
        quantity.text = rQ.Quantity.ToString();
        background.style.backgroundImage = new StyleBackground(item.Rarity.Background);
        image.style.backgroundImage = new StyleBackground(item.Sprite);

        background.RegisterCallback<ClickEvent>(evt => OnSelectedClick(card, item));

        return card;
    }

    public void OnSelectedClick(VisualElement itemCard, Item resource)
    {
        VisualElement root = uiDocument.rootVisualElement;
        root.Q<VisualElement>("container").Remove(itemCard);

        craftingManager.RemoveResource(resource);
        inventoryManager.IncreaseQuantity(resource, 1);
    }

}
