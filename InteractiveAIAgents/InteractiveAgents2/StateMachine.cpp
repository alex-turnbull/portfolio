#include "StateMachine.h"


StateMachine::StateMachine(InteractiveEntity *entity)
{
	//take in the entity to control and give it an initial state
	interactiveEntity = entity;
	currentState = new moveState(interactiveEntity);
	currentState->Enter();
}


StateMachine::~StateMachine()
{
}

void StateMachine::Update()
{
	activeState = currentState->activeState;
	currentState->Run();
	ChangeState(currentState->GetNewState());
}

void StateMachine::ChangeState(BaseState * newState)
{
	if (newState == nullptr)
	{ 
		return;
	}

	currentState->Exit();

	BaseState* oldState = currentState;

	currentState = newState;
	currentState->Enter();

	//delete the previous state to clean up memory
	delete oldState; 
}
