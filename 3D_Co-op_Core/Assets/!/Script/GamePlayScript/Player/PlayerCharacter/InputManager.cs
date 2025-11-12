using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("PlayerCharacter")]
    [SerializeField] PlayerLook playerLook;
    [SerializeField] PlayerMovement playerMovement;

    [Header("Modules")]
    [SerializeField] ItemEquipment itemEquipment;
    [SerializeField] ItemDrop itemDrop;

    private Dictionary<KeyCode, Action> keyDownBindings;


    void Start()
    {
        keyDownBindings = new Dictionary<KeyCode, Action> //ют╥б ╫ц
        {
            //{ KeyCode.Escape, () => gameManager.GetReverseSetting() },

            { KeyCode.Tab, () => playerLook.GetReverseMouseLocked() },

            { KeyCode.Space, () => playerMovement.GetJump() },

            { KeyCode.Alpha1, () => itemEquipment.GetEquipment(0) },
            { KeyCode.Alpha2, () => itemEquipment.GetEquipment(1) },
            { KeyCode.Alpha3, () => itemEquipment.GetEquipment(2) },
            { KeyCode.Alpha4, () => itemEquipment.GetEquipment(3) },
            { KeyCode.Alpha5, () => itemEquipment.GetEquipment(4) },
            { KeyCode.Alpha0, () => itemEquipment.GetEquipment(-1) },

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
    }
}
