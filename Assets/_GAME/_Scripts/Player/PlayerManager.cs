using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    private static UnityAction<Vector2> _onMove;
    private static UnityAction<Swipe> _onSwipe;

    [SerializeField] private Vector2 _playerPos;
    [SerializeField] private Vector2 _startPos = new Vector2(0, 0);
    [SerializeField] private int _gridLimit = 6;

    private int _playerStepCount = 0;
    private int _playerDirectionCount = 0;

    private int _playerStepLimit = 0;
    private int _playerDirectionLimit = 0;

    /// <summary>
    /// Dice x = step, dice y = direction
    /// </summary>
    private Vector2 dice;

    public Animator Animator { get; private set; }

    public Vector2 PlayerPosition
    {
        get => _playerPos;
        set
        {
           OnMove?.Invoke(new Vector2(_playerPos.x - _gridLimit / 2, _playerPos.y - _gridLimit / 2));
           _playerPos = value;
        }
    }

    public static UnityAction<Vector2> OnMove
    {
        get => _onMove;
        set => _onMove = value;
    }

    public static UnityAction<Swipe> OnSwipe
    {
        get => _onSwipe;
        set => _onSwipe = value;
    }

    public void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    void Start()
    {
        PlayerPosition = _startPos;
    }   

    void Update()
    {

    }

    public void Move (Swipe moveDirection)
    {
        var direc = new Swipe[(int)dice.x];

        for (int i =0; i< direc.Length; i++)
        {
            if (moveDirection == direc[i])
                break;

            if (direc[i] == null)
            {
                direc[i] = moveDirection;
                _playerDirectionCount++;
                break;
            }
        }

        
        if (_playerStepCount == _playerStepLimit) return;

        _playerStepCount++;
        switch (moveDirection)
        {
            case Swipe.Up:
                if (_playerPos.y == _gridLimit) return;
                PlayerPosition += Vector2.up;
                transform.Translate(Vector2.up);
                break;
            case Swipe.Down:
                if (_playerPos.y == 0) return;
                PlayerPosition += Vector2.down;
                transform.Translate(Vector2.down);
                break;
            case Swipe.Left:
                if (_playerPos.x == 0) return;
                PlayerPosition += Vector2.left ;
                transform.Translate(Vector2.left);
                break;
            case Swipe.Right:
                if (_playerPos.x == _gridLimit) return;
                PlayerPosition += Vector2.right;
                transform.Translate(Vector2.right);
                break;
        }
        
    }

    public void DiceRolled (int stepLimit, int directionLimit)
    {
        _playerStepCount = 0;
        _playerStepLimit = stepLimit;
        _playerDirectionLimit = directionLimit;
    }

    #region EVENTS REGISTER
    private void OnEnable()
    {
        RegisterEvent();
    }
    public void RegisterEvent()
    {
        IngameManager.OnRollDice += DiceRolled;
        OnSwipe += Move;
    }
    private void OnDisable()
    {
        UnregisterEvent();
    }
    public void UnregisterEvent()
    {
        OnSwipe -= Move;
        IngameManager.OnRollDice -= DiceRolled;
    }
    #endregion
}
