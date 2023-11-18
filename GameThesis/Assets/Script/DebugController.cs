using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : Auto_Singleton<DebugController>
{
    [HideInInspector] public bool showConsole;

    string input;

    //public static DebugCommand ClearInventory;
    //public static DebugCommand<int> UnlockIngredient;
    //public static DebugCommand<int, int> RemoveItem;

    public static DebugCommand<int> AddPocketCoin;

    public List<object> commandList = new List<object>();

    private void Awake()
    {

        AddPocketCoin = new DebugCommand<int>("add_pocket", "add pocket coint", "add_pocket", (i) =>
        {
            GameManager.Instance.AddPocketMoney(i);
        });

        //RemoveItem = new DebugCommand<int, int>("remove_item", "remove itemId count", "remove_item", (x, y) =>
        //{
        //    InventoryManager.Instance.RemoveItem(x, y);
        //});

        //ClearInventory = new DebugCommand("inventory_clear", "inventory_clear to Clear Inventory", "inventory_clear", () =>
        //{
        //    InventoryManager.Instance.ClearInventory();
        //});

        //UnlockIngredient = new DebugCommand<int>("unlock_ingredient", "unlock_ingredient ingredientID", "unlock_ingredient", (x) =>
        //{
        //    GameManager.Instance.unlock_Ingredient(x);
        //});

        commandList = new List<object>()
        {
            AddPocketCoin,
            //RemoveItem,ClearInventory,UnlockIngredient
        };

    }

    public void pressReturn()
    {
        if (showConsole)
        {
            HandheldInput();
            input = "";
        }
    }

    void HandheldInput()
    {
        string[] properties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandID))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if (commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
                else if (commandList[i] as DebugCommand<int, int> != null)
                {
                    (commandList[i] as DebugCommand<int, int>).Invoke(int.Parse(properties[1]), int.Parse(properties[2]));
                }
            }
        }
    }

    public void pressToggleConsole()
    {
        showConsole = !showConsole;
    }

    private void OnGUI()
    {
        if (!showConsole) return;

        float boxSize = 30;
        float y = Screen.height - boxSize;

        GUI.Box(new Rect(0, y - (boxSize / 2), Screen.width * 0.4f, boxSize), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextArea(new Rect(10f, y - ((boxSize / 2) / 2), (Screen.width * 0.4f) - boxSize * .7f, boxSize * .7f), input);

    }

}
