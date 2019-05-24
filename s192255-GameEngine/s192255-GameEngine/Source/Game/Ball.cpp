#include "Ball.h"



Ball::Ball(int posX, int posY, float rot)
{
	//init the ball and assign the relevant variables
	initalPos = b2Vec2(posX, posY);
	initalRot = rot;
}


Ball::~Ball()
{
}

void Ball::Load(b2World *World)
{
	//on Object load, give the object a Box2D body for physics and set it's position
	givePhysicsBody(World, b2BodyType::b2_dynamicBody);
	body->SetTransform(initalPos, 180 / 3.14 * initalRot);
}

void Ball::Update()
{
}