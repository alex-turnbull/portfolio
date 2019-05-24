#include "wanderEntity.h"

wanderEntity::wanderEntity(float x, float y, float radius, InteractiveEntity *otherEntity)
{
	pirateEntity = otherEntity;

	posX = x;
	posY = y;

	rotation = -20;

	entitySprite.setOrigin(20, 20);
	entitySprite.setPosition(getPosition());
	entitySprite.setRotation(rotation);

	std::string filename = "Assets\\skeleton.png";
	texture.loadFromFile(filename);	
	entitySprite.setTexture(texture);

	wanderCircle.setRadius(radius);
	wanderCircle.setOrigin(wanderCircle.getGlobalBounds().width/2, wanderCircle.getGlobalBounds().height / 2);
	wanderCircle.setFillColor(sf::Color::Transparent);
	wanderCircle.setOutlineColor(sf::Color::Red);
	wanderCircle.setOutlineThickness(3);

	velocity = sf::Vector2f(cos(rotation * 3.14 / 180), sin(rotation * 3.14 / 180));
	wanderCircle.setPosition(getPosition() + wanderDistance * velocity);

}


wanderEntity::~wanderEntity()
{
}

//called every tick
void wanderEntity::act()
{
	elapsed = internalClock.getElapsedTime();
	//checks if if's close enough to the pirate entity given a threshold and chooses the correct steering behaviour as appropriate
	distanceFromPirate = sqrt((pirateEntity->entitySprite.getPosition().x - entitySprite.getPosition().x)*(pirateEntity->entitySprite.getPosition().x - entitySprite.getPosition().x) + (pirateEntity->entitySprite.getPosition().y - entitySprite.getPosition().y)*(pirateEntity->entitySprite.getPosition().y - entitySprite.getPosition().y));
	if(distanceFromPirate < checkThreshold)
	{
		pursue();
	}else
	{
		wander();
	}
	internalClock.restart();
}

//wandering steering behaviour - if not in range of pirate
void wanderEntity::wander()	
{
	currentBehaviour = "Wandering";
	//generate a random point on the circumference of the wandering circle attached to the entity
	auto randomPoint = wanderCircle.getTransform().transformPoint(wanderCircle.getPoint(rand() * 31));

	float targetRot = atan2(randomPoint.y - entitySprite.getPosition().y, randomPoint.x - entitySprite.getPosition().x) * (180/3.14);

	//Constrain to the screen with extra threshold, add rotation if hitting the edges
	if (entitySprite.getPosition().x < windowLeftThreshold)
	{
		entitySprite.setPosition(windowLeftThreshold, entitySprite.getPosition().y);
		targetRot -= rotationAdjustment;

	}
	else if (entitySprite.getPosition().x > windowRightThreshold)
	{
		entitySprite.setPosition(windowRightThreshold, entitySprite.getPosition().y);
		targetRot -= rotationAdjustment;
	}

	if (entitySprite.getPosition().y < windowTopThreshold)
	{
		entitySprite.setPosition(entitySprite.getPosition().x, windowTopThreshold);
		targetRot -= rotationAdjustment;
	}
	else if (entitySprite.getPosition().y > windowBottomThreshold)
	{
		entitySprite.setPosition(entitySprite.getPosition().x, windowBottomThreshold);
		targetRot -= rotationAdjustment;
	}

	//
	sf::Vector2f direction = sf::Vector2f(cos(entitySprite.getRotation() * 3.14 / 180), sin(entitySprite.getRotation() * 3.14 / 180));
	sf::Vector2f pos = entitySprite.getPosition() + (direction * 50.0f) * elapsed.asSeconds();

	entitySprite.setPosition(pos);
	entitySprite.setRotation(targetRot);

	velocity = sf::Vector2f(cos(entitySprite.getRotation() * 3.14 / 180), sin(entitySprite.getRotation() * 3.14 / 180));
	wanderCircle.setPosition(entitySprite.getPosition() + wanderDistance * velocity);
	testCircle.setRadius(5);
	testCircle.setFillColor(sf::Color::Green);
	testCircle.setOrigin(testCircle.getGlobalBounds().width / 2, testCircle.getGlobalBounds().height / 2);
	testCircle.setPosition(randomPoint);

	//wanderTarget += sf::Vector2f(rand() % 3 + (-1) * wanderJitter, rand() % 3 + (-1) * wanderJitter);
}

//pursuing steering behaviour - if close enough to pirate
void wanderEntity::pursue()
{
	currentBehaviour = "Pursuing";
	pirateEntity->velocity;

	sf::Vector2f targetPos;	

	targetPos = pirateEntity->GetSprite().getPosition() + (pirateEntity->velocity * 75.0f);
	

	testLol.setPosition(targetPos);
	testLol.setOrigin(testLol.getGlobalBounds().width / 2, testLol.getGlobalBounds().height / 2);
	testLol.setFillColor(sf::Color::Green);
	testLol.setRadius(5.0f);

	sf::Vector2f direction = sf::Vector2f(cos(entitySprite.getRotation() * 3.14 / 180), sin(entitySprite.getRotation() * 3.14 / 180));
	sf::Vector2f pos = entitySprite.getPosition() + (direction * 50.0f) * elapsed.asSeconds();

	if(distanceFromPirate < 75)
	{
		targetPos = pirateEntity->GetSprite().getPosition();
	}

	float targetRot = atan2(targetPos.y - entitySprite.getPosition().y, targetPos.x - entitySprite.getPosition().x) * (180 / 3.14);


	entitySprite.setPosition(pos);
	entitySprite.setRotation(targetRot);

	if (distanceFromPirate < 10)
	{
		handlePirateAttack();
	}
}

void wanderEntity::handlePirateAttack()
{
	if(!(pirateEntity->treasureCount <= 0))
	{
		pirateEntity->treasureCount -= 1;
	}
	int randX = rand() % 100 + 110;
	int randY = rand() % 100 + 110;
	
	entitySprite.setPosition(randX,randY);
}
