using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using UnityEngine.TextCore.Text;
using Spine;
using Sirenix.OdinInspector;


public class CharacterManager : MonoBehaviour, Factory
{
    [EnumToggleButtons] public CharacterState characterState;
    [SerializeField] private Information information;

    private CharacterFactoryState _factory;
    private FactoryState missionState;
    private void Awake()
    {
        HandlerAnimationInGameplay animationHandler = new HandlerAnimationInGameplay(information.animationOfChangeState, information.textPraiseUI, this);
        _factory = new CharacterFactoryState(new CharacterStateDrag(this, information.objectSelectionArea), new CharacterStateChange(animationHandler, this), new CharacterStateComplete(animationHandler), new CharacterStateLose(this));
    }
    public void Start()
    {
        ChangeState(CharacterState.Drag);
    }
    public void ChangeState(CharacterState state)
    {
        missionState = _factory.CreateState(state);
        missionState.Start();
        characterState = state;
    }
    void Update()
    {
        missionState.Update();
        if (GameManager.Instance.gameState == GameState.Lose)
        {
            ChangeState(CharacterState.Lose);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        missionState.OnTriggerEnter2D(other);
    }

    [Serializable]
    public class Information
    {
        public UnityEngine.Animation animationOfChangeState;
        public GameObject textPraiseUI;
        public Collider2D objectSelectionArea;
    }
}

public enum CharacterState
{
    Drag,
    Change,
    Complete,
    Lose
}

public interface Factory
{
    public void ChangeState(CharacterState state);
}

public interface FactoryState
{
    void Start();

    void Update();

    void OnTriggerEnter2D(Collider2D other);

}

public class CharacterFactoryState
{
    private readonly CharacterStateDrag _dragFactory;
    private readonly CharacterStateChange _changeFactory;
    private readonly CharacterStateComplete _completeFactory;
    private readonly CharacterStateLose _loseFactory;
    public CharacterFactoryState(CharacterStateDrag dragFactory, CharacterStateChange changeFactory, CharacterStateComplete completeFactory, CharacterStateLose loseFactory)
    {
        _dragFactory = dragFactory;
        _changeFactory = changeFactory;
        _completeFactory = completeFactory;
        _loseFactory = loseFactory;
    }
    public FactoryState CreateState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Drag:
                return _dragFactory;
            case CharacterState.Change:
                return _changeFactory;
            case CharacterState.Complete:
                return _completeFactory;
            case CharacterState.Lose:
                return _loseFactory;
        }
        throw new ArgumentException("Invalid character state: " + state);
    }

}

public class CharacterStateDrag : FactoryState
{
    private GameObject selectedObject;
    private Vector3 initialPositionOfDragState;
    Vector3 vectorDistanceMourseAndImageObject;
    private CharacterManager characterManager;
    private Collider2D _collider;

    public CharacterStateDrag(CharacterManager manager, Collider2D collider)
    {
        characterManager = manager;
        _collider = collider;
    }
    public void Start()
    {
        HandlePositionToMoveWhenSwitchingState();
    }

    private void HandlePositionToMoveWhenSwitchingState()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject && targetObject == _collider)
            {
                selectedObject = targetObject.transform.gameObject;
                initialPositionOfDragState = selectedObject.transform.position;
                vectorDistanceMourseAndImageObject = selectedObject.transform.position - mousePosition;
            }
        }
    }
    public void Update()
    {
        HandlePositionToMoveImageObject();
        HandleToMoveImageObject();
    }

    private void HandlePositionToMoveImageObject()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                initialPositionOfDragState = selectedObject.transform.position;
                vectorDistanceMourseAndImageObject = selectedObject.transform.position - mousePosition;
            }
        }
    }

    private void HandleToMoveImageObject()
    {
        if (selectedObject && _collider.OverlapPoint(selectedObject.transform.position))
        {
            selectedObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + vectorDistanceMourseAndImageObject;
            HandleSwitchingState();
        }
    }

    private void HandleSwitchingState()
    {
        if (Input.GetMouseButtonUp(0))
        {
            float distanceMoved = Vector3.Distance(selectedObject.transform.position, initialPositionOfDragState);
            if (distanceMoved < 0.1f)
            {
                characterManager.ChangeState(CharacterState.Change);
            }
            selectedObject = null;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }
}

public class CharacterStateChange : FactoryState
{
    private int indexOfElementInObject = 0;
    private Vector3 initialPositionOfChangeState = Vector3.zero;
    private CharacterManager characterManager;
    private HandlerAnimationInGameplay _anim;


    public CharacterStateChange(HandlerAnimationInGameplay anim, CharacterManager manager)
    {
        _anim = anim;
        characterManager = manager;
    }

    public void Start()
    {
        CheckChildObjectIsTrue();
        HandleChangeObjectImageplay();
    }

    private void CheckChildObjectIsTrue()
    {
        for (int i = 0; i < characterManager.transform.childCount; i++)
        {
            if (characterManager.transform.GetChild(i).gameObject.activeSelf)
            {
                indexOfElementInObject = i;
                break;
            }
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialPositionOfChangeState = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (initialPositionOfChangeState != Vector3.zero)
        {
            if (initialPositionOfChangeState != Camera.main.ScreenToWorldPoint(Input.mousePosition))
            {
                characterManager.ChangeState(CharacterState.Drag);
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    HandleChangeObjectImageplay();
                }
            }
        }
    }

    private void HandleChangeObjectImageplay()
    {
        if (indexOfElementInObject < (characterManager.transform.childCount - 1))
        {
            _anim.PlayAnimationChange();
            SoundManager.Instance.PlaySoundStateChange();
            characterManager.transform.GetChild(indexOfElementInObject).gameObject.SetActive(false);
            indexOfElementInObject += 1;
            characterManager.transform.GetChild(indexOfElementInObject).gameObject.SetActive(true);
        }
        else if (indexOfElementInObject == (characterManager.transform.childCount - 1))
        {
            _anim.PlayAnimationChange();
            SoundManager.Instance.PlaySoundStateChange();
            characterManager.transform.GetChild(indexOfElementInObject).gameObject.SetActive(false);
            indexOfElementInObject = 0;
            characterManager.transform.GetChild(indexOfElementInObject).gameObject.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }
}

public class CharacterStateComplete : FactoryState
{
    private HandlerAnimationInGameplay _anim;
    private bool checkAnimation = false;

    public CharacterStateComplete(HandlerAnimationInGameplay anim)
    {
        _anim = anim;
    }
    public void Start()
    {
        if (!checkAnimation)
        {
            _anim.PlayAnimationTextPraise();
            checkAnimation = true;
        }

    }

    public void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }
}

public class CharacterStateLose : FactoryState
{
    private CharacterManager gameManager;
    public CharacterStateLose(CharacterManager manager)
    {
        gameManager = manager;
    }
    public void Start()
    {
        gameManager.transform.gameObject.SetActive(false);
    }

    public void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }
}

public class HandlerAnimationInGameplay
{
    private UnityEngine.Animation animationOfStateChange;
    private GameObject objectOfTextPraiseUI;
    private CharacterManager characterManager;

    public HandlerAnimationInGameplay(UnityEngine.Animation animStateChange, GameObject objectTextPraiseUI, CharacterManager manager)
    {
        animationOfStateChange = animStateChange;
        objectOfTextPraiseUI = objectTextPraiseUI;
        characterManager = manager;
    }

    public void PlayAnimationChange()
    {
        animationOfStateChange.Play();
    }


    public void PlayAnimationTextPraise()
    {
        float indexPos = UnityEngine.Random.Range(0, 3);
        Vector3 pos = new Vector3(indexPos, characterManager.transform.position.y + 3, 0);
        objectOfTextPraiseUI.transform.position = pos;
        int index = UnityEngine.Random.Range(0, 5);
        SoundManager.Instance.indexOfAudioTextPraise = index;
        objectOfTextPraiseUI.transform.GetChild(index).gameObject.SetActive(true);
        objectOfTextPraiseUI.transform.GetChild(index).gameObject.GetComponent<UnityEngine.Animation>().Play();
        SoundManager.Instance.PlaySoundTextPraise();
    }
}
