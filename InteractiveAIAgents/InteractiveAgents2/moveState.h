#pragma once
#include "BaseState.h"
#include "digState.h"
#include "bankState.h"
#include "InteractiveEntity.h"
#include "grid.h"

#include <math.h>

class grid;
class moveState :
	public BaseState
{
public:
	moveState(InteractiveEntity* newEntity);
	~moveState();

	void Enter();
	void Run();
	void Exit();

	BaseState* GetNewState();
	sf::Clock DeltaClock;

	InteractiveEntity* currentEntity;

	grid* activeGrid;

};

