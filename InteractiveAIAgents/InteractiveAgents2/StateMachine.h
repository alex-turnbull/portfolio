#pragma once
#include "InteractiveEntity.h"
#include "BaseState.h"
//include States
#include "moveState.h"
#include "digState.h"
#include "bankState.h"

#include <math.h>

class InteractiveEntity;
class BaseState;
class StateMachine
{
public:
	StateMachine(InteractiveEntity *entity);
	~StateMachine();

	void Update();

	void ChangeState(BaseState * newState);

	BaseState* currentState;
	InteractiveEntity* interactiveEntity;

	BaseState::States activeState;

};

