#include "ResourceManager.h"



ResourceManager::ResourceManager()
{
	//ensure the map is clean empty for loading new textures
	textureMap.clear();
}


ResourceManager::~ResourceManager()
{
}

sf::Texture ResourceManager::LoadTexture(std::string fileDir, std::string nameToStore)
{
	//Load a texture from file and apply it to the sprite
	sf::Texture texture;
	texture.loadFromFile(fileDir);	

	//store the loaded texture into the map with a reference key
	storeInMap(nameToStore,texture);

	return texture;
}

sf::Texture ResourceManager::GetTextureFromMap(std::string textureRef)
{
	//using the reference key, return it's pairing texture from the map
	sf::Texture returnTex = textureMap[textureRef];

	return returnTex;
}

void ResourceManager::storeInMap(std::string textureName, sf::Texture texture)
{
	//insert the texture and texture ref pairing into the map
	textureMap.insert(std::pair<std::string, sf::Texture>(textureName, texture));
}
