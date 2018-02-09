/*============================================================================== 
 Copyright (c) 2016-2017 PTC Inc. All Rights Reserved.
 
 Copyright (c) 2012-2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using System.Collections;
using UnityEngine;
using Vuforia;
using DogProject;

/// <summary>
/// This class implements the IVirtualButtonEventHandler interface and
/// contains the logic to start animations depending on what 
/// virtual button has been pressed.
/// </summary>
public class VirtualButtonEventHandler : MonoBehaviour,
                                         IVirtualButtonEventHandler
{
    #region PUBLIC_MEMBERS
    public Material[] m_VirtualButtonDefault;
    public Material[] m_VirtualButtonPressed;
    public float m_ButtonReleaseTimeDelay;
    #endregion // PUBLIC_MEMBERS

    #region PRIVATE_MEMBERS
    VirtualButtonBehaviour[] virtualButtonBehaviours;
    CharacterAnimation chAnim;
    #endregion // PRIVATE_MEMBERS

    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        // Register with the virtual buttons TrackableBehaviour
        virtualButtonBehaviours = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (int i = 0; i < virtualButtonBehaviours.Length; ++i)
        {
            virtualButtonBehaviours[i].RegisterEventHandler(this);
        }

        chAnim = GetComponentInChildren<CharacterAnimation>();
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    /// <summary>
    /// Called when the virtual button has just been pressed:
    /// </summary>
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        //Debug.Log("OnButtonPressed: " + vb.VirtualButtonName);

        //SetVirtualButtonMaterial(m_VirtualButtonPressed);

        //StopAllCoroutines();

        //BroadcastMessage("HandleVirtualButtonPressed", SendMessageOptions.DontRequireReceiver);

        switch (vb.VirtualButtonName)
        {
            case Define.AnimIdle:
                chAnim.PlayAnimation(Define.AnimIdle);
                SetVirtualButtonPressedMaterial(0);
                break;
            case Define.AnimWalk:
                chAnim.PlayAnimation(Define.AnimWalk);
                SetVirtualButtonPressedMaterial(1);
                break;
            case Define.AnimRun:
                chAnim.PlayAnimation(Define.AnimRun);
                SetVirtualButtonPressedMaterial(2);
                break;
            case Define.AnimAttack:
                chAnim.PlayAnimation(Define.AnimAttack);
                SetVirtualButtonPressedMaterial(3);
                break;
        }
    }

    /// <summary>
    /// Called when the virtual button has just been released:
    /// </summary>
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        //Debug.Log("OnButtonReleased: " + vb.VirtualButtonName);

        switch (vb.VirtualButtonName)
        {
            case Define.AnimIdle:
                SetVirtualButtonDefaultMaterial(0);
                break;
            case Define.AnimWalk:
                SetVirtualButtonDefaultMaterial(1);
                break;
            case Define.AnimRun:
                SetVirtualButtonDefaultMaterial(2);
                break;
            case Define.AnimAttack:
                SetVirtualButtonDefaultMaterial(3);
                break;
        }

        //StartCoroutine(DelayOnButtonReleasedEvent(m_ButtonReleaseTimeDelay, vb.VirtualButtonName));
    }
    #endregion //PUBLIC_METHODS


    #region PRIVATE_METHODS

    void SetVirtualButtonDefaultMaterial(int index)
    {
        virtualButtonBehaviours[index].GetComponent<MeshRenderer>().sharedMaterial = m_VirtualButtonDefault[index];
    }

    void SetVirtualButtonPressedMaterial(int index)
    {
        virtualButtonBehaviours[index].GetComponent<MeshRenderer>().sharedMaterial = m_VirtualButtonPressed[index];
    }

    IEnumerator DelayOnButtonReleasedEvent(float waitTime, string buttonName)
    {
        yield return new WaitForSeconds(waitTime);

        BroadcastMessage("HandleVirtualButtonReleased", SendMessageOptions.DontRequireReceiver);
    }
    #endregion // PRIVATE METHODS
}
