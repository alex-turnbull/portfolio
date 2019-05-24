#pragma once
#include <iostream>
#include <SFML/Graphics.hpp>

#include "InteractiveEntity.h"

#include <math.h>

class BaseState
{
public:
	virtual void Enter() = 0;
	virtual void Run() = 0;
	virtual void Exit() = 0;

	enum States
	{
		Moving,
		Digging,
		Banking
	};

	States activeState;

	virtual BaseState* GetNewState() = 0;
};