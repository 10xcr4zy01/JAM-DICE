using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManagerUI : MonoBehaviour
{
    [SerializeField] BaseTabUI[] _tabs;
    private BaseTabUI _currentTab;

    private void Start()
    {
        _currentTab = _tabs[(int)TAB.Start];
        _currentTab.gameObject.SetActive(true);
        _currentTab.Enter();
    }


    private void InitMainTab()
    {
        
    }

    public void SwitchTab(TAB tabID) => StartCoroutine(DoSwitchTab(tabID));

    public IEnumerator DoSwitchTab(TAB tabID)
    {
        _currentTab.Exit();

        yield return new WaitForSeconds(_currentTab.WalkOutTime);

        _currentTab = _tabs[(int)tabID];
        _currentTab.gameObject.SetActive(true);
        _currentTab.Enter();
    }
}
