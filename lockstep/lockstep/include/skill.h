#ifndef LOCKSTEP_SKILL_H
#define LOCKSTEP_SKILL_H
#include <vector>
namespace lockstep
{
	class Entity;
	class World;
	class Animation;
	class  Skill
	{
	public:
		Skill( World* pWorld,  Entity* pEntity, int id);
		~Skill();
	public:

		void Tick();
		int Use();

		bool IsEnd();
		bool IsCanBreak();

		inline int GetUsedIndex() { return mUseIndex; }
		inline int GetId() { return mId; }
	private:

		int mId;
		 Entity* mPEntity;
		 World* mPWorld;
		Animation* mPAni;
		std::vector<Animation*> mAnis;
		int mCurIndex;
		int mUseIndex;
	};
}

#endif // !_SKILL_H

