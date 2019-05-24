#include "digState.h"



digState::digState(InteractiveEntity* newEntity)
{
	currentEntity = newEntity;
	BaseState::activeState = States::Digging;
}


digState::~digState()
{
}

void digState::Enter()
{
	std::cout << "-- Entered Dig State --" << std::endl;
	currentEntity->internalClock.restart();
	currentEntity->entitySprite.setColor(sf::Color(73, 30, 0));
}

void digState::Run()
{
	sf::Time elapsed = currentEntity->internalClock.getElapsedTime();
	std::cout << "Running Dig State for: " << floorf(elapsed.asSeconds() * 100) / 100 << " seconds" << '\r';
}

void digState::Exit()
{
	std::cout << '\n' << "Successfully collected treasure";
	std::cout << '\n' << "-- Left Dig State --" << std::endl << '\n';
}

BaseState * digState::GetNewState()
{
	sf::Time elapsed = currentEntity->internalClock.getElapsedTime();

	if (elapsed.asSeconds() > 2.0f)
	{
		currentEntity->treasureCount += 1;
		currentEntity->currentGrid->resetValues();

		return new moveState(currentEntity);
	}
	return nullptr;
}
