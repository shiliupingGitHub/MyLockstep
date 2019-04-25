#include "entity.h"
#include "world.h"
#include "component.h"
using namespace lockstep;
Entity::Entity(World* pWorld, unsigned long long id, int type)
	:mWrold(pWorld),mId(id),mType(type),mD(1),
	mVisual(0),mCamp(0), mXMove(0)
	,mNextXMove(0), mState(0)
{
	memset(&mPos, 0, sizeof(Vector2));
}

 bool Entity::GetIsCmdCtr()
{
	 return mState & 1 << STATE_CMD;
}
 void Entity::SetIsCmdCtr(bool is_cmd_ctr)
{
	 if (is_cmd_ctr)
	 {
		 mState |= 1 << STATE_CMD;
	 }
	 else
	 {
		 if (GetIsCmdCtr())
		 {
			 mState &= (~0) ^ (1 << STATE_CMD);
		 }
	 }
}

 bool Entity::GetIsSkill()
 {
	 return mState & 1 << STATE_SKILL;
 }
 void Entity::SetIsSkill(bool is_skill)
 {
	 if (is_skill)
	 {
		 mState |= 1 << STATE_SKILL;
	 }
	 else
	 {
		 if (GetIsSkill())
			 mState &= (~0) ^ (1 << STATE_SKILL);
	 }
 }
Entity::~Entity()
{
	for (auto it = mComponent.begin(); it != mComponent.end(); it++)
	{
		mWrold->set_disable(it->first, it->second);

		delete it->second;
	}

	mComponent.clear();
}

void Entity::Add(string name, Component* pCompoent)
{
	Remove(name);
	mComponent[name] = pCompoent;
	mWrold->set_enable(name, pCompoent);
}
void Entity::Remove(string name)
{	
	auto it = mComponent.find(name);

	if (it != mComponent.end())
	{
		Component* pComponent = it->second;
		mComponent.erase(name);
		mWrold->set_disable(name, pComponent);
		delete pComponent;
	}


}

Component* Entity::Get(string name)
{
	auto it =  mComponent.find(name);

	if (it != mComponent.end())
	{
		return it->second;
	}

	return nullptr;
}
