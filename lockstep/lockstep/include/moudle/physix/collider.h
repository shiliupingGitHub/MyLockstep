#ifndef LOCKSTEP_COLLIDER_H
#define LOCKSTEP_COLLIDER_H
namespace lockstep
{
	class Collider
	{
	public:
		Collider();
		virtual ~Collider();

		inline void SetUserData(void* pData) { mUserData = pData; }
		inline void* GetUserData() { return mUserData; }
	public:
		virtual void Tick();
	protected:

		void* mUserData;
	};

}
#endif // !LOCKSTEP_COLLIDER_H

