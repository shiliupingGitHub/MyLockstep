#ifndef LOCKSTEP_SYSTEM_FACTORY_H
#define LOCKSTEP_SYSTEM_FACTORY_H
#include <map>
namespace lockstep
{
	class World;
	class BaseSystemCreator;
	class  SystemFactory
	{
	private:
		SystemFactory() = default;
		SystemFactory(SystemFactory&) = delete;
		SystemFactory& operator=(SystemFactory&) = delete;
	public:
		
		static SystemFactory& GetInstance();

		void Visit(World* pWorld);

		void Add(std::string name, BaseSystemCreator*);
	private:
		std::map<std::string, BaseSystemCreator*> mCreators;
	};


}
#endif // !_SYSTEM_FACTORY_H

