_eventsChecker = [] spawn
{
	hint "Инициализация...";
	waitUntil { sleep 1; ("dkab" callExtension "init") == "true" };
	hint "Гейм клиент доступен...";

	sleep 1;
	"dkab" callExtension "reset";

	_pos_offset = [0, 0];

	_units = [];
	_fauna = [];
	_flora = [];
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
					_units pushBack [_args select 1, _unit];
				};
    			case "move": 
				{
					_unit = [];
					{
						if((_x select 0) == (_args select 1)) then
						{
							_unit = _x select 1;
						};
					}forEach _units;


					_pos = [parseNumber (_args select 2) + (_pos_offset select 0), parseNumber (_args select 3) + (_pos_offset select 1)];
					_unit moveTo (_pos);
				};
				case "delpos":
				{
					_unit = [];
					{
						if((_x select 0) == (_args select 1)) then
						{
							_unit = _x select 1;
						};
					}forEach _units;

					deleteVehicle _unit;
				};
				case "fa_spawn":
				{
					_pos = [parseNumber (_args select 4) + (_pos_offset select 0), parseNumber (_args select 5) + (_pos_offset select 1)];
					_unit = createAgent ["C_man_polo_1_F_afro", _pos, [], 0, "FORM"];
					_unit disableAI "FSM";
					_unit setBehaviour "CARELESS";
					_unit setUnitPos "DOWN";

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


					_pos = [parseNumber (_args select 4) + (_pos_offset select 0), parseNumber (_args select 5) + (_pos_offset select 1)];
					_unit moveTo (_pos);
				};
				case "fl_spawn":
				{
					_pos = [parseNumber (_args select 4), parseNumber (_args select 5)];
					_unit = "palm_2" createVehicle _pos;
					_flora pushBack [_args select 1, _unit];

					hint "spawn пальма";
				};
				case "fl_rise":
				{

				};
				case "fl_remove":
				{

				};
			};
			
		} forEach _commands;

		sleep 0.2;
		_mypos = position player;
		"dkab" callExtension format ["mypos %1 %2", _mypos select 0, _mypos select 1];

		sleep 0.3;
	}
}