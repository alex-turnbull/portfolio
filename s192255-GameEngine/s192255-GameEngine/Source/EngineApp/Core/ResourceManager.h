#pragma once
#include <SFML/Graphics.hpp>

///handles the loading and storing of texture resources on the disk
class ResourceManager
{
public:
	ResourceManager(); //!< Constructor function
	~ResourceManager(); //!< Deconstructor

	sf::Texture LoadTexture(std::string fileDir, std::string nameToStore); //!< Load the texture from file giving it an appropriate name to store in the Texture Map
	sf::Texture GetTextureFromMap(std::string textureRef); //!< Return a texture from the Texture Map given its key index

	std::map<std::string, sf::Texture> textureMap; //!< A map storing all of the loaded textures

	void storeInMap(std::string textureName, sf::Texture texture); //!< Stores the loaded Texture into the Texture map with its key pairing

	static ResourceManager* instance() //!< Return the Singleton Instance of the Resource Manager
	{
		if (!_instance)
			_instance = new ResourceManager;
		return _instance;
	}
private:
	static ResourceManager* _instance;
	

};

