#include "world.h"
#include "entity.h"
#include "system.h"
#include "cmd_adapter.h"
#include <memory>
#include "rapidjson/document.h"
#include "controller.h"
#include "math/crandom.h"
#include "component/skill_component.h"
#include "math/rect.h"
#include "system_factory.h"
#include "moudle/physix/physix.h"
using namespace lockstep;
using namespace rapidjson;

#define LS_ENTITY_START_ID	1
World::World() :mEnityId(LS_ENTITY_START_ID),
mMinX(0), mMaxX(0),
mMaxY(0),
mRand(new CRandom()),
mPPhysx(new Physix() )
{
	SystemFactory::GetInstance().Visit(this);
}

World::~World()
{
	for (auto it = mEntitys.begin(); it != mEntitys.end(); it++)
	{
		delete it->second;
	}

	mEntitys.clear();

	for (auto it = mSystem.begin(); it != mSystem.end(); it++)
	{
		auto s = it->second;

		delete s;
	}

	mSystem.clear();
	delete mRand;
	delete mPPhysx;
}

void World::DoCmd(command_data* cmd, int size)
{
	for (int i = 0; i < size; i++)
	{
		CmdAdapter::GetInstance()->do_cmd(this, cmd[i]);

	}

}
void World::Tick()
{
	for (auto it = mSystem.begin(); it != mSystem.end(); it++)
	{
		it->second->tick();
	}
}
Entity* World::create_entity(int type)
{
	mEnityId++;
	return create_entity_with_id(mEnityId, type);
}
Entity* World::create_entity_with_id(unsigned long long id, int type)
{
	Entity* pEnity = new Entity(this, id, type);
	mEntitys[id] = pEnity;
	return pEnity;
}
void World::delete_entity(Entity* pEnity)
{
	auto it = mEntitys.find(pEnity->GetId());

	if (it != mEntitys.end())
	{
		mEntitys.erase(pEnity->GetId());
		Controller::GetInstance()->invoke_on_delete(pEnity->GetId());
		delete pEnity;
	}
}

Entity* World::FindEntity(unsigned long long id)
{
	auto it = mEntitys.find(id);

	if (it != mEntitys.end())
	{
		return it->second;
	}
	return nullptr;
}
bool World::Load(const level_data map)
{

	const char* data = Controller::GetInstance()->InvokeReadFile(MAP_CONFIG_TYPE, map.mapId);
	mRand->SetSeed(map.mapId);
	if (data != nullptr)
	{
		Document d;

		d.Parse(data);

		if(d.HasMember("minX"))
			mMinX = d["minX"].GetInt();
		if (d.HasMember("maxX"))
			mMaxX = d["maxX"].GetInt();
		if (d.HasMember("maxY"))
			mMaxY = d["maxY"].GetInt();

		return true;
	}


	return false;
}
bool World::load_entity(entity_data& ed)
{
	
	const char* data = Controller::GetInstance()->InvokeReadFile(ENTITY_CONFIG_TYPE, ed.config_id);

	if (nullptr != data)
	{

		Document d;
		d.Parse(data);
		if (!d.HasMember("type"))
			return false;
		Entity* pEntity = create_entity_with_id(ed.id, d["type"].GetInt());

		Vector2 pos;
		pos = ed.pos;
		pEntity->SetPos(pos);
		pEntity->SetD(ed.d);
		pEntity->SetCamp(ed.camp);

		if (d.HasMember("visual"))
		{
			pEntity->SetVisual(d["visual"].GetInt());
		}

		if (d.HasMember("components"))
		{
			Value& compoents = d["components"];

			auto components_array = compoents.GetArray();

			for (SizeType i = 0; i < components_array.Size(); i++)
			{
				Value& c = components_array[i];

				if (c.HasMember("name") && c.HasMember("value"))
				{
					const char* name = c["name"].GetString();
					int value = c["value"].GetInt();
					auto it = mSystem.find(name);

					if (it != mSystem.end())
					{
						Component* pCompoent = it->second->create_compoent(pEntity, value);

						pEntity->Add(name, pCompoent);

						if (strcmp(name, "skill") == 0)
						{
							SkillComponent* pSkillCom = (SkillComponent*)pCompoent;
							for (int i = 0; i < ed.skill_size; i++)
							{
								if (ed.skills[i] != 0)
								{
									pSkillCom->Add(ed.skills[i]);
								}
							}
						}
					}
				}
		
			}
		}


		Controller::GetInstance()->invoke_on_create(pEntity->GetType(), pEntity);


	}


	return true;
}
bool World::LoadEntitys(const entity_data* entityData, int num)
{
	for (int i = 0; i < num; i++)
	{
		auto ed = entityData[i];

		load_entity(ed);
	}

	return true;
}

void World::set_enable(string name, Component* pComponent)
{
	auto it = mSystem.find(name);

	if (it != mSystem.end())
	{
		it->second->add(pComponent);
	}
}
void World::set_disable(string name, Component* pComponent)
{
	auto it = mSystem.find(name);

	if (it != mSystem.end())
	{
		it->second->remove(pComponent);
	}
}

GSystem* World::GetSystem(string name)
{
	auto it = mSystem.find(name);

	if (it != mSystem.end())
		return it->second;
	else
		return nullptr;
}


