namespace iRSDKSharp
{
	public static class Enums
	{
		public enum iRSDK_StatusField: uint
		{
			irsdk_stConnected = 1
		};

		public enum iRSDK_VarType: uint
		{
			// 1 byte
			irsdk_char = 0,
			irsdk_bool,

			// 4 bytes
			irsdk_int,
			irsdk_bitField,
			irsdk_float,

			// 8 bytes
			irsdk_double,

			//index, don't use
			irsdk_ETCount
		};

		// Bit fields
		public enum iRSDK_EngineWarnings: uint
		{
			irsdk_waterTempWarning = 0x01,
			irsdk_fuelPressureWarning = 0x02,
			irsdk_oilPressureWarning = 0x04,
			irsdk_engineStalled = 0x08,
			irsdk_pitSpeedLimiter = 0x10,
			irsdk_revLimiterActive = 0x20,
			irsdk_oilTempWarning = 0x40,
		};

		// Global flags
		public enum irsdk_Flags: uint
		{
			// global flags
			irsdk_checkered = 0x00000001,
			irsdk_white = 0x00000002,
			irsdk_green = 0x00000004,
			irsdk_yellow = 0x00000008,
			irsdk_red = 0x00000010,
			irsdk_blue = 0x00000020,
			irsdk_debris = 0x00000040,
			irsdk_crossed = 0x00000080,
			irsdk_yellowWaving = 0x00000100,
			irsdk_oneLapToGreen = 0x00000200,
			irsdk_greenHeld = 0x00000400,
			irsdk_tenToGo = 0x00000800,
			irsdk_fiveToGo = 0x00001000,
			irsdk_randomWaving = 0x00002000,
			irsdk_caution = 0x00004000,
			irsdk_cautionWaving = 0x00008000,

			// drivers black flags
			irsdk_black = 0x00010000,
			irsdk_disqualify = 0x00020000,
			irsdk_servicible = 0x00040000, // car is allowed service (not a flag)
			irsdk_furled = 0x00080000,
			irsdk_repair = 0x00100000,

			// start lights
			irsdk_startHidden = 0x10000000,
			irsdk_startReady =  0x20000000,
			irsdk_startSet =    0x40000000,
			irsdk_startGo =     0x80000000,
		};

		// Status
		public enum iRSDK_TrkLoc: int
		{
			irsdk_NotInWorld = -1,
			irsdk_OffTrack,
			irsdk_InPitStall,
			irsdk_AproachingPits,
			irsdk_OnTrack
		};

		public enum iRSDK_TrkSurf : int
		{
			irsdk_SurfaceNotInWorld = -1,
			irsdk_UndefinedMaterial = 0,

			irsdk_Asphalt1Material,
			irsdk_Asphalt2Material,
			irsdk_Asphalt3Material,
			irsdk_Asphalt4Material,
			irsdk_Concrete1Material,
			irsdk_Concrete2Material,
			irsdk_RacingDirt1Material,
			irsdk_RacingDirt2Material,
			irsdk_Paint1Material,
			irsdk_Paint2Material,
			irsdk_Rumble1Material,
			irsdk_Rumble2Material,
			irsdk_Rumble3Material,
			irsdk_Rumble4Material,

			irsdk_Grass1Material,
			irsdk_Grass2Material,
			irsdk_Grass3Material,
			irsdk_Grass4Material,
			irsdk_Dirt1Material,
			irsdk_Dirt2Material,
			irsdk_Dirt3Material,
			irsdk_Dirt4Material,
			irsdk_SandMaterial,
			irsdk_Gravel1Material,
			irsdk_Gravel2Material,
			irsdk_GrasscreteMaterial,
			irsdk_AstroturfMaterial,
		};

		public enum iRSDK_SessionState : uint
		{
			irsdk_StateInvalid,
			irsdk_StateGetInCar,
			irsdk_StateWarmup,
			irsdk_StateParadeLaps,
			irsdk_StateRacing,
			irsdk_StateCheckered,
			irsdk_StateCoolDown
		};

		public enum iRSDK_CarLeftRight : int
		{
			irsdk_LROff,
			irsdk_LRClear,          // no cars around us.
			irsdk_LRCarLeft,        // there is a car to our left.
			irsdk_LRCarRight,       // there is a car to our right.
			irsdk_LRCarLeftRight,   // there are cars on each side.
			irsdk_LR2CarsLeft,      // there are two cars to our left.
			irsdk_LR2CarsRight      // there are two cars to our right.
		};

		public enum iRSDK_CameraState
		{
			irsdk_IsSessionScreen = 0x0001, // the camera tool can only be activated if viewing the session screen (out of car)
			irsdk_IsScenicActive = 0x0002,  // the scenic camera is active (no focus car)

			//these can be changed with a broadcast message
			irsdk_CamToolActive = 0x0004,
			irsdk_UIHidden = 0x0008,
			irsdk_UseAutoShotSelection = 0x0010,
			irsdk_UseTemporaryEdits = 0x0020,
			irsdk_UseKeyAcceleration = 0x0040,
			irsdk_UseKey10xAcceleration = 0x0080,
			irsdk_UseMouseAimMode = 0x0100
		};

		public enum iRSDK_PitSvFlags : uint
		{
			irsdk_LFTireChange = 0x0001,
			irsdk_RFTireChange = 0x0002,
			irsdk_LRTireChange = 0x0004,
			irsdk_RRTireChange = 0x0008,

			irsdk_FuelFill = 0x0010,
			irsdk_WindshieldTearoff = 0x0020,
			irsdk_FastRepair = 0x0040
		};

		public enum iRSDK_PitSvStatus : uint
		{
			// status
			irsdk_PitSvNone = 0,
			irsdk_PitSvInProgress,
			irsdk_PitSvComplete,

			// errors
			irsdk_PitSvTooFarLeft = 100,
			irsdk_PitSvTooFarRight,
			irsdk_PitSvTooFarForward,
			irsdk_PitSvTooFarBack,
			irsdk_PitSvBadAngle,
			irsdk_PitSvCantFixThat,
		};

		public enum iRSDK_PaceMode : uint
		{
			irsdk_PaceModeSingleFileStart = 0,
			irsdk_PaceModeDoubleFileStart,
			irsdk_PaceModeSingleFileRestart,
			irsdk_PaceModeDoubleFileRestart,
			irsdk_PaceModeNotPacing,
		};

		public enum iRSDK_PaceFlags : uint
		{
			irsdk_PaceFlagsEndOfLine = 0x01,
			irsdk_PaceFlagsFreePass = 0x02,
			irsdk_PaceFlagsWavedAround = 0x04,
		};

		public enum iRSDK_BroadcastMsg : uint
		{
			irsdk_BroadcastCamSwitchPos = 0,      // car position, group, camera
			irsdk_BroadcastCamSwitchNum,          // driver #, group, camera
			irsdk_BroadcastCamSetState,           // irsdk_CameraState, unused, unused 
			irsdk_BroadcastReplaySetPlaySpeed,    // speed, slowMotion, unused
			irskd_BroadcastReplaySetPlayPosition, // irsdk_RpyPosMode, Frame Number (high, low)
			irsdk_BroadcastReplaySearch,          // irsdk_RpySrchMode, unused, unused
			irsdk_BroadcastReplaySetState,        // irsdk_RpyStateMode, unused, unused
			irsdk_BroadcastReloadTextures,        // irsdk_ReloadTexturesMode, carIdx, unused
			irsdk_BroadcastChatComand,            // irsdk_ChatCommandMode, subCommand, unused
			irsdk_BroadcastPitCommand,            // irsdk_PitCommandMode, parameter
			irsdk_BroadcastTelemCommand,          // irsdk_TelemCommandMode, unused, unused
			irsdk_BroadcastFFBCommand,            // irsdk_FFBCommandMode, value (float, high, low)
			irsdk_BroadcastReplaySearchSessionTime, // sessionNum, sessionTimeMS (high, low)
			irsdk_BroadcastVideoCapture,          // irsdk_VideoCaptureMode, unused, unused
			irsdk_BroadcastLast                   // unused placeholder
		};

		public enum iRSDK_ChatCommandMode : uint
		{
			irsdk_ChatCommand_Macro = 0,        // pass in a number from 1-15 representing the chat macro to launch
			irsdk_ChatCommand_BeginChat,        // Open up a new chat window
			irsdk_ChatCommand_Reply,            // Reply to last private chat
			irsdk_ChatCommand_Cancel            // Close chat window
		};

		public enum iRSDK_PitCommandMode : uint  // this only works when the driver is in the car
		{
			irsdk_PitCommand_Clear = 0,         // Clear all pit checkboxes
			irsdk_PitCommand_WS,                // Clean the winshield, using one tear off
			irsdk_PitCommand_Fuel,              // Add fuel, optionally specify the amount to add in liters or pass '0' to use existing amount
			irsdk_PitCommand_LF,                // Change the left front tire, optionally specifying the pressure in KPa or pass '0' to use existing pressure
			irsdk_PitCommand_RF,                // right front
			irsdk_PitCommand_LR,                // left rear
			irsdk_PitCommand_RR,                // right rear
			irsdk_PitCommand_ClearTires,        // Clear tire pit checkboxes
			irsdk_PitCommand_FR,                // Request a fast repair
			irsdk_PitCommand_ClearWS,           // Uncheck Clean the winshield checkbox
			irsdk_PitCommand_ClearFR,           // Uncheck request a fast repair
			irsdk_PitCommand_ClearFuel,         // Uncheck add fuel
		};

		public enum iRSDK_TelemCommandMode : uint  // You can call this any time, but telemtry only records when driver is in there car
		{
			irsdk_TelemCommand_Stop = 0,        // Turn telemetry recording off
			irsdk_TelemCommand_Start,           // Turn telemetry recording on
			irsdk_TelemCommand_Restart,         // Write current file to disk and start a new one
		};

		public enum iRSDK_RpyStateMode : uint
		{
			irsdk_RpyState_EraseTape = 0,       // clear any data in the replay tape
			irsdk_RpyState_Last                 // unused place holder
		};

		public enum iRSDK_ReloadTexturesMode : uint
		{
			irsdk_ReloadTextures_All = 0,       // reload all textuers
			irsdk_ReloadTextures_CarIdx         // reload only textures for the specific carIdx
		};

		// Search replay tape for events
		public enum iRSDK_RpySrchMode : uint
		{
			irsdk_RpySrch_ToStart = 0,
			irsdk_RpySrch_ToEnd,
			irsdk_RpySrch_PrevSession,
			irsdk_RpySrch_NextSession,
			irsdk_RpySrch_PrevLap,
			irsdk_RpySrch_NextLap,
			irsdk_RpySrch_PrevFrame,
			irsdk_RpySrch_NextFrame,
			irsdk_RpySrch_PrevIncident,
			irsdk_RpySrch_NextIncident,
			irsdk_RpySrch_Last                   // unused placeholder
		};

		public enum iRSDK_RpyPosMode : uint
		{
			irsdk_RpyPos_Begin = 0,
			irsdk_RpyPos_Current,
			irsdk_RpyPos_End,
			irsdk_RpyPos_Last                   // unused placeholder
		};

		public enum iRSDK_FFBCommandMode : uint               // You can call this any time
		{
			irsdk_FFBCommand_MaxForce = 0,      // Set the maximum force when mapping steering torque force to direct input units (float in Nm)
			irsdk_FFBCommand_Last               // unused placeholder
		};

		// irsdk_BroadcastCamSwitchPos or irsdk_BroadcastCamSwitchNum camera focus defines
		// pass these in for the first parameter to select the 'focus at' types in the camera system.
		public enum iRSDK_csMode : int
		{
			irsdk_csFocusAtIncident = -3,
			irsdk_csFocusAtLeader = -2,
			irsdk_csFocusAtExiting = -1,
			// ctFocusAtDriver + car number...
			irsdk_csFocusAtDriver = 0
		};

		public enum iRSDK_VideoCaptureMode : uint
		{
			irsdk_VideoCapture_TriggerScreenShot = 0,   // save a screenshot to disk
			irsdk_VideoCaptuer_StartVideoCapture,       // start capturing video
			irsdk_VideoCaptuer_EndVideoCapture,         // stop capturing video
			irsdk_VideoCaptuer_ToggleVideoCapture,      // toggle video capture on/off
			irsdk_VideoCaptuer_ShowVideoTimer,          // show video timer in upper left corner of display
			irsdk_VideoCaptuer_HideVideoTimer,          // hide video timer
		};
	}
}