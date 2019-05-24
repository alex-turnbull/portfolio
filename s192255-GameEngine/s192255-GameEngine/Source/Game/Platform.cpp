#include "Platform.h"



Platform::Platform(int posX, int posY, float rot)
{
	initalPos = b2Vec2(posX, posY);
	initalRot = rot;
}


Platform::~Platform()
{
}

void Platform::Load(b2World *World)
{
	//give the platform a static body for the ground
	givePhysicsBody(World, b2BodyType::b2_staticBody);
	body->SetTransform(initalPos, 180 / 3.14 * initalRot);
}

void Platform::Update()
{
}