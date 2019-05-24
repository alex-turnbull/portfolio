#include "SceneManager.h"

#define FILE_BUFFER 32768


SceneManager::SceneManager()
{
}


SceneManager::~SceneManager()
{
}

void SceneManager::parseSceneFromFile(std::string levelFileDir)
{
	//empty all of the GameObjects ready for new generation
	GameObjects.clear();

	//open the JSON level file and prepare variables for reading
	FILE* fileToRead = fopen(levelFileDir.c_str(), "rb");
	char buffer[FILE_BUFFER];
	rapidjson::FileReadStream is(fileToRead, buffer, sizeof(buffer));

	rapidjson::Document doc;
	doc.ParseStream<0, rapidjson::UTF8<>, rapidjson::FileReadStream>(is);

	//if the file loaded is in a valid JSON format begin to parse
	if(doc.IsObject())
	{	
		//define a interator that will be used to count up and be included in the name finding of objects
		int iterator = 0;
		
		while(true)
		{
			//define the base name value
			char baseName[64] = "Object_";
			char intValue[8];

			iterator++;
			//assign the int value as a char
			sprintf(intValue, "%d", iterator);

			//concationate the base name and iterator string to create the Object name defined in the JSON
			strcat(baseName, intValue);

			//check if it's a valid object/reached the end of the file
			if(doc.HasMember(baseName))
			{
				if(doc[baseName].IsObject())
				{
					//start creating a new GameObject definition if JSON object is valid
					GameObjectDef* newGameObject = new GameObjectDef;

					if(doc[baseName].HasMember("Type"))
					{
						//find the Type member and assign it's value to the gameObject
						newGameObject->type = doc[baseName]["Type"].GetString();
					}
					if (doc[baseName].HasMember("Position"))
					{
						//find the Position value and assign them
						auto &position = doc[baseName]["Position"];

						newGameObject->position = sf::Vector2f(position[0].GetInt(), position[1].GetInt());
					}
					if (doc[baseName].HasMember("Rotation"))
					{
						//find the rotation value and assign it
						newGameObject->rotation = doc[baseName]["Rotation"].GetInt();
					}
					if (doc[baseName].HasMember("Sprite"))
					{
						//find the sprite value and assign it, this is the key used in the resource manager maps
						newGameObject->spriteName = doc[baseName]["Sprite"].GetString();
					}

					//with the gameObject defined, push back into the list ready for the game to run and make the GameObjects
					GameObjects.push_back(newGameObject);
				}
			}
			else
			{
				break;
			}
			
		}
	}
	
}
