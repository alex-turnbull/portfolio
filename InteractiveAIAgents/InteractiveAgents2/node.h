#pragma once
#include <SFML/Graphics.hpp>
#include <vector>

class node
{
public:
	node(sf::RectangleShape cellToRef);
	~node();

	enum nodeTypes
	{
		empty, //white
		start, //red
		path, //blue
		goal, //yellow
		checked, //pink
		obstacle, //black
		invisWall, //black
		bank, //yellow
	};

	nodeTypes nodeType;

	float posX;
	float posY;

	sf::Vector2f getPosition() { return sf::Vector2f(posX, posY); };

	int rowVal;
	int colVal;

	int costToTraverse = 1;
	int costSoFar = 0;
	double priority = 0;

	node* Parent = nullptr;
	node* Child = nullptr;

	node *north;
	node *south;
	node *west;
	node *east;

	std::vector<node*> neighbours;

	sf::RectangleShape cellRef;

	sf::Sprite cellSprite;
	sf::Texture spotTexture;
	sf::Texture blankTexture;
	sf::Texture rockTexture;

	void update();
};

