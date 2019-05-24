#pragma once

#include <SFML/Graphics.hpp>
#include <iostream>

///Window class defines and handles the drawing and processing of sprites and the game world
class Window
{
public:
	Window(std::string name); //!< Constructor taking in name for the Window
	~Window(); //!< Deconstructor

	void CreateWindow(); //!< Create a new Render Window given the parameters

	sf::RenderWindow* GameWindow; //!< Store a reference to the SFML Render Window

	int getWidth() { return m_width; } //!< return the width of the window
	int getHeight() { return m_height; } //!< return the height of the window

	Window* instance(){ return m_instance; } //!< return the instance of the window

private:
	int m_width;
	int m_height;
	std::string m_name;

	Window* m_instance;
};

