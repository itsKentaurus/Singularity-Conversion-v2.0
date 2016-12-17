//
// Script name: InputController
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;

public class InputController : Subject 
{
    #region Variables
    public const string RIGHT_INPUT = "INPUT_RIGHT";
    public const string LEFT_INPUT = "INPUT_LEFT";
    #endregion

    #region Unity API
    #endregion

    #region Public Methods
    public virtual void OnUpdate()
    {
        if (Input.GetKeyDown("d"))
        {
            NotifyObservers(RIGHT_INPUT);
        }

        if (Input.GetKeyDown("a"))
        {
            NotifyObservers(LEFT_INPUT);
        }
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

}
