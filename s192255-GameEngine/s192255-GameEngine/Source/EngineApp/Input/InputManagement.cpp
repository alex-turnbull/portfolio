#include "InputManagement.h"
#include <vector>



InputManagement::InputManagement()
{
}

InputManagement::~InputManagement()
{
}

//called every frame
void InputManagement::Listen()
{
	if(eventHandler->events.size() > 0)
	{
		//gets the list of events waiting to be handled from the Event System
		currentEvent = eventHandler->events.front();
		eventHandler->events.erase(eventHandler->events.begin()); //remove from event list

		if (&currentEvent != NULL)
		{
			//check the event and update the current action
			if (currentEvent.type == sf::Event::KeyPressed && currentEvent.key.code == sf::Keyboard::D)
			{
				currentAction = moveRight;
			}
			if (currentEvent.type == sf::Event::KeyPressed && currentEvent.key.code == sf::Keyboard::A)
			{
				currentAction = moveLeft;
			}
			if (currentEvent.type == sf::Event::KeyPressed && currentEvent.key.code == sf::Keyboard::Space)
			{
				currentAction = jump;
			}
			if (currentEvent.type == sf::Event::KeyReleased)
			{
				currentAction = stop;
			}
		}
		else
		{
			currentAction = no;
		}
	}	
}