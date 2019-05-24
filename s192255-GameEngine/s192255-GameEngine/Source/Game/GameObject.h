#pragma once

#include <SFML/Graphics.hpp>
#include <Box2D/Box2D.h>
#include "../EngineApp/Input/InputManagement.h"
#include "../EngineApp/Input/EventManager.h"

///Base GameObject class definition derived from a SFML Transformable
class GameObject: public sf::Transformable
{
public:
	GameObject();
	~GameObject();

	virtual void Load(b2World *World) = 0; //!< Virtal load function to be declared by derived Objects called on creation
	virtual void Update() = 0; //!< Virtal Update function to be declared by derived Objects called every tick
	void Destroy(); //!< Remove the GameObject in it's entirety from the GameWorld

	b2Vec2 initalPos; //!< Define the GameObject inital position on load
	float initalRot; //!< Define the GameObject inital rotation on load
	b2Vec2 velocity; //!< Storing of velocity for Physics calculations
	b2Body* body; //!< Storing it's physical collidable body/mass

	sf::Texture texture; //!< Store the material for the Object
	sf::Sprite sprite; //!< Store the sprite for the Object

	void SetSprite(); //!< Sets the texture of the sprite 

	void givePhysicsBody(b2World *World, b2BodyType physType); //!< Make the GameObject a physical interactable Object
	void gravity(); //!< Apply gravity where applicable
	void ApplyVelocity(); //!< Move the GameObject
	void UpdatePhysics(); //!< Update all of the Physics of the GameObject each frame
	
};

