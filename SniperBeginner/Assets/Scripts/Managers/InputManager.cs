
using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    // 플레이어 입력에 대한 접근성을 위해서 별도로 매니저를 만듦
    public PlayerInput Input { get; private set; }
    public PlayerInput.PlayerActions Actions { get; private set; }

    public event Action OnMenuEvent;
    public event Action<int> OnQuickSlotEvent;


    private void Awake() 
    {
        Input = new PlayerInput();
        Actions = Input.Player;

        Debug.Log("InputManager Awake");
    }

    private void OnEnable() 
    {
        AddInput();
        Input.Enable();
    }

    private void OnDisable() 
    {
        Input.Disable();
        RemoveInput();
    }


    void AddInput()
    {
        Actions.Menu.started += CallMenuEvent;
        Actions.QuickSlot.started += CallQuickSlotEvent;
    }

    void RemoveInput()
    {
        Actions.Menu.started -= CallMenuEvent;
        Actions.QuickSlot.started -= CallQuickSlotEvent;
    }


    void CallMenuEvent(InputAction.CallbackContext context)
    {
        OnMenuEvent?.Invoke();
    }

    void CallQuickSlotEvent(InputAction.CallbackContext context)
    {
        int num = (int)context.ReadValue<Single>();
        OnQuickSlotEvent?.Invoke(num);
    }
}