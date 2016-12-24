//
// Script name: Launcher
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;
using CustomCamera;

public class Launcher : View
{
    #region Variables
    #endregion

    #region Unity API
    #endregion

    #region Public Methods
    protected override void OnViewOpened()
    {
        base.OnViewOpened();

        CameraManager.Instance.Init();

        GoTo("Test");
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

}
