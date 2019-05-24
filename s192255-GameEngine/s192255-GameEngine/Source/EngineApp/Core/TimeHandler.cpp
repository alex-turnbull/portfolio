#include "TimeHandler.h"



TimeHandler::TimeHandler()
{
}


TimeHandler::~TimeHandler()
{
}

void TimeHandler::Update()
{
	//reset the clock each frame and store the deltatime
	sf::Time elapsedTime = clock.getElapsedTime();

	deltaTime = elapsedTime.asSeconds();

	clock.restart();
}
