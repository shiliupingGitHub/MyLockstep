﻿// ---------------------------------------------------------------------
// THIS FILE IS AUTO-GENERATED BY BEHAVIAC DESIGNER, SO PLEASE DON'T MODIFY IT BY YOURSELF!
// ---------------------------------------------------------------------

#ifndef _BEHAVIAC_MEMBER_VISITOR_H_
#define _BEHAVIAC_MEMBER_VISITOR_H_

#include "behaviac_agent_headers.h"

// Agent property and method handlers


struct METHOD_TYPE_AIAgent_DoHMove { };
template<> inline void AIAgent::_Execute_Method_<METHOD_TYPE_AIAgent_DoHMove>(int p0)
{
	this->AIAgent::DoHMove(p0);
}

struct METHOD_TYPE_AIAgent_GetFaceEnermyX { };
template<> inline int AIAgent::_Execute_Method_<METHOD_TYPE_AIAgent_GetFaceEnermyX>()
{
	return this->AIAgent::GetFaceEnermyX();
}

struct METHOD_TYPE_AIAgent_GetState { };
template<> inline int AIAgent::_Execute_Method_<METHOD_TYPE_AIAgent_GetState>()
{
	return this->AIAgent::GetState();
}

struct METHOD_TYPE_AIAgent_GetX { };
template<> inline int AIAgent::_Execute_Method_<METHOD_TYPE_AIAgent_GetX>()
{
	return this->AIAgent::GetX();
}


#endif // _BEHAVIAC_MEMBER_VISITOR_H_