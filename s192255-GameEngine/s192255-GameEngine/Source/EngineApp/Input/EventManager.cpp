#include "EventManager.h"



EventManager::EventManager()
{
}


EventManager::~EventManager()
{
}

void EventManager::pollEvents()
{
	//poll events from the SFML window
	sf::Event event;
	while (m_windowRef->instance()->GameWindow->pollEvent(event))
	{
		switch(event.type)
		{
			case sf::Event::Closed : std::cout << "\nClosing Program" << std::endl;
				break;

			case sf::Event::KeyPressed: //std::cout << "\nPressed Key: " << event.key.code << std::endl;
				storeEvent(event); //store the keypressed event
				break;

			case sf::Event::KeyReleased: //std::cout << "\nKey Released: " << event.key.code << std::endl;
				storeEvent(event); //store the keyreleased event
				break;
		}

		if (event.type == sf::Event::Closed)
			//close the SFML window on closed event
			m_windowRef->instance()->GameWindow->close();
	}
}

void EventManager::storeEvent(sf::Event currentEvent)
{
	//store the event into a list for the Input Manager to handle
	events.push_back(currentEvent);
}
