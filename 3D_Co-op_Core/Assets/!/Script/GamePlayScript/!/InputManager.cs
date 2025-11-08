using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerLook playerLook;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] ItemPickUp itemPickUp;
    [SerializeField] ItemEquipment ItemEquipment;
    [SerializeField] ItemDrop itemDrop;

    private Dictionary<KeyCode, Action> keyDownBindings;
    private Dictionary<KeyCode, Action> keyUpBindings;


    void Start()
    {
        keyDownBindings = new Dictionary<KeyCode, Action> //ют╥б ╫ц
        {
            { KeyCode.Escape, () => gameManager.GetReverseSetting() },

            { KeyCode.Tab, () => playerLook.GetReverseMouseLocked() },

            { KeyCode.Space, () => playerMovement.GetJump() },

            { KeyCode.Alpha1, () => ItemEquipment.GetEquipment(0) },
            { KeyCode.Alpha2, () => ItemEquipment.GetEquipment(1) },
            { KeyCode.Alpha3, () => ItemEquipment.GetEquipment(2) },
            { KeyCode.Alpha4, () => ItemEquipment.GetEquipment(3) },
            { KeyCode.Alpha5, () => ItemEquipment.GetEquipment(4) },
            { KeyCode.Alpha0, () => ItemEquipment.GetEquipment(-1) },

            { KeyCode.G, () => itemDrop.GetDropItem() }
        };
    }

    void Update()
    {
        if (keyDownBindings == null) return;
        foreach (var key in keyDownBindings.Keys)
        {
            if (Input.GetKeyDown(key) && keyDownBindings.TryGetValue(key, out var action))
            {
                action?.Invoke();
            }
        }

        if (keyUpBindings == null) return;
        foreach (var key in keyUpBindings.Keys)
        {
            if (Input.GetKeyUp(key) && keyUpBindings.TryGetValue(key, out var action))
            {
                action?.Invoke();
            }
        }
    }
}
