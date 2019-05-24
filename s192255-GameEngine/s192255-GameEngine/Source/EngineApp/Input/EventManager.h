#pragma once
#include "../Rendering/Window.h"

#include <SFML/Graphics.hpp>

class Window;
//typedef std::map<sf::Event, std::vector<IEventHandler*>> eventMap;
///polls and stores events from the SFML Game Window
class EventManager
{
public:
	EventManager(); //!< Constructor
	~EventManager(); //!< Deconstructor

	void pollEvents(); //!< called every frame to poll the window for events
	void storeEvent(sf::Event currentEvent); //!< store an event into the list of events for the input manager to handle

	void setWindowPtr(Window* window) { m_windowRef = window;  } //!< set the pointer for the the window the game is running in

	std::vector<sf::Event> events; //!< the list of events that have been polled
	
	static EventManager* instance() //!< return the singleton instance of the Event Manager
	{
		if (!_instance)
			_instance = new EventManager;
		return _instance;
	}

private:
	Window* m_windowRef;

	static EventManager* _instance;

	//Idealy would have liked to have implemented a proper Event Management System with delegates and callbacks
	//Attempted to do so but to no avail

	/*bool AddListener(sf::Event type, IEventHandler *listener)
	{
		listeners[type].push_back(listener);
	}

	void FireEvent(sf::Event type, IEvent *event)
	{
		for (IEventHandler *handler : listeners[type])
		{
			handler->OnEvent(event);
		}
	}*/

};

