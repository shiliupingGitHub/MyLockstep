#include "controller.h"
using namespace lockstep;
Controller::Controller() :mOnReadFile(nullptr),
mOnCreate(nullptr), mOnDelete(nullptr),
mOnAddEffect(nullptr),mOnUseSkill(nullptr),
mOnSkillEnd(nullptr),mOnHurt(nullptr),
mOnJump(nullptr),mOnLog(nullptr)
{}
Controller::~Controller(){}
void Controller::RegisterReadFile(IntIntStringMethod method)
{
	mOnReadFile = method;
}

void Controller::Log(int type, const char* log)
{
	if (nullptr != mOnLog)
		mOnLog(type, log);
}
void Controller::RegisterOnLog(IntStringMethod method)
{
	mOnLog = method;
}
void  Controller::RegisterOnCreate(IntPtrMethod method)
{
	mOnCreate = method;
}
void  Controller::RegisterOnDelete(ULongMethod method)
{
	mOnDelete = method;
}
void Controller::RegisterAddEffect(ULongIntStringMethod method)
{
	mOnAddEffect = method;
}
void Controller::RegisterOnHurt(ULongHurtNumMethod method)
{
	mOnHurt = method;
}
void Controller::RegisterUseSkill(ULongIntIntIntMethod method)
{
	mOnUseSkill = method;
}

void Controller::RegisterSkillEnd(ULongIntIntIntMethod method)
{
	mOnSkillEnd = method;
}

void Controller::RegisterOnJump(ULongIntIntMethod method)
{
	mOnJump = method;
}

void Controller::Jump(unsigned long long id, int state, int dir)
{
	if (nullptr != mOnJump)
	{
		mOnJump(id, state, dir);
	}
}
void Controller::UseSkill(unsigned long long id, int skill_id, int skill_index, int ani_index)
{
	if (nullptr != mOnUseSkill)
	{
		mOnUseSkill(id, skill_id, skill_index, ani_index);
	}
}
void Controller::Hurt(unsigned long long id, hurt_data h)
{
	if (nullptr != mOnHurt)
	{
		mOnHurt(id, h);
	}
}
void Controller::AddEffect(unsigned long long id, int effect, const char* bind)
{
	if (nullptr != mOnAddEffect)
	{
		mOnAddEffect(id, effect, bind);
	}
}
const char* Controller::ReadFile(int type, int id, const char* ex)
{
	if (mOnReadFile != nullptr)
		return mOnReadFile(type, id, ex);
	return nullptr;
}
void Controller::SkillEnd(unsigned long long id, int skill_id, int skill_index, int ani_index)
{
	if (nullptr != mOnSkillEnd)
		mOnSkillEnd(id, skill_id, skill_index, ani_index);
}
void Controller::invoke_on_create(int type, void* ptr)
{
	if (nullptr != mOnCreate)
	{
		mOnCreate(type, ptr);
	}
}
void Controller::invoke_on_delete(unsigned long long id)
{
	if (nullptr != mOnDelete)
	{
		mOnDelete(id);
	}
}

Controller* Controller:: GetInstance()
{
	static Controller s_instance;

	return &s_instance;
}



