using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RisingScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private FloatVariable score;

    private float _currentScore;

    [SerializeField] private float delayToRise;
    [SerializeField] private float pointsByRise;

    [SerializeField] private UnityEvent increase;
    [SerializeField] private UnityEvent end;

    public void StartRisingValue()
    {
        StartCoroutine(Rising());
    }

    public float GetCurrentScore()
    {
        return _currentScore / score.value;
    }

    private IEnumerator Rising()
    {
        while (_currentScore < score.value)
        {
            yield return new WaitForSeconds(delayToRise);
            _currentScore += pointsByRise;
            if (_currentScore >= score.value)
            {
                _currentScore = score.value;
                end.Invoke();
            }
            else
            {
                increase.Invoke();
            }
            tmp.text = _currentScore.ToString();
        }
    }
}
