#include "skill.h"
#include "world.h"
#include "controller.h"
#include "rapidjson/document.h"
#include "animation.h"
#include "entity.h"
using namespace lockstep;
using namespace rapidjson;
Skill::Skill( World* pWorld, Entity* pEntity, int id):mId(id),
mPEntity(pEntity), mPWorld(pWorld),mPAni(nullptr), mCurIndex(0),
mUseIndex(0)
{
	auto data = Controller::GetInstance()->ReadFile(SKILL_CONFIG_TYPE, id);
	
	if (nullptr != data)
	{
		Document d;
		d.Parse(data);

		if (d.HasMember("ani"))
		{
			auto data_anis = d["ani"].GetArray();

			for (SizeType i = 0; i < data_anis.Size(); i++)
			{
				auto ani = new Animation(pWorld, pEntity, data_anis[i].GetInt());
				mAnis.push_back(ani);
			}
			
		}
	}
}
Skill::~Skill()
{
	for (auto it = mAnis.begin(); it != mAnis.end(); it++)
	{
		delete *it;
	}

	mAnis.clear();
}


void Skill::Tick()
{
	if (nullptr != mPAni)
	{
		mPAni->Tick();
		if (mPAni->IsEnd())
			mPAni = nullptr;
	}
}
int Skill::Use()
{

	if (nullptr == mPAni)
	{
		mCurIndex = 0;
	}

	else
	{
		mCurIndex++;

		if (mCurIndex >= mAnis.size())
			mCurIndex = 0;

	}

	if (mCurIndex <= mAnis.size())
	{
		mPAni = mAnis[mCurIndex];
		
		mUseIndex = mPAni->GetAniIndex();

	}
	if (nullptr != mPAni)
	{
		mPAni->Reset();
	}

	return mUseIndex;
}

bool Skill::IsEnd()
{
	if (nullptr != mPAni)
		return mPAni->IsEnd();
	else
		return true;

}
bool Skill::IsCanBreak()
{
	if (nullptr != mPAni)
		return mPAni->IsCanBreak();
	else
		return true;
}