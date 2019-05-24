#include <SFML/Graphics.hpp>
#include <Box2D/Box2D.h>
#include <iostream>

#include "Core/App.h"
//#include "Core/ResourceManager.h"
//#include "Core/SceneManager.h"
//#include "Core/TimeHandler.h"

//#include "Input/EventManager.h"
//#include "Input/InputManagement.h"

//#include "Physics/ContactListener.h"

//#include "Rendering/Window.h"

//#include "../Game/GameObject.h"
//#include "../Game/Game.h"
//#include "../Game/Player.h"
//#include "../Game/Ball.h"

///The main thread that generates the App and runs
int main()
{
	//create a new GameApp with a given Window Name
	App gameApp = App("ROME");

	//call the start function of the Game
	gameApp.getGame()->start();
	//Assign the InputComponent of the Player with reference to the Event Manager
	gameApp.getGame()->givePlayerInput(gameApp.getEventManager());

	//On tick
	while (gameApp.getWindow()->instance()->GameWindow->isOpen())
	{
		std::cout << "Delta Time: " << gameApp.getDeltaTime() << '\r';
		//Clear and update the App and Window
		gameApp.getWindow()->instance()->GameWindow->clear(sf::Color(123,132,123,255));
		gameApp.Update();		
		gameApp.getWindow()->instance()->GameWindow->display();
	}
	return 0;
}