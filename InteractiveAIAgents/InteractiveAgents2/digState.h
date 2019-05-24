#pragma once
#include "BaseState.h"
#include "moveState.h"
#include "bankState.h"

class InteractiveEntity;
class digState :
	public BaseState
{
public:
	digState(InteractiveEntity* newEntity);
	~digState();

	void Enter();
	void Run();
	void Exit();

	BaseState* GetNewState();

	InteractiveEntity* currentEntity;

};

