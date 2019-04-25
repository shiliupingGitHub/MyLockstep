#include "component/skill_component.h"
#include "skill.h"
#include "entity.h"
#include "lockstep.h"
#include "controller.h"
using namespace lockstep;
SkillComponent::SkillComponent(World* pWorld, Entity* pEnity, int id):Component(pWorld, pEnity)
,mPCurSkill(nullptr),mUseIndex(0), mNextUseIndex(-1)
{
	
}
void SkillComponent::Tick()
{
	if (nullptr != mPCurSkill)
	{
		mPCurSkill->Tick();

		//if (mPCurSkill->IsCanBreak())
		//{
		//	if (mNextUseIndex >= 0)
		//		on_use_skill(mNextUseIndex);
		//}
		if (mPCurSkill->IsEnd())
		{
			on_end();
		}
	}
}
SkillComponent::~SkillComponent()
{

	for (auto it = mSkills.begin(); it != mSkills.end(); it++)
	{
		delete *it;
	}

	mSkills.clear();
}
void SkillComponent::on_use_skill(int index)
{
	if (index >= 0 && index < mSkills.size())
	{
		auto t = mSkills[index];
		int ani_index = t->Use();

		if (ani_index >= 0)
		{

			mPCurSkill = t;
			mUseIndex = index;
			mPEntity->SetIsSkill(true);
			Controller::GetInstance()->UseSkill(mPEntity->GetId(), t->GetId(), index, ani_index);


		}
	}
	mNextUseIndex = -1;
}
void SkillComponent::UseSkill(int index)
{
	if (!IsCanUseSkill())
	{
	/*	if (nullptr != mPCurSkill)
		{
			if(!mPCurSkill->IsCanBreak())
				mNextUseIndex = index;
		}*/
		
		return;
	}
	
	on_use_skill(index);
}

void SkillComponent::Stop()
{
	if (nullptr != mPCurSkill)
	{
		on_end();
	}


}
void SkillComponent::Add(int id)
{
	Skill* pSkill = new Skill(mPWorld, mPEntity, id);
	mSkills.push_back(pSkill);
}
bool SkillComponent::IsCanUseSkill()
{
	if (mPEntity->GetFrozen() > 0)
		return false;
	if (nullptr == mPCurSkill)
		return true;
	if (mPCurSkill->IsCanBreak())
		return true;

	return false;
}

bool SkillComponent::IsUsingSkill()
{
	return mPCurSkill != nullptr;
}

void SkillComponent::on_end()
{
	mPEntity->SetIsSkill(false);
	Controller::GetInstance()->SkillEnd(mPEntity->GetId(), mPCurSkill->GetId(), mUseIndex, mPCurSkill->GetUsedIndex());
	mPCurSkill = nullptr;
}