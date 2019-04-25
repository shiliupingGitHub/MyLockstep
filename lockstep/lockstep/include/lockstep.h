#ifndef LOCKSTEP_LOCKSTEP_H
#define LOCKSTEP_LOCKSTEP_H
#include <string>
#include <vector>
#include "cmd_handler.h"
#include "creator/cmd_creater.h"
#include "creator/animation_event_creator.h"
#include "creator/system_creator.h"
#include "creator/config_creator.h"
#include "data.h"
#include "utility.h"
using namespace std;
#define LS_IMP_SYSTEM(self)	friend class World; \
									self(World* pWorld);	\
									virtual ~self() override;\
									virtual Component* create_compoent(Entity* pEnity, int id) override; \
									virtual void tick();
#define LS_CMD_TYPE(type) namespace lockstep \
								{ \
									class Entity; \
									class type : public CmdHandler \
									{\
										public: \
											virtual void handle(Entity* pEntity, char* data) override; \
									};\
								}
#define IMPLEMENT_CMD_HANDLER(type, name) CmdCreator<type> s_cmd_handler_ ##name(#name)
#define IMPLEMENT_ANIMATION_EVENT(type,name) AnimationEventCreator<type> s_ ##type(#name)
#define IMPLEMENT_SYSTEM(type,name) SystemCreator<type> s_ ##type(#name)
#define IMPLEMENT_CONFIG(type) ConfigCreator<type> s_##type##_creator(#type)
#define MAP_CONFIG_TYPE 1
#define ENTITY_CONFIG_TYPE 2
#define SKILL_CONFIG_TYPE 3
#define BEHAVOUR_CONFIG_TYPE 4
#define ANIMATION_CONFIG_TYPE 5
#define ANIMATION_EVENT_CONFIG_TYPE 6
#define COMPONENT_CONFIG_FILE 7
#define CONFIG_FILE 8


enum STATE
{
	STATE_CMD = 0, //有命令
	STATE_SKILL = 1 //释放技能
};

extern "C"
{
	namespace lockstep
	{
		typedef const char* (*IntIntStringMethod) (int type, int id, const char*);
		typedef void(*IntPtrMethod)(int type, void* ptr);
		typedef void(*ULongMethod)(unsigned long long id);
		typedef void(*ULongIntStringMethod)(unsigned long long id, int, const char*);
		typedef void(*ULongIntIntIntMethod)(unsigned long long id, int index1, int index2, int index3);
		typedef void(*ULongHurtNumMethod)(unsigned long long id, hurt_data h);
		typedef void(*ULongIntIntMethod)(unsigned long long id, int index, int index2);
	}

}

#endif // !_LS_LOCKSTEP_H
