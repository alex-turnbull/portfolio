#include "node.h"



node::node(sf::RectangleShape cellToRef)
{
	nodeType = empty;
	cellRef = cellToRef;

	spotTexture.loadFromFile("Assets\\xSpot.png");
	blankTexture.loadFromFile("Assets\\blank.png");
	rockTexture.loadFromFile("Assets\\rock.png");
	cellSprite.setOrigin(25, 25);
}


node::~node()
{
}

void node::update()
{
	
	switch(nodeType)
	{
	case empty: cellRef.setFillColor(sf::Color(255,255,255,0));
		cellSprite.setTexture(blankTexture);
		break;

	case start: cellRef.setFillColor(sf::Color(255, 0, 0, 255));
		cellSprite.setTexture(blankTexture);
		break;

	case goal: cellRef.setFillColor(sf::Color(0, 255, 255, 0));
		//std::string filename = "Assets\\xSpot.png";
		cellSprite.setTexture(spotTexture);
		cellSprite.setPosition(this->posX, this->posY);
		break;

	case path: cellRef.setFillColor(sf::Color(145, 145, 145, 75));
		cellSprite.setTexture(blankTexture);
		break;

	case checked: cellRef.setFillColor(sf::Color(110, 0, 114, 75));
		cellSprite.setTexture(blankTexture);
		break;

	case obstacle: cellRef.setFillColor(sf::Color(0, 0, 0, 0));
		cellSprite.setTexture(rockTexture);
		cellSprite.setPosition(this->posX, this->posY);
		break;

	case invisWall: cellRef.setFillColor(sf::Color(0, 0, 0, 0));
		cellSprite.setTexture(blankTexture);
		break;

	case bank: cellRef.setFillColor(sf::Color(0, 255, 255, 75));
		cellSprite.setTexture(blankTexture);
		break;

	default: cellRef.setFillColor(sf::Color::Green);
		break;
	}
}
