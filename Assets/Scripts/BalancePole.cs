using System;
using System.Collections;
using System.Data.Common;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BalancePole : MonoBehaviour
{
    [SerializeField] private PoleData Data;
    [SerializeField] private float circleSize = 0.5f;
    [SerializeField] private float circle2Size = 0.5f;
    private float angle;
    private float swayTime = 0f;

    private bool dead = false;

    private State currentState = State.SWAYING;
    private AudioSource _sound;

    enum State { TILTING, SWAYING };

    private void Start()
    {
        _sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        float swayAngleAmplitude = Math.Abs(angle) * Data.swayAngleAmplitude / 5 + Data.swayAngleStartAmplitude;
        float swayTimeAmplitude = Math.Abs(angle) * Data.swaySpeedAmplitude / 5 + Data.swaySpeedStartAmplitude;
        if (currentState == State.SWAYING)
        {
            swayTime += Time.deltaTime * Data.swaySpeed * swayTimeAmplitude;
        }
        float wave = Mathf.Sin(swayTime) * swayAngleAmplitude;
        transform.rotation = Quaternion.Euler(0, 0, angle + wave);
    }

    public void UpdateAngle(float newAngle)
    {

        var amplifiedAngle = newAngle * Data.angleAmplitude;
        if (amplifiedAngle != angle)
        {
            currentState = State.TILTING;
            print("Angle: " + angle);
            print("New Angle: " + amplifiedAngle);
            if (Math.Abs(newAngle) >= Data.fallAngle)
            {
                newAngle = Math.Sign(newAngle) * 120;
                dead = true;
                StartCoroutine(UpdateAngleCoroutine(newAngle, 1f));
            }
            else
            {
                StartCoroutine(UpdateAngleCoroutine(amplifiedAngle, 0.3f));
            }
        }
    }

    private IEnumerator UpdateAngleCoroutine(float newAngle, float time)
    {
        Tween tween = DOTween.To(() => angle, x => angle = x, newAngle, 0.5f).SetEase(Ease.OutQuad);
        yield return tween.WaitForCompletion();
        if (dead)
        {
            _sound.Play();
            UiManager.instance.Restart();
        }
        currentState = State.SWAYING;
        
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, circleSize);
        Gizmos.DrawWireSphere(transform.position, circle2Size);
    }

    public bool IsDead()
    {
        return dead;
    }

}
