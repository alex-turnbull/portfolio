#include "App.h"

///Construct an App to Contain all the stuff
App::App(std::string winName)
{
	//Create all of the required objects and store them
	m_windowName = winName;
	m_windowPtr = new Window(m_windowName);

	m_eventMangerPtr = new EventManager();
	m_eventMangerPtr->setWindowPtr(m_windowPtr);

	m_timeHandlerPtr = new TimeHandler();

	m_gamePtr = new Game(m_windowPtr->GameWindow, m_timeHandlerPtr);
}

///Delete the pointer stored in order to clean up memory
App::~App()
{
	delete m_windowPtr;
	delete m_eventMangerPtr;
	delete m_timeHandlerPtr;
	delete m_gamePtr;
}

void App::Update()
{
	//Run the different components of the app
	m_timeHandlerPtr->Update();
	m_eventMangerPtr->pollEvents();
	m_gamePtr->update();
}
