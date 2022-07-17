using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IngameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;

    public static IngameManager Instance { get; private set; }

    private static UnityAction<int, int> _onRollDice;
    public static UnityAction<int, int> OnRollDice
    {
        get => _onRollDice;
        set => _onRollDice = value;
    }
    public PlayerManager Player 
    {
        get => _player;
        private set => _player = value;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void DiceRoll ()
    {
        int roll1 = Random.Range(1, 4);
        int roll2 = Random.Range(1, 4);
        if (roll1 < roll2)
        {
            int tempswap = roll1;
            roll1 = roll2;
            roll2 = tempswap;
        }
        Debug.Log(roll1 + " " + roll2);
        OnRollDice?.Invoke(roll1, roll2);
    }


  

}
