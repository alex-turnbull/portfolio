#pragma once
#include <sfml/Graphics.hpp>
#include <iostream>
#include "node.h"
#include <algorithm>
#include <queue>
#include <random>

using namespace sf;

class grid
{
public:
	grid(RenderWindow *window, int col);
	~grid();

	float cellSizes = 50.0f;
	Color outlineColour = Color::Red;
	float outlineThickness = 2.0f;

	void draw();
	void resetValues();
	bool getRandomTarget();

	RenderWindow *currentWindow;

	std::vector<node> listOfNodes;

	node* startNode = nullptr;
	node* targetNode = nullptr;

	node* bankNode = nullptr;

	bool PathSet = false;

	void breadthFirst();
	void breadthFirstCheckNode(node* currentNode);
	void DrawPath(node* node);

	bool reachedGoal = false;
	std::vector<node*> currentNodesToCheck = std::vector<node*>();
	std::vector<node*> pathToTake = std::vector<node*>();

	void astar();
	void astarCheckNode(node* checkNode);
	double heuristic(node* target, node* next);

	struct NodeCompare
	{
		bool operator()(const node* firstNode, const node* secondNode) const
		{
			return firstNode->priority > secondNode->priority;
		}
	};

	std::priority_queue<node*, std::vector<node*>, NodeCompare> priorityQueue;

private:
	void assignNeighbours();
	bool nodeValid;
};

