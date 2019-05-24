#include "Game.h"

Game::Game(sf::RenderWindow *window, TimeHandler* time)
{
	gameWindow = window;
	m_timeHandler = time;
	m_sceneMan = new SceneManager();
	m_resMan = new ResourceManager();
}


Game::~Game()
{
}

void Game::start()
{
	//create a new Box2D world for the game
	world = new b2World(b2Vec2(0.0f,9.8f));

	//create the collider/contact listener and assign it to the current world
	ContactListener *listener = new ContactListener();
	world->SetContactListener(listener);

	//use the resource manager to load and store the textures from disk ready for the sprites to use
	m_resMan->LoadTexture("Source\\Assets\\floor.png", "floorTex");
	m_resMan->LoadTexture("Source\\Assets\\pirateMan.png", "pirate");
	m_resMan->LoadTexture("Source\\Assets\\ball.png", "ball");

	//give in the first level JSON
	loadScene("Source\\Assets\\Levels\\level1.json");	

	//call the load function for each game object in the world
	for (GameObject* gameO : gameObjectList)
	{
		gameO->Load(world);
	}
}

void Game::update()
{
	//update the GameObject and it's physics, drawing it's sprite 
	for(GameObject* gameO : gameObjectList)
	{
		gameO->Update();
		gameO->UpdatePhysics();		
		gameWindow->draw(gameO->sprite);
		
	}
	
	//drive the simulation of the world and physics
	world->Step(m_timeHandler->deltaTime*10,8, 8);
}

void Game::loadScene(std::string levelFileDir)
{
	//parse the JSON in the Scene Manager and get the GameObject definitions it created
	m_sceneMan->parseSceneFromFile(levelFileDir);

	//Loop through all of the definitions and create the respective game objects and push into the world's list of game objects
	for (SceneManager::GameObjectDef* GameObj : m_sceneMan->GameObjects)
	{
		//construct and assign texture for each one
		if (GameObj->type == std::string("Platform"))
		{
			Platform *platform = new Platform(GameObj->position.x, GameObj->position.y, GameObj->rotation);
			platform->texture = m_resMan->GetTextureFromMap(GameObj->spriteName);
			platform->sprite.setTexture(platform->texture);
			gameObjectList.push_back(platform);
		}

		if (GameObj->type == std::string("Player"))
		{
			player = new Player(GameObj->position.x, GameObj->position.y, GameObj->rotation);
			player->texture = m_resMan->GetTextureFromMap(GameObj->spriteName);
			player->sprite.setTexture(player->texture);
			gameObjectList.push_back(player);
		}

		if (GameObj->type == std::string("Ball"))
		{
			Ball *ball = new Ball(GameObj->position.x, GameObj->position.y, GameObj->rotation);
			ball->texture = m_resMan->GetTextureFromMap(GameObj->spriteName);
			ball->sprite.setTexture(ball->texture);
			gameObjectList.push_back(ball);
		}

	}
}

void Game::givePlayerInput(EventManager* eventManager)
{
	//give the player object the Input manager based on the Event Manager
	player->inputComponent->eventHandler = eventManager;
}
