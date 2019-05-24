#pragma once
#include <iostream>
#include "Box2D/Box2D.h"
#include "Box2D/Collision/b2Collision.h"

///Derived from Box2D ContactListener - handles the Physics of collision between GameObjects
class ContactListener : public b2ContactListener
{
public:

	ContactListener();
	~ContactListener();

	void BeginContact(b2Contact* contact); //!< Callback when Objects begin touching
	void EndContact(b2Contact* contact); //!< Callback when Objects stop colliding
	void PostSolve(b2Contact* contact, const b2ContactImpulse* impulse); //!< Post solving of Object Collision
	void PreSolve(b2Contact* contact, const b2Manifold* oldManifold); //!< Pre solving of Object Collision
};
