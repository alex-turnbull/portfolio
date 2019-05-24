#pragma once
#include <iostream>

#include "GameObject.h"
#include "../EngineApp/Input/InputManagement.h"
#include "../EngineApp/Core/ResourceManager.h"

class GameObject;
///Controllable playable entity GameObject
class Player :
	public GameObject
{
public:
	Player(int posX, int posY, float rot); //!< Constructor taking in positional/rotational arguments
	~Player(); //!< Deconstructor

	void GameObject::Load(b2World *World); //!< Load the GameObject into the world
	void GameObject::Update(); //!< Update the GameObject of all functionallity

	b2Vec2 speed = b2Vec2(50, 0); //!< the base speed applied when moving

	enum movementDirections
	{
		Left,
		Right,
		Jump,
		Stop
	};

	movementDirections playerMovement; //!< stores the current action the Player is doing based on keyboard input
	 
	void move(movementDirections moveType); //!< move the entity based on direction using linear velocity
	
	InputManagement* inputComponent; //!< store a reference to the Input Management Component

private:
};

