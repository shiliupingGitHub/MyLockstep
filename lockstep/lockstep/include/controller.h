#ifndef LOCKSTEP_CONTROLLER_H
#define LOCKSTEP_CONTROLLER_H
#include "lockstep.h"
namespace lockstep
{
	class Controller
	{
		friend class World;
	private:
		Controller();
		virtual ~Controller();
		Controller(Controller&) = delete;
		Controller& operator=(const Controller&) = delete;

	public:
		void RegisterReadFile(IntIntStringMethod method);
		void RegisterOnCreate(IntPtrMethod method);
		void RegisterOnDelete(ULongMethod method);
		void RegisterAddEffect(ULongIntStringMethod method);
		void RegisterUseSkill(ULongIntIntIntMethod method);
		void RegisterSkillEnd(ULongIntIntIntMethod method);
		void RegisterOnHurt(ULongHurtNumMethod method);
		void RegisterOnJump(ULongIntIntMethod method);
	public:
		const char* InvokeReadFile(int type, int id, const char* ex = nullptr);
		void InvokeAddEffect(unsigned long long id, int, const char*);
		void InvokeUseSkill(unsigned long long id, int skill_id, int skill_index, int ani_index);
		void InvokeSkillEnd(unsigned long long id, int skill_id, int skill_index, int ani_index);
		void InvokeHurt(unsigned long long id, hurt_data h);
		void InvokeJump(unsigned long long id,int state, int dir);
		static Controller* GetInstance();
	private:
		void invoke_on_create( int type, void* ptr);
		void invoke_on_delete( unsigned long long id);
		
	private:
		IntIntStringMethod mOnReadFile;
		IntPtrMethod mOnCreate;
		ULongMethod mOnDelete;
		ULongIntStringMethod mOnAddEffect;
		ULongIntIntIntMethod mOnUseSkill;
		ULongIntIntIntMethod mOnSkillEnd;
		ULongHurtNumMethod mOnHurt;
		ULongIntIntMethod mOnJump;
	};

}
#endif // !_CONTROLLER_H


