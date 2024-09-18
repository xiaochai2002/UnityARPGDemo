using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public static class PlayerInputData
{
    public static Vector2 ButtonVector2 = new Vector2();
    public static Vector2 MoveVector2 = new Vector2();
    public static bool ShiftDown;
    public static bool WDown;
    public static bool SDown;
    public static bool ADown;
    public static bool DDown;
    public static bool EDown;
    public static bool LeftMoveDown;
    public static bool Key1Down;
    public static bool Key2Down;
}

public enum InputKey
{
    Move
}

public class PlayerInputMgr : BaseManager<PlayerInputMgr>
{
    private PlayerInput playerInput;
    public PlayerObject playerObject;
    private PlayerInputMgr()
    {

    }

    public void InfoInputMgr(PlayerInput input, PlayerObject playerObject)
    {
        this.playerObject = playerObject;
        playerInput = input;
        input.onActionTriggered += OnActionTrigger;
    }

    public void OnActionTrigger(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Move":
                if (context.phase == InputActionPhase.Performed)
                {
                    PlayerInputData.ButtonVector2 = context.action.ReadValue<Vector2>();
                    SetBoolValue(context, true);
                }

                if (context.phase == InputActionPhase.Canceled)
                {
                    SetBoolValue(context, false);
                    PlayerInputData.ButtonVector2 = new Vector2();
                }
                break;
            case "Function":
                if (context.phase == InputActionPhase.Performed)
                {
                    SetBoolValue(context, true);
                }
                else if (context.phase == InputActionPhase.Canceled)
                {
                    SetBoolValue(context, false);
                }
                break;
            case "Look":
                if (context.phase == InputActionPhase.Performed)
                {
                    PlayerInputData.MoveVector2 = context.ReadValue<Vector2>();
                }

                if (context.phase == InputActionPhase.Canceled)
                {
                    PlayerInputData.MoveVector2 = new Vector2();
                }
                break;
            case "Fire":
                if (context.phase == InputActionPhase.Performed)
                {
                    PlayerInputData.LeftMoveDown = true;
                }
                break;
        }
    }

    public void SetBoolValue(InputAction.CallbackContext context, bool value)
    {
        switch (context.control.name)
        {
            case "w":
                PlayerInputData.WDown = value;
                break;
            case "s":
                PlayerInputData.SDown = value;
                break;
            case "a":
                PlayerInputData.ADown = value;
                break;
            case "d":
                PlayerInputData.DDown = value;
                break;
            case "leftShift":
                if (value)
                {
                    if (context.interaction is HoldInteraction)
                    {
                        PlayerInputData.ShiftDown = true;
                        playerObject.CheckKeyState(true);
                    }
                }
                else
                {
                    PlayerInputData.ShiftDown = false;
                    playerObject.CheckKeyState(false);
                }
                break;
            case "e":
                PlayerInputData.EDown = value;
                break;
            case "1":
                PlayerInputData.Key1Down = value;
                break;
            case "2":
                PlayerInputData.Key2Down = value;
                break;
        }
    }
}
