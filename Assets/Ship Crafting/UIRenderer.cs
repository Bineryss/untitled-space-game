using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UIRenderer : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset itemCard;
    [SerializeField] private VisualTreeAsset inventoryRow;
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private int itemsPerRow = 7;

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
        SetupListView(displayList);
    }

    public void SetupListView(List<ResourceQuantity> displayList)
    {
        ListView itemList = uiDocument.rootVisualElement.Q<ListView>("inventory-list");

        itemList.itemsSource = displayList;
        itemList.makeItem = () => inventoryRow.Instantiate();
        itemList.bindItem = (element, index) =>
        {
            element.Clear();
            element.style.flexDirection = FlexDirection.Row;
            element.style.paddingBottom = 24;

            for (int i = 0; i < itemsPerRow; i++)
            {
                if (i + index * itemsPerRow >= displayList.Count)
                {
                    return;
                }
                InventoryItem item = displayList[index * itemsPerRow + i].Ressorce;
                int quantity = displayList[index * itemsPerRow + i].Quantity;

                VisualElement itemElement = itemCard.Instantiate();
                Label label = itemElement.Q<Label>("label");
                Label quantityDisplay = itemElement.Q<Label>("quantity");
                Button background = itemElement.Q<Button>();
                VisualElement image = itemElement.Q<VisualElement>("image");

                label.text = item.Name;
                quantityDisplay.text = quantity.ToString();
                background.style.backgroundImage = new StyleBackground(item.Rarity.Background);
                image.style.backgroundImage = new StyleBackground(item.Icon);

                background.RegisterCallback<ClickEvent>((evt) => InventoryClick(item));
                element.Add(itemElement);
            }
        };
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
        InventoryItem value = craftingManager.Craft();
        Debug.Log($"crafted: {value.Name}");

        craftedBG.style.backgroundImage = new StyleBackground(value.Rarity.Background);
        craftedImage.style.backgroundImage = new StyleBackground(value.Icon);
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
        image.style.backgroundImage = new StyleBackground(item.Icon);

        // background.RegisterCallback<ClickEvent, Item>(InventoryClick, item);

        return card;
    }

    public void InventoryClick(InventoryItem item)
    {
        if (item is not Item)
        {
            Debug.Log($"the provided inventory item {item.Name} cannot be used as crafting material!");
            return;
        }

        Item resource = item as Item;

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
        image.style.backgroundImage = new StyleBackground(item.Icon);

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
