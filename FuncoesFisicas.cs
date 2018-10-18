using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncoesFisicas : MonoBehaviour {

    public Transform translationPivot;

    public Vector3 gravityVelocity = Vector3.zero;

    public bool isOnGround = false;

    public List<string> currentCollisionTags = new List<string>();

    #region Gravidade
    public float gravityStrenght = 9.8f;//Gravidade local
    float gravityCharacterCoeficient = 1;//Permite cair mais rápido sem mexer no valor da gravidade

    public void InerciaGravitacional(float timeStep)//Move o transform de translação usando a velocidade gravitacional do personagem
    {
        //Checa as colisões e decide a quantidade de translação a se fazer baseado no time step
        Vector3 rayOrigin = translationPivot.position;

        float safetyRaycastExtraHeight = 0.2f;
        Vector3 currentGravityDirection = CurrentGravityDirectionAndMagnitude().normalized;
        if (gravityVelocity.magnitude != 0)
            rayOrigin -= gravityVelocity.normalized * safetyRaycastExtraHeight;//Essa distância evita que o transform fique vibrando no chão
        else
            rayOrigin -= currentGravityDirection * safetyRaycastExtraHeight;

        Ray ray = new Ray(rayOrigin, currentGravityDirection);//Cria o raio que será disparado para checagem de colisão

        float rayLength = safetyRaycastExtraHeight + gravityVelocity.magnitude * timeStep;
        RaycastHit[] hits = Physics.RaycastAll(ray, rayLength); //Obtem as possíveis colisões

        Debug.DrawLine(ray.origin, ray.origin + ray.direction.normalized * rayLength, Color.magenta);

        //Itera para descobrir qual é o mais próximo do pivo de translação      
        int hitsLenght = hits.Length;
        float nexterDist = float.MaxValue;
        int nexterIndex = -1;
        for (int i = 0; i < hitsLenght; i++)
        {
            if (currentCollisionTags.Contains(hits[i].collider.gameObject.tag))
            {
                float thisDist = Vector3.Distance(hits[i].point, translationPivot.position);
                if (thisDist < nexterDist)
                {
                    nexterDist = thisDist;
                    nexterIndex = i;
                }
            }
        }

        if (nexterIndex == -1)//Caso não haja colisões, translada usando toda a toda a velocidade
        {
            isOnGround = false;

            MoveTranslatationPivot(gravityVelocity * timeStep);
        }
        else//Caso haja colisão
        {
            isOnGround = true;

            //Se houve colisão é porque iríamos alcançar ou passar além do ponto de colisão caso ela não houvesse acontecido, logo podemos nos teleportaremos para o ponto da colisão.
            ChangeTranslationPivotPosition(hits[nexterIndex].point);

            ZerarGravidade();//não precisamos mais de nos mover pois estamos no ponto da colisão
        }
    }
    public void ApplyGravityAcceleration(float timeStep)//Aplica uma mudança de velocidade na velocidade gravitacional
    {
        Vector3 currentGravityAcceleration = CurrentGravityDirectionAndMagnitude() * timeStep * gravityCharacterCoeficient;
        gravityVelocity += currentGravityAcceleration;
    }
    public void ZerarGravidade()
    {
        gravityVelocity = Vector3.zero;
    }    
    Vector3 CurrentGravityDirectionAndMagnitude()
    {
        return Vector3.down * gravityStrenght;
    } 
    #endregion

    public void MoveTranslatationPivot(Vector3 amount)
    {
        ChangeTranslationPivotPosition(translationPivot.position + amount);
    }
    public void ChangeTranslationPivotPosition(Vector3 newPosition)
    {
        translationPivot.position = newPosition;
    }//The only that can change the translationPivot Position 
}
