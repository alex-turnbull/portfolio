#include "ContactListener.h"



ContactListener::ContactListener()
{
}


ContactListener::~ContactListener()
{
}

void ContactListener::BeginContact(b2Contact* contact)
{
	std::cout << "\nBegin Contact" << std::endl;
}

void ContactListener::EndContact(b2Contact* contact)
{
	std::cout << "\nEnd Contact" << std::endl;
}

void ContactListener::PostSolve(b2Contact* contact, const b2ContactImpulse* impulse)
{
	//std::cout << "\nPost Solve" << std::endl;
}

void ContactListener::PreSolve(b2Contact* contact, const b2Manifold* oldManifold)
{
	//std::cout << "\nPre Solve" << std::endl;
}
