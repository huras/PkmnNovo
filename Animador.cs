using UnityEngine;
using System.Collections;

public class Animador : MonoBehaviour
{

    public Animator an;
    [SerializeField]
    string currentAnimation = "",
        crossFadedAnimation = ""; //the animation it was playig before crossfade to a new one
    bool isCrossfading = false;

    public void SetAnimator(Animator an)
    {
        this.an = an;
    }

    public void Play(string animation)
    {
        isCrossfading = false;
        currentAnimation = animation;
        crossFadedAnimation = "";
        an.Play(animation, 0, 0);
    }

    public void CrossFadePlay(string animation, float time)
    {
        if (isCrossfading)
        {
            if (currentAnimation == animation)
                return;
        }
        isCrossfading = true;
        crossFadedAnimation = currentAnimation;
        currentAnimation = animation;
        an.CrossFadeInFixedTime(animation, time);
    }
}