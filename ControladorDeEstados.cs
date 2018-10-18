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
    public Personagem character;
    public void EvoluirEstadoAtual(float timeStep)
    {
        FuncoesFisicas physics = character.physics;

        switch (currentState)
        {
            case states.FreeFall:
                {
                    if (gravityAtracionOn) physics.ApplyGravityAcceleration(timeStep);
                    if (gravityInertiaOn) physics.InerciaGravitacional(timeStep);

                    if (physics.isOnGround)
                    {
                        ChangeState(states.NormalTerrain);
                    }
                }
                break;
        }
    }

    public enum states { FreeFall, NormalTerrain, None };
    void ChangeState(states newState)//Only the StateEvolution method call this function
    {
        currentState = newState;
        switch (newState)//Seta os valores fundamentais para que cada estado possa iniciar e executar corretamente
        {
            case states.NormalTerrain:
                {
                    character.animationController.CrossFadePlay("GroundWalk Idle", 0.12f);
                }
                break;
        }
    }
}
