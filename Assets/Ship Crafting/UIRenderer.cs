using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UIRenderer : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset itemCard;
    [SerializeField] private UIDocument uiDocument;

    void Awake()
    {

    }
    void Start()
    {
        List<ResourceQuantity> displayList = InventoryManager.Instance.Inventory.Values.ToList().Where(r => r.Quantity > 0).OrderByDescending(r => r.Ressorce.Rarity.SortOrder).ToList();
        foreach (ResourceQuantity item in displayList)
        {
            VisualElement itemCard = CreateItemCard(item);
            uiDocument.rootVisualElement.Q<VisualElement>("items").Add(itemCard);

        }
    }

    void Update()
    {

    }

    public VisualElement CreateItemCard(ResourceQuantity rQ)
    {
        Item item = rQ.Ressorce;

        VisualElement card = new VisualElement();
        itemCard.CloneTree(card);

        Label label = card.Q<Label>("label");
        Label quantity = card.Q<Label>("quantity");
        Button background = card.Q<Button>();
        VisualElement image = card.Q<VisualElement>("image");

        label.text = item.Name;
        quantity.text = rQ.Quantity.ToString();
        background.style.backgroundImage = new StyleBackground(item.Rarity.Background);
        image.style.backgroundImage = new StyleBackground(item.Sprite);

        background.RegisterCallback<ClickEvent, string>(DebugClick, item.Name);

        return card;
    }

    public void DebugClick(ClickEvent evt, string name)
    {
        Debug.Log($"item {name} clicked");
    }
}
