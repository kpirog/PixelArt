using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    private float time = 0.5f;
    private float timer = 0f;

    private bool isPlaying = false;
    public RawImage animationView;

    public Text animationTimeLabel;
    public Text animationButtonLabel;
    public List<AnimationField> animationFields;
    public int currentId = 0;

    void Start()
    {
        Stop();
    }

    private void Update()
    {
        if(isPlaying)
        {
            timer -= Time.deltaTime;
            if(timer < 0f)
            {
                PlayAnimationFrame(currentId);
                currentId++;

                if(currentId > animationFields.Count - 1)
                {
                    currentId = 0;
                }
            }
            
        }
    }

    public void SubstractAnimationTime()
    {
        if ((time - 0.1f) >= 0.05f)
        {
            time -= 0.1f;
            animationTimeLabel.text = time.ToString("n1") + "s";
        }
    }
    public void AddAnimationTime()
    {
        if ((time + 0.1f) < 2.1f)
        {
            time += 0.1f;
            animationTimeLabel.text = time.ToString("n1") + "s";
        }
    }
    public void Play()
    {
        isPlaying = true;
        animationView.gameObject.SetActive(true);
        animationButtonLabel.text = "Stop";
    }
    public void Stop()
    {
        isPlaying = false;
        animationView.gameObject.SetActive(false);
        animationButtonLabel.text = "Play";
    }
    public void AnimationSwitch()
    {
        if (isPlaying)
        {
            Stop();
        }
        else
        {
            Play();
        }
    }
    public void PlayAnimationFrame(int id)
    {
        if(animationFields[id].field.texture != null)
        {
            timer = time;
            animationView.texture = animationFields[id].field.texture;
        }
    }
    public void ClearAnimationFields()
    {
        foreach (var animationField in animationFields)
        {
            animationField.field.texture = null;
        }
        animationView.texture = null;
        Stop();
        time = 0.5f;
        animationTimeLabel.text = time.ToString("n1") + "s";
    }
}
