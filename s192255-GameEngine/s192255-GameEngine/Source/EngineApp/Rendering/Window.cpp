#include "Window.h"

Window::Window(std::string name)
{
	//define the variables for the window
	m_name = name;
	m_width = sf::VideoMode::getDesktopMode().width;
	m_height = sf::VideoMode::getDesktopMode().height;
	CreateWindow();

	m_instance = this;
}

Window::~Window()
{
}

void Window::CreateWindow()
{
	//create the Render Window	
	GameWindow = new sf::RenderWindow(sf::VideoMode(m_width, m_height), m_name);
}
