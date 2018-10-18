using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour {

	// Use this for initialization
	void Start () {
        physics.currentCollisionTags.Add("ground");
    }

    // Update is called once per frame
    void Update()
    {
        KeyLoop();
    }
    void FixedUpdate()
    {
        PhysiscsLoop();
    }

    void KeyLoop()//Coloque aqui coisas que devem ser atualizadas junto com os check-ups de inputs de teclado/mouse ou joystick
    {
        stateController.EvoluirEstadoAtual(Time.deltaTime);
    }
    void PhysiscsLoop()//Coloque aqui coisas que devem ser atualizadas junto com a física do jogo
    {

    }

    public ControladorDeEstados stateController;
    public FuncoesFisicas physics;
    public Animador animationController;

}
