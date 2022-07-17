using System.Collections;
using UnityEngine;
public enum TAB
{
    Start = 0,
    Level = 1
}

public abstract class BaseTabUI : MonoBehaviour
{
    public TAB tabID;

    [Header("Effect Properties")]
    [SerializeField] protected float _walkInTime;
    [SerializeField] protected float _walkOutTime;

    public float WalkOutTime
    {
        get { return _walkOutTime; }
    }

    public float WalkInTime
    {
        get { return _walkInTime; }
    }


    /// <summary>
    /// Like start method, this method will run when you enter the tab.
    /// </summary>
    public virtual void Setup()
    {

    }

    public void Enter()
    {
        StartCoroutine(WalkIn());
    }

    public virtual void Exit() => StartCoroutine(WalkOut());

    /// <summary>
    /// Set properties before entering the tab to make the effect better.
    /// </summary>
    public virtual void PreWalkin()
    {

    }

    /// <summary>
    /// A coroutine for effect when you enter the tab
    /// </summary>
    public virtual IEnumerator WalkIn()
    {
        PreWalkin();

        yield return null;

        WalkInEffect(_walkInTime);

        yield return _walkInTime;

        Setup();
    }

    /// <summary>
    /// A coroutine for effect when you exit the tab
    /// </summary>
    public virtual IEnumerator WalkOut()
    {
        WalkOutEffect(_walkOutTime);
        yield return new WaitForSeconds(_walkOutTime);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// The effect of the tab when you enter the tab.
    /// </summary>
    public virtual void WalkInEffect(float walkInTime) { }

    /// <summary>
    /// The effect of the tab when you exit the tab.
    /// </summary>
    public virtual void WalkOutEffect(float walkOutTime) { }
}
