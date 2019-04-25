#include "wrapper_define.h"
#include "controller.h"
using namespace lockstep;
extern "C"
{

	_DLL_EXPORT void ControllerRegisterReadFile(IntIntStringMethod method)
	{
		Controller::GetInstance()->RegisterReadFile(method);
	}

	_DLL_EXPORT void ControllerRegisterOnCreate(IntPtrMethod method)
	{
		Controller::GetInstance()->RegisterOnCreate(method);
	}

	_DLL_EXPORT void ControllerRegisterOnDelete( ULongMethod method)
	{
		Controller::GetInstance()->RegisterOnDelete(method);
	}

	_DLL_EXPORT void ControllerRegisterOnAddEffect(ULongIntStringMethod method)
	{
		Controller::GetInstance()->RegisterAddEffect(method);
	}

	_DLL_EXPORT void ControllerRegisterOnUseSkill(ULongIntIntIntMethod method)
	{
		Controller::GetInstance()->RegisterUseSkill(method);
	}

	_DLL_EXPORT void ControllerRegisterOnSkillEnd(ULongIntIntIntMethod method)
	{
		Controller::GetInstance()->RegisterSkillEnd(method);
	}

	_DLL_EXPORT void ControllerRegisterOnHurt(ULongHurtNumMethod method)
	{
		Controller::GetInstance()->RegisterOnHurt(method);
	}

	_DLL_EXPORT void ControllerRegisterOnJump(ULongIntIntMethod method)
	{
		Controller::GetInstance()->RegisterOnJump(method);
	}


}
