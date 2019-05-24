#include <SFML/Graphics.hpp>
#include <iostream>
#include <iomanip>

#include "InteractiveEntity.h"
#include "wanderEntity.h"
#include "grid.h"

using namespace sf;

int main()
{
	RenderWindow *window = new RenderWindow(VideoMode(1507.5, 805), "Scoop");

	grid activeGrid = grid(window, 10);
	InteractiveEntity *entity = new InteractiveEntity(&activeGrid);
	wanderEntity *wanderE = new wanderEntity(120, 200, 15, entity);

	sf::Sprite backgroundS;
	sf::Texture backgoundT;
	backgoundT.loadFromFile("Assets\\island2.png");
	backgroundS.setTexture(backgoundT);
	backgroundS.setPosition(2.5, 2.5);

	enum pathfindingAlgorithms
	{
		aStar,
		breadth
	};

	pathfindingAlgorithms currentAlgorithm = breadth;

	
	//activeGrid.startNode = &activeGrid.listOfNodes[10];

	entity->entitySprite.setPosition(activeGrid.startNode->getPosition());
	//entity->entitySprite.setPosition(500, 500);

	activeGrid.listOfNodes[300].nodeType = node::goal;
	activeGrid.targetNode = &activeGrid.listOfNodes[300];
	//Poorly predefined nodes for the obstacles and invisible walls around the island
#pragma region invisibleWalls

	//first two columns
	for (size_t i = 0; i <= 32; i++)
	{
		activeGrid.listOfNodes[i].nodeType = node::invisWall;
	}

	//top row
	for (size_t i = 0; i <= 368; i+= 16)
	{
		activeGrid.listOfNodes[i].nodeType = node::invisWall;
	}

	//bottom row
	for (size_t i = 15; i <= 383; i+= 16)
	{
		activeGrid.listOfNodes[i].nodeType = node::invisWall;
	}

	//col 2
	activeGrid.listOfNodes[33].nodeType = node::invisWall;
	activeGrid.listOfNodes[34].nodeType = node::invisWall;
	activeGrid.listOfNodes[35].nodeType = node::invisWall;
	activeGrid.listOfNodes[44].nodeType = node::invisWall;
	activeGrid.listOfNodes[45].nodeType = node::invisWall;
	activeGrid.listOfNodes[46].nodeType = node::invisWall;
	activeGrid.listOfNodes[47].nodeType = node::invisWall;

	//col 3
	activeGrid.listOfNodes[49].nodeType = node::invisWall;
	activeGrid.listOfNodes[50].nodeType = node::invisWall;
	activeGrid.listOfNodes[61].nodeType = node::invisWall;
	activeGrid.listOfNodes[62].nodeType = node::invisWall;

	//col 4
	activeGrid.listOfNodes[65].nodeType = node::invisWall;
	activeGrid.listOfNodes[77].nodeType = node::invisWall;
	activeGrid.listOfNodes[78].nodeType = node::invisWall;

	//col 5
	activeGrid.listOfNodes[81].nodeType = node::invisWall;
	activeGrid.listOfNodes[93].nodeType = node::invisWall;
	activeGrid.listOfNodes[94].nodeType = node::invisWall;

	//col 6
	activeGrid.listOfNodes[110].nodeType = node::invisWall;

	//col 7
	activeGrid.listOfNodes[126].nodeType = node::invisWall;

	//col 8
	activeGrid.listOfNodes[142].nodeType = node::invisWall;

	//col 9
	activeGrid.listOfNodes[158].nodeType = node::invisWall;

	//col 10
	activeGrid.listOfNodes[174].nodeType = node::invisWall;

	//col 11
	activeGrid.listOfNodes[190].nodeType = node::invisWall;

	//col 12
	activeGrid.listOfNodes[193].nodeType = node::invisWall;
	activeGrid.listOfNodes[206].nodeType = node::invisWall;

	//col 13
	activeGrid.listOfNodes[222].nodeType = node::invisWall;

	//col 14
	activeGrid.listOfNodes[238].nodeType = node::invisWall;

	//col 15
	activeGrid.listOfNodes[254].nodeType = node::invisWall;

	//col 16
	activeGrid.listOfNodes[270].nodeType = node::invisWall;

	//col 20
	activeGrid.listOfNodes[321].nodeType = node::invisWall;
	activeGrid.listOfNodes[323].nodeType = node::invisWall;

	//col 21
	activeGrid.listOfNodes[337].nodeType = node::invisWall;
	activeGrid.listOfNodes[338].nodeType = node::invisWall;
	activeGrid.listOfNodes[339].nodeType = node::invisWall;
	activeGrid.listOfNodes[344].nodeType = node::invisWall;
	activeGrid.listOfNodes[345].nodeType = node::invisWall;
	activeGrid.listOfNodes[346].nodeType = node::invisWall;
	activeGrid.listOfNodes[347].nodeType = node::invisWall;

	//col 22
	for (size_t i = 352; i <= 367; i++)
	{
		if (activeGrid.listOfNodes[i].nodeType != node::bank)
		{
			activeGrid.listOfNodes[i].nodeType = node::invisWall;
		}
	}

	//col 23
	for (size_t i = 368; i <= 383; i++)
	{
		activeGrid.listOfNodes[i].nodeType = node::invisWall;
	}

#pragma endregion

#pragma region obstacles
	activeGrid.listOfNodes[39].nodeType = node::obstacle;
	activeGrid.listOfNodes[55].nodeType = node::obstacle;
	activeGrid.listOfNodes[68].nodeType = node::obstacle;
	activeGrid.listOfNodes[69].nodeType = node::obstacle;
	//activeGrid.listOfNodes[70].nodeType = node::obstacle;
	activeGrid.listOfNodes[71].nodeType = node::obstacle;
	activeGrid.listOfNodes[84].nodeType = node::obstacle;
	activeGrid.listOfNodes[90].nodeType = node::obstacle;
	activeGrid.listOfNodes[91].nodeType = node::obstacle;
	activeGrid.listOfNodes[92].nodeType = node::obstacle;
	activeGrid.listOfNodes[100].nodeType = node::obstacle;
	activeGrid.listOfNodes[103].nodeType = node::obstacle;
	activeGrid.listOfNodes[104].nodeType = node::obstacle;
	activeGrid.listOfNodes[107].nodeType = node::obstacle;
	activeGrid.listOfNodes[113].nodeType = node::obstacle;
	activeGrid.listOfNodes[114].nodeType = node::obstacle;
	activeGrid.listOfNodes[116].nodeType = node::obstacle;
	activeGrid.listOfNodes[123].nodeType = node::obstacle;
	activeGrid.listOfNodes[132].nodeType = node::obstacle;
	activeGrid.listOfNodes[133].nodeType = node::obstacle;
	activeGrid.listOfNodes[134].nodeType = node::obstacle;
	activeGrid.listOfNodes[139].nodeType = node::obstacle;
	activeGrid.listOfNodes[154].nodeType = node::obstacle;
	activeGrid.listOfNodes[155].nodeType = node::obstacle;
	activeGrid.listOfNodes[165].nodeType = node::obstacle;
	activeGrid.listOfNodes[168].nodeType = node::obstacle;
	activeGrid.listOfNodes[178].nodeType = node::obstacle;
	activeGrid.listOfNodes[181].nodeType = node::obstacle;
	activeGrid.listOfNodes[188].nodeType = node::obstacle;
	//activeGrid.listOfNodes[189].nodeType = node::obstacle;
	activeGrid.listOfNodes[194].nodeType = node::obstacle;
	activeGrid.listOfNodes[197].nodeType = node::obstacle;
	activeGrid.listOfNodes[198].nodeType = node::obstacle;
	activeGrid.listOfNodes[199].nodeType = node::obstacle;
	activeGrid.listOfNodes[204].nodeType = node::obstacle;
	//activeGrid.listOfNodes[217].nodeType = node::obstacle;
	activeGrid.listOfNodes[218].nodeType = node::obstacle;
	activeGrid.listOfNodes[219].nodeType = node::obstacle;
	activeGrid.listOfNodes[220].nodeType = node::obstacle;
	activeGrid.listOfNodes[225].nodeType = node::obstacle;
	activeGrid.listOfNodes[226].nodeType = node::obstacle;
	activeGrid.listOfNodes[246].nodeType = node::obstacle;
	activeGrid.listOfNodes[247].nodeType = node::obstacle;
	activeGrid.listOfNodes[252].nodeType = node::obstacle;
	activeGrid.listOfNodes[263].nodeType = node::obstacle;
	activeGrid.listOfNodes[264].nodeType = node::obstacle;
	activeGrid.listOfNodes[275].nodeType = node::obstacle;
	activeGrid.listOfNodes[283].nodeType = node::obstacle;
	activeGrid.listOfNodes[290].nodeType = node::obstacle;
	activeGrid.listOfNodes[291].nodeType = node::obstacle;
	activeGrid.listOfNodes[299].nodeType = node::obstacle;
	activeGrid.listOfNodes[315].nodeType = node::obstacle;

#pragma endregion

	//window->setFramerateLimit(30);

	//force floats to display in 2 decimal places within the console, just for visual consistancy
	std::cout << std::setprecision(2) << std::fixed;

	//std::cout << "Start Node: Row: " << activeGrid.startNode->rowVal << " Col: " << activeGrid.startNode->colVal << std::endl;

	sf::Clock globalClock;
	float lastTime = 0;
	
	//load a font for printing text through SFML on the window
	sf::Font font;
	if (!font.loadFromFile("arial.ttf"))
	{
		std::cout << "Error loading font" << std::endl;
	}

	//Handling information section
	sf::RectangleShape infoRect;
	infoRect.setSize(sf::Vector2f(300, 800));
	infoRect.setPosition(1205, 2.5);
	infoRect.setFillColor(sf::Color(80, 80, 80, 200));

	sf::Text title;
	title.setFont(font);
	title.setCharacterSize(32);
	title.setFillColor(sf::Color::White);
	title.setPosition(infoRect.getPosition() + sf::Vector2f(110, 10));
	title.setString("Info:");

	sf::Text pirateInfo;
	pirateInfo.setFont(font);
	pirateInfo.setCharacterSize(24);
	pirateInfo.setFillColor(sf::Color::White);
	pirateInfo.setPosition(infoRect.getPosition() + sf::Vector2f(75, 150));

	sf::Text stateInfo;
	stateInfo.setFont(font);
	stateInfo.setPosition(infoRect.getPosition() + sf::Vector2f(15, 110));
	stateInfo.setCharacterSize(24);
	stateInfo.setFillColor(sf::Color::White);

	sf::Text skeletonState;
	skeletonState.setFont(font);
	skeletonState.setCharacterSize(18);
	skeletonState.setFillColor(sf::Color::White);
	skeletonState.setPosition(infoRect.getPosition() + sf::Vector2f(15, 670));

	sf::Text skeletonDist;
	skeletonDist.setFont(font);
	skeletonDist.setPosition(infoRect.getPosition() + sf::Vector2f(15, 720));
	skeletonDist.setCharacterSize(18);
	skeletonDist.setFillColor(sf::Color::White);

	sf::Text algoText;
	algoText.setFont(font);
	algoText.setPosition(infoRect.getPosition() + sf::Vector2f(15, 400));
	algoText.setCharacterSize(18);
	algoText.setFillColor(sf::Color::White);

	//main loop for the program
	while (window->isOpen())
	{
		//update information section
		stateInfo.setString("Pirate in State: " + entity->currentState);
		pirateInfo.setString("Treasures: " + std::to_string(entity->treasureCount));		
		skeletonState.setString("Skeleton Behaviour: " + wanderE->currentBehaviour);
		skeletonDist.setString("Distance to Pirate: " + std::to_string(wanderE->distanceFromPirate));

		if(currentAlgorithm == aStar)
		{
			algoText.setString("Pathfinding Algo: A Star");
		}else
		{
			algoText.setString("Pathfinding Algo: Breadth First");
		}
		

		float currentTime = globalClock.restart().asSeconds();
		float fps = 1.f / currentTime;
		fps = floorf(fps);
		int fpsInt = static_cast<int>(fps);

		/*sf::Text text;
		text.setFont(font);
		text.setString(std::to_string(fpsInt));
		text.setCharacterSize(24);
		text.setFillColor(sf::Color::Red);
		text.setStyle(sf::Text::Bold);*/

		//event handler
		sf::Event event;
		while (window->pollEvent(event))
		{
			if (event.type == Event::Closed)
			{
				window->close();
			}

			if(event.type == Event::KeyPressed)
			{
				if (sf::Keyboard::isKeyPressed(sf::Keyboard::Space))
				{
					if(currentAlgorithm == breadth)
					{
						currentAlgorithm = aStar;
					}else
					{
						currentAlgorithm = breadth;
					}					
				}
			}
		}

		window->clear();		

		//control the various AI implementations
		//Path Finding
		if(currentAlgorithm == breadth)
		{
			activeGrid.breadthFirst();
		}
		else if(currentAlgorithm == aStar)
		{
			activeGrid.astar();
		}
		 
		entity->think(); //Finite State Machine
		wanderE->act(); //Steering Behvaiours

		//drawing of elements
		window->draw(backgroundS);		
		activeGrid.draw();
		window->draw(entity->GetSprite());

		window->draw(wanderE->wanderCircle);
		window->draw(wanderE->GetSprite());
		window->draw(wanderE->testCircle);
		
		//window->draw(text);

		window->draw(infoRect);
		window->draw(title);
		window->draw(pirateInfo);
		window->draw(stateInfo);
		window->draw(skeletonState);
		window->draw(skeletonDist);
		window->draw(algoText);

		window->draw(wanderE->testLol);

		window->display();
	}
	return 0;
}