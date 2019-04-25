#include "component.h"
using namespace lockstep;
Component::Component(World* pWorld, Entity* pEntity)
	:mPWorld(pWorld), mPEntity(pEntity)
{

}

Component::~Component()
{

}