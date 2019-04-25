#ifndef  LOCKSTEP_ANIMATION_H
#define LOCKSTEP_ANIMATION_H
#include <map>
#include <vector>
#include "config/ani_config.h"
namespace lockstep
{
	class World;
	class AnimationEvent;
	class Entity;
	class ani_config;
	class Animation
	{
	public:
		Animation(World* pWorld ,Entity* pEntity ,int id);
		~Animation();
	public:
		void Tick();
		void Reset();
		bool IsEnd();
		bool IsCanBreak();

		inline int  GetAniIndex() { return mAniIndex; }
	private:

		int mFrameNum;
		int mBreakFrame;
		int mCurFrame;
		int mAniIndex;
		World* mPWorld;
		Entity* mPEntity;
		std::map<int, std::vector<AnimationEvent*>> mEvent;
		ani_config_data mAniData;
	};
}
#endif // ! _ANIMATION_H

