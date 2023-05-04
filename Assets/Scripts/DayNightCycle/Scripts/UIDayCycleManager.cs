using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class UIDayCycleManager : MonoBehaviour
{
    [Header(("Day Transition"))]
    [SerializeField] private PlayableDirector _dayTransitionTimeline = null;
    
    [Header(("Night Transition"))]
    [SerializeField] private PlayableDirector _nightTransitionTimeline = null;
    
    public bool IsTransitionRunning { get; private set; }

    private Coroutine _currentTransitionCoroutine = null;
    
    public static UIDayCycleManager Instance {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void StartDayTransition()
    {
        if (IsTransitionRunning && (null != _currentTransitionCoroutine))
        {
            StopCoroutine(_currentTransitionCoroutine);
            _currentTransitionCoroutine = null;
        }
        StartCoroutine(_CoroutineStartDayTransition());
    }
    
    public void StartNightTransition()
    {
        if (IsTransitionRunning && (null != _currentTransitionCoroutine))
        {
            StopCoroutine(_currentTransitionCoroutine);
            _currentTransitionCoroutine = null;
        }
        StartCoroutine(_CoroutineStartNightTransition());
    }

    private IEnumerator _CoroutineStartDayTransition()
    {
        IsTransitionRunning = true;
        _dayTransitionTimeline.Play();
        yield return new WaitForSeconds((float)_dayTransitionTimeline.duration);
        IsTransitionRunning = false;
    }
    
    private IEnumerator _CoroutineStartNightTransition()
    {
        IsTransitionRunning = true;
        _nightTransitionTimeline.Play();
        yield return new WaitForSeconds((float)_nightTransitionTimeline.duration);
        IsTransitionRunning = false;
    }
}