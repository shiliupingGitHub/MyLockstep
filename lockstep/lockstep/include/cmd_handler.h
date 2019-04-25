#ifndef LOCKSTEP_CMD_HANDLER_H
#define LOCKSTEP_CMD_HANDLER_H
#include <string>
using namespace std;
namespace lockstep
{
	class Entity; 
	class CmdHandler
	{

		friend class CmdAdapter;
	public:
		virtual ~CmdHandler() {}
	private :

		virtual void handle(Entity* pEntity, char* data) = 0;		
	};
}


#endif // !CMD_HANDLER_H

