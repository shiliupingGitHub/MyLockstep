#ifndef LOCKSTEP_MAP_H
#define LOCKSTEP_MAP_H
#define MAX_SKILL_EQUIP_NUM 5
namespace lockstep
{
	extern "C"
	{
		struct Rect
		{
			int minX;
			int minY;
			int maxX;
			int maxY;
		};

		struct Vector2
		{
			int x;
			int y;
		};

		struct Bound
		{
			Vector2 size;
			Vector2 center;

		};

		struct entity_data
		{
			unsigned long long id; //角色ID
			Vector2 pos; //角色出生点Id;
			int d;//出生的朝向
			int camp;//阵营
			int config_id;// 角色配置数据文件
			int skills[MAX_SKILL_EQUIP_NUM];
			int skill_size;
		};
		struct level_data
		{
			int mapId; //地图数据文件
			unsigned int seed;
		};

		struct hurt_data
		{
			int hurt;
		};

		struct command_data
		{
			char* op; //操作
			unsigned long long id; //对哪个实体的命令
			char* data; //数据
		};


	

	}
	struct hit
	{
		unsigned long long id;
		int param;
		int frozen;
		int x;
		int y;
	};

}


#endif
