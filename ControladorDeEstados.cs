using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeEstados : MonoBehaviour {

    public bool gravityAtracionOn = true;
    public bool gravityInertiaOn = true;
    public bool inerciaMovimentacionalOn = true;
    public bool jumpInertiaOn = true;
    public bool frictionOn = true;

    public states currentState = states.FreeFall;
    public void EvoluirEstadoAtual(Personagem character, float timeStep)
    {
        FuncoesFisicas physics = character.physics;

        switch (currentState)
        {
            case states.FreeFall:
                {
                    if (gravityAtracionOn) physics.ApplyGravityAcceleration(timeStep);
                    if (gravityInertiaOn) physics.InerciaGravitacional(timeStep);
                }
                break;
        }
    }

    public enum states { FreeFall, None };
    

}
