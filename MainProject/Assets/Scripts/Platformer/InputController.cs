//
// Script name: InputController
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;

public enum ePlayer
{
    PLAYER_1 = 1,
    PLAYER_2,
    PLAYER_3,
    PLAYER_4
}

public class InputController : SubjectObserver
{
    #region Variables
    /// <summary>
    /// {0} = Player, {1} = Button
    /// </summary>
    protected string BUTTONS_KEY= "Joystick{0}Button{1}";
    protected string DIRECTION_KEY = "Axis {0}";

    protected enum eDirections
    {
        LEFT_HORIZONTAL_JS = 1,
        LEFT_VERTOCAL_JS,
        RIGHT_HORIZONTAL_JS,
        RIGHT_VERTICAL_JS,
        DONT_KNOW1,
        DONT_KNOW2,
        DPAD_HORIZONTAL,
        DPAD_VERTICAL
    }

    protected enum eButtons
    {
        LEFT = 0,
        BOTTOM,
        RIGHT,
        TOP,
        LEFT_BUTTON,
        RIGHT_BUTTON,
        LEFT_TRIGGER,
        RIGHT_TRIGGER
    }

    [SerializeField] protected ePlayer m_PlayerID = ePlayer.PLAYER_1;
    [SerializeField, ReadOnly] protected bool m_IsGrounded = false;

    public bool IsGrounded
    {
        get { return m_IsGrounded; }
    }
    #endregion

    #region Unity API
    #endregion

    #region Public Methods
    public virtual void SetGrounded(bool isGrounded)
    {
        m_IsGrounded = isGrounded;
    }

    public virtual void OnUpdate()
    {
        // STUB
    }
    #endregion

    #region Protected Methods
    protected string GetAxis(eDirections direction)
    {
        return string.Format(DIRECTION_KEY, (int)direction);
    }

    protected KeyCode GetKeyCode(int index)
    {
        return EnumExtensions.ParseEnum<KeyCode>(string.Format(BUTTONS_KEY, (int)m_PlayerID, index));
    }
    #endregion

    #region Private Methods
    #endregion

}
