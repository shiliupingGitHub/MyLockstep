#ifndef LOCKSTEP_SKILL_COMPONENT_H
#define LOCKSTEP_SKILL_COMPONENT_H
#include "rapidjson/document.h"
#include "component.h"
#include <vector>
using namespace rapidjson;
using namespace std;
namespace lockstep
{
	class Entity;
	class Skill;
	class World;
	class SkillComponent : public Component
	{
	public:
		SkillComponent(World* pWorld, Entity* pEnity, int id);
		virtual ~SkillComponent() override;

		void Add(int id);
		void UseSkill(int index);
		void Stop();
		void Tick();
		bool IsCanUseSkill();
		bool IsUsingSkill();
	private:
		void on_end();
		void on_use_skill(int index);
	private:
		vector<Skill*> mSkills;
		
		Skill* mPCurSkill;
		int mUseIndex;
		int mNextUseIndex;
		
	};
}



#endif
