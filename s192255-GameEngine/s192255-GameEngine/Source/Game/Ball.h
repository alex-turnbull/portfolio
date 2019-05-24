#pragma once
#include "GameObject.h"

///Derived from a base GameObject, create a pushable ball object
class Ball :
	public GameObject
{
public:
	Ball(int posX, int posY, float rot); //!< Construct a ball given the position and rotation
	~Ball(); //!< Deconstructor

	void GameObject::Load(b2World *World); //!< Load the GameObject into the world
	void GameObject::Update(); //!< Update the GameObject of all functionallity
};

