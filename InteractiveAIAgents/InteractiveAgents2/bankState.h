#pragma once
#include "BaseState.h"
#include "moveState.h"
#include "digState.h"

class InteractiveEntity;
class bankState :
	public BaseState
{
public:
	bankState(InteractiveEntity* newEntity);
	~bankState();

	void Enter();
	void Run();
	void Exit();

	BaseState* GetNewState();

	InteractiveEntity* currentEntity;

};

