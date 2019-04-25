#include "animation.h"
#include "world.h"
#include "controller.h"
#include "rapidjson/document.h"
#include "animation_event_factory.h"
#include "animaton_event.h"
#include "config_manager.h"
#include "config/ani_config.h"
using namespace rapidjson;
namespace lockstep
{
	Animation::Animation(World* pWorld, Entity* pEntity,int id):
		mFrameNum(0),mBreakFrame(0),mCurFrame(0),mPWorld(pWorld)
		,mPEntity(pEntity), mAniData({0}),mAniIndex(0)
	{
		ani_config* pAniConfig = (ani_config*)ConfigManager::GetInstance().Get("ani_config");
		mAniData = *pAniConfig->Get(id);
		const char* data = Controller::GetInstance()->ReadFile(ANIMATION_CONFIG_TYPE, id);

		Document d;
		d.Parse(data);
		if (d.HasMember("frame_num"))
		{
			mFrameNum = d["frame_num"].GetInt();

		}

		if (d.HasMember("break_frame"))
		{
			mBreakFrame = d["break_frame"].GetInt();
		}
		else
			mBreakFrame = mFrameNum;

		if (d.HasMember("ani_index"))
		{
			mAniIndex = d["ani_index"].GetInt();
		}

		if (d.HasMember("events"))
		{
			auto events =  d["events"].GetArray();

			for (SizeType i = 0; i < events.Size(); i++)
			{
				Value& ed = events[i];
				if ( ed.HasMember("frame")
					&&ed.HasMember("data")
					)
				{
					int frame = ed["frame"].GetInt(); 
					Value& event_data = ed["data"];
					
					auto event_type = event_data["type"].GetString();
					auto event_data_id = event_data["id"].GetInt();

					auto e = AnimationEventFactory::GetInstance().Create(event_type, event_data_id);

					if (nullptr != e)
					{
						auto it = mEvent.find(frame);

						if (it != mEvent.end())
						{
							it->second.push_back(e);
						}
						else
						{
							vector<AnimationEvent*> events;
							events.push_back(e);
							mEvent[frame] = events;
						}
		
					}
					
				
				}
			}
		}
	}
	Animation::~Animation()
	{
		for (auto it = mEvent.begin(); it != mEvent.end(); it++)
		{
			auto events = it->second;

			for (auto it2 = events.begin(); it2 != events.end(); it2++)
			{
				delete (*it2);
			}

			events.clear();
		}

		mEvent.clear();
	}
	void Animation::Reset()
	{
		mCurFrame = 0;
	}
	void Animation::Tick()
	{
		auto it = mEvent.find(mCurFrame);
		if (it != mEvent.end())
		{
			auto events = it->second;

			for (auto it2 = events.begin(); it2 != events.end(); it2++)
			{
				(*it2)->Fire(mPWorld, mPEntity, mAniData);
			}
			
		}

		mCurFrame++;
	}

	bool Animation::IsEnd()
	{
		return mCurFrame > mFrameNum;
	}

	bool Animation::IsCanBreak()
	{
		return mCurFrame > mBreakFrame;
	}
}