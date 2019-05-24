#pragma once
#include <SFML/Graphics.hpp>

///part of the App responsible for maintaining a global clock
class TimeHandler
{
public:
	TimeHandler(); //!< Construct the Time Handler
	~TimeHandler(); //!< Deconstruction function

	float deltaTime; //!< Store the Delta-Time value between frames

	sf::Clock clock = sf::Clock(); //!< Define the global clock to run

	void Update(); //!< Update and calculate DeltaTime

	

	static TimeHandler* instance() //!< Return the singleton instance of the Global Clock
	{
		if (!_instance)
			_instance = new TimeHandler;
		return _instance;
	}

private:
	static TimeHandler* _instance;
};

