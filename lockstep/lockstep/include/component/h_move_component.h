#ifndef LOCKSTEP_H_MOVE_COMPONENT_H
#define LOCKSTEP_H_MOVE_COMPONENT_H
#include "component.h"
namespace lockstep
{

	class HMoveComponent :public Component
	{
	public:
		HMoveComponent(World* pWorld, Entity* pEntity, int id);
		virtual ~HMoveComponent() override;
		 
	public:
		void Tick();
		void SetEnable(bool enable);

		inline void SetD(int d) { mD = d; }

	private:
		int mSpeed;
		bool mEnable;
		int mXMoveReduce;
		int mD;

	};
}
#endif
