_eventsChecker = [] spawn
{
	"dkab" callExtension "connect judgeGC LoadLibrary";

	hint "Подключаемся...";
	waitUntil { sleep 1; ("dkab" callExtension "iscon") == "true" };
	hint "Подключение установлено";

	addMissionEventHandler ["Ended", {"dkab" callExtension "disconnect"}];

	_pos_offset = [0, 0];

	myAllPlayers = [];
	_fauna = [];
	_flora = [];

	addMissionEventHandler ["Draw3D", 
	{
		{
			_iconPos = position (_x select 1);
			_iconPos set [2, (_iconPos select 2) + 2];
			drawIcon3D ["", [0.22, 0.23, 0.26, 1], _iconPos, 0, 0, 0, _x select 0, 1, 0.05, "PuristaMedium"];
		} forEach myAllPlayers;
						
	}];

	while {true} do
	{
		_news = "dkab" callExtension "events";
		_commands = _news splitString "|";

		{
			_args = _x splitString " ";

			switch (_args select 0) do 
			{
    			case "spawn": 
				{
					_pos = [parseNumber (_args select 2) + (_pos_offset select 0), parseNumber (_args select 3) + (_pos_offset select 1)];
					_unit = createAgent ["B_Soldier_F", _pos, [], 0, "FORM"];
					_unit disableAI "FSM";
					_unit setBehaviour "CARELESS";
					_unit allowDamage false;
					myAllPlayers pushBack [_args select 1, _unit];
				};
    			case "move": 
				{
					_unit = [];
					{
						if((_x select 0) == (_args select 1)) then
						{
							_unit = _x select 1;
						};
					}forEach myAllPlayers;


					_pos = [parseNumber (_args select 2) + (_pos_offset select 0), parseNumber (_args select 3) + (_pos_offset select 1)];
					_unit moveTo (_pos);
				};
				case "delpos":
				{
					_exit = false;
					_count = count myAllPlayers;
					for [{_i = 0},{_i < _count && !_exit},{_i = _i + 1}] do 
					{
						_current = myAllPlayers select _i;
						if((_current select 0) == (_args select 1)) then
						{
							_unit = _current select 1;
							
							deleteVehicle _unit;
							myAllPlayers set [_i,-1];
							myAllPlayers = myAllPlayers - [-1];
							_exit = true;
						};
					};				
				};
				case "fa_spawn":
				{
					_pos = [parseNumber (_args select 4) + (_pos_offset select 0), parseNumber (_args select 5) + (_pos_offset select 1)];
					_unit = createAgent ["C_man_polo_1_F_afro", _pos, [], 0, "FORM"];
					_unit disableAI "FSM";
					_unit setBehaviour "CARELESS";
					_unit setUnitPos "DOWN";
					_unit allowDamage false;

					_fauna pushBack [_args select 1, _unit];
				};
				case "fa_move":
				{
					_unit = [];
					{
						if((_x select 0) == (_args select 1)) then
						{
							_unit = _x select 1;
						};
					}forEach _fauna;

					switch(parseNumber (_args select 6)) do
					{
						case 0:
						{
							_uniform = "U_B_Protagonist_VR";
							if(uniform _unit != _uniform) then
							{
								_unit forceAddUniform _uniform;
							};							
						};
						case 1:
						{
							_uniform = "U_O_Protagonist_VR";
							if(uniform _unit != _uniform) then
							{
								_unit forceAddUniform _uniform;
							};
						};
						case 2:
						{
							_uniform = "U_I_Protagonist_VR";
							if(uniform _unit != _uniform) then
							{
								_unit forceAddUniform _uniform;
							};
						};
					};

					_pos = [parseNumber (_args select 4) + (_pos_offset select 0), parseNumber (_args select 5) + (_pos_offset select 1)];
					_unit moveTo (_pos);
				};
				case "fl_spawn":
				{
					_pos = [parseNumber (_args select 4), parseNumber (_args select 5)];
					switch(parseNumber (_args select 2)) do
					{
						case 0:
						{	
							_level = parseNumber(_args select 3);
							if(1 <= _level && _level <= 3) then
							{
								_unit = "Pine_1" createVehicle _pos;
								_flora pushBack [_args select 1, _unit];
							} 
							else
							{
								if (4 < _level && _level <= 6) then
								{
									_unit = "Pine_3" createVehicle _pos;
									_flora pushBack [_args select 1, _unit];
								}
								else
								{
									_unit = "Pine_2" createVehicle _pos;
									_flora pushBack [_args select 1, _unit];
								};
							};
							
						};
						case 1:
						{
							_unit = "Flower_Cakile" createVehicle _pos;
							_flora pushBack [_args select 1, _unit];
						};
					};									
				};
				case "fl_growth":
				{
					hint "Пальма растет";
				};
				case "fl_remove":
				{
					_unit = [];
					{
						if((_x select 0) == (_args select 1)) then
						{
							_unit = _x select 1;
						};
					}forEach _flora;

					deleteVehicle _unit;
				};
			};
			
		} forEach _commands;

		sleep 0.2;
		_mypos = position player;
		"dkab" callExtension format ["mypos %1 %2", _mypos select 0, _mypos select 1];

		sleep 0.3;
	}
}