#ifndef LOCKSTEP_SYSTEM_CREATOR_H
#define LOCKSTEP_SYSTEM_CREATOR_H
#include <string>
#include "system_factory.h"
namespace lockstep
{
	class GSystem;
	class World;
	class BaseSystemCreator
	{
	public:
		virtual ~BaseSystemCreator() {}
	public:
		virtual GSystem* Create(World* pWorld) = 0;
	};
	template<class T>
	class SystemCreator : public BaseSystemCreator
	{
	public:
		SystemCreator(std::string name)
		{
			SystemFactory::GetInstance().Add(name, this);
		}

		virtual ~SystemCreator() {}


		virtual GSystem* Create(World* pWorld)
		{
			return new T(pWorld);
		}

	private:

	};


}
#endif // !_SYSTEM_CREATOR_H

