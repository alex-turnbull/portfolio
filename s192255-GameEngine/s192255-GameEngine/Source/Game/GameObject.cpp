#include "GameObject.h"

GameObject::GameObject()
{
}


GameObject::~GameObject()
{
}

void GameObject::Destroy()
{
	//remove the GameObject from the world
	body->GetWorld()->DestroyBody(body);
}

void GameObject::SetSprite()
{ 	
	//assign the sprites texture using the GameObjects stored texture
	sprite.setTexture(texture);
}

void GameObject::givePhysicsBody(b2World *World, b2BodyType physType)
{
	//create a box2D bodyb for the GameObject to use to simulate physics
	b2BodyDef BodyDef;
	BodyDef.position = b2Vec2(initalPos.x, initalPos.y);
	BodyDef.fixedRotation = true;
	BodyDef.type = physType;
	b2Body* Body = World->CreateBody(&BodyDef);
	body = Body;

	//create the box shape of the collider based on the texture
	b2PolygonShape Shape;
	Shape.SetAsBox((GameObject::texture.getSize().x / 2), (GameObject::texture.getSize().y / 2));
	b2FixtureDef FixtureDef;
	FixtureDef.density = 1.f;
	FixtureDef.friction = 0.05f;
	FixtureDef.shape = &Shape;
	Body->CreateFixture(&FixtureDef);
}

void GameObject::gravity()
{
	//simulate gravity every frame for dynamic objects
	b2Vec2 vel = body->GetLinearVelocity();
	vel.y = 100;
	body->SetLinearVelocity(vel);
}

void GameObject::ApplyVelocity()
{
	body->SetLinearVelocity(velocity);
}

void GameObject::UpdatePhysics()
{
	//udpate the sprite position based on the body position every frame and call the gravity function
	sprite.setOrigin(sf::Vector2f(texture.getSize().x / 2, texture.getSize().y / 2));
	sprite.setRotation(180 /3.14 * body->GetAngle());
	sprite.setPosition(sf::Vector2f(body->GetPosition().x, body->GetPosition().y));	
	velocity = body->GetLinearVelocity();
	gravity();
}
